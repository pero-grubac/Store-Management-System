using Prodavnica.App;
using Prodavnica.Database;
using Prodavnica.Database.DTO;
using Prodavnica.Database.Repositories;
using Prodavnica.Editors;
using Prodavnica.Services;
using Prodavnica.UI;
using Prodavnica.UI.Controls;
using System.Globalization;

using Theme = Prodavnica.UI.Theme;
namespace Prodavnica.Pages
{
    // Product picker for the till and for procurement: bar code scanning, search, category filter and cards.
    public class ProductBrowser : Panel
    {
        private readonly UiInput scan = new UiInput(T.Get("pos.scanPlaceholder"), Glyphs.Barcode);
        private readonly UiInput search = new UiInput(T.Get("products.searchPlaceholder"), Glyphs.Search);
        private readonly UiSelect category = new UiSelect(T.Get("products.allCategories"));
        private readonly ProductGrid grid = new ProductGrid { Dock = DockStyle.Fill, EmptyText = T.Get("common.noResults") };
        private readonly Pager pager = new Pager { Dock = DockStyle.Bottom };
        private readonly Debouncer debouncer;
        private readonly bool sellableOnly;
        private int page = 1;

        public ProductBrowser(bool sellableOnly)
        {
            this.sellableOnly = sellableOnly;
            scan.Box.Name = "scanBox";
            search.Box.Name = "searchBox";
            BackColor = Color.Transparent;
            var toolbar = new Panel { Dock = DockStyle.Top, Height = LogicalToDeviceUnits(58), BackColor = Color.Transparent };
            toolbar.Controls.AddRange(new Control[] { scan, search, category });
            toolbar.Layout += (_, _) =>
            {
                int gap = LogicalToDeviceUnits(12);
                int categoryWidth = LogicalToDeviceUnits(200);
                int rest = toolbar.Width - categoryWidth - gap * 2;
                scan.SetBounds(0, 0, rest / 2, scan.Height);
                search.SetBounds(scan.Right + gap, 0, rest - rest / 2, search.Height);
                category.SetBounds(search.Right + gap, 0, categoryWidth, category.Height);
            };
            Controls.Add(grid);
            Controls.Add(pager);
            Controls.Add(toolbar);

            debouncer = new Debouncer(300, async () => { page = 1; await ReloadAsync(); });
            search.TextChanged += (_, _) => debouncer.Trigger();
            category.SelectionChanged += async (_, _) => { page = 1; await ReloadAsync(); };
            pager.PageRequested += async (_, p) => { page = p; await ReloadAsync(); };
            grid.ItemClicked += (_, p) => ProductChosen?.Invoke(this, p);
            scan.KeyDown += async (_, e) =>
            {
                if (e.KeyCode != Keys.Enter) return;
                e.SuppressKeyPress = true;
                await ScanAsync();
            };
        }

        public event EventHandler<Product>? ProductChosen;

        public void FocusScanner() => scan.FocusInput();

        public async Task LoadAsync()
        {
            List<Category> categories = await Session.Categories.GetAllAsync();
            category.SetItems(new (string, object?)[] { (T.Get("products.allCategories"), null) }.Concat(categories.Select(c => (c.Name, (object?)c.Id))));
            category.SelectedIndex = 0;
            await ReloadAsync();
        }

        public async Task ReloadAsync()
        {
            var query = new ProductQuery(new PageRequest(page, 18, search.Text), category.SelectedValue as int?, sellableOnly, DateTime.Today);
            PagedResult<Product> result = await Session.Products.GetPageAsync(query);
            grid.SetProducts(result.Items, DateTime.Today);
            pager.Show(result);
        }

        // Bar code scanners type the code and press Enter.
        private async Task ScanAsync()
        {
            string code = scan.Text.Trim();
            if (code.Length == 0) return;
            Product? product = null;
            if (!await UI.Dialogs.RunAsync(this, async () => product = await Session.Products.GetByBarCodeAsync(code))) return;
            scan.Text = string.Empty;
            scan.FocusInput();
            if (product == null)
            {
                Toast.Show(FindForm(), T.Format("pos.notFound", code), ToastKind.Error);
                return;
            }
            ProductChosen?.Invoke(this, product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) debouncer.Dispose();
            base.Dispose(disposing);
        }
    }

