using Prodavnica.App;
using Prodavnica.Pages;
using Prodavnica.UI;
using Prodavnica.UI.Controls;

namespace Prodavnica.Shell
{
    public enum PageKey
    {
        Dashboard,
        Sale,
        Products,
        Procurement,
        Documents,
        Employees,
        Manufacturers,
        Suppliers,
        Categories,
        Activity,
        Settings
    }

    // The main window: a normal resizable window (maximized on start) with a sidebar and the current page.
    public class MainForm : Form
    {
        private readonly Panel sidebar = new SidebarPanel();
        private readonly Panel header = new Panel();
        private readonly Panel host = new Panel();
        private readonly UiLabel pageTitle = new UiLabel(string.Empty, 18f, semibold: true);
        private readonly UiLabel pageSubtitle = new UiLabel(string.Empty, 10f, color: Theme.Muted);
        private readonly FlowLayoutPanel actions = new FlowLayoutPanel { FlowDirection = FlowDirection.RightToLeft, BackColor = Color.Transparent, WrapContents = false };
        private readonly List<NavItem> navItems = new();
        private Page? current;

        public MainForm()
        {
            Text = T.Get("app.name");
            StartPosition = FormStartPosition.CenterScreen;
            AutoScaleMode = AutoScaleMode.None;
            MinimumSize = new Size(LogicalToDeviceUnits(1180), LogicalToDeviceUnits(720));
            Size = new Size(LogicalToDeviceUnits(1440), LogicalToDeviceUnits(880));
            WindowState = FormWindowState.Maximized;
            BackColor = Theme.Background;
            Font = Theme.Font(10);
            KeyPreview = true;

            header.Dock = DockStyle.Top;
            header.Height = LogicalToDeviceUnits(96);
            header.BackColor = Theme.Background;
            header.Controls.AddRange(new Control[] { pageTitle, pageSubtitle, actions });
            header.Layout += (_, _) =>
            {
                int left = LogicalToDeviceUnits(32);
                pageTitle.Location = new Point(left - LogicalToDeviceUnits(1), LogicalToDeviceUnits(24));
                pageSubtitle.Location = new Point(left, pageTitle.Bottom + LogicalToDeviceUnits(2));
                actions.SetBounds(header.Width / 3, LogicalToDeviceUnits(30), header.Width - header.Width / 3 - left, LogicalToDeviceUnits(44));
            };
            host.Dock = DockStyle.Fill;
            host.BackColor = Theme.Background;
            sidebar.Dock = DockStyle.Left;
            sidebar.Width = LogicalToDeviceUnits(252);

            Controls.Add(host);
            Controls.Add(header);
            Controls.Add(sidebar);
            BuildSidebar();
            Shown += async (_, _) => await NavigateAsync(PageKey.Dashboard);
        }

        public bool SignedOut { get; private set; }

        public PageKey CurrentKey { get; private set; }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            Native.LightCaption(this);
        }

