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
    public partial class Category : Form
    {
        private User user;
        private List<Database.DTO.Category> categories;
        private CategoryDAOImpl categoryDAO = new CategoryDAOImpl();
        private string deleteConfirmation;
        private string confirmation;
        public Category(User user)
        {
            InitializeComponent();
            this.user = user;
            ChangeText();
            gbCategory.Resize += gbCategory_Resize;
            SetDataToDGV();
        }

        private void SetDataToDGV()
        {
            categories = categoryDAO.GetAll();
            dgvCategory.DataSource = null;
            dgvCategory.DataSource = categories;
            dgvCategory.Refresh();
        }

        private void gbCategory_Resize(object? sender, EventArgs e)
        {
            int padding = 5;
            int margin = 10;
            int totalWidth = gbCategory.ClientSize.Width;
            int usedWidth = margin;

            List<System.Windows.Forms.Button> buttons = gbCategory.Controls.OfType<System.Windows.Forms.Button>().OrderBy(b => b.Location.X).ToList();
            Label labelControl = gbCategory.Controls.OfType<Label>().FirstOrDefault();
            System.Windows.Forms.TextBox textBox = gbCategory.Controls.OfType<System.Windows.Forms.TextBox>().FirstOrDefault();

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

        private void ChangeText()
        {
            LoadSettings.ApplySettins(user, this);
            btnAdd.Text = LanguageHelper.GetString("btnAdd");
            btnUpdate.Text = LanguageHelper.GetString("btnUpdate");
            lblSearch.Text = LanguageHelper.GetString("lblSearch");
            deleteConfirmation = LanguageHelper.GetString("deleteConfirmation");
            confirmation = LanguageHelper.GetString("confirmation");
            name.HeaderText = LanguageHelper.GetString("name");
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            Database.DTO.Category cat = new Database.DTO.Category();
            using (Popup.Category.CategoryPopup categoryPopup = new Popup.Category.CategoryPopup(user, cat, false))
            {
                if (categoryPopup.ShowDialog() == DialogResult.OK)
                {
                    SetDataToDGV();
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvCategory.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvCategory.SelectedRows[0];
                Database.DTO.Category cat = new Database.DTO.Category
                {
                    Name = row.Cells["name"].Value?.ToString(),
                };
                cat.Id = categories.FirstOrDefault(c => c.Name == cat.Name).Id;
                using (Popup.Category.CategoryPopup categoryPopup = new Popup.Category.CategoryPopup(user, cat, true))
                {
                    if (categoryPopup.ShowDialog() == DialogResult.OK)
                    {
                        SetDataToDGV();
                    }
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
                    List<Database.DTO.Category> categorieList = categories;
                    
                    List<Database.DTO.Category> filteredList = categorieList
                        .Where(c => c.Name.ToLower().Contains(searchText))
                        .ToList();
                    dgvCategory.DataSource = null;
                    dgvCategory.DataSource = filteredList;
                    dgvCategory.Refresh();
                }
                else
                {
                    SetDataToDGV();
                }
            }
        }
    }
}
