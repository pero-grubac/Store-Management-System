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
    }
}
