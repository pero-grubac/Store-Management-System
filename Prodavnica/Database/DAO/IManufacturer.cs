using Prodavnica.Database.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prodavnica.Database.DAO
{
    public interface IManufacturer
    {
        public List<Manufacturer> GetAll();
        public void AddManufactuer(Manufacturer manufacturer);
        void Update(Manufacturer manufacturer);
        void Delete(Manufacturer manufacturer); 
    }
}
