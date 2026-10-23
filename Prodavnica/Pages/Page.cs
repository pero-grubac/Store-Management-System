using Prodavnica.Database;
using Prodavnica.UI;
using Prodavnica.UI.Controls;

namespace Prodavnica.Pages
{
    // A screen shown in the main window; the header shows Title, Subtitle and HeaderActions.
    public abstract class Page : UserControl
    {
        protected Page()
        {
            BackColor = Theme.Background;
            Font = Theme.Font(10);
            DoubleBuffered = true;
            Padding = new Padding(LogicalToDeviceUnits(32), 0, LogicalToDeviceUnits(32), LogicalToDeviceUnits(24));
        }

        public abstract string Title { get; }

        public virtual string Subtitle => string.Empty;

        public virtual IEnumerable<Control> HeaderActions => Array.Empty<Control>();

        public virtual Task LoadAsync() => Task.CompletedTask;

        protected int S(int value) => LogicalToDeviceUnits(value);

        protected static ContextMenuStrip Menu(params (string Text, string Glyph, Action Action, bool Danger)[] items)
        {
            var menu = new ContextMenuStrip { Renderer = new MenuRenderer(), ShowImageMargin = false, Font = Theme.Font(10), Padding = new Padding(4) };
            foreach (var (text, glyph, action, danger) in items)
            {
                var item = new ToolStripMenuItem(text) { ForeColor = danger ? Theme.Danger : Theme.Text, Padding = new Padding(6, 4, 6, 4) };
                item.Click += (_, _) => action();
                menu.Items.Add(item);
            }
            return menu;
        }
    }

    // Search box, a table in a card, and paging; subclasses describe the columns and the actions.
    public abstract class ListPage<TItem> : Page where TItem : class
    {
        private readonly Debouncer debouncer;
        private int page = 1;

        protected ListPage()
        {
            Search = new UiInput(T.Get("common.search"), Glyphs.Search) { Width = S(380) };
            Table = new DataTable { Dock = DockStyle.Fill, EmptyText = T.Get("common.noResults") };
            Pager = new Pager { Dock = DockStyle.Bottom };
            Toolbar = new FlowLayoutPanel { Dock = DockStyle.Top, Height = S(56), BackColor = Color.Transparent, WrapContents = false };
            Toolbar.Controls.Add(Search);

            var card = new Card { Dock = DockStyle.Fill, Padding = new Padding(S(8), S(4), S(20), S(4)) };
            card.Controls.Add(Table);
            card.Controls.Add(Pager);
            Controls.Add(card);
            Controls.Add(Toolbar);

            debouncer = new Debouncer(300, async () => { page = 1; await ReloadAsync(); });
            Search.TextChanged += (_, _) => debouncer.Trigger();
            Pager.PageRequested += async (_, p) => { page = p; await ReloadAsync(); };
            Table.ItemActivated += async (_, item) => { if (CanEdit) await EditAsync((TItem)item); };
            Table.KeyDown += async (_, e) => { if (e.KeyCode == Keys.Delete && CanEdit && Table.SelectedItem is TItem item) await DeleteAsync(item); };
            if (CanEdit)
            {
                Table.ContextMenuStrip = Menu(
                    (T.Get("common.edit"), Glyphs.Edit, async () => { if (Table.SelectedItem is TItem i) await EditAsync(i); }, false),
                    (T.Get("common.delete"), Glyphs.Delete, async () => { if (Table.SelectedItem is TItem i) await DeleteAsync(i); }, true));
            }
        }

        protected UiInput Search { get; }
        protected DataTable Table { get; }
        protected Pager Pager { get; }
        protected FlowLayoutPanel Toolbar { get; }

        protected virtual bool CanEdit => true;
        protected virtual int PageSize => 15;

        protected abstract Task<PagedResult<TItem>> FetchAsync(PageRequest request);
        protected abstract object?[] Cells(TItem item);
        protected virtual Task EditAsync(TItem item) => Task.CompletedTask;
        protected virtual Task DeleteAsync(TItem item) => Task.CompletedTask;

        public override Task LoadAsync() => ReloadAsync();

        protected async Task ReloadAsync()
        {
            PagedResult<TItem> result = await FetchAsync(new PageRequest(page, PageSize, Search.Text));
            if (result.Items.Count == 0 && page > 1 && result.TotalCount > 0)
            {
                page = result.PageCount;
                result = await FetchAsync(new PageRequest(page, PageSize, Search.Text));
            }
            Table.SetRows(result.Items, Cells);
            Pager.Show(result);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) debouncer.Dispose();
            base.Dispose(disposing);
        }
    }
}
