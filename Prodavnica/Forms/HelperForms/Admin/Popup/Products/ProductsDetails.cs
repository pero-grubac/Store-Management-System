using Prodavnica.Database.DTO;
using Prodavnica.Database.Repository;
using Prodavnica.Language;
using Prodavnica.Util;

namespace Prodavnica.Forms.HelperForms.Admin.Popup.Products
{
    public partial class ProductsDetails : Form
    {
        private User user;
        private Product product;
        private List<Product> products;
        private List<Category> categories;
        private List<Manufacturer> manufacturers;
        private string invalidNumber;
        private string invalidBarcode;

        private ProductsDAOImpl productsDAO = new ProductsDAOImpl();
        private CategoryDAOImpl categoryDAO = new CategoryDAOImpl();
        private ManufacturerDAOImpl manufacturerDAO = new ManufacturerDAOImpl();
        private int indicator = 0;
        private bool update;
        public ProductsDetails(User user, Product product, bool update)
        {
            InitializeComponent();
            this.user = user;
            this.product = product;
            products = productsDAO.GetAll();
            categories = categoryDAO.GetAll();
            manufacturers = manufacturerDAO.GetAll();
            this.update = update;
            if (update)
                setProduct();
            else
            {
                txtSupplies.Text = "0";
                txtSupplies.ReadOnly = true;
                setComboBoxes();
            }
            rtbMessage.SelectionAlignment = HorizontalAlignment.Center;
        }
        public void ChangeText()
        {
            LoadSettings.ApplySettins(user, this);
            lblName.Text = LanguageHelper.GetString("name");
            lblManufacturer.Text = LanguageHelper.GetString("manufacturer");
            lblCategory.Text = LanguageHelper.GetString("category");
            lblBarCode.Text = LanguageHelper.GetString("barcode");
            lblDate.Text = LanguageHelper.GetString("expirationDate");
            lblDescription.Text = LanguageHelper.GetString("description");
            lblPrice.Text = LanguageHelper.GetString("price");
            lblSupplies.Text = LanguageHelper.GetString("supplies");
            btnSave.Text = LanguageHelper.GetString("btnSave");
            btnCancel.Text = LanguageHelper.GetString("btnCancel");
            invalidBarcode = LanguageHelper.GetString("invalidBarcode");
            invalidNumber = LanguageHelper.GetString("invalidNumber");
        }
        private void setProduct()
        {
            txtName.Text = product.Name;
            txtBarcode.Text = product.BarCode;
            txtPrice.Text = product.Price.ToString();
            dtpDate.Value = product.ExpirationDate;
            txtSupplies.Text = product.Supplies.ToString();
            txtDescription.Text = product.Description;
            setComboBoxes();
        }
        private void setComboBoxes()
        {
            foreach (Manufacturer manufacturer in manufacturers)
            {
                cbManufacturer.Items.Add(manufacturer.Name);
            }
            cbManufacturer.SelectedItem = !string.IsNullOrEmpty(product.ManufacturerName) ?
                product.ManufacturerName : manufacturers.FirstOrDefault()?.Name;
            foreach (Category category in categories)
            {
                cbCategory.Items.Add(category.Name);
            }
            cbCategory.SelectedItem = !string.IsNullOrEmpty(product.CategoryName) ?
                product.CategoryName : categories.FirstOrDefault()?.Name;
        }
        private void btnCencel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void EnableSaveButton(bool enable)
        {
            btnSave.Enabled = enable;
            btnSave.Visible = enable;
        }

        private void txtSupplies_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(txtSupplies.Text, out _))
            {
                txtSupplies.ForeColor = Color.Red;
                rtbMessage.Text = invalidNumber;
                EnableSaveButton(false);
                ++indicator;
            }
            else
            {
                --indicator;
                txtSupplies.ForeColor = Color.Black;
                if (indicator == 0)
                {
                    rtbMessage.Clear();
                    EnableSaveButton(true);
                }
                else if (indicator < 0)
                {
                    indicator = 0;
                }
            }
        }

        private void txtBarcode_TextChanged(object sender, EventArgs e)
        {

            if (!int.TryParse(txtBarcode.Text, out _))
            {
                txtBarcode.ForeColor = Color.Red;
                rtbMessage.Text = invalidNumber;
                EnableSaveButton(false);
                ++indicator;
            }
            else
            {
                --indicator;
                txtBarcode.ForeColor = Color.Black;
                if (indicator == 0)
                {
                    rtbMessage.Clear();
                    EnableSaveButton(true);
                }
                else if (indicator < 0)
                {
                    indicator = 0;
                }
                List<Product> uniqueProducts = products;
                uniqueProducts.RemoveAll(prod => prod.BarCode == product.BarCode);
                if (uniqueProducts.Any(product => product.BarCode == txtBarcode.Text))
                {
                    txtBarcode.ForeColor = Color.Red;
                    EnableSaveButton(false);
                    ++indicator;
                    MessageBox.Show(invalidBarcode);
                }
            }
        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtPrice.Text, out _))
            {
                txtPrice.ForeColor = Color.Red;
                rtbMessage.Text = invalidNumber;
                EnableSaveButton(false);
                ++indicator;
            }
            else
            {
                txtPrice.ForeColor = Color.Black;
                --indicator;
                if (indicator == 0)
                {
                    rtbMessage.Clear();
                    EnableSaveButton(true);
                }
                else if (indicator < 0)
                {
                    indicator = 0;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBarcode.Text) &&
                !string.IsNullOrEmpty(txtDescription.Text) &&
                !string.IsNullOrEmpty(txtName.Text) &&
                !string.IsNullOrEmpty(txtPrice.Text) &&
                !string.IsNullOrEmpty(txtSupplies.Text) &&
                cbCategory.SelectedItem != null &&
                cbManufacturer.SelectedItem != null &&
                dtpDate.Value != DateTime.MinValue)
            {
                decimal price;
                decimal.TryParse(txtPrice.Text, out price);
                int supplies;
                int.TryParse(txtSupplies.Text, out supplies);
                Product newProduct = new Product()
                {
                    BarCode = txtBarcode.Text,
                    Name = txtName.Text,
                    ExpirationDate = dtpDate.Value.Date,
                    CategoryName = cbCategory.SelectedItem.ToString(),
                    Price = price,
                    Supplies = supplies,
                    ManufacturerName = cbManufacturer.SelectedItem.ToString(),
                    Description = txtDescription.Text,
                };
                newProduct.IdCategory = categories.FirstOrDefault(c => c.Name == newProduct.CategoryName).Id;
                newProduct.IdManufacturer = manufacturers.FirstOrDefault(m => m.Name == newProduct.ManufacturerName).id;

                if (update)
                {
                    newProduct.Id = product.Id;
                    if (!newProduct.IsEquivalent(product))
                        productsDAO.UpdateProduct(newProduct);
                }
                else
                {
                    productsDAO.CreateProduct(newProduct);
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
