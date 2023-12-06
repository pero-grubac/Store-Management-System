using Org.BouncyCastle.Bcpg.Sig;
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

namespace Prodavnica.Forms.HelperForms.Admin.Popup.ProcurementPopup
{
    public partial class ProcurementDetails : Form
    {
        private Database.DTO.User user;
        public ProcurementDetails(User user, List<ProcurementItem> list)
        {
            InitializeComponent();
            this.user = user;
            dgvProcurement.DataSource = list;
            ChangeText();
        }
        private void ChangeText()
        {
            LoadSettings.ApplySettins(user, this);
            manufacturer.HeaderText= LanguageHelper.GetString("manufacturer");
            name.HeaderText = LanguageHelper.GetString("name");
            amount.HeaderText = LanguageHelper.GetString("amount");
            price.HeaderText = LanguageHelper.GetString("price");

        }
    }
}
