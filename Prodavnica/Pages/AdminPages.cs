using Prodavnica.App;
using Prodavnica.Database;
using Prodavnica.Database.DTO;
using Prodavnica.Editors;
using Prodavnica.UI;
using Prodavnica.UI.Controls;

using Theme = Prodavnica.UI.Theme;
namespace Prodavnica.Pages
{
    public class EmployeesPage : ListPage<User>
    {
        private readonly UiButton add = new UiButton(T.Get("employees.add"), Glyphs.Add, ButtonKind.Primary);

        public EmployeesPage()
        {
            add.Click += async (_, _) => await CreateAsync();
            Table.AddCustom(T.Get("employees.name"), 2.2f, (g, r, item, _) =>
            {
                var user = (User)item;
                DataTable.PaintIdentity(g, r, user.FullName, "@" + user.UserName, Table.CachedImage(user, user.Image), round: true, Table);
            });
            Table.AddText(T.Get("employees.email"), 1.8f);
            Table.AddText(T.Get("employees.phone"), 1.2f);
            Table.AddCustom(T.Get("employees.role"), 1f, (g, r, item, _) =>
            {
                bool admin = ((User)item).IsAdmin;
                DataTable.PaintPill(g, r, T.Get(admin ? "role.admin" : "role.cashier"), admin ? Theme.Accent : Theme.Muted);
            });
        }

        public override string Title => T.Get("nav.employees");
        public override string Subtitle => T.Get("employees.subtitle");
        public override IEnumerable<Control> HeaderActions => new[] { add };

        protected override Task<PagedResult<User>> FetchAsync(PageRequest request) => Session.Users.GetPageAsync(request);

        protected override object?[] Cells(User u) => new object?[] { null, u.Email, u.PhoneNumber, null };

        private async Task CreateAsync()
        {
            using var dialog = new EmployeeDialog(null);
            if (dialog.ShowOver(this) == DialogResult.OK)
            {
                Toast.Show(FindForm(), T.Format("employees.created", dialog.Result!.FullName));
                await ReloadAsync();
            }
        }

        protected override async Task EditAsync(User item)
        {
            using var dialog = new EmployeeDialog(item);
            if (dialog.ShowOver(this) == DialogResult.OK)
            {
                Toast.Show(FindForm(), T.Get("common.saved"));
                if (item.Id == Session.User.Id) await Session.RefreshUserAsync();
                await ReloadAsync();
            }
        }

        protected override async Task DeleteAsync(User item)
        {
            if (!UI.Dialogs.Confirm(this, T.Get("employees.deleteTitle"), T.Format("employees.deleteMessage", item.FullName), T.Get("common.delete"), danger: true)) return;
            if (await UI.Dialogs.RunAsync(this, () => Session.Users.DeleteAsync(item.Id, Session.User.Id)))
            {
                await Session.LogAsync("USER_DELETE", item.FullName);
                Toast.Show(FindForm(), T.Get("common.deleted"));
                await ReloadAsync();
            }
        }
    }

    public class CompaniesPage : ListPage<Company>
    {
        private readonly CompanyKind kind;
        private readonly UiButton add;

        public CompaniesPage(CompanyKind kind)
        {
            this.kind = kind;
            add = new UiButton(T.Get(Key("add")), Glyphs.Add, ButtonKind.Primary);
            add.Click += async (_, _) => await EditAsync(new Company());
            Table.AddCustom(T.Get("common.name"), 2f, (g, r, item, _) => DataTable.PaintIdentity(g, r, ((Company)item).Name, string.Empty, null, round: true, Table));
            Table.AddText(T.Get("employees.email"), 2f);
            Table.AddText(T.Get(Key("usage")), 1f, right: true);
        }

        private string Key(string suffix) => (kind == CompanyKind.Manufacturer ? "manufacturers." : "suppliers.") + suffix;

        private Database.Repositories.CompanyRepository Repository => kind == CompanyKind.Manufacturer ? Session.Manufacturers : Session.Suppliers;

        public override string Title => T.Get(kind == CompanyKind.Manufacturer ? "nav.manufacturers" : "nav.suppliers");
        public override string Subtitle => T.Get(Key("subtitle"));
        public override IEnumerable<Control> HeaderActions => new[] { add };

        protected override Task<PagedResult<Company>> FetchAsync(PageRequest request) => Repository.GetPageAsync(request);

        protected override object?[] Cells(Company c) => new object?[] { null, c.Email, c.UsageCount };

        protected override async Task EditAsync(Company item)
        {
            bool isNew = item.Id == 0;
            using var dialog = new NameDialog(T.Get(isNew ? Key("add") : Key("edit")), item.Name, item.Email, withEmail: true);
            if (dialog.ShowOver(this) != DialogResult.OK) return;
            var company = new Company { Id = item.Id, Name = dialog.NameValue, Email = dialog.EmailValue };
            bool ok = await UI.Dialogs.RunAsync(this, async () =>
            {
                if (isNew) await Repository.CreateAsync(company); else await Repository.UpdateAsync(company);
            });
            if (!ok) return;
            await Session.LogAsync(isNew ? "COMPANY_CREATE" : "COMPANY_UPDATE", company.Name);
            Toast.Show(FindForm(), T.Get("common.saved"));
            await ReloadAsync();
        }