        private void BuildSidebar()
        {
            sidebar.SuspendLayout();
            foreach (Control c in sidebar.Controls.Cast<Control>().ToList()) c.Dispose();
            sidebar.Controls.Clear();
            navItems.Clear();

            var items = new List<(PageKey Key, string Glyph, string Text, bool Section)>();
            void Add(PageKey key, string glyph, string text) => items.Add((key, glyph, text, false));
            void Section(string text) => items.Add((PageKey.Dashboard, string.Empty, text, true));

            Section(T.Get("nav.section.store"));
            Add(PageKey.Dashboard, Glyphs.Home, T.Get("nav.dashboard"));
            Add(PageKey.Sale, Glyphs.Cart, T.Get("nav.sale"));
            Add(PageKey.Products, Glyphs.Package, T.Get("nav.products"));
            if (Session.IsAdmin) Add(PageKey.Procurement, Glyphs.Import, T.Get("nav.procurement"));
            Add(PageKey.Documents, Glyphs.Receipt, T.Get(Session.IsAdmin ? "nav.documents" : "nav.bills"));
            if (Session.IsAdmin)
            {
                Section(T.Get("nav.section.admin"));
                Add(PageKey.Employees, Glyphs.People, T.Get("nav.employees"));
                Add(PageKey.Manufacturers, Glyphs.Factory, T.Get("nav.manufacturers"));
                Add(PageKey.Suppliers, Glyphs.Truck, T.Get("nav.suppliers"));
                Add(PageKey.Categories, Glyphs.Tag, T.Get("nav.categories"));
                Add(PageKey.Activity, Glyphs.History, T.Get("nav.activity"));
            }
            Section(T.Get("nav.section.account"));
            Add(PageKey.Settings, Glyphs.Settings, T.Get("nav.settings"));

            int y = LogicalToDeviceUnits(92);
            foreach (var item in items)
            {
                if (item.Section)
                {
                    var label = new UiLabel(item.Text.ToUpperInvariant(), 8f, semibold: true, Theme.Mix(Theme.SidebarText, Theme.Sidebar, 0.35))
                    {
                        Location = new Point(LogicalToDeviceUnits(26), y + LogicalToDeviceUnits(14))
                    };
                    sidebar.Controls.Add(label);
                    y += LogicalToDeviceUnits(40);
                    continue;
                }
                var nav = new NavItem(item.Key, item.Glyph, item.Text) { Location = new Point(LogicalToDeviceUnits(14), y) };
                nav.Width = sidebar.Width - LogicalToDeviceUnits(28);
                nav.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
                nav.Click += async (_, _) => await NavigateAsync(nav.Key);
                sidebar.Controls.Add(nav);
                navItems.Add(nav);
                y += nav.Height + LogicalToDeviceUnits(2);
            }

            var user = new UserBadge();
            var signOut = new NavItem(PageKey.Dashboard, Glyphs.SignOut, T.Get("nav.signOut")) { Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right };
            signOut.Click += async (_, _) => await SignOutAsync();
            sidebar.Controls.Add(user);
            sidebar.Controls.Add(signOut);
            sidebar.Layout += (_, _) =>
            {
                signOut.SetBounds(LogicalToDeviceUnits(14), sidebar.Height - LogicalToDeviceUnits(64), sidebar.Width - LogicalToDeviceUnits(28), signOut.Height);
                user.SetBounds(LogicalToDeviceUnits(14), signOut.Top - LogicalToDeviceUnits(70), sidebar.Width - LogicalToDeviceUnits(28), LogicalToDeviceUnits(62));
            };
            sidebar.ResumeLayout();
            sidebar.Invalidate(true);
            foreach (NavItem nav in navItems) nav.Active = nav.Key == CurrentKey;
        }

        public async Task NavigateAsync(PageKey key)
        {
            Page page = key switch
            {
                PageKey.Sale => new SalePage(),
                PageKey.Products => new ProductsPage(),
                PageKey.Procurement => new ProcurementPage(),
                PageKey.Documents => new DocumentsPage(),
                PageKey.Employees => new EmployeesPage(),
                PageKey.Manufacturers => new CompaniesPage(Database.DTO.CompanyKind.Manufacturer),
                PageKey.Suppliers => new CompaniesPage(Database.DTO.CompanyKind.Supplier),
                PageKey.Categories => new CategoriesPage(),
                PageKey.Activity => new ActivityPage(),
                PageKey.Settings => new SettingsPage(this),
                _ => new DashboardPage(this)
            };
            CurrentKey = key;
            foreach (NavItem nav in navItems) nav.Active = nav.Key == key;

            SuspendLayout();
            Page? old = current;
            current = page;
            pageTitle.Text = page.Title;
            pageSubtitle.Text = page.Subtitle;
            actions.Controls.Clear();
            foreach (Control action in page.HeaderActions.Reverse()) { action.Margin = new Padding(LogicalToDeviceUnits(10), 0, 0, 0); actions.Controls.Add(action); }
            page.Dock = DockStyle.Fill;
            host.Controls.Add(page);
            page.BringToFront();
            if (old != null) { host.Controls.Remove(old); old.Dispose(); }
            header.PerformLayout();
            ResumeLayout();
            await Dialogs.RunAsync(page, page.LoadAsync);
        }

        // After the theme or language changes: rebuild everything that shows text or colour.
        public async Task ReloadAppearanceAsync()
        {
            Text = T.Get("app.name");
            BackColor = Theme.Background;
            BuildSidebar();
            await NavigateAsync(CurrentKey);
        }

        public void UpdateUserBadge()
        {
            foreach (UserBadge badge in sidebar.Controls.OfType<UserBadge>()) badge.Invalidate();
        }

