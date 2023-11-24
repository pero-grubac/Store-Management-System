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

namespace Prodavnica.Forms.HelperForms.Admin.Popup.Manufactuer
{
    public partial class NewManufactuer : Form
    {
        private User user;
        private List<Database.DTO.Manufacturer> manufactuers;
        private ManufacturerDAOImpl manufacturerDAO = new ManufacturerDAOImpl();
        public string name;
        public string email;

        public NewManufactuer(User user)
        {
            InitializeComponent();
            this.user = user;
            LoadSettings.ApplySettins(user, this);
            manufactuers = manufacturerDAO.GetAll();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtName.Text) && !manufactuers.Any(man => man.Name == txtName.Text))
            {
                name = txtName.Text;
                email = txtEmail.Text;
                manufacturerDAO.AddManufactuer(new Database.DTO.Manufacturer()
                {
                    Name = name,
                    Email = email
                });
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Add a valid  manufactuer.");
            }
        }

        private void btnCencel_Click(object sender, EventArgs e)
        {
            name = null;
           this.Close();
        }
    }
}
