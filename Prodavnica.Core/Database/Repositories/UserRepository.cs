using MySqlConnector;
using Prodavnica.Database.DTO;
using Prodavnica.Security;
using Prodavnica.Services;

namespace Prodavnica.Database.Repositories
{
    public class UserRepository
    {
        private const string Select = @"
            SELECT idKorisnik, Ime, Prezime, Email, Lozinka, KorisnickoIme, BrojTelefona, isAdmin, idTema, idJezik, Slika
            FROM korisnik";

        private static User Map(MySqlDataReader r) => new User
        {
            Id = r.GetInt32("idKorisnik"),
            FirstName = r.GetString("Ime"),
            LastName = r.GetString("Prezime"),
            Email = r.GetString("Email"),
            Password = r.GetString("Lozinka"),
            UserName = r.GetString("KorisnickoIme"),
            PhoneNumber = r.GetString("BrojTelefona"),
            IsAdmin = r.GetBoolean("isAdmin"),
            IdTheme = r.GetInt32("idTema"),
            IdLanguage = r.GetInt32("idJezik"),
            Image = r.Bytes("Slika")
        };

        // Returns the user when the password matches, otherwise null.
        public async Task<User?> AuthenticateAsync(string userName, string password)
        {
            User? user = await Db.SingleAsync(Select + " WHERE KorisnickoIme = @name", Map, ("@name", userName.Trim()));
            return user != null && Password.Verify(password, user.Password) ? user : null;
        }

        public Task<User?> GetByIdAsync(int id)
        {
            return Db.SingleAsync(Select + " WHERE idKorisnik = @id", Map, ("@id", id));
        }

        public Task<PagedResult<User>> GetPageAsync(PageRequest page)
        {
            const string filter = @" WHERE (@like IS NULL OR Ime LIKE @like OR Prezime LIKE @like OR KorisnickoIme LIKE @like
                                            OR Email LIKE @like OR CONCAT(Ime, ' ', Prezime) LIKE @like)";
            return Db.PageAsync(Select + filter + " ORDER BY isAdmin DESC, Ime, Prezime", "SELECT COUNT(*) FROM korisnik" + filter,
                Map, page, ("@like", page.Like));
        }

        public async Task<bool> UserNameExistsAsync(string userName, int? exceptId = null)
        {
            object? count = await Db.ScalarAsync(
                "SELECT COUNT(*) FROM korisnik WHERE KorisnickoIme = @name AND (@except IS NULL OR idKorisnik <> @except)",
                ("@name", userName.Trim()), ("@except", exceptId));
            return Convert.ToInt32(count) > 0;
        }

        // Every user has their own theme row, created with the default look.
        public async Task<int> CreateAsync(User user, string password)
        {
            try
            {
                await using var connection = await Db.OpenAsync();
                await using var transaction = await connection.BeginTransactionAsync();

                await using (var theme = Db.Command(connection, @"
                    INSERT INTO tema (boja, font, `veličina`, fontStil, strikeout, underline) VALUES (@color, 'Segoe UI', '11', 'Regular', 0, 0);
                    SELECT LAST_INSERT_ID();", new (string, object?)[] { ("@color", ThemeFormat.DefaultColor) }, transaction))
                {
                    user.IdTheme = Convert.ToInt32(await theme.ExecuteScalarAsync());
                }

                if (user.IdLanguage == 0) user.IdLanguage = 1;
                user.Password = Password.HashValue(password);
                await using (var insert = Db.Command(connection, @"
                    INSERT INTO korisnik (Ime, Prezime, Email, Lozinka, KorisnickoIme, BrojTelefona, isAdmin, idTema, idJezik, Slika)
                    VALUES (@first, @last, @email, @hash, @name, @phone, @admin, @theme, @language, @image);
                    SELECT LAST_INSERT_ID();", Args(user).Append(("@hash", user.Password)).ToArray(), transaction))
                {
                    user.Id = Convert.ToInt32(await insert.ExecuteScalarAsync());
                }

                await transaction.CommitAsync();
                return user.Id;
            }
            catch (MySqlException ex)
            {
                throw DataErrors.Translate(ex);
            }
        }

        // Saves profile fields, role and picture; the password only when a new one is given.
        public async Task UpdateAsync(User user, string? newPassword = null)
        {
            await Db.ExecuteAsync(@"
                UPDATE korisnik SET Ime = @first, Prezime = @last, Email = @email, KorisnickoIme = @name,
                       BrojTelefona = @phone, isAdmin = @admin, Slika = @image
                WHERE idKorisnik = @id", Args(user));
            if (!string.IsNullOrEmpty(newPassword))
            {
                user.Password = Password.HashValue(newPassword);
                await Db.ExecuteAsync("UPDATE korisnik SET Lozinka = @hash WHERE idKorisnik = @id", ("@hash", user.Password), ("@id", user.Id));
            }
        }

        // Checks the current password first; returns false when it does not match.
        public async Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword)
        {
            User? user = await GetByIdAsync(userId);
            if (user == null || !Password.Verify(currentPassword, user.Password))
            {
                return false;
            }
            await Db.ExecuteAsync("UPDATE korisnik SET Lozinka = @hash WHERE idKorisnik = @id",
                ("@hash", Password.HashValue(newPassword)), ("@id", userId));
            return true;
        }

        public Task SetLanguageAsync(int userId, int languageId)
        {
            return Db.ExecuteAsync("UPDATE korisnik SET idJezik = @language WHERE idKorisnik = @id", ("@language", languageId), ("@id", userId));
        }

        // A user cannot delete their own account, and the last administrator cannot be deleted.
        public async Task DeleteAsync(int id, int actingUserId)
        {
            if (id == actingUserId)
            {
                throw new StoreDataException("CANNOT_DELETE_SELF");
            }
            User? user = await GetByIdAsync(id);
            if (user == null) return;
            if (user.IsAdmin && Convert.ToInt32(await Db.ScalarAsync("SELECT COUNT(*) FROM korisnik WHERE isAdmin = 1")) <= 1)
            {
                throw new StoreDataException("LAST_ADMIN");
            }
            await Db.ExecuteAsync("DELETE FROM korisnik WHERE idKorisnik = @id", ("@id", id));
            await Db.ExecuteAsync("DELETE FROM tema WHERE idTema = @theme", ("@theme", user.IdTheme));
        }

        private static (string, object?)[] Args(User u) => new (string, object?)[]
        {
            ("@id", u.Id),
            ("@first", u.FirstName.Trim()),
            ("@last", u.LastName.Trim()),
            ("@email", u.Email.Trim()),
            ("@name", u.UserName.Trim()),
            ("@phone", u.PhoneNumber.Trim()),
            ("@admin", u.IsAdmin),
            ("@theme", u.IdTheme),
            ("@language", u.IdLanguage),
            ("@image", u.Image)
        };
    }
}
