using Prodavnica.Database.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prodavnica.Database.DAO
{
    public interface IBill
    {
        void Create(Bill bill, List<BillItem> billItems);
        List<BillItem> GetAllBillItem();
        List<Bill> GetAllBill();
    }
}
