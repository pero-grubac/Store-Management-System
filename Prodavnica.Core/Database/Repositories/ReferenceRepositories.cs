using MySqlConnector;
using Prodavnica.Database.DTO;

namespace Prodavnica.Database.Repositories
{
    public class CategoryRepository
    {
        private const string Select = @"
            SELECT k.idKategorija, k.Naziv, (SELECT COUNT(*) FROM proizvod p WHERE p.idKategorija = k.idKategorija) AS Broj
            FROM kategorija k";

        private static Category Map(MySqlDataReader r) => new Category
        {
            Id = r.GetInt32("idKategorija"),
            Name = r.GetString("Naziv"),
            ProductCount = r.GetInt32("Broj")
        };

        public Task<List<Category>> GetAllAsync()
        {
            return Db.QueryAsync(Select + " ORDER BY k.Naziv", Map);
        }

        public Task<PagedResult<Category>> GetPageAsync(PageRequest page)
        {
            const string filter = " WHERE (@like IS NULL OR k.Naziv LIKE @like)";
            return Db.PageAsync(Select + filter + " ORDER BY k.Naziv", "SELECT COUNT(*) FROM kategorija k" + filter, Map, page, ("@like", page.Like));
        }

        public async Task<int> CreateAsync(Category category)
        {
            category.Id = Convert.ToInt32(await Db.ScalarAsync("INSERT INTO kategorija (Naziv) VALUES (@name); SELECT LAST_INSERT_ID();",
                ("@name", category.Name.Trim())));
            return category.Id;
        }

        public Task UpdateAsync(Category category)
        {
            return Db.ExecuteAsync("UPDATE kategorija SET Naziv = @name WHERE idKategorija = @id", ("@name", category.Name.Trim()), ("@id", category.Id));
        }

        public Task DeleteAsync(int id)
        {
            return Db.ExecuteAsync("DELETE FROM kategorija WHERE idKategorija = @id", ("@id", id));
        }
    }

    // Manufacturers and suppliers share a shape: a name and an e-mail address.
    public class CompanyRepository
    {
        private readonly string table;
        private readonly string idColumn;
        private readonly string usage;

        public CompanyRepository(CompanyKind kind)
        {
            Kind = kind;
            (table, idColumn, usage) = kind == CompanyKind.Manufacturer
                ? ("proizvodjac", "idProizvodjac", "(SELECT COUNT(*) FROM proizvod x WHERE x.idProizvodjac = c.idProizvodjac)")
                : ("dobavljac", "idDobavljac", "(SELECT COUNT(*) FROM nabavka x WHERE x.`idDobavljač` = c.idDobavljac)");
        }

        public CompanyKind Kind { get; }

        private string Select => $"SELECT c.{idColumn} AS Id, c.Ime, c.Email, {usage} AS Broj FROM {table} c";

        private static Company Map(MySqlDataReader r) => new Company
        {
            Id = r.GetInt32("Id"),
            Name = r.GetString("Ime"),
            Email = r.GetString("Email"),
            UsageCount = r.GetInt32("Broj")
        };

        public Task<List<Company>> GetAllAsync()
        {
            return Db.QueryAsync(Select + " ORDER BY c.Ime", Map);
        }

        public Task<PagedResult<Company>> GetPageAsync(PageRequest page)
        {
            const string filter = " WHERE (@like IS NULL OR c.Ime LIKE @like OR c.Email LIKE @like)";
            return Db.PageAsync(Select + filter + " ORDER BY c.Ime", $"SELECT COUNT(*) FROM {table} c" + filter, Map, page, ("@like", page.Like));
        }

        public async Task<int> CreateAsync(Company company)
        {
            company.Id = Convert.ToInt32(await Db.ScalarAsync($"INSERT INTO {table} (Ime, Email) VALUES (@name, @email); SELECT LAST_INSERT_ID();",
                ("@name", company.Name.Trim()), ("@email", company.Email.Trim())));
            return company.Id;
        }

        public Task UpdateAsync(Company company)
        {
            return Db.ExecuteAsync($"UPDATE {table} SET Ime = @name, Email = @email WHERE {idColumn} = @id",
                ("@name", company.Name.Trim()), ("@email", company.Email.Trim()), ("@id", company.Id));
        }

        public Task DeleteAsync(int id)
        {
            return Db.ExecuteAsync($"DELETE FROM {table} WHERE {idColumn} = @id", ("@id", id));
        }
    }

    public class ThemeRepository
    {
        private static Theme Map(MySqlDataReader r) => new Theme
        {
            Id = r.GetInt32("idTema"),
            ColorName = r.GetString("boja"),
            FontName = r.GetString("font"),
            Size = int.TryParse(r.GetString("veličina"), out int size) ? size : 11,
            FontStyle = r.Text("fontStil"),
            IsStrikeout = !r.IsDBNull(r.GetOrdinal("strikeout")) && r.GetBoolean("strikeout"),
            IsUnderline = !r.IsDBNull(r.GetOrdinal("underline")) && r.GetBoolean("underline")
        };

        public Task<Theme?> GetByIdAsync(int id)
        {
            return Db.SingleAsync("SELECT * FROM tema WHERE idTema = @id", Map, ("@id", id));
        }

        public Task UpdateAsync(Theme theme)
        {
            return Db.ExecuteAsync(@"
                UPDATE tema SET boja = @color, font = @font, `veličina` = @size, fontStil = @style, strikeout = @strike, underline = @under
                WHERE idTema = @id",
                ("@color", theme.ColorName), ("@font", theme.FontName), ("@size", theme.Size.ToString()), ("@style", theme.FontStyle),
                ("@strike", theme.IsStrikeout), ("@under", theme.IsUnderline), ("@id", theme.Id));
        }
    }

    public class LanguageRepository
    {
        public Task<List<Language>> GetAllAsync()
        {
            return Db.QueryAsync("SELECT idJezik, Naziv FROM jezik ORDER BY idJezik",
                r => new Language { Id = r.GetInt32("idJezik"), Name = r.GetString("Naziv") });
        }
    }

    public class AuditRepository
    {
        public Task LogAsync(int? userId, string action, string details)
        {
            return Db.ExecuteAsync("INSERT INTO audit_log (Vrijeme, idKorisnik, Akcija, Opis) VALUES (@time, @user, @action, @details)",
                ("@time", DateTime.Now), ("@user", userId), ("@action", action),
                ("@details", details.Length > 255 ? details[..255] : details));
        }

        public Task<PagedResult<AuditEntry>> GetPageAsync(PageRequest page)
        {
            const string from = @" FROM audit_log a LEFT JOIN korisnik k ON k.idKorisnik = a.idKorisnik
                WHERE (@like IS NULL OR a.Akcija LIKE @like OR a.Opis LIKE @like OR k.KorisnickoIme LIKE @like OR CONCAT(k.Ime, ' ', k.Prezime) LIKE @like)";
            return Db.PageAsync(
                "SELECT a.idLog, a.Vrijeme, a.Akcija, a.Opis, CONCAT(IFNULL(k.Ime, ''), ' ', IFNULL(k.Prezime, '')) AS Korisnik" + from + " ORDER BY a.Vrijeme DESC, a.idLog DESC",
                "SELECT COUNT(*)" + from,
                r => new AuditEntry
                {
                    Id = r.GetInt32("idLog"),
                    Time = r.GetDateTime("Vrijeme"),
                    Action = r.GetString("Akcija"),
                    Details = r.GetString("Opis"),
                    UserName = r.GetString("Korisnik").Trim()
                },
                page, ("@like", page.Like));
        }
    }
}
