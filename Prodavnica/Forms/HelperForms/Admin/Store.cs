using Org.BouncyCastle.Asn1.X509;
using Prodavnica.Database.DTO;
using Prodavnica.Database.Repository;
using Prodavnica.Forms.HelperForms.Admin.Popup;
using Prodavnica.Forms.HelperForms.Admin.Popup.Products;
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
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Prodavnica.Forms.HelperForms.Admin
{
    public partial class StoreTable : Form
    {
        private User user;
        private List<Product> products;
        private ProductsDAOImpl productsDAO = new ProductsDAOImpl();
        private List<Category> categories;
        private CategoryDAOImpl categoryDAO = new CategoryDAOImpl();
        private List<Manufacturer> manufacturers;
        private ManufacturerDAOImpl manufacturerDAO = new ManufacturerDAOImpl();
        private string deleteConfirmation;
        private string confirmation;
        private string selectedSupplier;
        private string selectedManufactuer;
        public StoreTable(User user)
        {
            InitializeComponent();
            this.user = user;
            LoadSettings.ApplySettins(user, this);
            this.Resize += Store_Resize;

            SetProdutsToDGV();

            ProductsMenuResize();
            gbProducts.Resize += gbProduct_Resize;
            gbAdd.Resize += gbAdd_Resize; ;
            ChangeText();
            btnDeleteItem.BackColor = Color.White;
            /*     btnAdd.Enabled = false;
                 btnAdd.Visible = false;
            AKO NISI ADMIN 
            jos u gbProduct_Resize
            jedna if isAdmin prije  foreach (System.Windows.Forms.Button btn in buttons)
            i u changetext napraviti da pise 
             */
        }

        private void gbAdd_Resize(object? sender, EventArgs e)
        {
            txtPrice.Width = gbAdd.ClientSize.Width - txtPrice.Margin.Horizontal - lblPrice.Width - 70;
            txtAmount.Width = txtPrice.Width;
            txtTotal.Width = txtPrice.Width;
            txtSearchBarCode.Width = txtPrice.Width;
        }

        public void ChangeText()
        {
            LoadSettings.ApplySettins(user, this);
            btnAdd.Text = LanguageHelper.GetString("btnAdd");
            btnAddToCart.Text = LanguageHelper.GetString("btnAdd");
            btnDelete.Text = LanguageHelper.GetString("btnDelete");
            btnUpdate.Text = LanguageHelper.GetString("btnUpdate");
            btnBuy.Text = LanguageHelper.GetString("btnBuy");
            dgvProducts.Columns[0].HeaderText = LanguageHelper.GetString("barcode");
            dgvProductsBill.Columns[0].HeaderText = LanguageHelper.GetString("barcode");
            dgvProducts.Columns[1].HeaderText = LanguageHelper.GetString("name");
            dgvProductsBill.Columns[1].HeaderText = LanguageHelper.GetString("name");
            dgvBill.Columns[0].HeaderText = LanguageHelper.GetString("name");
            dgvProducts.Columns[2].HeaderText = LanguageHelper.GetString("price");
            dgvBill.Columns[2].HeaderText = LanguageHelper.GetString("price");
            lblPrice.Text = LanguageHelper.GetString("price");
            dgvProducts.Columns[3].HeaderText = LanguageHelper.GetString("supplies");
            dgvProducts.Columns[4].HeaderText = LanguageHelper.GetString("expirationDate");
            dgvProducts.Columns[5].HeaderText = LanguageHelper.GetString("description");
            dgvProducts.Columns[6].HeaderText = LanguageHelper.GetString("category");
            dgvProducts.Columns[7].HeaderText = LanguageHelper.GetString("manufacturer");
            lblSearch.Text = LanguageHelper.GetString("lblSearch");
            lblSearchProduct.Text = LanguageHelper.GetString("lblSearch");
            deleteConfirmation = LanguageHelper.GetString("deleteConfirmation");
            lblAmount.Text = LanguageHelper.GetString("lblAmount");
            confirmation = LanguageHelper.GetString("confirmation");
            dgvBill.Columns[1].HeaderText = LanguageHelper.GetString("amount");
            btnDeleteItem.Text = LanguageHelper.GetString("btnDeleteItem");
        }

        private void GetProduts()
        {
            products = productsDAO.GetAll();
            categories = categoryDAO.GetAll();
            manufacturers = manufacturerDAO.GetAll();
            foreach (Product product in products)
            {
                Category matchingCategory = categories.FirstOrDefault(cat => cat.Id == product.IdCategory);
                if (matchingCategory != null)
                {
                    product.CategoryName = matchingCategory.Name;
                }
                Manufacturer manufacturer = manufacturers.FirstOrDefault(man => man.id == product.IdManufacturer);
                if (manufacturer != null)
                {
                    product.ManufacturerName = manufacturer.Name;
                }
            }
        }
        private void SetProdutsToDGV()
        {
            GetProduts();
            dgvProducts.DataSource = null;
            dgvProducts.DataSource = products;
            dgvProducts.Refresh();

            dgvProductsBill.DataSource = products;
        }
        private void Store_Resize(object? sender, EventArgs e)
        {

            if (dgvProducts.AutoSizeColumnsMode == DataGridViewAutoSizeColumnsMode.None)
            {
                int totalWidth = dgvProducts.ClientSize.Width - (dgvProducts.RowHeadersVisible ? dgvProducts.RowHeadersWidth : 0);

                int visibleColumnCount = dgvProducts.Columns.GetColumnCount(DataGridViewElementStates.Visible);
                int resizableColumnCount = visibleColumnCount;
                int totalFixedWidth = 0;

                foreach (DataGridViewColumn column in dgvProducts.Columns)
                {
                    if (column.Visible)
                    {
                        if (column.Resizable == DataGridViewTriState.False)
                        {
                            totalFixedWidth += column.Width;
                            resizableColumnCount--;
                        }
                    }
                }

                int resizableColumnWidth = (totalWidth - totalFixedWidth) / resizableColumnCount;

                foreach (DataGridViewColumn column in dgvProducts.Columns)
                {
                    if (column.Visible && column.Resizable != DataGridViewTriState.False)
                    {
                        column.Width = resizableColumnWidth;
                    }
                }
            }
        }

        private void ProductsMenuResize()
        {
            int padding = 5;
            int xPos = padding;

            var sortedControls = gbProducts.Controls.Cast<Control>()
                                   .Where(ctrl => ctrl is System.Windows.Forms.Button || ctrl is Label || ctrl is System.Windows.Forms.TextBox)
                                   .OrderBy(ctrl => ctrl.Location.X)
                                   .ToList();

            int maxHeight = 0;

            foreach (Control ctrl in sortedControls)
            {
                if (ctrl is System.Windows.Forms.Button || ctrl is Label || ctrl is System.Windows.Forms.TextBox)
                {
                    ctrl.Location = new Point(xPos, ctrl.Location.Y);

                    Size textSize = TextRenderer.MeasureText(ctrl.Text, ctrl.Font);

                    if (ctrl is Label || ctrl is System.Windows.Forms.TextBox)
                    {
                        ctrl.Size = new Size(textSize.Width, textSize.Height);
                    }

                    maxHeight = Math.Max(maxHeight, ctrl.Height);
                    xPos += ctrl.Width + padding;
                }
            }

            foreach (Control ctrl in sortedControls)
            {
                ctrl.Height = maxHeight;
            }
        }
        private void gbProduct_Resize(object sender, EventArgs e)
        {
            int padding = 5;
            int margin = 10;
            int totalWidth = gbProducts.ClientSize.Width;
            int usedWidth = margin;

            List<System.Windows.Forms.Button> buttons = gbProducts.Controls.OfType<System.Windows.Forms.Button>().OrderBy(b => b.Location.X).ToList();
            Label labelControl = gbProducts.Controls.OfType<Label>().FirstOrDefault();
            System.Windows.Forms.TextBox textBox = gbProducts.Controls.OfType<System.Windows.Forms.TextBox>().FirstOrDefault();

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
        }

        private void txtSeatch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                string searchText = txtSearch.Text.Trim().ToLower();

                if (!string.IsNullOrEmpty(searchText))
                {
                    List<Product> productList = products;

                    if (productList != null)
                    {
                        List<Product> filteredList = productList
                            .Where(p => p.Name.ToLower().Contains(searchText))
                            .ToList();

                        dgvProducts.DataSource = null;
                        dgvProducts.DataSource = filteredList;
                    }
                }
                else
                {
                    dgvProducts.DataSource = products;
                }
            }
        }

        private void dgvProducts_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgvProducts.ClearSelection();
            if (e.RowIndex >= 0 && e.RowIndex < dgvProducts.Rows.Count)
            {
                dgvProducts.Rows[e.RowIndex].Selected = true;
            }
        }
        private void tcShop_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcShop.SelectedTab?.Name == "tpProducts")
            {
                Store_Resize(sender, e);
            }
            if (tcShop.SelectedTab?.Name == "tbProcurement")
            {

            }
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvProducts.SelectedRows[0];

                decimal.TryParse(row.Cells["Price"].Value?.ToString(), out decimal price);
                DateTime.TryParse(row.Cells["ExpirationDate"].Value?.ToString(), out DateTime date);
                int.TryParse(row.Cells["Supplies"].Value?.ToString(), out int supplies);

                Product product = new Product
                {
                    Name = row.Cells["NameColumn"].Value?.ToString(),
                    BarCode = row.Cells["BarCode"].Value?.ToString(),
                    Price = price,
                    ExpirationDate = date,
                    Supplies = supplies,
                    Description = row.Cells["Description"].Value?.ToString(),
                    ManufacturerName = row.Cells["ManufacturerName"].Value?.ToString(),
                    CategoryName = row.Cells["CategoryName"].Value?.ToString(),
                };
                product.Id = products.FirstOrDefault(p => p.BarCode == product.BarCode).Id;
                using (ProductsDetails productsDetails = new ProductsDetails(user, product, true))
                {
                    if (productsDetails.ShowDialog() == DialogResult.OK)
                    {
                        SetProdutsToDGV();
                    }
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Product product = new Product();
            using (ProductsDetails productsDetails = new ProductsDetails(user, product, false))
            {
                if (productsDetails.ShowDialog() == DialogResult.OK)
                {
                    SetProdutsToDGV();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvProducts.SelectedRows[0];
                string barcode = row.Cells["BarCode"].Value?.ToString();
                int id = products.FirstOrDefault(p => p.BarCode == barcode).Id;
                DialogResult result = MessageBox.Show(deleteConfirmation, confirmation, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    productsDAO.DeleteProduct(id);
                    SetProdutsToDGV();
                }

            }
        }
    }
}
