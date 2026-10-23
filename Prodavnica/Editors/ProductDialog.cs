using Prodavnica.App;
using Prodavnica.Database.DTO;
using Prodavnica.Services;
using Prodavnica.UI;
using Prodavnica.UI.Controls;
using System.Globalization;

using Theme = Prodavnica.UI.Theme;
namespace Prodavnica.Editors
{
    // Add, edit or (for cashiers) view a product.
    public class ProductDialog : DialogForm
    {
        private readonly Product? existing;
        private readonly ImagePicker picture = new ImagePicker(round: false, string.Empty);
        private readonly UiInput name = new UiInput();
        private readonly UiInput barCode = new UiInput(string.Empty, Glyphs.Barcode);
        private readonly UiSelect category = new UiSelect(T.Get("products.chooseCategory"));
        private readonly UiSelect manufacturer = new UiSelect(T.Get("products.chooseManufacturer"));
        private readonly UiInput price = new UiInput("0,00");
        private readonly UiInput supplies = new UiInput("0");
        private readonly UiDate expires = new UiDate();
        private readonly UiInput description = new UiInput(string.Empty, multiline: true);
        private readonly Dictionary<ProductField, Field> fields = new();
        private readonly bool readOnly;

        public ProductDialog(Product? product, bool readOnly) : base(
            T.Get(product == null ? "products.add" : readOnly ? "products.details" : "products.edit"),
            product == null ? T.Get("products.addSubtitle") : $"{product.CategoryName} · {product.ManufacturerName}", 900)
        {
            existing = product;
            this.readOnly = readOnly;
            price.Box.Name = "priceBox";
            picture.Location = new Point(LogicalToDeviceUnits(24), LogicalToDeviceUnits(8));
            picture.Width = LogicalToDeviceUnits(200);
            var grid = new FieldGrid(2) { Location = new Point(LogicalToDeviceUnits(248), LogicalToDeviceUnits(8)), Width = LogicalToDeviceUnits(612) };
            fields[ProductField.Name] = grid.Add(new Field(T.Get("products.name"), name), span: 2);
            fields[ProductField.BarCode] = grid.Add(new Field(T.Get("products.barCode"), barCode));
            fields[ProductField.Price] = grid.Add(new Field(T.Get("products.priceKm"), price));
            fields[ProductField.Category] = grid.Add(new Field(T.Get("products.category"), category));
            fields[ProductField.Manufacturer] = grid.Add(new Field(T.Get("products.manufacturer"), manufacturer));
            fields[ProductField.Supplies] = grid.Add(new Field(T.Get("products.stock"), supplies));
            grid.Add(new Field(T.Get("products.expires"), expires));
            fields[ProductField.Description] = grid.Add(new Field(T.Get("products.description"), description), span: 2);
            Body.Controls.Add(picture);
            Body.Controls.Add(grid);

            if (product != null)
            {
                name.Text = product.Name;
                barCode.Text = product.BarCode;
                price.Text = product.Price.ToString("0.00", CultureInfo.CurrentCulture);
                supplies.Text = T.Number(product.Supplies);
                expires.Value = product.ExpirationDate;
                description.Text = product.Description;
                picture.ImageData = product.Image;
            }
            else
            {
                expires.Value = DateTime.Today.AddMonths(6);
                picture.ImageData = null;
            }

            if (readOnly)
            {
                foreach (UiInput input in new[] { name, barCode, price, supplies, description }) input.ReadOnly = true;
                category.Enabled = manufacturer.Enabled = expires.Enabled = false;
                foreach (Control c in picture.Controls.OfType<UiButton>()) c.Visible = false;
                AcceptButton = AddButton(T.Get("common.close"), ButtonKind.Primary, result: DialogResult.Cancel);
            }
            else
            {
                var save = AddButton(T.Get("common.save"), ButtonKind.Primary, Glyphs.Check);
                save.Click += async (_, _) => await SaveAsync();
                AcceptButton = save;
                CancelButton = AddButton(T.Get("common.cancel"), ButtonKind.Secondary, result: DialogResult.Cancel);
            }
            SetBodyHeight(436);
            Load += async (_, _) => await UI.Dialogs.RunAsync(this, LoadListsAsync);
            Shown += (_, _) => { if (!readOnly) name.FocusInput(); };
        }

        private async Task LoadListsAsync()
        {
            List<Category> categories = await Session.Categories.GetAllAsync();
            List<Company> manufacturers = await Session.Manufacturers.GetAllAsync();
            category.SetItems(categories.Select(c => (c.Name, (object?)c.Id)), existing?.IdCategory);
            manufacturer.SetItems(manufacturers.Select(m => (m.Name, (object?)m.Id)), existing?.IdManufacturer);
        }

        private async Task SaveAsync()
        {
            foreach (Field f in fields.Values) f.SetError(null);
            var input = new ProductInput
            {
                Name = name.Text,
                BarCode = barCode.Text,
                Price = price.Text,
                Supplies = supplies.Text,
                Description = description.Text,
                Category = category.SelectedText,
                Manufacturer = manufacturer.SelectedText
            };
            bool taken = false;
            if (!await UI.Dialogs.RunAsync(this, async () => taken = await Session.Products.BarCodeExistsAsync(barCode.Text, existing?.Id))) return;
            List<ValidationError> errors = ProductValidator.Validate(input, taken, CultureInfo.CurrentCulture);
            foreach (var error in errors.GroupBy(e => e.Field).Select(g => g.First()))
            {
                fields[error.Field].SetError(T.Get("val.product." + error.Error));
            }
            if (errors.Count > 0) return;

            ProductValidator.TryParseAmount(price.Text, CultureInfo.CurrentCulture, out decimal priceValue);
            ProductValidator.TryParseAmount(supplies.Text, CultureInfo.CurrentCulture, out decimal suppliesValue);
            var product = new Product
            {
                Id = existing?.Id ?? 0,
                Name = name.Text.Trim(),
                BarCode = barCode.Text.Trim(),
                Price = priceValue,
                Supplies = suppliesValue,
                ExpirationDate = expires.Value,
                IdCategory = (int)category.SelectedValue!,
                IdManufacturer = (int)manufacturer.SelectedValue!,
                Description = description.Text.Trim(),
                Image = picture.ImageData
            };
            bool ok = await UI.Dialogs.RunAsync(this, async () =>
            {
                if (existing == null) await Session.Products.CreateAsync(product);
                else await Session.Products.UpdateAsync(product);
            });
            if (!ok) return;
            await Session.LogAsync(existing == null ? "PRODUCT_CREATE" : "PRODUCT_UPDATE", product.Name);
            DialogResult = DialogResult.OK;
        }
    }
}
