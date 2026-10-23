namespace Prodavnica.Database.DTO
{
    // A line on a bill or a procurement: which product, how much, at what unit price.
    public interface ICartLine
    {
        int IdProduct { get; }
        decimal Amount { get; }
        decimal Price { get; }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal Supplies { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string BarCode { get; set; } = string.Empty;
        public int IdManufacturer { get; set; }
        public string ManufacturerName { get; set; } = string.Empty;
        public int IdCategory { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public byte[]? Image { get; set; }
    }

    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public bool IsAdmin { get; set; }
        public int IdTheme { get; set; }
        public int IdLanguage { get; set; }
        public byte[]? Image { get; set; }

        public string FullName => $"{FirstName} {LastName}".Trim();
    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int ProductCount { get; set; }
    }

    public enum CompanyKind
    {
        Manufacturer,
        Supplier
    }

    // A manufacturer or a supplier: both are a name and an e-mail address.
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        // Products made (manufacturer) or procurements delivered (supplier).
        public int UsageCount { get; set; }
    }

    public class Bill
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Total { get; set; }
        public int ItemCount { get; set; }
        public string CashierName { get; set; } = string.Empty;
    }

    public class BillItem : ICartLine
    {
        public int IdBill { get; set; }
        public int IdProduct { get; set; }
        public string Name { get; set; } = string.Empty;
        public string BarCode { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public decimal Price { get; set; }
        public decimal LineTotal => Amount * Price;
    }

    public class Procurement
    {
        public int Id { get; set; }
        public int IdSupplier { get; set; }
        public string SupplierName { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public decimal Total { get; set; }
        public int ItemCount { get; set; }
        public string UserName { get; set; } = string.Empty;
    }

    public class ProcurementItem : ICartLine
    {
        public int IdProcurement { get; set; }
        public int IdProduct { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ManufacturerName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public decimal Price { get; set; }
        public decimal LineTotal => Amount * Price;
    }

    public class Theme
    {
        public int Id { get; set; }
        public string ColorName { get; set; } = string.Empty;
        public string FontName { get; set; } = "Segoe UI";
        public int Size { get; set; } = 11;
        public string FontStyle { get; set; } = "Regular";
        public bool IsStrikeout { get; set; }
        public bool IsUnderline { get; set; }
    }

    public class Language
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class AuditEntry
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
    }

    public sealed record DailySales(DateTime Day, decimal Total);

    // The numbers on the home screen, computed by the database.
    public class DashboardData
    {
        public int ProductCount { get; init; }
        public int LowStockCount { get; init; }
        public int OutOfStockCount { get; init; }
        public int ExpiredCount { get; init; }
        public int ExpiringSoonCount { get; init; }
        public decimal StockValue { get; init; }
        public int BillsToday { get; init; }
        public decimal SalesToday { get; init; }
        public decimal SalesLast30Days { get; init; }
        public IReadOnlyList<DailySales> DailySales { get; init; } = Array.Empty<DailySales>();
        public IReadOnlyList<Product> NeedsAttention { get; init; } = Array.Empty<Product>();
        public IReadOnlyList<Bill> RecentBills { get; init; } = Array.Empty<Bill>();
    }
}
