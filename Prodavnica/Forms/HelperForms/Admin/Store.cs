using Org.BouncyCastle.Asn1.X509;
using Prodavnica.Database.DTO;
using Prodavnica.Database.Repository;
using Prodavnica.Forms.HelperForms.Admin.Popup;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Prodavnica.Forms.HelperForms.Admin
{
    public partial class Store : Form
    {
        private User user;
        private List<Product> products;
        private ProductsDAOImpl productsDAO = new ProductsDAOImpl();
        private List<Category> categories;
        private CategoryDAOImpl categoryDAO = new CategoryDAOImpl();
        private List<Manufacturer> manufacturers;
        private ManufacturerDAOImpl manufacturerDAO = new ManufacturerDAOImpl();


        private string selectedSupplier;
        public Store(User user)
        {
            InitializeComponent();
            this.user = user;
            LoadSettings.ApplySettins(user, this);
            this.Resize += Store_Resize;
            GetProduts();
            SetProdutsToDGV();

            ProductsMenuResize();
            gbProducts.Resize += gbProduct_Resize;

            gbSupplier.Resize += gbSupplier_Resize;
            gbManufactuer.Resize += gbManufactuer_Resize;
            gbCategoryName.Resize += gbCategoryName_Resize;
            gbBarcode.Resize += gbBarcode_Resize;
            gbDescription.Resize += gbDescription_Resize;
            gbPrice.Resize += gbPrice_Resize;
            gbProductName.Resize += gbProductName_Resize;
            gbPurchasePrice.Resize += gbPurchasePrice_Resize;
            gbSupplies.Resize += gbSupplies_Resize;

            btnAddBasket.Enabled = false;
            btnAddBasket.Visible = false;

            btnBuy.Enabled = false;
            btnBuy.Visible = false;

            btnBskRemove.Enabled = false;
            btnBskRemove.Visible = false;
        }

        private void gbSupplies_Resize(object? sender, EventArgs e)
        {
            txtSupplies.Width = gbSupplies.ClientSize.Width - SystemInformation.VerticalScrollBarWidth;
        }

        private void gbPurchasePrice_Resize(object? sender, EventArgs e)
        {
            txtPurchasePrice.Width = gbPurchasePrice.ClientSize.Width - SystemInformation.VerticalScrollBarWidth;
        }

        private void gbProductName_Resize(object? sender, EventArgs e)
        {
            txtName.Width = gbProductName.ClientSize.Width - SystemInformation.VerticalScrollBarWidth;
        }

        private void gbPrice_Resize(object? sender, EventArgs e)
        {
            txtPrice.Width = gbPrice.ClientSize.Width - SystemInformation.VerticalScrollBarWidth;
        }

        private void gbDescription_Resize(object? sender, EventArgs e)
        {
            txtDescription.Width = gbDescription.ClientSize.Width - SystemInformation.VerticalScrollBarWidth;
        }

        private void gbBarcode_Resize(object? sender, EventArgs e)
        {
            txtBarCode.Width = gbBarcode.ClientSize.Width - SystemInformation.VerticalScrollBarWidth;
        }

        private void gbCategoryName_Resize(object? sender, EventArgs e)
        {
            txtCategoryName.Width = gbCategoryName.ClientSize.Width - SystemInformation.VerticalScrollBarWidth;
        }

        private void gbManufactuer_Resize(object? sender, EventArgs e)
        {
            int totalButtonWidth = btnNewMan.Width + btnOldMan.Width + btnNewMan.Margin.Horizontal + btnOldMan.Margin.Horizontal;
            txtManufactuer.Width = gbManufactuer.ClientSize.Width - totalButtonWidth - SystemInformation.VerticalScrollBarWidth;
        }

        private void gbSupplier_Resize(object sender, EventArgs e)
        {
            int totalButtonWidth = btnNewSup.Width + btnOldSup.Width + btnNewSup.Margin.Horizontal + btnOldSup.Margin.Horizontal;
            txtSupplier.Width = gbSupplier.ClientSize.Width - totalButtonWidth - SystemInformation.VerticalScrollBarWidth;
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

            dgvProducts.DataSource = products;

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
                                   .Where(ctrl => ctrl is Button || ctrl is Label || ctrl is TextBox)
                                   .OrderBy(ctrl => ctrl.Location.X)
                                   .ToList();

            int maxHeight = 0;

            foreach (Control ctrl in sortedControls)
            {
                if (ctrl is Button || ctrl is Label || ctrl is TextBox)
                {
                    ctrl.Location = new Point(xPos, ctrl.Location.Y);

                    Size textSize = TextRenderer.MeasureText(ctrl.Text, ctrl.Font);

                    if (ctrl is Label || ctrl is TextBox)
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
            int usedWidth = 0;

            foreach (Control ctrl in gbProducts.Controls)
            {
                if (ctrl is Button || ctrl is Label)
                {
                    usedWidth += ctrl.Width + padding;
                }
            }

            int textBoxWidth = totalWidth - usedWidth - padding - margin;

            TextBox textBox = gbProducts.Controls.OfType<TextBox>().FirstOrDefault();

            if (textBox != null)
            {
                textBox.Width = textBoxWidth >= 0 ? textBoxWidth : 0;
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

        private void btnSuppliers_Click(object sender, EventArgs e)
        {
            using (SelectSupplier selectSupplier = new SelectSupplier(user))
            {
                if (selectSupplier.ShowDialog() == DialogResult.OK)
                {
                    selectedSupplier = selectSupplier.selectSupplier;
                    if (selectedSupplier != null)
                    {
                        txtSupplier.Text = selectedSupplier;
                    }
                }
                else
                {
                    MessageBox.Show("Operation canceled.");
                }
            }
        }
        private void ResizeProcurement(object sender, EventArgs e)
        {
            gbSupplier_Resize(sender, e);
            gbManufactuer_Resize(sender, e);
            gbCategoryName_Resize(sender, e);
            gbBarcode_Resize(sender, e);
            gbDescription_Resize(sender, e);
            gbPrice_Resize(sender, e);
            gbProductName_Resize(sender, e);
            gbPurchasePrice_Resize(sender, e);
            gbSupplies_Resize(sender, e);
        }
        private void tcShop_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcShop.SelectedTab?.Name == "tpProducts")
            {
                Store_Resize(sender, e);
            }
            if (tcShop.SelectedTab?.Name == "tbProcurement")
            {
                ResizeProcurement(sender, e);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            using (NewSupplier newSupplier = new NewSupplier(user))
            {
                if (newSupplier.ShowDialog() == DialogResult.OK)
                {
                    selectedSupplier = newSupplier.name;
                    if (selectedSupplier != null)
                    {
                        txtSupplier.Text = selectedSupplier;
                    }

                }
                else
                {
                    MessageBox.Show("Operation canceled.");
                }
            }
        }

        private void btnAddBasket_Click(object sender, EventArgs e)
        {
            btnNewSup.Enabled = false;
            btnOldSup.Enabled = false;

            btnBuy.Enabled = true;
            btnBuy.Visible = true;

            btnBskRemove.Enabled = true;
            btnBskRemove.Visible = true;
        }

        private void txtSupplier_TextChanged(object sender, EventArgs e)
        {
            btnAddBasket.Visible = true;
            btnAddBasket.Enabled=true;

        }
    }
}
