using Prodavnica.App;
using Prodavnica.Database.DTO;
using Prodavnica.Editors;
using Prodavnica.Services;
using Prodavnica.Shell;
using Prodavnica.UI;
using Prodavnica.UI.Controls;
using System.Globalization;

using Theme = Prodavnica.UI.Theme;
namespace Prodavnica.Pages
{
    public class DashboardPage : Page
    {
        private readonly MainForm main;
        private readonly StatTile sales = new StatTile();
        private readonly StatTile products = new StatTile();
        private readonly StatTile lowStock = new StatTile();
        private readonly StatTile expiry = new StatTile();
        private readonly BarChart chart = new BarChart { Dock = DockStyle.Fill };
        private readonly DataTable recent = new DataTable { Dock = DockStyle.Fill, EmptyText = T.Get("documents.empty") };
        private readonly DataTable attention = new DataTable { Dock = DockStyle.Fill, EmptyText = T.Get("dashboard.allGood") };
        private readonly Card attentionCard;

        public DashboardPage(MainForm main)
        {
            this.main = main;
            AutoScroll = true;
            var tiles = new TableLayoutPanel { Dock = DockStyle.Top, Height = S(132), ColumnCount = 4, BackColor = Color.Transparent };
            for (int i = 0; i < 4; i++) tiles.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            foreach (StatTile tile in new[] { sales, products, lowStock, expiry })
            {
                tile.Dock = DockStyle.Fill;
                tile.Margin = new Padding(0, 0, S(16), 0);
                tiles.Controls.Add(tile);
            }
            expiry.Margin = Padding.Empty;

            var chartCard = new Card { Title = T.Get("dashboard.salesChart"), Subtitle = T.Get("dashboard.salesChartSubtitle"), Dock = DockStyle.Fill, Margin = new Padding(0, 0, S(16), 0) };
            chartCard.Controls.Add(chart);
            var recentCard = new Card { Title = T.Get("dashboard.recent"), Subtitle = T.Get("dashboard.recentSubtitle"), Dock = DockStyle.Fill, Margin = Padding.Empty, Padding = new Padding(S(8), S(76), S(16), S(8)) };
            recent.ColumnHeadersVisible = false;
            recent.AddCustom(string.Empty, 2f, (g, r, item, _) =>
            {
                var bill = (Bill)item;
                int size = recent.LogicalToDeviceUnits(34);
                var icon = new Rectangle(r.X, r.Y + (r.Height - size) / 2, size, size);
                Draw.Fill(g, Theme.Mix(Theme.Success, Color.White, 0.86f), icon, recent.LogicalToDeviceUnits(9));
                Draw.Glyph(g, Glyphs.Receipt, 11, Theme.Success, icon);
                int x = icon.Right + recent.LogicalToDeviceUnits(12), h = recent.LogicalToDeviceUnits(19);
                int y = r.Y + (r.Height - h * 2) / 2;
                TextRenderer.DrawText(g, T.DocumentNumber(bill.Id), Theme.Semibold(10f), new Rectangle(x, y, r.Right - x, h), Theme.Text, Draw.LeftMiddle);
                TextRenderer.DrawText(g, $"{bill.CashierName} · {bill.Date:HH:mm}", Theme.Font(9f), new Rectangle(x, y + h, r.Right - x, h), Theme.Muted, Draw.LeftMiddle);
            });
            DataGridViewColumn amount = recent.AddText(string.Empty, 1f, right: true);
            amount.DefaultCellStyle.Font = Theme.Semibold(10f);
            recentCard.Controls.Add(recent);
            var middle = new TableLayoutPanel { Dock = DockStyle.Top, Height = S(360), ColumnCount = 2, BackColor = Color.Transparent };
            middle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 62));
            middle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38));
            middle.Controls.Add(chartCard);
            middle.Controls.Add(recentCard);

            attentionCard = new Card { Title = T.Get("dashboard.attention"), Subtitle = T.Get("dashboard.attentionSubtitle"), Dock = DockStyle.Top, Padding = new Padding(S(8), S(76), S(16), S(8)) };
            attention.AddCustom(T.Get("products.product"), 2.4f, (g, r, item, _) =>
            {
                var p = (Product)item;
                DataTable.PaintIdentity(g, r, p.Name, $"{p.CategoryName} · {p.ManufacturerName}", attention.CachedImage(p, p.Image), round: false, attention);
            });
            attention.AddCustom(T.Get("products.stock"), 1.1f, (g, r, item, _) => ProductsPage.PaintStock(g, r, (Product)item));
            attention.AddCustom(T.Get("products.expires"), 1f, (g, r, item, _) => ProductsPage.PaintExpiry(g, r, (Product)item));
            attention.AddCustom(T.Get("dashboard.problem"), 1.2f, (g, r, item, _) =>
            {
                var p = (Product)item;
                ExpiryState state = Inventory.GetExpiryState(p, DateTime.Today);
                (string key, Color color) = state == ExpiryState.Expired ? ("stock.expired", Theme.Danger)
                    : Inventory.GetStockLevel(p) == StockLevel.OutOfStock ? ("stock.out", Theme.Danger)
                    : state == ExpiryState.ExpiringSoon ? ("stock.expiringSoon", Theme.Warning)
                    : ("dashboard.lowStock", Theme.Warning);
                DataTable.PaintPill(g, r, T.Get(key), color);
            });
            attention.ItemActivated += async (_, item) =>
            {
                using var dialog = new ProductDialog((Product)item, readOnly: !Session.IsAdmin);
                if (dialog.ShowOver(this) == DialogResult.OK) await LoadAsync();
            };
            recent.ItemActivated += (_, item) =>
            {
                using var dialog = new DocumentDialog(DocumentKind.Bill, ((Bill)item).Id);
                dialog.ShowOver(this);
            };
            attentionCard.Controls.Add(attention);

            Controls.Add(attentionCard);
            Controls.Add(Spacer());
            Controls.Add(middle);
            Controls.Add(Spacer());
            Controls.Add(tiles);
        }

        // Keep the scroll position when a control inside gets focus.
        protected override Point ScrollToControl(Control activeControl) => DisplayRectangle.Location;

        private Panel Spacer() => new Panel { Dock = DockStyle.Top, Height = S(16), BackColor = Color.Transparent };

        public override string Title => T.Get("nav.dashboard");

        public override string Subtitle => T.Format("dashboard.welcome", Session.User.FirstName, DateTime.Today.ToString("D", CultureInfo.CurrentCulture));

        public override IEnumerable<Control> HeaderActions
        {
            get
            {
                var sell = new UiButton(T.Get("dashboard.newSale"), Glyphs.Cart, ButtonKind.Primary);
                sell.Click += async (_, _) => await main.NavigateAsync(PageKey.Sale);
                return new[] { sell };
            }
        }

        public override async Task LoadAsync()
        {
            DashboardData data = await Session.Dashboard.GetAsync(DateTime.Now);
            sales.SetContent(T.Get("dashboard.salesToday"), T.Money(data.SalesToday), T.Format("dashboard.billsToday", data.BillsToday, T.Money(data.SalesLast30Days)), Glyphs.Money, Theme.Success);
            products.SetContent(T.Get("dashboard.products"), data.ProductCount.ToString(CultureInfo.CurrentCulture), T.Format("dashboard.stockValue", T.Money(data.StockValue)), Glyphs.Package, Theme.Accent);
            lowStock.SetContent(T.Get("dashboard.lowStock"), (data.LowStockCount + data.OutOfStockCount).ToString(CultureInfo.CurrentCulture), T.Format("dashboard.outOfStock", data.OutOfStockCount), Glyphs.Warning, Theme.Warning);
            expiry.SetContent(T.Get("dashboard.expired"), data.ExpiredCount.ToString(CultureInfo.CurrentCulture), T.Format("dashboard.expiringSoon", data.ExpiringSoonCount), Glyphs.Clock, Theme.Danger);
            chart.SetData(data.DailySales);
            recent.SetRows(data.RecentBills, b => new object?[] { null, T.Money(b.Total) });
            attention.SetRows(data.NeedsAttention, _ => new object?[] { null, null, null, null });
            attentionCard.Height = S(76 + 44 + 8) + Math.Max(1, data.NeedsAttention.Count) * attention.RowTemplate.Height + S(12);
        }
    }
}
