using Prodavnica.Database.DTO;
using Prodavnica.Language;
using Prodavnica.Util;
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
        private Database.DTO.User user;
        public BillDetails(User user,List<BillItem> billItems)
        {
            InitializeComponent();
            this.user = user;
            dgvBillItems.DataSource = billItems;
            ChangeText();
        }
        private void ChangeText()
        {
            LoadSettings.ApplySettins(user, this);
            name.HeaderText = LanguageHelper.GetString("name");
            amount.HeaderText = LanguageHelper.GetString("amount");
            price.HeaderText = LanguageHelper.GetString("price");
        }
    }
}
