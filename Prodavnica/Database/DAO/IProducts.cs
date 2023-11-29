using Prodavnica.Database.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prodavnica.Database.DAO
{
    public interface IProducts
    {
        public List<Prodavnica.Database.DTO.Product> GetAll();
        public void UpdateProduct(Product product);
        public void DeleteProduct(int id);
        public void CreateProduct(Product product);
    }
}
