using Org.BouncyCastle.Asn1.X509;
using Prodavnica.Database.DTO;
using Prodavnica.Database.Repository;
using Prodavnica.Language;
using Prodavnica.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.PerformanceData;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prodavnica.Forms.HelperForms.Admin.Popup.Category
{
    public partial class CategoryPopup : Form
    {
        private User user;
        private Database.DTO.Category selectedCategory;
        private List<Database.DTO.Category> categories;
        private CategoryDAOImpl categoryDAO = new CategoryDAOImpl();
        private bool isUpdate;
        public CategoryPopup(User user, Database.DTO.Category category, bool isUpdate)
        {
            InitializeComponent();
            this.user = user;
            this.selectedCategory = category;
            this.isUpdate = isUpdate;
            if (isUpdate)
            {
                SetData();
            }
            ChangeText();
            lblWarrning.Visible = false;
            categories = categoryDAO.GetAll();
        }
        private void SetData()
        {
            txtName.Text = selectedCategory.Name;
        }
        private void ChangeText()
        {
            LoadSettings.ApplySettins(user, this);
            lblName.Text = LanguageHelper.GetString("lblName");
            btnSave.Text = LanguageHelper.GetString("btnSave");
            btnCancel.Text = LanguageHelper.GetString("btnCancel");
            lblWarrning.Text = LanguageHelper.GetString("categoryExists");
            lblWarrning.ForeColor = Color.Red;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtName.Text))
            {
                Database.DTO.Category category = new Database.DTO.Category();
                category.Name = txtName.Text;
                if (isUpdate)
                {
                    if (categories.Any(c => c.Name == txtName.Text) && category.Name != selectedCategory.Name)
                    {
                        lblWarrning.Visible = true;
                    }
                    else
                    {
                        lblWarrning.Visible = false;

                        category.Id=selectedCategory.Id;
                        categoryDAO.Update(category);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                else
                {
                    if (categories.Any(c => c.Name == txtName.Text))
                    {
                        lblWarrning.Visible = true;
                    }
                    else
                    {
                        lblWarrning.Visible = false;
                        categoryDAO.Create(category);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
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