    // One line in the cart: name, unit price, quantity stepper, (purchase price), line total and remove.
    public class CartLineView : Panel
    {
        private readonly UiButton minus = new UiButton(string.Empty, Glyphs.Minus);
        private readonly UiButton plus = new UiButton(string.Empty, Glyphs.Add);
        private readonly UiButton remove = new UiButton(string.Empty, Glyphs.Delete, ButtonKind.Ghost);
        private readonly UiInput amount = new UiInput();
        private readonly UiInput? price;
        private bool updating;

        public CartLineView(Product product, decimal quantity, decimal? purchasePrice, bool editablePrice)
        {
            Product = product;
            BackColor = Theme.Surface;
            Height = LogicalToDeviceUnits(editablePrice ? 108 : 86);
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw, true);
            foreach (UiButton b in new[] { minus, plus }) { b.Height = LogicalToDeviceUnits(32); b.AutoWidth = false; b.Width = LogicalToDeviceUnits(32); }
            remove.Height = LogicalToDeviceUnits(32);
            amount.Height = LogicalToDeviceUnits(32);
            amount.Width = LogicalToDeviceUnits(64);
            amount.Box.TextAlign = HorizontalAlignment.Center;
            amount.Text = T.Number(quantity);
            Controls.AddRange(new Control[] { minus, amount, plus, remove });
            if (editablePrice)
            {
                price = new UiInput(T.Get("procurement.pricePlaceholder")) { Height = LogicalToDeviceUnits(32), Width = LogicalToDeviceUnits(110) };
                price.Box.Name = "linePrice";
                price.Box.TextAlign = HorizontalAlignment.Right;
                if (purchasePrice.HasValue) price.Text = purchasePrice.Value.ToString("0.00", CultureInfo.CurrentCulture);
                price.TextChanged += (_, _) => { Invalidate(); Changed?.Invoke(this, EventArgs.Empty); };
                Controls.Add(price);
            }
            minus.Click += (_, _) => Step(-1);
            plus.Click += (_, _) => Step(1);
            remove.Click += (_, _) => Removed?.Invoke(this, EventArgs.Empty);
            amount.TextChanged += (_, _) => { if (!updating) { Invalidate(); Changed?.Invoke(this, EventArgs.Empty); } };
        }

        public event EventHandler? Changed;
        public event EventHandler? Removed;

        public Product Product { get; }

        public decimal Quantity => amount.TryGetDecimal(out decimal q) ? q : 0;

        public decimal Price => price == null ? Product.Price : price.TryGetDecimal(out decimal p) ? p : 0;

        public decimal LineTotal => Quantity * Price;

        public void Step(decimal delta)
        {
            decimal next = Math.Max(0, Quantity + delta);
            if (next == 0) { Removed?.Invoke(this, EventArgs.Empty); return; }
            SetQuantity(next);
        }

        public void SetQuantity(decimal value)
        {
            updating = true;
            amount.Text = T.Number(value);
            updating = false;
            Invalidate();
            Changed?.Invoke(this, EventArgs.Empty);
        }

