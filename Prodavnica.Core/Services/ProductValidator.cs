using Prodavnica.Database.DTO;
using System.Globalization;

namespace Prodavnica.Services
{
    public enum ProductField
    {
        Name,
        BarCode,
        Price,
        Supplies,
        Description,
        Category,
        Manufacturer
    }

    public enum ProductError
    {
        Required,
        TooLong,
        InvalidNumber,
        MustBePositive,
        CannotBeNegative,
        InvalidBarCode,
        DuplicateBarCode
    }

    public sealed record ValidationError(ProductField Field, ProductError Error);

    // What the product dialog holds before it becomes a Product: raw text from the inputs.
    public sealed class ProductInput
    {
        public string Name { get; init; } = string.Empty;
        public string BarCode { get; init; } = string.Empty;
        public string Price { get; init; } = string.Empty;
        public string Supplies { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public string? Category { get; init; }
        public string? Manufacturer { get; init; }
    }

    public static class ProductValidator
    {
        // Column sizes from Model/DDL.sql.
        public const int MaxNameLength = 45;
        public const int MaxBarCodeLength = 45;
        public const int MaxDescriptionLength = 255;

        // originalBarCode is the product's own bar code when editing, so it does not count as a duplicate.
        public static List<ValidationError> Validate(
            ProductInput input,
            IEnumerable<Product> existing,
            string? originalBarCode,
            CultureInfo culture)
        {
            string code = input.BarCode.Trim();
            bool taken = code != originalBarCode && existing.Any(p => p.BarCode == code);
            return Validate(input, taken, culture);
        }

        // barCodeTaken: another product already uses this bar code (checked against the database).
        public static List<ValidationError> Validate(ProductInput input, bool barCodeTaken, CultureInfo culture)
        {
            var errors = new List<ValidationError>();

            CheckText(errors, ProductField.Name, input.Name, MaxNameLength);
            CheckText(errors, ProductField.Description, input.Description, MaxDescriptionLength);

            string barCode = input.BarCode.Trim();
            if (barCode.Length == 0)
                errors.Add(new ValidationError(ProductField.BarCode, ProductError.Required));
            else if (barCode.Length > MaxBarCodeLength || !barCode.All(char.IsAsciiDigit))
                errors.Add(new ValidationError(ProductField.BarCode, ProductError.InvalidBarCode));
            else if (barCodeTaken)
                errors.Add(new ValidationError(ProductField.BarCode, ProductError.DuplicateBarCode));

            CheckNumber(errors, ProductField.Price, input.Price, culture, allowZero: false);
            CheckNumber(errors, ProductField.Supplies, input.Supplies, culture, allowZero: true);

            if (string.IsNullOrWhiteSpace(input.Category))
                errors.Add(new ValidationError(ProductField.Category, ProductError.Required));
            if (string.IsNullOrWhiteSpace(input.Manufacturer))
                errors.Add(new ValidationError(ProductField.Manufacturer, ProductError.Required));

            return errors;
        }

        public static bool TryParseAmount(string? text, CultureInfo culture, out decimal value)
        {
            return decimal.TryParse(text?.Trim(), NumberStyles.Number, culture, out value);
        }

        private static void CheckText(List<ValidationError> errors, ProductField field, string value, int maxLength)
        {
            if (string.IsNullOrWhiteSpace(value))
                errors.Add(new ValidationError(field, ProductError.Required));
            else if (value.Trim().Length > maxLength)
                errors.Add(new ValidationError(field, ProductError.TooLong));
        }

        private static void CheckNumber(List<ValidationError> errors, ProductField field, string text, CultureInfo culture, bool allowZero)
        {
            if (string.IsNullOrWhiteSpace(text))
                errors.Add(new ValidationError(field, ProductError.Required));
            else if (!TryParseAmount(text, culture, out decimal value))
                errors.Add(new ValidationError(field, ProductError.InvalidNumber));
            else if (allowZero && value < 0)
                errors.Add(new ValidationError(field, ProductError.CannotBeNegative));
            else if (!allowZero && value <= 0)
                errors.Add(new ValidationError(field, ProductError.MustBePositive));
        }
    }
}
