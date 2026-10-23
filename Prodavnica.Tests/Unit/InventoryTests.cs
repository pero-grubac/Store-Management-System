using Prodavnica.Database.DTO;
using Prodavnica.Services;

namespace Prodavnica.Tests.Unit
{
    public class InventoryTests
    {
        [Theory]
        [InlineData(-3, StockLevel.OutOfStock)]
        [InlineData(0, StockLevel.OutOfStock)]
        [InlineData(0.5, StockLevel.Low)]
        [InlineData(9.99, StockLevel.Low)]
        [InlineData(10, StockLevel.InStock)]
        [InlineData(200, StockLevel.InStock)]
        public void Stock_level_follows_the_low_stock_threshold(decimal supplies, StockLevel expected)
        {
            Assert.Equal(expected, Inventory.GetStockLevel(TestData.Product(supplies: supplies)));
        }

        [Theory]
        [InlineData(-1, ExpiryState.Expired)]
        [InlineData(0, ExpiryState.ExpiringSoon)]
        [InlineData(30, ExpiryState.ExpiringSoon)]
        [InlineData(31, ExpiryState.Fresh)]
        public void Expiry_state_is_based_on_whole_days(int daysLeft, ExpiryState expected)
        {
            Product product = TestData.Product(expires: TestData.Today.AddDays(daysLeft));

            Assert.Equal(expected, Inventory.GetExpiryState(product, TestData.Today));
        }

        [Fact]
        public void Time_of_day_does_not_change_the_expiry_state()
        {
            Product product = TestData.Product(expires: TestData.Today.AddHours(1));

            Assert.Equal(ExpiryState.ExpiringSoon, Inventory.GetExpiryState(product, TestData.Today.AddHours(23)));
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(5, true)]
        [InlineData(5.01, false)]
        [InlineData(0, false)]
        [InlineData(-1, false)]
        public void CanSell_requires_a_positive_amount_within_stock(decimal amount, bool expected)
        {
            Product product = TestData.Product(supplies: 5m);

            Assert.Equal(expected, Inventory.CanSell(product, amount, TestData.Today));
        }

        [Fact]
        public void CanSell_refuses_expired_products()
        {
            Product product = TestData.Product(supplies: 100m, expires: TestData.Today.AddDays(-1));

            Assert.False(Inventory.CanSell(product, 1m, TestData.Today));
        }

        [Fact]
        public void Sellable_keeps_only_products_in_stock_and_not_expired()
        {
            Product fresh = TestData.Product(name: "Fresh");
            Product empty = TestData.Product(name: "Empty", supplies: 0m);
            Product expired = TestData.Product(name: "Expired", expires: TestData.Today.AddDays(-10));
            Product lastDay = TestData.Product(name: "Last day", expires: TestData.Today);

            List<Product> sellable = Inventory.Sellable(new[] { fresh, empty, expired, lastDay }, TestData.Today);

            Assert.Equal(new[] { "Fresh", "Last day" }, sellable.Select(p => p.Name));
        }
    }
}
