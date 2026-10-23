using MySqlConnector;
using Prodavnica.Database;
using Prodavnica.Database.DTO;
using Prodavnica.Database.Repositories;
using Prodavnica.Security;
using Prodavnica.Services;

namespace Prodavnica.Tests.Database
{
    [Collection(DatabaseCollection.Name)]
    [Trait("Category", "Integration")]
    public class UserRepositoryTests
    {
        private readonly UserRepository users = new UserRepository();

        private static User NewUser()
        {
            string suffix = TestData.UniqueDigits()[^8..];
            return new User
            {
                FirstName = "Test",
                LastName = "User",
                UserName = "user" + suffix,
                Email = $"user{suffix}@example.com",
                PhoneNumber = "000",
                IdLanguage = 2
            };
        }

        [Fact]
        public async Task The_documented_demo_accounts_can_sign_in()
        {
            User? admin = await users.AuthenticateAsync("admin", "admin123");
            User? cashier = await users.AuthenticateAsync("kasir", "kasir123");
            User? staff = await users.AuthenticateAsync("miha", "lozinka123");

            Assert.True(admin!.IsAdmin);
            Assert.False(cashier!.IsAdmin);
            Assert.NotNull(staff);
        }

        [Theory]
        [InlineData("admin", "wrong")]
        [InlineData("admin", "")]
        [InlineData("nobody", "admin123")]
        public async Task Wrong_credentials_return_no_user(string userName, string password)
        {
            Assert.Null(await users.AuthenticateAsync(userName, password));
        }

        [Fact]
        public async Task A_new_user_gets_a_hashed_password_and_an_own_theme()
        {
            User user = NewUser();
            user.Image = new byte[] { 9, 9 };

            int id = await users.CreateAsync(user, "s3cret!");
            User? stored = await users.AuthenticateAsync(user.UserName, "s3cret!");

            Assert.Equal(id, stored!.Id);
            Assert.True(Password.Verify("s3cret!", stored.Password));
            Assert.NotEqual(0, stored.IdTheme);
            Assert.Equal(new byte[] { 9, 9 }, stored.Image);
            Assert.NotNull(await new ThemeRepository().GetByIdAsync(stored.IdTheme));
        }

        [Fact]
        public async Task Changing_the_password_requires_the_current_one()
        {
            User user = NewUser();
            int id = await users.CreateAsync(user, "old-pass");

            Assert.False(await users.ChangePasswordAsync(id, "not-it", "new-pass"));
            Assert.True(await users.ChangePasswordAsync(id, "old-pass", "new-pass"));
            Assert.Null(await users.AuthenticateAsync(user.UserName, "old-pass"));
            Assert.NotNull(await users.AuthenticateAsync(user.UserName, "new-pass"));
        }

        [Fact]
        public async Task Updating_keeps_the_password_unless_a_new_one_is_given()
        {
            User user = NewUser();
            int id = await users.CreateAsync(user, "first-pass");
            user.FirstName = "Changed";

            await users.UpdateAsync(user);
            Assert.Equal("Changed", (await users.GetByIdAsync(id))!.FirstName);
            Assert.NotNull(await users.AuthenticateAsync(user.UserName, "first-pass"));

            await users.UpdateAsync(user, "second-pass");
            Assert.NotNull(await users.AuthenticateAsync(user.UserName, "second-pass"));
        }

        [Fact]
        public async Task Users_cannot_delete_themselves_or_the_last_admin()
        {
            User user = NewUser();
            int id = await users.CreateAsync(user, "pass123");

            var self = await Assert.ThrowsAsync<StoreDataException>(() => users.DeleteAsync(id, id));
            Assert.Equal("CANNOT_DELETE_SELF", self.Code);

            await users.DeleteAsync(id, actingUserId: 1);
            Assert.Null(await users.GetByIdAsync(id));
        }

        [Fact]
        public async Task Staff_list_is_searchable_and_paged()
        {
            PagedResult<User> page = await users.GetPageAsync(new PageRequest(1, 10));
            PagedResult<User> search = await users.GetPageAsync(new PageRequest(1, 10, "kasir"));

            Assert.Equal(10, page.Items.Count);
            Assert.True(page.TotalCount >= 20);
            Assert.Contains(search.Items, u => u.UserName == "kasir");
            Assert.True(await users.UserNameExistsAsync("kasir"));
            Assert.False(await users.UserNameExistsAsync("kasir", exceptId: 2));
        }
    }

