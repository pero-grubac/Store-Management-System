using MySqlX.XDevAPI.Relational;
using Newtonsoft.Json;
using Prodavnica.Database.DAO;
using Prodavnica.Database.DTO;
using Prodavnica.Database.Repository;
using Prodavnica.Forms.HelperForms.Admin.Popup.BillPopup;
using Prodavnica.Forms.HelperForms.Admin.Popup.ProcurementPopup;
using Prodavnica.Forms.HelperForms.Admin.Popup.Products;
using Prodavnica.Language;
using Prodavnica.Util;
using System.Data;
using System.Security.AccessControl;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Prodavnica.Forms.HelperForms.Admin
{
    public partial class StoreTable : Form
    {
        private User user;
        private List<Product> products;
        private ProductsDAOImpl productsDAO = new ProductsDAOImpl();

        private List<Database.DTO.Category> categories;
        private CategoryDAOImpl categoryDAO = new CategoryDAOImpl();

        private List<Manufacturer> manufacturers;
        private ManufacturerDAOImpl manufacturerDAO = new ManufacturerDAOImpl();

        private List<Database.DTO.Supplier> suppliers;
        private SupplierDAOImpl SupplierDAO = new SupplierDAOImpl();

        private string deleteConfirmation;
        private string confirmation;
        private string selectedSupplier;
        private string selectedManufactuer;

        private List<ProcurementItem> procurementItems = new List<ProcurementItem>();
        private ProcurementDAOImpl procurementDAO = new ProcurementDAOImpl();

        private List<BillItem> billItems = new List<BillItem>();
        private BillDAOImpl billDAO = new BillDAOImpl();

        private List<Bill> bills;
        private List<BillItem> billsItems;

        private List<Procurement> procurements;
        private List<ProcurementItem> procurementsItems;

        private decimal amount = 1;
        private string noSupplier;
        private string noMoreSupplies;
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
            gbDateFrom.Resize += gbDateFrom_Resize;
            cbSupplier.DataSource = suppliers.Select(sup => sup.Name).ToList();
            cbSupplier.SelectedItem = null;
            isAdmin(user.IsAdmin);
            txtAmount.Text = amount.ToString();
            dtpFrom.ValueChanged += DTP_ValueChanged;
            dtpTo.ValueChanged += DTP_ValueChanged;
        }

        private void DTP_ValueChanged(object? sender, EventArgs e)
        {
            FilterBills();
        }
        private void FilterBills()
        {
            dgvBills.DataSource = null;
            DateTime startDate = dtpFrom.Value.Date;
            DateTime endDate = dtpTo.Value.Date.AddDays(1).AddTicks(-1);
            if (user.IsAdmin)
            {
                List<Procurement> filteredBills = procurements
                                        .Where(proc => proc.Date >= startDate && proc.Date <= endDate)
                                        .ToList();
                dgvBills.DataSource = filteredBills;
            }
            else
            {
                List<Bill> filteredBills = bills
                                        .Where(bill => bill.Date >= startDate && bill.Date <= endDate)
                                        .ToList();
                dgvBills.DataSource = filteredBills;
                dgvBills.AutoGenerateColumns = true;

                dgvBills.Columns[0].Visible = false;
                dgvBills.Columns[1].Visible = false;
                foreach (DataGridViewColumn column in dgvBills.Columns)
                {
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            if (dgvBills.Columns["total"] is DataGridViewColumn priceColumn)
            {
                priceColumn.DefaultCellStyle.Format = "N2";
            }

            dgvBills.Refresh();
        }
        private void isAdmin(bool isAdmin)
        {
            if (!isAdmin)
            {
                btnAdd.Enabled = isAdmin;
                btnAdd.Visible = isAdmin;

                btnUpdate.Enabled = isAdmin;
                btnUpdate.Visible = isAdmin;

                btnDelete.Enabled = isAdmin;
                btnDelete.Visible = isAdmin;

                lblPrice.Enabled = isAdmin;
                lblPrice.Visible = isAdmin;
                txtPrice.Enabled = isAdmin;
                txtPrice.Visible = isAdmin;

                int rowIndexToDelete = 0;
                foreach (Control control in tlypSelect.Controls)
                {
                    if (tlypSelect.GetRow(control) == rowIndexToDelete)
                    {
                        tlypSelect.Controls.Remove(control);
                        control.Dispose();
                    }
                }
                tlypSelect.RowStyles.RemoveAt(rowIndexToDelete);
                foreach (Control control in tlypSelect.Controls)
                {
                    int row = tlypSelect.GetRow(control);
                    if (row > rowIndexToDelete)
                    {
                        tlypSelect.SetRow(control, row - 1);
                    }
                }
                tlypSelect.RowStyles[2].Height = 150;
                lblAmount.Location = new Point(6, 62);
                txtAmount.Location = new Point(102, 59);

                tlypSelect.PerformLayout();
                dgvBills.AutoGenerateColumns = true;

                dgvBills.Columns[0].Visible = isAdmin;
                dgvBills.Columns[1].Visible = isAdmin;
                foreach (DataGridViewColumn column in dgvBills.Columns)
                {
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                if (dgvBills.Columns["total"] is DataGridViewColumn priceColumn)
                {
                    priceColumn.DefaultCellStyle.Format = "N2";
                }
            }

        }
        private void gbDateFrom_Resize(object? sender, EventArgs e)
        {
            dtpFrom.Width = gbDateFrom.ClientSize.Width - dtpFrom.Margin.Horizontal - lblFrom.Width - 32;
            dtpTo.Width = dtpFrom.Width;
        }

        private void gbAdd_Resize(object? sender, EventArgs e)
        {
            txtPrice.Width = gbAdd.ClientSize.Width - txtPrice.Margin.Horizontal - lblPrice.Width - 70;
            txtAmount.Width = txtPrice.Width;
            txtTotal.Width = txtPrice.Width;
            txtSearchBarCode.Width = txtPrice.Width;
            cbSupplier.Width = txtPrice.Width;
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
            lblSupplier.Text = LanguageHelper.GetString("lblSupplier");
            noSupplier = LanguageHelper.GetString("noSupplier");
            noMoreSupplies = LanguageHelper.GetString("noMoreSupplies");
            tpProducts.Text = LanguageHelper.GetString("tpProducts");
            if (user.IsAdmin)
            {
                tbProcurement.Text = LanguageHelper.GetString("tbProcurement");
            }
            else
            {
                tbProcurement.Text = LanguageHelper.GetString("buying");
            }

            tbBills.Text = LanguageHelper.GetString("tbBills");
        }

        private void GetProduts()
        {
            suppliers = SupplierDAO.GetAll();
            products = productsDAO.GetAll();
            categories = categoryDAO.GetAll();
            manufacturers = manufacturerDAO.GetAll();
            if (user.IsAdmin)
            {
                procurements = procurementDAO.GetAll();
                procurementsItems = procurementDAO.GetItems();
            }
            else
            {
                bills = billDAO.GetAllBill();
                billsItems = billDAO.GetAllBillItem();
            }
            foreach (Product product in products)
            {
                Database.DTO.Category matchingCategory = categories.FirstOrDefault(cat => cat.Id == product.IdCategory);
                if (matchingCategory != null)
                {
                    product.CategoryName = matchingCategory.Name;
                }
                Manufacturer manufacturer = manufacturers.FirstOrDefault(man => man.id == product.IdManufacturer);
                if (manufacturer != null)
                {
                    product.ManufacturerName = manufacturer.Name;
                }
                if (user.IsAdmin)
                {
                    var list = procurementsItems.Where(pi => pi.IdProduct == product.Id).ToList();
                    if (list.Count() > 0)
                    {
                        foreach (var item in list)
                        {
                            item.Name = product.Name;
                        }
                    }
                }
                else
                {
                    var list = billsItems.Where(bill => bill.IdProduct == product.Id).ToList();

                    if (list.Count() > 0)
                    {
                        foreach (var item in list)
                        {
                            item.Name = product.Name;
                        }
                    }
                }
            }
            if (user.IsAdmin)
            {
                foreach (Database.DTO.Supplier supplier in suppliers)
                {
                    var list = procurements.Where(p => p.IdSupplier == supplier.Id).ToList();
                    if (list.Count() > 0)
                    {
                        foreach (var item in list)
                        {
                            item.SupplierName = supplier.Name;
                        }
                    }
                }
                foreach (Manufacturer man in manufacturers)
                {
                    var list = procurementsItems.Where(pi => pi.IdManufacturer == man.id).ToList();
                    if (list.Count() > 0)
                    {
                        foreach (var item in list)
                        {
                            item.ManufacturerName = man.Name;
                        }
                    }
                }
            }
        }
        private void SetProdutsToDGV()
        {
            GetProduts();
            dgvProducts.DataSource = null;
            dgvProducts.DataSource = products;
            dgvProducts.Refresh();
            if (user.IsAdmin)
            {
                dgvProductsBill.DataSource = products;
            }
            else
            {
                dgvProductsBill.DataSource = products.Where(pi => pi.Supplies > 0).ToList();
            }
            if (user.IsAdmin)
            {
                foreach (Procurement procurement in procurements)
                {
                    procurement.Total = procurementsItems
                                            .Where(item => item.IdProcurement == procurement.Id)
                                            .Sum(item => item.Amount * item.Price);
                }
            }
            else
            {
                decimal total = 0;
                foreach (Bill bill in bills)
                {
                    bill.Total = billsItems
                        .Where(item => item.IdBill == bill.Id)
                        .Sum(item => item.Amount * item.Price);
                }
            }
            FilterBills();
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
            if (user.IsAdmin)
            {
                foreach (System.Windows.Forms.Button btn in buttons)
                {
                    int btnWidth = TextRenderer.MeasureText(btn.Text, btn.Font).Width + padding * 2;
                    btn.Width = btnWidth;
                    btn.Location = new Point(usedWidth, btn.Location.Y);
                    usedWidth += btnWidth + padding;
                }
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
                SetProdutsToDGV();
            }
            else if (tcShop.SelectedTab?.Name == "tbProcurement")
            {
                Store_Resize(sender, e);
                SetProdutsToDGV();
            }
            else
            {
                Store_Resize(sender, e);
                SetProdutsToDGV();
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

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAmount.Text))
            {
                decimal amountRead = decimal.Parse(txtAmount.Text);
                if (amountRead > 0)
                {
                    if (dgvProductsBill.SelectedRows.Count > 0)
                    {
                        DataGridViewRow row = dgvProductsBill.SelectedRows[0];
                        string barcode = row.Cells["barCodeColumn"].Value?.ToString();
                        Product selectedProduct = products.FirstOrDefault(pr => pr.BarCode == barcode);
                        if (user.IsAdmin)
                        {
                            if (!string.IsNullOrEmpty(txtPrice.Text))
                            {
                                decimal price = decimal.Parse(txtPrice.Text);
                                if (price > 0)
                                {
                                    ProcurementItem procurement = new ProcurementItem
                                    {
                                        IdProduct = selectedProduct.Id,
                                        Amount = amountRead,
                                        Price = price,
                                        IdManufacturer = selectedProduct.IdManufacturer,
                                        Name = selectedProduct.Name,
                                    };
                                    if (procurementItems.Any(pi => pi.IdProduct == procurement.IdProduct))
                                    {
                                        procurementItems.RemoveAll(pi => pi.IdProduct == procurement.IdProduct);
                                    }
                                    procurementItems.Add(procurement); ;
                                }
                            }
                        }
                        else
                        {
                            if (amountRead <= selectedProduct.Supplies)
                            {
                                BillItem billItem = new BillItem
                                {
                                    IdProduct = selectedProduct.Id,
                                    Amount = amountRead,
                                    Price = selectedProduct.Price,
                                    Name = selectedProduct.Name,
                                };
                                if (billItems.Any(bi => bi.IdProduct == billItem.IdProduct))
                                {
                                    billItems.RemoveAll(bi => bi.IdProduct == billItem.IdProduct);
                                }

                                billItems.Add(billItem);

                            }
                            else
                            {
                                MessageBox.Show(noMoreSupplies);
                            }
                        }
                        SetDataToDgvBill();
                        txtAmount.Text = amount.ToString();
                        txtPrice.Clear();
                        CalculateTotal();
                    }
                }
            }
        }
        private void SetDataToDgvBill()
        {
            dgvBill.DataSource = null;

            if (user.IsAdmin)
            {
                dgvBill.DataSource = procurementItems;
            }
            else
            {
                dgvBill.DataSource = billItems;
            }
            dgvBill.Refresh();
        }
        private void CalculateTotal()
        {
            decimal total = 0;
            if (user.IsAdmin)
            {
                foreach (ProcurementItem item in procurementItems)
                {
                    total = total + item.Amount * item.Price;
                }
            }
            else
            {
                foreach (BillItem item in billItems)
                {
                    total = total + item.Amount * item.Price;
                }
            }
            txtTotal.Text = total.ToString();
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            if (dgvProductsBill.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvBill.SelectedRows[0];
                int.TryParse(row.Cells["IdProduct"].Value?.ToString(), out int id);
                if (user.IsAdmin)
                {
                    procurementItems.RemoveAll(pr => pr.IdProduct == id);
                }
                else
                {
                    billItems.RemoveAll(bi => bi.IdProduct == id);
                }
                SetDataToDgvBill();
                CalculateTotal();
            }
        }

        private void txtSearchBarCode_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)Keys.Enter)
            {
                string searchText = txtSearchBarCode.Text.Trim().ToLower();

                if (!string.IsNullOrEmpty(searchText))
                {
                    List<Product> productList = products;

                    if (productList != null)
                    {
                        List<Product> filteredList = productList
                            .Where(p => p.BarCode.ToLower().Contains(searchText))
                            .ToList();

                        dgvProductsBill.DataSource = null;
                        dgvProductsBill.DataSource = filteredList;
                    }
                }
                else
                {
                    dgvProductsBill.DataSource = null;
                    dgvProductsBill.DataSource = products;
                }
                dgvProductsBill.Refresh();
            }
        }

        private void btnBuy_Click(object sender, EventArgs e)
        {
            if (user.IsAdmin)
            {
                if (procurementItems.Count > 0)
                {
                    if (cbSupplier.SelectedIndex != -1)
                    {
                        Procurement procurement = new Procurement
                        {
                            IdSupplier = suppliers.FirstOrDefault(s => s.Name == cbSupplier.SelectedItem.ToString()).Id,
                            Date = DateTime.Now
                        };
                        procurementDAO.Create(procurement, procurementItems);
                        procurementItems.Clear();
                        cbSupplier.SelectedIndex = -1;
                    }
                    else
                    {
                        MessageBox.Show(noSupplier);
                    }
                }
            }
            else
            {
                if (billItems.Count > 0)
                {
                    Bill bill = new Bill
                    {
                        Date = DateTime.Now
                    };
                    billDAO.Create(bill, billItems);
                    billItems.Clear();
                }
            }
            txtTotal.Clear();
            SetDataToDgvBill();
        }

        private void dgvBills_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvBills.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvBills.SelectedRows[0];
                if (row.Cells.Count > 0)
                {
                    int.TryParse(row.Cells[1].Value?.ToString(), out int id);
                    if (user.IsAdmin)
                    {
                        ProcurementDetails procurementDetails = new ProcurementDetails(user, procurementsItems.Where(pi => pi.IdProcurement == id).ToList());
                        procurementDetails.ShowDialog();
                    }
                    else
                    {
                        BillDetails billDetails = new BillDetails(user, billsItems.Where(bi => bi.IdBill == id).ToList());
                        billDetails.ShowDialog();
                    }
                }
            }
        }
    }
}
