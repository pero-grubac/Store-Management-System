using Prodavnica.Database.DTO;

namespace Prodavnica.Services
{
    public enum StockLevel
    {
        OutOfStock,
        Low,
        InStock
    }

    public enum ExpiryState
    {
        Expired,
        ExpiringSoon,
        Fresh
    }

    public static class Inventory
    {
        public const decimal LowStockThreshold = 10;
        public const int ExpiringSoonDays = 30;

        public static StockLevel GetStockLevel(Product product)
        {
            if (product.Supplies <= 0) return StockLevel.OutOfStock;
            if (product.Supplies < LowStockThreshold) return StockLevel.Low;
            return StockLevel.InStock;
        }

        // A product is still good on its expiration date and expired from the next day.
        public static ExpiryState GetExpiryState(Product product, DateTime today)
        {
            DateTime expires = product.ExpirationDate.Date;
            if (expires < today.Date) return ExpiryState.Expired;
            if (expires <= today.Date.AddDays(ExpiringSoonDays)) return ExpiryState.ExpiringSoon;
            return ExpiryState.Fresh;
        }

        public static bool CanSell(Product product, decimal amount, DateTime today)
        {
            return amount > 0
                && amount <= product.Supplies
                && GetExpiryState(product, today) != ExpiryState.Expired;
        }

        // What a cashier may put on a bill: in stock and not past its expiration date.
        public static List<Product> Sellable(IEnumerable<Product> products, DateTime today)
        {
            return products
                .Where(p => p.Supplies > 0 && GetExpiryState(p, today) != ExpiryState.Expired)
                .ToList();
        }
    }
}
