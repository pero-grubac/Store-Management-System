using Prodavnica.App;
using Prodavnica.Database;
using Prodavnica.Database.DTO;
using Prodavnica.Editors;
using Prodavnica.UI;
using Prodavnica.UI.Controls;

using Theme = Prodavnica.UI.Theme;
namespace Prodavnica.Pages
{
    // History of bills (and, for the administrator, procurements) in a date range.
    public class DocumentsPage : Page
    {
        private readonly Segmented kind = new Segmented((T.Get("documents.sales"), Glyphs.Receipt), (T.Get("documents.procurements"), Glyphs.Import));
        private readonly UiDate from = new UiDate();
        private readonly UiDate to = new UiDate();
        private readonly Segmented range = new Segmented((T.Get("documents.today"), string.Empty), (T.Get("documents.week"), string.Empty),
            (T.Get("documents.month"), string.Empty), (T.Get("documents.quarter"), string.Empty));
        private readonly UiInput search = new UiInput(T.Get("documents.searchPlaceholder"), Glyphs.Search);
        private readonly DataTable table = new DataTable { Dock = DockStyle.Fill, EmptyText = T.Get("documents.empty") };
        private readonly Pager pager = new Pager { Dock = DockStyle.Bottom };
        private readonly Debouncer debouncer;
        private bool settingRange;
        private int page = 1;

        public DocumentsPage()
        {
            search.Width = S(300);
            var toolbar = new Panel { Dock = DockStyle.Top, Height = S(58), BackColor = Color.Transparent };
            var dash = new UiLabel("–", 11f, color: Theme.Muted);
            kind.Visible = Session.IsAdmin;
            toolbar.Controls.AddRange(new Control[] { kind, from, dash, to, range, search });
            toolbar.Layout += (_, _) =>
            {
                int x = 0, gap = S(12);
                if (kind.Visible) { kind.Location = new Point(0, S(1)); x = kind.Right + gap * 2; }
                from.Location = new Point(x, 0);
                dash.Location = new Point(from.Right + S(6), S(10));
                to.Location = new Point(from.Right + S(22), 0);
                range.Location = new Point(to.Right + gap, S(1));
                search.SetBounds(Math.Max(range.Right + gap, toolbar.Width - search.Width), 0, search.Width, search.Height);
            };
            BuildColumns();

            var card = new Card { Dock = DockStyle.Fill, Padding = new Padding(S(8), S(4), S(20), S(4)) };
            card.Controls.Add(table);
            card.Controls.Add(pager);
            Controls.Add(card);
            Controls.Add(toolbar);

            from.Value = DateTime.Today.AddDays(-29);
            to.Value = DateTime.Today;
            range.SelectedIndex = 2;
            debouncer = new Debouncer(300, async () => { page = 1; await ReloadAsync(); });
            search.TextChanged += (_, _) => debouncer.Trigger();
            kind.SelectedIndexChanged += async (_, _) => { BuildColumns(); page = 1; await ReloadAsync(); };
            range.SelectedIndexChanged += async (_, _) =>
            {
                settingRange = true;
                to.Value = DateTime.Today;
                from.Value = DateTime.Today.AddDays(-(new[] { 0, 6, 29, 89 }[range.SelectedIndex]));
                settingRange = false;
                page = 1;
                await ReloadAsync();
            };
            from.ValueChanged += async (_, _) => { if (!settingRange) { page = 1; await ReloadAsync(); } };
            to.ValueChanged += async (_, _) => { if (!settingRange) { page = 1; await ReloadAsync(); } };
            pager.PageRequested += async (_, p) => { page = p; await ReloadAsync(); };
            table.ItemActivated += (_, item) => Open(item);
            table.ContextMenuStrip = Menu((T.Get("common.details"), Glyphs.Receipt, () => { if (table.SelectedItem is { } i) Open(i); }, false));
        }

        public override string Title => T.Get(Session.IsAdmin ? "nav.documents" : "nav.bills");
        public override string Subtitle => T.Get("documents.subtitle");

        private bool Procurements => kind.SelectedIndex == 1;

        private void BuildColumns()
        {
            table.Rows.Clear();
            table.Columns.Clear();
            table.AddText(T.Get("documents.number"), 0.9f);
            table.AddText(T.Get("documents.date"), 1.2f);
            table.AddCustom(T.Get(Procurements ? "documents.supplier" : "documents.cashier"), 1.8f, (g, r, item, _) =>
            {
                string person = item is Procurement p ? p.SupplierName : ((Bill)item).CashierName;
                string sub = item is Procurement q ? q.UserName : string.Empty;
                DataTable.PaintIdentity(g, r, person.Length > 0 ? person : "—", sub, null, round: true, table);
            });
            table.AddText(T.Get("documents.items"), 0.7f, right: true);
            DataGridViewColumn total = table.AddText(T.Get("documents.total"), 1f, right: true);
            total.DefaultCellStyle.Font = Theme.Semibold(10f);
        }

        public override Task LoadAsync() => ReloadAsync();

        private async Task ReloadAsync()
        {
            var request = new PageRequest(page, 15, search.Text);
            if (Procurements)
            {
                PagedResult<Procurement> result = await Session.Procurements.GetPageAsync(from.Value, to.Value, request);
                table.SetRows(result.Items, p => new object?[] { T.DocumentNumber(p.Id), T.DateTime(p.Date), null, p.ItemCount, T.Money(p.Total) });
                pager.Show(result);
            }
            else
            {
                PagedResult<Bill> result = await Session.Sales.GetPageAsync(from.Value, to.Value, request);
                table.SetRows(result.Items, b => new object?[] { T.DocumentNumber(b.Id), T.DateTime(b.Date), null, b.ItemCount, T.Money(b.Total) });
                pager.Show(result);
            }
        }

        private void Open(object item)
        {
            using var dialog = item is Procurement p ? new DocumentDialog(DocumentKind.Procurement, p.Id) : new DocumentDialog(DocumentKind.Bill, ((Bill)item).Id);
            dialog.ShowOver(this);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) debouncer.Dispose();
            base.Dispose(disposing);
        }
    }
}
