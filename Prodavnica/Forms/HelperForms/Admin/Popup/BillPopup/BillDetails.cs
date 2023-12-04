using Prodavnica.Database.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prodavnica.Forms.HelperForms.Admin.Popup.BillPopup
{
    public partial class BillDetails : Form
    {
        public BillDetails(User user,List<BillItem> billItems)
        {
            InitializeComponent();
            dgvBillItems.DataSource = billItems;

        }
    }
}
