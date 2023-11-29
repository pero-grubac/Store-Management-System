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
    public partial class Producer : Form
    {
        private List<Manufacturer> manufacturers;
        private ManufacturerDAOImpl manufacturerDAO = new ManufacturerDAOImpl();
        private bool isProducer;
        private List<Supplier> suppliers;
        private SupplierDAOImpl supplierDAO = new SupplierDAOImpl();
        public Producer(User user, bool isProducer)
        {
            InitializeComponent();
            this.isProducer = isProducer;

            gbCreate.Resize += gbCreate_Resize;
            gbUpdate.Resize += gbUpdate_Resize;

                SetDataToDGV();
            


            LoadSettings.ApplySettins(user, this);
            btnDelete.BackColor = Color.White;
            ChangeText();

        }
        private void ChangeText()
        {
            lblEmail.Text = LanguageHelper.GetString("lblEmail");
            lblUpdateEmail.Text = LanguageHelper.GetString("lblEmail");
            lblName.Text = LanguageHelper.GetString("name");
            lblUpdateName.Text = LanguageHelper.GetString("name");
            btnCreate.Text = LanguageHelper.GetString("btnSave");
            btnUpdate.Text = LanguageHelper.GetString("btnSave");
            btnDelete.Text = LanguageHelper.GetString("btnDelete");
            dgvProducer.Columns[0].HeaderText = LanguageHelper.GetString("name");
            dgvProducer.Columns[1].HeaderText = LanguageHelper.GetString("lblEmail");
            gbCreate.Text = LanguageHelper.GetString("create");
            gbUpdate.Text = LanguageHelper.GetString("btnUpdate");
        }
        private void SetDataToDGV()
        {

            dgvProducer.DataSource = null;
            if (isProducer)
            {
                manufacturers = manufacturerDAO.GetAll();
                dgvProducer.DataSource = manufacturers;
            }
            else
            {
                suppliers = supplierDAO.GetAll();
                dgvProducer.DataSource = suppliers;
            }

            dgvProducer.Refresh();
        }
        private void gbUpdate_Resize(object? sender, EventArgs e)
        {
            txtName.Width = gbUpdate.ClientSize.Width - txtName.Margin.Horizontal - lblName.Width - 44;
            txtEmail.Width = txtName.Width;
        }

        private void gbCreate_Resize(object? sender, EventArgs e)
        {
            txtUpdateName.Width = gbCreate.ClientSize.Width - txtUpdateName.Margin.Horizontal - lblUpdateName.Width - 44;
            txtUpdateEmail.Width = txtUpdateName.Width;
        }

        private void dgvProducer_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgvProducer.ClearSelection();
            if (e.RowIndex >= 0 && e.RowIndex < dgvProducer.Rows.Count)
            {
                dgvProducer.Rows[e.RowIndex].Selected = true;
            }
        }
    }
}
