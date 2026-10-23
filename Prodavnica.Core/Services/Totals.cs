using Prodavnica.Database.DTO;

namespace Prodavnica.Services
{
    public static class Totals
    {
        public static decimal Of<T>(IEnumerable<T> lines) where T : ICartLine
        {
            return lines.Sum(line => line.Amount * line.Price);
        }
    }
}
