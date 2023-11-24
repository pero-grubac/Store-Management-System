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

namespace Prodavnica.Forms.HelperForms.Admin.Popup.Manufactuer
{
    public partial class SelectManufactuer : Form
    {
        private User user;
        private List<Database.DTO.Manufacturer> manufacturers;
        private ManufacturerDAOImpl manufacturerDAO = new ManufacturerDAOImpl();
        public string selectManufactuer;
        public SelectManufactuer(User user)
        {
            InitializeComponent();
            this.user = user;
            LoadSettings.ApplySettins(user, this);
            manufacturers = manufacturerDAO.GetAll();
            foreach (Database.DTO.Manufacturer manufacturer in manufacturers)
            {
                cbSupplier.Items.Add(manufacturer.Name);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (cbSupplier.SelectedItem != null)
            {
                selectManufactuer = cbSupplier.SelectedItem?.ToString();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Please select a manufacturer.");
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            selectManufactuer = null;
            this.Close();
        }
    }
}
