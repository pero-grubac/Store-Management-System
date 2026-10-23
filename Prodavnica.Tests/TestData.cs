using Prodavnica.Database.DTO;

namespace Prodavnica.Tests
{
    internal static class TestData
    {
        public static readonly DateTime Today = new DateTime(2026, 10, 23);

        private static int counter;

        public static Product Product(
            string name = "Mlijeko 1l",
            decimal price = 2m,
            decimal supplies = 50m,
            DateTime? expires = null,
            string? barCode = null,
            string? category = "Mlijeko",
            string? manufacturer = "Pađani")
        {
            int id = Interlocked.Increment(ref counter);
            return new Product
            {
                Id = id,
                Name = name,
                Price = price,
                Supplies = supplies,
                ExpirationDate = expires ?? Today.AddDays(180),
                BarCode = barCode ?? (1000 + id).ToString(),
                CategoryName = category!,
                ManufacturerName = manufacturer!,
                IdCategory = 1,
                IdManufacturer = 1,
                Description = name
            };
        }

        public static BillItem BillItem(int idProduct, decimal amount, decimal price, int idBill = 0)
        {
            return new BillItem { IdBill = idBill, IdProduct = idProduct, Amount = amount, Price = price };
        }

        // Digits only and unique per call, so integration tests never collide on UNIQUE columns.
        public static string UniqueDigits()
        {
            return $"{DateTime.UtcNow.Ticks % 1_000_000_000_000L:D12}{Interlocked.Increment(ref counter):D4}";
        }
    }
}
