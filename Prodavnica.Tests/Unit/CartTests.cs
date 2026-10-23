using Prodavnica.Database.DTO;
using Prodavnica.Services;

namespace Prodavnica.Tests.Unit
{
    public class CartTests
    {
        [Fact]
        public void New_cart_is_empty_with_zero_total()
        {
            var cart = new Cart<BillItem>();

            Assert.True(cart.IsEmpty);
            Assert.Equal(0m, cart.Total);
        }

        [Fact]
        public void Adding_lines_sums_amount_times_price()
        {
            var cart = new Cart<BillItem>();

            cart.AddOrReplace(TestData.BillItem(1, 2m, 2.50m));
            cart.AddOrReplace(TestData.BillItem(2, 0.5m, 8m));

            Assert.Equal(2, cart.Items.Count);
            Assert.Equal(9m, cart.Total);
        }

        [Fact]
        public void Adding_the_same_product_again_replaces_the_line()
        {
            var cart = new Cart<BillItem>();

            cart.AddOrReplace(TestData.BillItem(1, 2m, 2m));
            cart.AddOrReplace(TestData.BillItem(1, 5m, 2m));

            BillItem line = Assert.Single(cart.Items);
            Assert.Equal(5m, line.Amount);
            Assert.Equal(10m, cart.Total);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Amount_must_be_positive(decimal amount)
        {
            var cart = new Cart<BillItem>();

            Assert.Throws<ArgumentOutOfRangeException>(() => cart.AddOrReplace(TestData.BillItem(1, amount, 2m)));
            Assert.True(cart.IsEmpty);
        }

        [Fact]
        public void Price_cannot_be_negative()
        {
            var cart = new Cart<ProcurementItem>();

            Assert.Throws<ArgumentOutOfRangeException>(() =>
                cart.AddOrReplace(new ProcurementItem { IdProduct = 1, Amount = 1m, Price = -0.01m }));
        }

        [Fact]
        public void Remove_reports_whether_a_line_was_removed()
        {
            var cart = new Cart<BillItem>();
            cart.AddOrReplace(TestData.BillItem(1, 1m, 1m));

            Assert.False(cart.Remove(99));
            Assert.True(cart.Remove(1));
            Assert.True(cart.IsEmpty);
        }

        [Fact]
        public void ToList_returns_a_snapshot()
        {
            var cart = new Cart<BillItem>();
            cart.AddOrReplace(TestData.BillItem(1, 1m, 1m));

            List<BillItem> snapshot = cart.ToList();
            cart.Clear();

            Assert.Single(snapshot);
            Assert.True(cart.IsEmpty);
        }
    }
}