        public void MarkErrors(bool quantityError, bool priceError)
        {
            amount.HasError = quantityError;
            if (price != null) price.HasError = priceError;
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            int y = LogicalToDeviceUnits(price != null ? 60 : 42);
            minus.Location = new Point(0, y);
            amount.Location = new Point(minus.Right + LogicalToDeviceUnits(6), y);
            plus.Location = new Point(amount.Right + LogicalToDeviceUnits(6), y);
            if (price != null) price.Location = new Point(plus.Right + LogicalToDeviceUnits(16), y);
            remove.Location = new Point(Width - remove.Width, LogicalToDeviceUnits(4));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            int right = Width - LogicalToDeviceUnits(40);
            TextRenderer.DrawText(g, Product.Name, Theme.Semibold(10.25f), new Rectangle(0, LogicalToDeviceUnits(6), right, LogicalToDeviceUnits(22)), Theme.Text, Draw.Line);
            string sub = price == null ? $"{T.Money(Product.Price)} · {T.Format("stock.in", T.Number(Product.Supplies))}" : $"{Product.BarCode} · {T.Format("stock.in", T.Number(Product.Supplies))}";
            TextRenderer.DrawText(g, sub, Theme.Font(9f), new Rectangle(0, LogicalToDeviceUnits(26), right, LogicalToDeviceUnits(18)), Theme.Muted, Draw.Line);
            if (price != null)
            {
                TextRenderer.DrawText(g, T.Get("procurement.quantity"), Theme.Font(8.5f), new Point(0, LogicalToDeviceUnits(44)), Theme.Subtle, TextFormatFlags.NoPadding);
                TextRenderer.DrawText(g, T.Get("procurement.unitPrice"), Theme.Font(8.5f), new Point(price.Left, LogicalToDeviceUnits(44)), Theme.Subtle, TextFormatFlags.NoPadding);
            }
            int y = minus.Top;
            TextRenderer.DrawText(g, T.Money(LineTotal), Theme.Semibold(11f), new Rectangle(0, y, Width, minus.Height), Theme.Text,
                TextFormatFlags.Right | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);
            using var pen = new Pen(Theme.Border);
            g.DrawLine(pen, 0, Height - 1, Width, Height - 1);
        }
    }

    // The right-hand panel listing the cart lines with the total and the main action.
    public class CartPanel : Card
    {
        private readonly FlowLayoutPanel lines = new FlowLayoutPanel { FlowDirection = FlowDirection.TopDown, WrapContents = false, AutoScroll = true, BackColor = Theme.Surface };
        private readonly UiLabel empty;
        private readonly UiLabel totalCaption = new UiLabel(T.Get("pos.total"), 10.5f, color: Theme.Muted);
        private readonly UiLabel total = new UiLabel(T.Money(0), 22f, semibold: true);
        private readonly Panel top = new Panel { Dock = DockStyle.Top, BackColor = Theme.Surface };
        private readonly Panel bottom = new Panel { Dock = DockStyle.Bottom, BackColor = Theme.Surface };

        public CartPanel(string title, string emptyText, UiButton primary, UiButton secondary, Control? topControl)
        {
            Title = title;
            Primary = primary;
            empty = new UiLabel(emptyText, 10f, color: Theme.Subtle) { AutoSize = false, TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Fill };
            if (topControl != null)
            {
                top.Height = topControl.Height + LogicalToDeviceUnits(8);
                topControl.Dock = DockStyle.Top;
                top.Controls.Add(topControl);
                Controls.Add(top);
            }
            bottom.Height = LogicalToDeviceUnits(140);
            primary.AutoWidth = false;
            primary.Height = LogicalToDeviceUnits(48);
            primary.Font = Theme.Semibold(11f);
            secondary.AutoWidth = false;
            secondary.Height = LogicalToDeviceUnits(48);
            bottom.Controls.AddRange(new Control[] { totalCaption, total, primary, secondary });
            bottom.Layout += (_, _) =>
            {
                int w = bottom.Width;
                totalCaption.Location = new Point(0, LogicalToDeviceUnits(22));
                total.Location = new Point(w - total.PreferredWidth, LogicalToDeviceUnits(10));
                secondary.SetBounds(0, bottom.Height - secondary.Height, LogicalToDeviceUnits(120), secondary.Height);
                primary.SetBounds(secondary.Right + LogicalToDeviceUnits(10), bottom.Height - primary.Height, w - secondary.Width - LogicalToDeviceUnits(10), primary.Height);
            };
            bottom.Paint += (_, e) => { using var pen = new Pen(Theme.Border); e.Graphics.DrawLine(pen, 0, 0, bottom.Width, 0); };
            lines.Dock = DockStyle.Fill;
            lines.Resize += (_, _) => FitLines();
            Controls.Add(empty);
            Controls.Add(lines);
            Controls.Add(bottom);
            // Docked first (last in z-order), so the supplier field sits above the lines instead of over them.
            if (topControl != null) top.SendToBack();
            ShowState(Array.Empty<CartLineView>(), 0);
        }

        public UiButton Primary { get; }

        public void Add(CartLineView line)
        {
            lines.Controls.Add(line);
            FitLines();
            lines.ScrollControlIntoView(line);
        }

        public void Remove(CartLineView line)
        {
            lines.Controls.Remove(line);
            line.Dispose();
        }

        public void Clear()
        {
            foreach (Control c in lines.Controls.Cast<Control>().ToList()) c.Dispose();
            lines.Controls.Clear();
        }

        public void ShowState(IReadOnlyCollection<CartLineView> current, decimal sum)
        {
            empty.Visible = current.Count == 0;
            lines.Visible = current.Count > 0;
            total.Text = T.Money(sum);
            Subtitle = T.Format("pos.items", current.Count);
            Primary.Enabled = current.Count > 0;
            bottom.PerformLayout();
        }

        private void FitLines()
        {
            int width = lines.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - LogicalToDeviceUnits(4);
            foreach (Control c in lines.Controls) c.Width = width;
        }
    }

    public class SalePage : Page
    {
        private readonly ProductBrowser browser = new ProductBrowser(sellableOnly: true) { Dock = DockStyle.Fill };
        private readonly CartPanel cart;
        private readonly List<CartLineView> lines = new();

        public SalePage()
        {
            var checkout = new UiButton(T.Get("pos.checkout"), Glyphs.Money, ButtonKind.Primary);
            var clear = new UiButton(T.Get("pos.clear"), string.Empty, ButtonKind.Secondary);
            cart = new CartPanel(T.Get("pos.bill"), T.Get("pos.empty"), checkout, clear, null) { Dock = DockStyle.Right, Width = S(430) };
            checkout.Click += async (_, _) => await CheckoutAsync();
            clear.Click += (_, _) => ClearCart();
            browser.ProductChosen += (_, p) => AddProduct(p);
            var gap = new Panel { Dock = DockStyle.Right, Width = S(20), BackColor = Color.Transparent };
            Controls.Add(browser);
            Controls.Add(gap);
            Controls.Add(cart);
        }

        public override string Title => T.Get("nav.sale");
        public override string Subtitle => T.Get("pos.subtitle");

        public override async Task LoadAsync()
        {
            await browser.LoadAsync();
            browser.FocusScanner();
        }

        private void AddProduct(Product product)
        {
            if (!Inventory.CanSell(product, 1, DateTime.Today) && product.Supplies < 1)
            {
                Toast.Show(FindForm(), T.Format("pos.unavailable", product.Name), ToastKind.Error);
                return;
            }
            CartLineView? existing = lines.FirstOrDefault(l => l.Product.Id == product.Id);
            if (existing != null)
            {
                if (!Inventory.CanSell(product, existing.Quantity + 1, DateTime.Today))
                {
                    Toast.Show(FindForm(), T.Format("pos.noMoreStock", product.Name), ToastKind.Error);
                    return;
                }
                existing.Step(1);
                return;
            }
            if (!Inventory.CanSell(product, 1, DateTime.Today))
            {
                Toast.Show(FindForm(), T.Format("pos.unavailable", product.Name), ToastKind.Error);
                return;
            }
            var line = new CartLineView(product, 1, null, editablePrice: false);
            line.Changed += (_, _) => Recalculate();
            line.Removed += (_, _) => { lines.Remove(line); cart.Remove(line); Recalculate(); };
            lines.Add(line);
            cart.Add(line);
            Recalculate();
        }

        private bool Recalculate()
        {
            bool valid = true;
            foreach (CartLineView line in lines)
            {
                bool ok = Inventory.CanSell(line.Product, line.Quantity, DateTime.Today);
                line.MarkErrors(!ok, false);
                valid &= ok;
            }
            cart.ShowState(lines, lines.Sum(l => l.LineTotal));
            cart.Primary.Enabled = lines.Count > 0 && valid;
            return valid;
        }

        private void ClearCart()
        {
            lines.Clear();
            cart.Clear();
            Recalculate();
            browser.FocusScanner();
        }

        private async Task CheckoutAsync()
        {
            if (lines.Count == 0 || !Recalculate()) return;
            var cartLines = new Cart<BillItem>();
            foreach (CartLineView line in lines)
            {
                cartLines.AddOrReplace(new BillItem { IdProduct = line.Product.Id, Name = line.Product.Name, Amount = line.Quantity, Price = line.Product.Price });
            }
            int billId = 0;
            if (!await UI.Dialogs.RunAsync(this, async () => billId = await Session.Sales.CreateBillAsync(Session.User.Id, cartLines.ToList()))) return;
            await Session.LogAsync("SALE", T.Format("auditText.sale", T.DocumentNumber(billId), T.Money(cartLines.Total)));
            ClearCart();
            await browser.ReloadAsync();
            Toast.Show(FindForm(), T.Format("pos.saved", T.DocumentNumber(billId)));
            using var receipt = new DocumentDialog(DocumentKind.Bill, billId, afterSale: true);
            receipt.ShowOver(this);
            browser.FocusScanner();
        }
    }

    public class ProcurementPage : Page
    {
        private readonly ProductBrowser browser = new ProductBrowser(sellableOnly: false) { Dock = DockStyle.Fill };
        private readonly UiSelect supplier = new UiSelect(T.Get("procurement.chooseSupplier"));
        private readonly CartPanel cart;
        private readonly List<CartLineView> lines = new();

        public ProcurementPage()
        {
            var confirm = new UiButton(T.Get("procurement.confirm"), Glyphs.Check, ButtonKind.Primary);
            var clear = new UiButton(T.Get("pos.clear"), string.Empty, ButtonKind.Secondary);
            supplier.Name = "supplierSelect";
            var supplierField = new Field(T.Get("procurement.supplier"), supplier);
            cart = new CartPanel(T.Get("procurement.order"), T.Get("procurement.empty"), confirm, clear, supplierField) { Dock = DockStyle.Right, Width = S(460) };
            confirm.Click += async (_, _) => await ConfirmAsync();
            clear.Click += (_, _) => { lines.Clear(); cart.Clear(); Recalculate(); };
            supplier.SelectionChanged += (_, _) => Recalculate();
            browser.ProductChosen += async (_, p) => await AddProductAsync(p);
            var gap = new Panel { Dock = DockStyle.Right, Width = S(20), BackColor = Color.Transparent };
            Controls.Add(browser);
            Controls.Add(gap);
            Controls.Add(cart);
        }

        public override string Title => T.Get("nav.procurement");
        public override string Subtitle => T.Get("procurement.subtitle");

        public override async Task LoadAsync()
        {
            List<Company> suppliers = await Session.Suppliers.GetAllAsync();
            supplier.SetItems(suppliers.Select(s => (s.Name, (object?)s.Id)));
            await browser.LoadAsync();
        }

        private async Task AddProductAsync(Product product)
        {
            CartLineView? existing = lines.FirstOrDefault(l => l.Product.Id == product.Id);
            if (existing != null)
            {
                existing.Step(1);
                return;
            }
            decimal? lastPrice = null;
            await UI.Dialogs.RunAsync(this, async () => lastPrice = await Session.Procurements.GetLastPriceAsync(product.Id));
            var line = new CartLineView(product, 1, lastPrice, editablePrice: true);
            line.Changed += (_, _) => Recalculate();
            line.Removed += (_, _) => { lines.Remove(line); cart.Remove(line); Recalculate(); };
            lines.Add(line);
            cart.Add(line);
            Recalculate();
        }

        private bool Recalculate()
        {
            bool valid = supplier.SelectedIndex >= 0;
            foreach (CartLineView line in lines)
            {
                bool quantityOk = line.Quantity > 0, priceOk = line.Price > 0;
                line.MarkErrors(!quantityOk, !priceOk);
                valid &= quantityOk && priceOk;
            }
            cart.ShowState(lines, lines.Sum(l => l.LineTotal));
            cart.Primary.Enabled = lines.Count > 0 && valid;
            return valid;
        }

        private async Task ConfirmAsync()
        {
            if (supplier.SelectedValue is not int supplierId)
            {
                supplier.HasError = true;
                return;
            }
            if (!Recalculate()) return;
            var order = new Cart<ProcurementItem>();
            foreach (CartLineView line in lines)
            {
                order.AddOrReplace(new ProcurementItem { IdProduct = line.Product.Id, Name = line.Product.Name, Amount = line.Quantity, Price = line.Price });
            }
            int id = 0;
            if (!await UI.Dialogs.RunAsync(this, async () => id = await Session.Procurements.CreateAsync(supplierId, Session.User.Id, order.ToList()))) return;
            await Session.LogAsync("PROCUREMENT", T.Format("auditText.procurement", T.DocumentNumber(id), supplier.SelectedText, T.Money(order.Total)));
            lines.Clear();
            cart.Clear();
            Recalculate();
            await browser.ReloadAsync();
            Toast.Show(FindForm(), T.Format("procurement.saved", T.DocumentNumber(id)));
        }
    }
}