        protected override async Task DeleteAsync(Company item)
        {
            if (!UI.Dialogs.Confirm(this, T.Get("common.deleteTitle"), T.Format("common.deleteMessage", item.Name), T.Get("common.delete"), danger: true)) return;
            if (await UI.Dialogs.RunAsync(this, () => Repository.DeleteAsync(item.Id)))
            {
                await Session.LogAsync("COMPANY_DELETE", item.Name);
                Toast.Show(FindForm(), T.Get("common.deleted"));
                await ReloadAsync();
            }
        }
    }

    public class CategoriesPage : ListPage<Category>
    {
        private readonly UiButton add = new UiButton(T.Get("categories.add"), Glyphs.Add, ButtonKind.Primary);

        public CategoriesPage()
        {
            add.Click += async (_, _) => await EditAsync(new Category());
            Table.AddCustom(T.Get("common.name"), 3f, (g, r, item, _) =>
            {
                string name = ((Category)item).Name;
                int size = Table.LogicalToDeviceUnits(30);
                var icon = new Rectangle(r.X, r.Y + (r.Height - size) / 2, size, size);
                Draw.Fill(g, Theme.Mix(Theme.ColorFor(name), Color.White, 0.85f), icon, Table.LogicalToDeviceUnits(8));
                Draw.Glyph(g, Glyphs.Tag, 10, Theme.ColorFor(name), icon);
                TextRenderer.DrawText(g, name, Theme.Semibold(10f), new Rectangle(icon.Right + Table.LogicalToDeviceUnits(12), r.Y, r.Width - size, r.Height), Theme.Text, Draw.LeftMiddle);
            });
            Table.AddText(T.Get("categories.products"), 1f, right: true);
        }

        public override string Title => T.Get("nav.categories");
        public override string Subtitle => T.Get("categories.subtitle");
        public override IEnumerable<Control> HeaderActions => new[] { add };

        protected override Task<PagedResult<Category>> FetchAsync(PageRequest request) => Session.Categories.GetPageAsync(request);

        protected override object?[] Cells(Category c) => new object?[] { null, c.ProductCount };

        protected override async Task EditAsync(Category item)
        {
            bool isNew = item.Id == 0;
            using var dialog = new NameDialog(T.Get(isNew ? "categories.add" : "categories.edit"), item.Name, null, withEmail: false);
            if (dialog.ShowOver(this) != DialogResult.OK) return;
            var category = new Category { Id = item.Id, Name = dialog.NameValue };
            bool ok = await UI.Dialogs.RunAsync(this, async () =>
            {
                if (isNew) await Session.Categories.CreateAsync(category); else await Session.Categories.UpdateAsync(category);
            });
            if (!ok) return;
            await Session.LogAsync(isNew ? "CATEGORY_CREATE" : "CATEGORY_UPDATE", category.Name);
            Toast.Show(FindForm(), T.Get("common.saved"));
            await ReloadAsync();
        }

        protected override async Task DeleteAsync(Category item)
        {
            if (!UI.Dialogs.Confirm(this, T.Get("common.deleteTitle"), T.Format("common.deleteMessage", item.Name), T.Get("common.delete"), danger: true)) return;
            if (await UI.Dialogs.RunAsync(this, () => Session.Categories.DeleteAsync(item.Id)))
            {
                await Session.LogAsync("CATEGORY_DELETE", item.Name);
                Toast.Show(FindForm(), T.Get("common.deleted"));
                await ReloadAsync();
            }
        }
    }

    public class ActivityPage : ListPage<AuditEntry>
    {
        public ActivityPage()
        {
            Table.AddText(T.Get("activity.time"), 1.1f);
            Table.AddCustom(T.Get("activity.user"), 1.6f, (g, r, item, _) =>
                DataTable.PaintIdentity(g, r, ((AuditEntry)item).UserName is { Length: > 0 } n ? n : "—", string.Empty, null, round: true, Table));
            Table.AddCustom(T.Get("activity.action"), 1.2f, (g, r, item, _) =>
            {
                string action = ((AuditEntry)item).Action;
                Color color = action switch
                {
                    "SALE" => Theme.Success,
                    "PROCUREMENT" => Theme.Info,
                    "LOGIN" or "LOGOUT" => Theme.Muted,
                    _ when action.EndsWith("DELETE") => Theme.Danger,
                    _ => Theme.Accent
                };
                DataTable.PaintPill(g, r, T.Get("action." + action) is { } text && text != "action." + action ? text : action, color);
            });
            Table.AddText(T.Get("activity.details"), 3f);
        }

        public override string Title => T.Get("nav.activity");
        public override string Subtitle => T.Get("activity.subtitle");
        protected override bool CanEdit => false;
        protected override int PageSize => 18;

        protected override Task<PagedResult<AuditEntry>> FetchAsync(PageRequest request) => Session.Audit.GetPageAsync(request);

        protected override object?[] Cells(AuditEntry a) => new object?[] { T.DateTime(a.Time), null, null, a.Details };
    }
}
