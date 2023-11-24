using Prodavnica.Database.DTO;
using Prodavnica.Database.Repository;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Prodavnica.Forms.HelperForms.Admin.Popup.Supplier
{
    public partial class SelectSupplier : Form
    {
        private User user;
        private List<Database.DTO.Supplier> suppliers;
        private SupplierDAOImpl supplierDAO = new SupplierDAOImpl();
        public string selectSupplier;
        public SelectSupplier(User user)
        {
            InitializeComponent();
            this.user = user;
            LoadSettings.ApplySettins(user, this);
            suppliers = supplierDAO.GetAll();
            foreach (Database.DTO.Supplier supplier in suppliers)
            {
                cbSupplier.Items.Add(supplier.Name);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (cbSupplier.SelectedItem != null)
            {
                selectSupplier = cbSupplier.SelectedItem?.ToString();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Please select a supplier.");
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            selectSupplier = null;
            this.Close();
        }
    }
}
