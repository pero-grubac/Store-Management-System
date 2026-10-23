using MySqlConnector;
using Prodavnica.Database;
using Prodavnica.Database.DTO;
using Prodavnica.Database.Repositories;
using Prodavnica.Services;

namespace Prodavnica.Tests.Database
{
    // Sales and procurements go through the InsertRacunWithStavka / InsertNabavka stored procedures.
    [Collection(DatabaseCollection.Name)]
    [Trait("Category", "Integration")]
    public class DocumentTests
    {
        private readonly MySqlFixture database;
        private readonly ProductRepository products = new ProductRepository();
        private readonly SalesRepository sales = new SalesRepository();
        private readonly ProcurementRepository procurements = new ProcurementRepository();

        public DocumentTests(MySqlFixture database)
        {
            this.database = database;
        }

        private async Task<int> BillCountAsync()
        {
            await using var connection = new MySqlConnection(database.RootConnectionString);
            await connection.OpenAsync();
            await using var command = new MySqlCommand("SELECT COUNT(*) FROM racun", connection);
            return Convert.ToInt32(await command.ExecuteScalarAsync());
        }

        [Fact]
        public async Task A_sale_stores_the_bill_at_database_prices_and_lowers_stock()
        {
            Product cheese = await ProductRepositoryTests.CreateAsync(supplies: 20m, price: 8m);
            Product milk = await ProductRepositoryTests.CreateAsync(supplies: 5m, price: 2m);
            var cart = new Cart<BillItem>();
            cart.AddOrReplace(new BillItem { IdProduct = cheese.Id, Amount = 1.5m, Price = 999m });
            cart.AddOrReplace(new BillItem { IdProduct = milk.Id, Amount = 5m, Price = 2m });

            int billId = await sales.CreateBillAsync(userId: 2, cart.ToList());

            Assert.Equal(18.5m, (await products.GetByIdAsync(cheese.Id))!.Supplies);
            Assert.Equal(0m, (await products.GetByIdAsync(milk.Id))!.Supplies);
            List<BillItem> items = await sales.GetItemsAsync(billId);
            Assert.Equal(2, items.Count);
            Assert.Equal(8m, items.Single(i => i.IdProduct == cheese.Id).Price);
            Bill? bill = await sales.GetBillAsync(billId);
            Assert.Equal(22m, bill!.Total);
            Assert.Equal("Kasir Kasirović", bill.CashierName);
        }

        [Fact]
        public async Task Selling_more_than_in_stock_fails_and_saves_nothing()
        {
            Product plenty = await ProductRepositoryTests.CreateAsync(supplies: 50m);
            Product scarce = await ProductRepositoryTests.CreateAsync(supplies: 1m);
            int billsBefore = await BillCountAsync();

            await Assert.ThrowsAsync<InsufficientStockException>(() => sales.CreateBillAsync(2, new[]
            {
                new BillItem { IdProduct = plenty.Id, Amount = 3m },
                new BillItem { IdProduct = scarce.Id, Amount = 2m }
            }));

            Assert.Equal(billsBefore, await BillCountAsync());
            Assert.Equal(50m, (await products.GetByIdAsync(plenty.Id))!.Supplies);
        }

        [Fact]
        public async Task Expired_products_cannot_be_sold()
        {
            Product expired = await ProductRepositoryTests.CreateAsync(supplies: 10m, expiresInDays: -1);

            var error = await Assert.ThrowsAsync<StoreDataException>(() =>
                sales.CreateBillAsync(2, new[] { new BillItem { IdProduct = expired.Id, Amount = 1m } }));
            Assert.Equal("PRODUCT_EXPIRED", error.Code);
        }

        [Fact]
        public async Task An_empty_bill_is_rejected()
        {
            var error = await Assert.ThrowsAsync<StoreDataException>(() => sales.CreateBillAsync(2, Array.Empty<BillItem>()));
            Assert.Equal("EMPTY_DOCUMENT", error.Code);
        }

        [Fact]
        public async Task Bills_are_paged_newest_first_within_the_date_range()
        {
            PagedResult<Bill> page = await sales.GetPageAsync(DateTime.Today.AddDays(-30), DateTime.Today, new PageRequest(1, 15));

            Assert.True(page.TotalCount > 15);
            Assert.Equal(15, page.Items.Count);
            Assert.Equal(page.Items.OrderByDescending(b => b.Date).Select(b => b.Id), page.Items.Select(b => b.Id));
            Assert.All(page.Items, b => Assert.InRange(b.Date, DateTime.Today.AddDays(-30), DateTime.Today.AddDays(1)));
            Assert.All(page.Items, b => Assert.True(b.Total > 0));
        }

        [Fact]
        public async Task A_procurement_raises_stock_and_keeps_purchase_prices()
        {
            Product chocolate = await ProductRepositoryTests.CreateAsync(supplies: 3m, price: 4m);
            var cart = new Cart<ProcurementItem>();
            cart.AddOrReplace(new ProcurementItem { IdProduct = chocolate.Id, Amount = 40m, Price = 1.75m });

            int id = await procurements.CreateAsync(supplierId: 1, userId: 1, cart.ToList());

            Assert.Equal(43m, (await products.GetByIdAsync(chocolate.Id))!.Supplies);
            ProcurementItem stored = Assert.Single(await procurements.GetItemsAsync(id));
            Assert.Equal(1.75m, stored.Price);
            Procurement? procurement = await procurements.GetAsync(id);
            Assert.Equal(70m, procurement!.Total);
            Assert.Equal("DobavljačiSrbija", procurement.SupplierName);
        }

        [Fact]
        public async Task The_dashboard_reflects_seeded_activity()
        {
            DashboardData data = await new DashboardRepository().GetAsync(DateTime.Now);

            Assert.True(data.ProductCount >= 100);
            Assert.True(data.SalesLast30Days > 0);
            Assert.Equal(DashboardRepository.ChartDays, data.DailySales.Count);
            Assert.NotEmpty(data.NeedsAttention);
            Assert.True(data.NeedsAttention.Count <= DashboardRepository.AttentionLimit);
            Assert.NotEmpty(data.RecentBills);
        }
    }
}
