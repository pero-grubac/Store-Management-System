using Prodavnica.App;
using Prodavnica.Database;
using Prodavnica.Database.DTO;
using Prodavnica.Database.Repositories;
using Prodavnica.Editors;
using Prodavnica.Services;
using Prodavnica.UI;
using Prodavnica.UI.Controls;

using Theme = Prodavnica.UI.Theme;
namespace Prodavnica.Pages
{
    public class ProductsPage : Page
    {
        private const int CardPageSize = 24;
        private const int TablePageSize = 15;

        private readonly UiInput search = new UiInput(T.Get("products.searchPlaceholder"), Glyphs.Search);
        private readonly UiSelect category = new UiSelect(T.Get("products.allCategories"));
        private readonly Segmented view = new Segmented((T.Get("products.cards"), Glyphs.Grid), (T.Get("products.table"), Glyphs.List));
        private readonly UiButton add = new UiButton(T.Get("products.add"), Glyphs.Add, ButtonKind.Primary);
        private readonly ProductGrid grid = new ProductGrid { Dock = DockStyle.Fill, EmptyText = T.Get("common.noResults") };
        private readonly Card tableCard = new Card { Dock = DockStyle.Fill, Padding = new Padding(8, 4, 20, 4) };
        private readonly DataTable table = new DataTable { Dock = DockStyle.Fill, EmptyText = T.Get("common.noResults") };
        private readonly Pager pager = new Pager { Dock = DockStyle.Bottom };
        private readonly Debouncer debouncer;
        private int page = 1;

        public ProductsPage()
        {
            search.Width = S(400);
            search.Box.Name = "searchBox";
            category.Width = S(240);
            var toolbar = new Panel { Dock = DockStyle.Top, Height = S(58), BackColor = Color.Transparent };
            toolbar.Controls.AddRange(new Control[] { search, category, view });
            toolbar.Layout += (_, _) =>
            {
                search.Location = new Point(0, 0);
                category.Location = new Point(search.Right + S(12), 0);
                view.Location = new Point(toolbar.Width - view.Width, S(1));
            };

            table.AddCustom(T.Get("products.product"), 2.6f, (g, r, item, _) =>
            {
                var p = (Product)item;
                DataTable.PaintIdentity(g, r, p.Name, p.BarCode, table.CachedImage(p, p.Image), round: false, table);
            });
            table.AddText(T.Get("products.category"), 1.3f);
            table.AddText(T.Get("products.manufacturer"), 1.3f);
            table.AddText(T.Get("products.price"), 0.9f, right: true);
            table.AddCustom(T.Get("products.stock"), 1.1f, (g, r, item, _) => PaintStock(g, r, (Product)item));
            table.AddCustom(T.Get("products.expires"), 1.1f, (g, r, item, _) => PaintExpiry(g, r, (Product)item));
            tableCard.Controls.Add(table);
            tableCard.Visible = false;

            var menuItems = new List<(string, string, Action, bool)>
            {
                (T.Get(Session.IsAdmin ? "common.edit" : "common.details"), Glyphs.Edit, async () => await OpenAsync(Selected), false)
            };
            if (Session.IsAdmin) menuItems.Add((T.Get("common.delete"), Glyphs.Delete, async () => await DeleteAsync(Selected), true));
            ContextMenuStrip menu = Menu(menuItems.ToArray());
            grid.CardMenu = menu;
            table.ContextMenuStrip = menu;

            Controls.Add(grid);
            Controls.Add(tableCard);
            Controls.Add(pager);
            Controls.Add(toolbar);

            debouncer = new Debouncer(300, async () => { page = 1; await ReloadAsync(); });
            search.TextChanged += (_, _) => debouncer.Trigger();
            category.SelectionChanged += async (_, _) => { page = 1; await ReloadAsync(); };
            view.SelectedIndexChanged += async (_, _) =>
            {
                grid.Visible = view.SelectedIndex == 0;
                tableCard.Visible = view.SelectedIndex == 1;
                page = 1;
                await ReloadAsync();
            };
            pager.PageRequested += async (_, p) => { page = p; await ReloadAsync(); };
            grid.ItemActivated += async (_, p) => await OpenAsync(p);
            table.ItemActivated += async (_, p) => await OpenAsync((Product)p);
            add.Click += async (_, _) => await OpenAsync(null);
        }

