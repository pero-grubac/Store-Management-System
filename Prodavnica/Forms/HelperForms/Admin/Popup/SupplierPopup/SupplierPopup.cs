using Prodavnica.Database.DTO;
using Prodavnica.Database.Repository;
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

namespace Prodavnica.Forms.HelperForms.Admin.Popup.SupplierPopup
{
    public partial class SupplierPopup : Form
    {
        private User user;
        private bool isSupplier;
        private List<Database.DTO.Supplier> suppliers;
        private List<Database.DTO.Manufacturer> manufacturers;
        private SupplierDAOImpl supplierDAO = new SupplierDAOImpl();
        private ManufacturerDAOImpl manufacturerDAO = new ManufacturerDAOImpl();
        private string name;
        private string email;
        private bool isUpdate;
        public SupplierPopup(User user, bool isSupplier, string name, string email, bool isUpdate)
        {
            InitializeComponent();
            this.user = user;
            this.isSupplier = isSupplier;
            this.name = name;
            this.email = email;
            this.isUpdate = isUpdate;
            if (isSupplier)
            {
                suppliers = supplierDAO.GetAll();
            }
            else
            {
                manufacturers = manufacturerDAO.GetAll();
            }
            if (isUpdate)
            {
                txtEmail.Text = email;
                txtName.Text = name;
            }
            LoadSettings.ApplySettins(user, this);
            ChangeText();
            lblWarrning.Visible = false;
        }

        private void ChangeText()
        {
            lblEmail.Text = LanguageHelper.GetString("lblEmail");
            lblName.Text = LanguageHelper.GetString("name");
            btnSave.Text = LanguageHelper.GetString("btnSave");
            btnCancel.Text = LanguageHelper.GetString("btnCancel");
            if (isSupplier)
            {
                lblWarrning.Text = LanguageHelper.GetString("supplierExists");
            }
            else
            {
                lblWarrning.Text = LanguageHelper.GetString("manufacturerExists");
            }
            lblWarrning.ForeColor = Color.Red;
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmail.Text) && !string.IsNullOrEmpty(txtName.Text))
            {
                if (isUpdate)
                {
                    if (isSupplier)
                    {
                        Database.DTO.Supplier supplier = suppliers.FirstOrDefault(s => s.Name == name);
                        supplier.Name = txtName.Text;
                        supplier.Email = txtEmail.Text;
                        supplierDAO.UpdateSupplier(supplier);
                    }
                    else
                    {
                        Manufacturer manufacturer = manufacturers.FirstOrDefault(m => m.Name == name);
                        manufacturer.Name = txtName.Text;
                        manufacturer.Email = txtEmail.Text;
                        manufacturerDAO.Update(manufacturer);
                    }
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    if (isSupplier)
                    {
                        if (suppliers.Any(s => s.Name == txtName.Text))
                        {
                            lblWarrning.Visible = true;
                        }
                        else
                        {

                            Database.DTO.Supplier supplier = new Database.DTO.Supplier
                            {
                                Name = txtName.Text,
                                Email = txtEmail.Text
                            };
                            lblWarrning.Visible = false;
                            supplierDAO.AddSupplier(supplier);
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                    }
                    else
                    {
                        if (manufacturers.Any(m => m.Name == txtName.Text))
                        {
                            lblWarrning.Visible = true;
                        }
                        else
                        {
                            Manufacturer manufacturer = new Manufacturer
                            {
                                Name = txtName.Text,
                                Email = txtEmail.Text,
                            };
                            manufacturerDAO.AddManufactuer(manufacturer);
                            lblWarrning.Visible = false;
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                    }
                }

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