    [Collection(DatabaseCollection.Name)]
    [Trait("Category", "Integration")]
    public class ReferenceDataTests
    {
        private readonly MySqlFixture database;

        public ReferenceDataTests(MySqlFixture database)
        {
            this.database = database;
        }

        [Fact]
        public async Task Category_crud_and_in_use_protection()
        {
            var categories = new CategoryRepository();
            var category = new Category { Name = "Kat " + TestData.UniqueDigits()[^8..] };

            int id = await categories.CreateAsync(category);
            category.Name += "x";
            await categories.UpdateAsync(category);
            Assert.Contains(await categories.GetAllAsync(), c => c.Id == id && c.Name == category.Name);

            await categories.DeleteAsync(id);
            Assert.DoesNotContain(await categories.GetAllAsync(), c => c.Id == id);
            await Assert.ThrowsAsync<InUseException>(() => categories.DeleteAsync(1));
            await Assert.ThrowsAsync<DuplicateValueException>(() => categories.CreateAsync(new Category { Name = "Pekara" }));
        }

        [Theory]
        [InlineData(CompanyKind.Manufacturer)]
        [InlineData(CompanyKind.Supplier)]
        public async Task Company_crud_and_usage_counts(CompanyKind kind)
        {
            var companies = new CompanyRepository(kind);
            var company = new Company { Name = "Firma " + TestData.UniqueDigits()[^8..], Email = "f@example.com" };

            int id = await companies.CreateAsync(company);
            company.Email = "novi@example.com";
            await companies.UpdateAsync(company);
            PagedResult<Company> found = await companies.GetPageAsync(new PageRequest(1, 10, company.Name));
            Assert.Equal("novi@example.com", Assert.Single(found.Items).Email);

            await companies.DeleteAsync(id);
            Assert.Contains(await companies.GetAllAsync(), c => c.UsageCount > 0);
        }

        [Fact]
        public async Task A_theme_can_be_changed()
        {
            var themes = new ThemeRepository();
            Theme theme = (await themes.GetByIdAsync(3))!;
            theme.ColorName = ThemeFormat.FormatArgb(255, 1, 2, 3);
            theme.FontName = "Calibri";

            await themes.UpdateAsync(theme);
            Theme stored = (await themes.GetByIdAsync(3))!;

            Assert.Equal("255,1,2,3", stored.ColorName);
            Assert.Equal("Calibri", stored.FontName);
        }

        [Fact]
        public async Task Every_language_in_the_database_has_a_translation()
        {
            List<Language> languages = await new LanguageRepository().GetAllAsync();

            Assert.Equal(3, languages.Count);
            Assert.All(languages.Where(l => l.Name != "English"), l => Assert.NotEqual(LanguageCodes.Default, LanguageCodes.FromName(l.Name)));
        }

        [Fact]
        public async Task Activity_is_logged_and_searchable()
        {
            var audit = new AuditRepository();
            string marker = "marker-" + TestData.UniqueDigits();

            await audit.LogAsync(1, "TEST", marker);
            PagedResult<AuditEntry> page = await audit.GetPageAsync(new PageRequest(1, 10, marker));

            AuditEntry entry = Assert.Single(page.Items);
            Assert.Equal("TEST", entry.Action);
            Assert.Equal("Admin Adminović", entry.UserName);
        }

        [Fact]
        public async Task The_app_account_can_use_data_but_not_change_the_schema()
        {
            await using var connection = new MySqlConnection(database.AppConnectionString);
            await connection.OpenAsync();

            await using var select = new MySqlCommand("SELECT COUNT(*) FROM proizvod", connection);
            Assert.True(Convert.ToInt32(await select.ExecuteScalarAsync()) > 0);

            await using var drop = new MySqlCommand("DROP TABLE jezik", connection);
            var error = await Assert.ThrowsAsync<MySqlException>(() => drop.ExecuteNonQueryAsync());
            Assert.Equal(1142, error.Number);
        }
    }
}
