using Prodavnica.Database;
using Prodavnica.Database.DTO;
using Prodavnica.Database.Repositories;

namespace Prodavnica.Tests.Database
{
    [Collection(DatabaseCollection.Name)]
    [Trait("Category", "Integration")]
    public class ProductRepositoryTests
    {
        private readonly ProductRepository products = new ProductRepository();

        internal static Product NewProduct(decimal supplies = 10m, decimal price = 3.25m, int expiresInDays = 90)
        {
            string code = TestData.UniqueDigits();
            return new Product
            {
                Name = "Test " + code[^6..],
                BarCode = code,
                Price = price,
                Supplies = supplies,
                ExpirationDate = DateTime.Today.AddDays(expiresInDays),
                IdCategory = 1,
                IdManufacturer = 1,
                Description = "Integration test product"
            };
        }

        internal static async Task<Product> CreateAsync(decimal supplies = 10m, decimal price = 3.25m, int expiresInDays = 90)
        {
            Product product = NewProduct(supplies, price, expiresInDays);
            await new ProductRepository().CreateAsync(product);
            return product;
        }

        [Fact]
        public async Task Pages_through_the_seeded_catalogue()
        {
            PagedResult<Product> first = await products.GetPageAsync(new ProductQuery(new PageRequest(1, 25)));
            PagedResult<Product> second = await products.GetPageAsync(new ProductQuery(new PageRequest(2, 25)));

            Assert.True(first.TotalCount >= 100);
            Assert.Equal(25, first.Items.Count);
            Assert.Empty(first.Items.Select(p => p.Id).Intersect(second.Items.Select(p => p.Id)));
            Assert.All(first.Items, p => Assert.False(string.IsNullOrEmpty(p.CategoryName)));
        }

        [Fact]
        public async Task Search_matches_name_bar_code_category_and_manufacturer()
        {
            var byName = await products.GetPageAsync(new ProductQuery(new PageRequest(1, 50, "milka")));
            var byCode = await products.GetPageAsync(new ProductQuery(new PageRequest(1, 50, "1111")));
            var byManufacturer = await products.GetPageAsync(new ProductQuery(new PageRequest(1, 50, "Mesnica Tarana")));

            Assert.All(byName.Items, p => Assert.True(p.Name.Contains("Milka", StringComparison.OrdinalIgnoreCase) || p.ManufacturerName == "Milka"));
            Assert.Contains(byCode.Items, p => p.BarCode == "1111");
            Assert.NotEmpty(byManufacturer.Items);
            Assert.All(byManufacturer.Items, p => Assert.Equal("Mesnica Tarana", p.ManufacturerName));
        }

        [Fact]
        public async Task Category_filter_and_sellable_filter_apply()
        {
            var dairy = await products.GetPageAsync(new ProductQuery(new PageRequest(1, 100), CategoryId: 1));
            var sellable = await products.GetPageAsync(new ProductQuery(new PageRequest(1, 500), SellableOnly: true, Today: DateTime.Today));
            var all = await products.GetPageAsync(new ProductQuery(new PageRequest(1, 500)));

            Assert.All(dairy.Items, p => Assert.Equal(1, p.IdCategory));
            Assert.All(sellable.Items, p => Assert.True(p.Supplies > 0 && p.ExpirationDate.Date >= DateTime.Today));
            Assert.True(sellable.TotalCount < all.TotalCount);
        }

        [Fact]
        public async Task Create_update_and_delete_round_trip_with_an_image()
        {
            Product product = NewProduct();
            product.Image = new byte[] { 1, 2, 3, 4 };

            int id = await products.CreateAsync(product);
            Product? stored = await products.GetByIdAsync(id);
            Assert.NotNull(stored);
            Assert.Equal(product.Name, stored!.Name);
            Assert.Equal(new byte[] { 1, 2, 3, 4 }, stored.Image);

            stored.Price = 9.99m;
            stored.Image = null;
            await products.UpdateAsync(stored);
            Product? updated = await products.GetByBarCodeAsync(product.BarCode);
            Assert.Equal(9.99m, updated!.Price);
            Assert.Null(updated.Image);

            await products.DeleteAsync(id);
            Assert.Null(await products.GetByIdAsync(id));
        }

        [Fact]
        public async Task Bar_codes_are_unique()
        {
            Product product = await CreateAsync();
            Product duplicate = NewProduct();
            duplicate.BarCode = product.BarCode;

            Assert.True(await products.BarCodeExistsAsync(product.BarCode));
            Assert.False(await products.BarCodeExistsAsync(product.BarCode, exceptId: product.Id));
            await Assert.ThrowsAsync<DuplicateValueException>(() => products.CreateAsync(duplicate));
        }

        [Fact]
        public async Task A_sold_product_cannot_be_deleted()
        {
            // Product 1 appears on seeded bills.
            await Assert.ThrowsAsync<InUseException>(() => products.DeleteAsync(1));
        }

        [Fact]
        public async Task Negative_stock_is_rejected_by_the_database()
        {
            Product product = await CreateAsync();
            product.Supplies = -1;

            var error = await Assert.ThrowsAsync<StoreDataException>(() => products.UpdateAsync(product));
            Assert.Equal("INVALID_VALUE", error.Code);
        }
    }
}
