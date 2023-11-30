namespace Prodavnica.Database.DTO
{
    public class Procurement
    {
        public int Id { get; set; }
        public int IdManufacturer { get; set; }
        public DateOnly Date { get; set; }
    }
}
