using Prodavnica.Database.DTO;

namespace Prodavnica.Services
{
    // The open bill (cashier) or the open procurement (admin) before it is saved.
    // A product appears at most once: adding it again replaces the earlier line.
    public class Cart<T> where T : ICartLine
    {
        private readonly List<T> items = new List<T>();

        public IReadOnlyList<T> Items => items;

        public bool IsEmpty => items.Count == 0;

        public decimal Total => Totals.Of(items);

        public void AddOrReplace(T line)
        {
            if (line.Amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(line), "Amount must be greater than zero.");
            if (line.Price < 0)
                throw new ArgumentOutOfRangeException(nameof(line), "Price cannot be negative.");

            items.RemoveAll(item => item.IdProduct == line.IdProduct);
            items.Add(line);
        }

        public bool Remove(int idProduct)
        {
            return items.RemoveAll(item => item.IdProduct == idProduct) > 0;
        }

        public void Clear()
        {
            items.Clear();
        }

        // A copy, so a grid bound to it does not see later changes until it is rebound.
        public List<T> ToList()
        {
            return new List<T>(items);
        }
    }
}
