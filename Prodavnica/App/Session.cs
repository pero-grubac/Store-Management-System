using Prodavnica.Database.DTO;
using Prodavnica.Database.Repositories;
using Prodavnica.Language;

namespace Prodavnica.App
{
    // The signed-in user and the repositories the screens share.
    public static class Session
    {
        public static readonly ProductRepository Products = new ProductRepository();
        public static readonly UserRepository Users = new UserRepository();
        public static readonly CategoryRepository Categories = new CategoryRepository();
        public static readonly CompanyRepository Manufacturers = new CompanyRepository(CompanyKind.Manufacturer);
        public static readonly CompanyRepository Suppliers = new CompanyRepository(CompanyKind.Supplier);
        public static readonly SalesRepository Sales = new SalesRepository();
        public static readonly ProcurementRepository Procurements = new ProcurementRepository();
        public static readonly ThemeRepository Themes = new ThemeRepository();
        public static readonly LanguageRepository Languages = new LanguageRepository();
        public static readonly AuditRepository Audit = new AuditRepository();
        public static readonly DashboardRepository Dashboard = new DashboardRepository();

        public static User User { get; private set; } = new User();

        public static Theme ThemeSettings { get; private set; } = new Theme();

        public static bool IsAdmin => User.IsAdmin;

        public static async Task StartAsync(User user)
        {
            User = user;
            await ReloadPreferencesAsync();
        }

        // Applies the user's colour, font and language; called again after Settings changes them.
        public static async Task ReloadPreferencesAsync()
        {
            ThemeSettings = await Themes.GetByIdAsync(User.IdTheme) ?? new Theme { Id = User.IdTheme };
            UI.Theme.Apply(ThemeSettings);
            List<Database.DTO.Language> languages = await Languages.GetAllAsync();
            LanguageHelper.ChangeLanguage(languages.FirstOrDefault(l => l.Id == User.IdLanguage)?.Name ?? "Srpski(Latinica)");
        }

        public static async Task RefreshUserAsync()
        {
            User = await Users.GetByIdAsync(User.Id) ?? User;
        }

        // Activity logging never interrupts the user's work.
        public static async Task LogAsync(string action, string details)
        {
            try
            {
                await Audit.LogAsync(User.Id == 0 ? null : User.Id, action, details);
            }
            catch (Exception)
            {
                // The action itself succeeded; a missing log line is not worth an error dialog.
            }
        }

        public static void End()
        {
            User = new User();
            UI.Theme.Apply(null);
            LanguageHelper.ChangeLanguage("Srpski(Latinica)");
        }
    }
}