        private async Task SignOutAsync()
        {
            if (!Dialogs.Confirm(this, T.Get("signOut.title"), T.Get("signOut.message"), T.Get("nav.signOut"))) return;
            await Session.LogAsync("LOGOUT", T.Get("auditText.logout"));
            SignedOut = true;
            Close();
        }

        private sealed class SidebarPanel : Panel
        {
            public SidebarPanel()
            {
                SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true);
                BackColor = Theme.Sidebar;
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                Graphics g = e.Graphics;
                Draw.Smooth(g);
                var logo = new Rectangle(LogicalToDeviceUnits(22), LogicalToDeviceUnits(26), LogicalToDeviceUnits(40), LogicalToDeviceUnits(40));
                Draw.Fill(g, Theme.Accent, logo, LogicalToDeviceUnits(10));
                Draw.Glyph(g, Glyphs.Shop, 14, Theme.OnAccent, logo);
                TextRenderer.DrawText(g, T.Get("app.name"), Theme.Semibold(12.5f), new Point(logo.Right + LogicalToDeviceUnits(12), logo.Y + LogicalToDeviceUnits(1)), Color.White, TextFormatFlags.NoPadding);
                TextRenderer.DrawText(g, T.Get("app.tagline"), Theme.Font(8.5f), new Point(logo.Right + LogicalToDeviceUnits(12), logo.Y + LogicalToDeviceUnits(22)), Theme.SidebarText, TextFormatFlags.NoPadding);
            }
        }

        private sealed class NavItem : UiControl
        {
            private readonly string glyph;
            private bool active;

            public NavItem(PageKey key, string glyph, string text)
            {
                Key = key;
                this.glyph = glyph;
                Text = text;
                Height = S(42);
                Cursor = Cursors.Hand;
            }

            public PageKey Key { get; }

            public bool Active { get => active; set { active = value; Invalidate(); } }

            protected override void OnPaint(PaintEventArgs e)
            {
                Graphics g = e.Graphics;
                using (var back = new SolidBrush(Theme.Sidebar)) g.FillRectangle(back, ClientRectangle);
                Draw.Smooth(g);
                var r = new RectangleF(0, 0, Width - 1, Height - 1);
                if (active) Draw.Fill(g, Color.FromArgb(46, Theme.Accent), r, SF(9));
                else if (Hovered) Draw.Fill(g, Theme.SidebarHover, r, SF(9));
                if (active) Draw.Fill(g, Theme.Mix(Theme.Accent, Color.White, 0.2), new RectangleF(0, SF(10), SF(3), Height - SF(20)), SF(1.5f));
                Color fore = active ? Color.White : Hovered ? Color.White : Theme.SidebarText;
                Draw.Glyph(g, glyph, 11.5f, active ? Theme.Mix(Theme.Accent, Color.White, 0.45) : fore, new Rectangle(S(10), 0, S(30), Height));
                TextRenderer.DrawText(g, Text, active ? Theme.Semibold(10f) : Theme.Font(10f), new Rectangle(S(48), 0, Width - S(52), Height), fore, Draw.LeftMiddle);
            }
        }

        private sealed class UserBadge : UiControl
        {
            private Image? picture;
            private byte[]? source;

            protected override void OnPaint(PaintEventArgs e)
            {
                Graphics g = e.Graphics;
                using (var back = new SolidBrush(Theme.Sidebar)) g.FillRectangle(back, ClientRectangle);
                Draw.Smooth(g);
                Draw.Fill(g, Theme.SidebarHover, new RectangleF(0, 0, Width - 1, Height - 1), SF(10));
                if (!ReferenceEquals(source, Session.User.Image))
                {
                    picture?.Dispose();
                    source = Session.User.Image;
                    picture = Images.Decode(source);
                }
                var avatar = new Rectangle(S(12), (Height - S(38)) / 2, S(38), S(38));
                Draw.Avatar(g, avatar, Session.User.FullName, picture);
                int x = avatar.Right + S(12);
                TextRenderer.DrawText(g, Session.User.FullName, Theme.Semibold(9.75f), new Rectangle(x, S(12), Width - x - S(8), S(20)), Color.White, Draw.Line);
                TextRenderer.DrawText(g, T.Get(Session.IsAdmin ? "role.admin" : "role.cashier"), Theme.Font(8.75f), new Rectangle(x, S(32), Width - x - S(8), S(18)), Theme.SidebarText, Draw.Line);
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing) picture?.Dispose();
                base.Dispose(disposing);
            }
        }
    }
}
