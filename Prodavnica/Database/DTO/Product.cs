using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prodavnica.Database.DTO
{
    public class Product
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Supplies { get; set; }
        public DateTime ExpirationDate { get; set; }
        public  string BarCode { get; set; }
        public int IdManufacturer { get; set; }
        public int IdCategory { get; set; }
        public string Description { get; set; }

    }
}
