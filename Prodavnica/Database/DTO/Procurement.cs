namespace Prodavnica.Database.DTO
{
    public class Procurement
    {
        public int Id { get; set; }
        public int IdSupplier { get; set; }
        public string SupplierName { get; set; }
        public DateTime Date { get; set; }
        public decimal Total { get; set; }
    }
}
