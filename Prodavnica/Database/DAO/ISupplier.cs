using Prodavnica.Database.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prodavnica.Database.DAO
{
    public interface ISupplier
    {
        public List<Supplier> GetAll();
        public void AddSupplier(Supplier supplier);
    }
}