        public override string Title => T.Get("nav.products");
        public override string Subtitle => T.Get(Session.IsAdmin ? "products.subtitleAdmin" : "products.subtitle");
        public override IEnumerable<Control> HeaderActions => Session.IsAdmin ? new[] { add } : Array.Empty<Control>();

        private Product? Selected => view.SelectedIndex == 0 ? grid.SelectedProduct : table.SelectedItem as Product;

        public override async Task LoadAsync()
        {
            List<Category> categories = await Session.Categories.GetAllAsync();
            category.SetItems(new (string, object?)[] { (T.Get("products.allCategories"), null) }.Concat(categories.Select(c => (c.Name, (object?)c.Id))));
            category.SelectedIndex = 0;
            await ReloadAsync();
        }

        private async Task ReloadAsync()
        {
            int size = view.SelectedIndex == 0 ? CardPageSize : TablePageSize;
            var query = new ProductQuery(new PageRequest(page, size, search.Text), category.SelectedValue as int?);
            PagedResult<Product> result = await Session.Products.GetPageAsync(query);
            if (view.SelectedIndex == 0) grid.SetProducts(result.Items, DateTime.Today);
            else table.SetRows(result.Items, p => new object?[] { null, p.CategoryName, p.ManufacturerName, T.Money(p.Price), null, null });
            pager.Show(result);
        }

        private async Task OpenAsync(Product? product)
        {
            if (product == null && !Session.IsAdmin) return;
            using var dialog = new ProductDialog(product, readOnly: !Session.IsAdmin);
            if (dialog.ShowOver(this) == DialogResult.OK)
            {
                Toast.Show(FindForm(), T.Get("common.saved"));
                await ReloadAsync();
            }
        }

        private async Task DeleteAsync(Product? product)
        {
            if (product == null) return;
            if (!UI.Dialogs.Confirm(this, T.Get("products.deleteTitle"), T.Format("products.deleteMessage", product.Name), T.Get("common.delete"), danger: true)) return;
            if (await UI.Dialogs.RunAsync(this, () => Session.Products.DeleteAsync(product.Id)))
            {
                await Session.LogAsync("PRODUCT_DELETE", product.Name);
                Toast.Show(FindForm(), T.Get("common.deleted"));
                await ReloadAsync();
            }
        }

        internal static void PaintStock(Graphics g, Rectangle r, Product p)
        {
            (string text, Color color) = Inventory.GetStockLevel(p) switch
            {
                StockLevel.OutOfStock => (T.Get("stock.out"), Theme.Danger),
                StockLevel.Low => (T.Format("stock.low", T.Number(p.Supplies)), Theme.Warning),
                _ => (T.Format("stock.in", T.Number(p.Supplies)), Theme.Success)
            };
            DataTable.PaintPill(g, r, text, color);
        }

        internal static void PaintExpiry(Graphics g, Rectangle r, Product p)
        {
            ExpiryState state = Inventory.GetExpiryState(p, DateTime.Today);
            Color color = state switch { ExpiryState.Expired => Theme.Danger, ExpiryState.ExpiringSoon => Theme.Warning, _ => Theme.Text };
            TextRenderer.DrawText(g, T.Date(p.ExpirationDate), state == ExpiryState.Fresh ? Theme.Font(10f) : Theme.Semibold(10f), r, color, Draw.LeftMiddle);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) debouncer.Dispose();
            base.Dispose(disposing);
        }
    }
}
