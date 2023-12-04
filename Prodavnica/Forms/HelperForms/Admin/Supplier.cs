using Prodavnica.Database.DAO;
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

namespace Prodavnica.Forms.HelperForms.Admin
{
    public partial class Supplier : Form
    {
        private User user;
        private bool isSupplier;
        private List<Database.DTO.Supplier> suppliers;
        private List<Database.DTO.Manufacturer> manufacturers;
        private SupplierDAOImpl supplierDAO = new SupplierDAOImpl();
        private ManufacturerDAOImpl manufacturerDAO = new ManufacturerDAOImpl();
        private string deleteConfirmation;
        private string confirmation;
        public Supplier(User user, bool isSupplier)
        {
            InitializeComponent();
            this.user = user;
            this.isSupplier = isSupplier;

            gb.Resize += gb_Resize;
            SetDataToDGV();
            ChangeText();

        }

        private void ChangeText()
        {
            LoadSettings.ApplySettins(user, this);
            btnAdd.Text = LanguageHelper.GetString("btnAdd");
            btnDelete.Text = LanguageHelper.GetString("btnDelete");
            btnUpdate.Text = LanguageHelper.GetString("btnUpdate");
            lblSearch.Text = LanguageHelper.GetString("lblSearch");
            deleteConfirmation = LanguageHelper.GetString("deleteConfirmation");
            confirmation = LanguageHelper.GetString("confirmation");
            nameColumn.HeaderText = LanguageHelper.GetString("name");
            emailColumn.HeaderText= LanguageHelper.GetString("lblEmail");
        }

        private void SetDataToDGV()
        {
            dgv.DataSource = null;

            if (isSupplier)
            {
                suppliers = supplierDAO.GetAll();
                dgv.DataSource = suppliers;
            }
            else
            {
                manufacturers = manufacturerDAO.GetAll();
                dgv.DataSource = manufacturers;
            }
            dgv.Refresh();
        }

        private void gb_Resize(object? sender, EventArgs e)
        {
            int padding = 5;
            int margin = 10;
            int totalWidth = gb.ClientSize.Width;
            int usedWidth = margin;

            List<System.Windows.Forms.Button> buttons = gb.Controls.OfType<System.Windows.Forms.Button>().OrderBy(b => b.Location.X).ToList();
            Label labelControl = gb.Controls.OfType<Label>().FirstOrDefault();
            System.Windows.Forms.TextBox textBox = gb.Controls.OfType<System.Windows.Forms.TextBox>().FirstOrDefault();

            foreach (System.Windows.Forms.Button btn in buttons)
            {
                int btnWidth = TextRenderer.MeasureText(btn.Text, btn.Font).Width + padding * 2;
                btn.Width = btnWidth;
                btn.Location = new Point(usedWidth, btn.Location.Y);
                usedWidth += btnWidth + padding;
            }

            if (labelControl != null)
            {
                int labelWidth = TextRenderer.MeasureText(labelControl.Text, labelControl.Font).Width + padding * 2;
                labelControl.Width = labelWidth;
                labelControl.Location = new Point(usedWidth, labelControl.Location.Y);
                usedWidth += labelWidth + padding;
            }

            if (textBox != null)
            {
                int textBoxWidth = totalWidth - usedWidth - margin;
                textBox.Width = textBoxWidth >= 0 ? textBoxWidth : 0;
                textBox.Location = new Point(usedWidth, textBox.Location.Y);
            }
            btnAdd.Height = btnUpdate.Height;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (Popup.SupplierPopup.SupplierPopup supplierPopup = new Popup.SupplierPopup.SupplierPopup(user, isSupplier, "", "", false))
            {
                if (supplierPopup.ShowDialog() == DialogResult.OK)
                {
                    SetDataToDGV();
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgv.SelectedRows[0];
                string name = row.Cells["NameColumn"].Value?.ToString();
                string email = row.Cells["EmailColumn"].Value?.ToString();
                using (Popup.SupplierPopup.SupplierPopup supplierPopup = new Popup.SupplierPopup.SupplierPopup(user, isSupplier, name, email, true))
                {
                    if (supplierPopup.ShowDialog() == DialogResult.OK)
                    {
                        SetDataToDGV();
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgv.SelectedRows[0];
                string name = row.Cells["NameColumn"].Value?.ToString();
                Database.DTO.Supplier supplier = new Database.DTO.Supplier();
                Manufacturer manufacturer = new Manufacturer();
                if (isSupplier)
                {
                    supplier = suppliers.FirstOrDefault(sup => sup.Name == name);
                }
                else
                {
                    manufacturer = manufacturers.FirstOrDefault(man => man.Name == name);
                }
                DialogResult result = MessageBox.Show(deleteConfirmation, confirmation, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (isSupplier)
                    {
                        supplierDAO.DeleteSupplier(supplier);
                    }
                    else
                    {
                        manufacturerDAO.Delete(manufacturer);
                    }
                    SetDataToDGV();
                }
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                string searchText = txtSearch.Text.Trim().ToLower();

                if (!string.IsNullOrEmpty(searchText))
                {
                    List<Database.DTO.Supplier> supplierList = suppliers;
                    List<Database.DTO.Manufacturer> manufacturerList = manufacturers;

                    if (isSupplier)
                    {
                        List<Database.DTO.Supplier> filteredList = supplierList
                            .Where(s => s.Name.ToLower().Contains(searchText))
                        .ToList();
                        dgv.DataSource = null;
                        dgv.DataSource = filteredList;
                        dgv.Refresh();
                    }
                    else
                    {
                        List<Manufacturer> filteredList = manufacturerList
                            .Where(m => m.Name.ToLower().Contains(searchText))
                        .ToList();
                        dgv.DataSource = null;
                        dgv.DataSource = filteredList;
                        dgv.Refresh();
                    }
                }
                else
                {
                    SetDataToDGV();
                }
            }
        }
    }
}
