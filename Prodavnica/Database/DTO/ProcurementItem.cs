namespace Prodavnica.Database.DTO
{
    public class ProcurementItem
    {
        public decimal Amount { get; set; }
        public decimal Price { get; set; }
        public int IdProduct { get; set; }
        public int IdProcurement { get; set; }
        public int IdManufacturer { get; set; }
        public string ManufacturerName { get; set; }
        public string Name { get; set; }
    }
}
