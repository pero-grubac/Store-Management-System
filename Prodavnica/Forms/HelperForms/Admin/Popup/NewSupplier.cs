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

namespace Prodavnica.Forms.HelperForms.Admin.Popup
{
    public partial class NewSupplier : Form
    {
        private User user;
        private List<Supplier> suppliers;
        private SupplierDAOImpl supplierDAO = new SupplierDAOImpl();
        public string name;

        public NewSupplier(User user)
        {
            InitializeComponent();
            this.user = user;
            LoadSettings.ApplySettins(user, this);
            suppliers = supplierDAO.GetAll();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtName.Text) && !suppliers.Any(sup => sup.Name == txtName.Text))
            {
                name = txtName.Text;
                supplierDAO.AddSupplier(new Supplier()
                {
                    Name = name
                });
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Add a valid  supplier.");
            }
        }

        private void btnCencel_Click(object sender, EventArgs e)
        {
            name = null;
           this.Close();
        }
    }
}
