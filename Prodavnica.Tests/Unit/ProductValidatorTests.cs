using Prodavnica.Database.DTO;
using Prodavnica.Services;
using System.Globalization;

namespace Prodavnica.Tests.Unit
{
    public class ProductValidatorTests
    {
        private static readonly CultureInfo Serbian = new CultureInfo("sr-Latn-RS");
        private static readonly CultureInfo English = new CultureInfo("en-US");

        private readonly List<Product> existing = new List<Product> { TestData.Product(barCode: "1111"), TestData.Product(barCode: "1112") };

        private static ProductInput Valid(string? barCode = null, string? price = null, string? supplies = null, string? name = null) => new ProductInput
        {
            Name = name ?? "Kajmak",
            BarCode = barCode ?? "5555",
            Price = price ?? "6,50",
            Supplies = supplies ?? "0",
            Description = "Mladi kajmak",
            Category = "Mlijeko",
            Manufacturer = "Pađani"
        };

        private List<ValidationError> Validate(ProductInput input, string? original = null, CultureInfo? culture = null) =>
            ProductValidator.Validate(input, existing, original, culture ?? Serbian);

        [Fact]
        public void A_complete_product_is_valid()
        {
            Assert.Empty(Validate(Valid()));
        }

        [Fact]
        public void Every_empty_field_is_reported_as_required()
        {
            List<ValidationError> errors = Validate(new ProductInput());

            Assert.All(errors, e => Assert.Equal(ProductError.Required, e.Error));
            Assert.Equal(Enum.GetValues<ProductField>().OrderBy(f => f), errors.Select(e => e.Field).OrderBy(f => f));
        }

        [Theory]
        [InlineData("12a4")]
        [InlineData("12 34")]
        [InlineData("-123")]
        [InlineData("١٢٣")]
        public void A_bar_code_may_contain_ascii_digits_only(string barCode)
        {
            Assert.Contains(new ValidationError(ProductField.BarCode, ProductError.InvalidBarCode), Validate(Valid(barCode: barCode)));
        }

        [Fact]
        public void A_bar_code_longer_than_the_column_is_rejected()
        {
            Assert.Contains(new ValidationError(ProductField.BarCode, ProductError.InvalidBarCode), Validate(Valid(barCode: new string('1', 46))));
        }

        [Fact]
        public void A_bar_code_used_by_another_product_is_a_duplicate()
        {
            Assert.Contains(new ValidationError(ProductField.BarCode, ProductError.DuplicateBarCode), Validate(Valid(barCode: "1111")));
        }

        [Fact]
        public void Keeping_the_own_bar_code_while_editing_is_allowed()
        {
            Assert.Empty(Validate(Valid(barCode: "1111"), original: "1111"));
        }

        [Fact]
        public void Changing_to_another_products_bar_code_while_editing_is_a_duplicate()
        {
            Assert.Contains(new ValidationError(ProductField.BarCode, ProductError.DuplicateBarCode), Validate(Valid(barCode: "1112"), original: "1111"));
        }

        [Theory]
        [InlineData("0", ProductError.MustBePositive)]
        [InlineData("-1", ProductError.MustBePositive)]
        [InlineData("abc", ProductError.InvalidNumber)]
        public void Price_must_be_a_positive_number(string price, ProductError expected)
        {
            Assert.Contains(new ValidationError(ProductField.Price, expected), Validate(Valid(price: price)));
        }

        [Fact]
        public void Supplies_may_be_zero_but_not_negative()
        {
            Assert.Empty(Validate(Valid(supplies: "0")));
            Assert.Contains(new ValidationError(ProductField.Supplies, ProductError.CannotBeNegative), Validate(Valid(supplies: "-2")));
        }

        [Fact]
        public void Numbers_are_read_in_the_users_culture()
        {
            Assert.True(ProductValidator.TryParseAmount("2,50", Serbian, out decimal serbian));
            Assert.True(ProductValidator.TryParseAmount("2.50", English, out decimal english));

            Assert.Equal(2.50m, serbian);
            Assert.Equal(2.50m, english);
            Assert.Empty(Validate(Valid(price: "1,234.50"), culture: English));
        }

        [Fact]
        public void A_name_longer_than_the_column_is_too_long()
        {
            Assert.Contains(new ValidationError(ProductField.Name, ProductError.TooLong), Validate(Valid(name: new string('a', 46))));
        }
    }
}
