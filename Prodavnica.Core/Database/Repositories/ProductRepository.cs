using MySqlConnector;
using Prodavnica.Database.DTO;

namespace Prodavnica.Database.Repositories
{
    public sealed record ProductQuery(PageRequest Page, int? CategoryId = null, bool SellableOnly = false, DateTime? Today = null);

    public class ProductRepository
    {
        private const string Select = @"
            SELECT p.idProizvod, p.Naziv, p.Cijena, p.Zalihe, p.DatumIsteka, p.BarKod, p.Opis, p.Slika,
                   p.idKategorija, k.Naziv AS Kategorija, p.idProizvodjac, m.Ime AS Proizvodjac
            FROM proizvod p
            JOIN kategorija k ON k.idKategorija = p.idKategorija
            JOIN proizvodjac m ON m.idProizvodjac = p.idProizvodjac";

        private const string Filter = @"
            WHERE (@like IS NULL OR p.Naziv LIKE @like OR p.BarKod LIKE @like OR k.Naziv LIKE @like OR m.Ime LIKE @like)
              AND (@category IS NULL OR p.idKategorija = @category)
              AND (@sellable = 0 OR (p.Zalihe > 0 AND p.DatumIsteka >= @today))";

        internal static Product Map(MySqlDataReader r) => new Product
        {
            Id = r.GetInt32("idProizvod"),
            Name = r.GetString("Naziv"),
            Price = r.GetDecimal("Cijena"),
            Supplies = r.GetDecimal("Zalihe"),
            ExpirationDate = r.GetDateTime("DatumIsteka"),
            BarCode = r.GetString("BarKod"),
            Description = r.Text("Opis"),
            Image = r.Bytes("Slika"),
            IdCategory = r.GetInt32("idKategorija"),
            CategoryName = r.GetString("Kategorija"),
            IdManufacturer = r.GetInt32("idProizvodjac"),
            ManufacturerName = r.GetString("Proizvodjac")
        };

        public Task<PagedResult<Product>> GetPageAsync(ProductQuery query)
        {
            var args = new (string, object?)[]
            {
                ("@like", query.Page.Like),
                ("@category", query.CategoryId),
                ("@sellable", query.SellableOnly ? 1 : 0),
                ("@today", (query.Today ?? DateTime.Today).Date)
            };
            return Db.PageAsync(
                Select + Filter + " ORDER BY p.Naziv, p.idProizvod",
                "SELECT COUNT(*) FROM proizvod p JOIN kategorija k ON k.idKategorija = p.idKategorija JOIN proizvodjac m ON m.idProizvodjac = p.idProizvodjac" + Filter,
                Map, query.Page, args);
        }

        public Task<Product?> GetByIdAsync(int id)
        {
            return Db.SingleAsync(Select + " WHERE p.idProizvod = @id", Map, ("@id", id));
        }

        public Task<Product?> GetByBarCodeAsync(string barCode)
        {
            return Db.SingleAsync(Select + " WHERE p.BarKod = @code", Map, ("@code", barCode.Trim()));
        }

        public async Task<bool> BarCodeExistsAsync(string barCode, int? exceptId = null)
        {
            object? count = await Db.ScalarAsync(
                "SELECT COUNT(*) FROM proizvod WHERE BarKod = @code AND (@except IS NULL OR idProizvod <> @except)",
                ("@code", barCode.Trim()), ("@except", exceptId));
            return Convert.ToInt32(count) > 0;
        }

        public async Task<int> CreateAsync(Product product)
        {
            object? id = await Db.ScalarAsync(@"
                INSERT INTO proizvod (BarKod, Naziv, DatumIsteka, idKategorija, Cijena, Zalihe, idProizvodjac, Opis, Slika)
                VALUES (@code, @name, @expires, @category, @price, @supplies, @manufacturer, @description, @image);
                SELECT LAST_INSERT_ID();", Args(product));
            product.Id = Convert.ToInt32(id);
            return product.Id;
        }

        public Task UpdateAsync(Product product)
        {
            return Db.ExecuteAsync(@"
                UPDATE proizvod SET BarKod = @code, Naziv = @name, DatumIsteka = @expires, idKategorija = @category,
                       Cijena = @price, Zalihe = @supplies, idProizvodjac = @manufacturer, Opis = @description, Slika = @image
                WHERE idProizvod = @id", Args(product));
        }

        public Task DeleteAsync(int id)
        {
            return Db.ExecuteAsync("DELETE FROM proizvod WHERE idProizvod = @id", ("@id", id));
        }

        private static (string, object?)[] Args(Product p) => new (string, object?)[]
        {
            ("@id", p.Id),
            ("@code", p.BarCode.Trim()),
            ("@name", p.Name.Trim()),
            ("@expires", p.ExpirationDate.Date),
            ("@category", p.IdCategory),
            ("@price", p.Price),
            ("@supplies", p.Supplies),
            ("@manufacturer", p.IdManufacturer),
            ("@description", string.IsNullOrWhiteSpace(p.Description) ? null : p.Description.Trim()),
            ("@image", p.Image)
        };
    }
}
