using Prodavnica.Database.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prodavnica.Database.DAO
{
    public interface IProcurement
    {
        void Create(Procurement procurement,List<ProcurementItem> procurementItems);
        List<Procurement> GetAll();
        List<ProcurementItem> GetItems();
    }
}
