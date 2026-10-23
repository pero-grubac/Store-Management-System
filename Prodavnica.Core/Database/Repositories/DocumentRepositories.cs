using MySqlConnector;
using Prodavnica.Database.DTO;
using Prodavnica.Services;
using System.Data;
using System.Text.Json;

namespace Prodavnica.Database.Repositories
{
    public class SalesRepository
    {
        private const string BillSelect = @"
            SELECT r.idRacun, r.DatumKupovine,
                   IFNULL(SUM(s.Kolicina * s.Cijena), 0) AS Ukupno, COUNT(s.idProizvod) AS Stavke,
                   IFNULL(CONCAT(k.Ime, ' ', k.Prezime), '') AS Kasir
            FROM racun r
            LEFT JOIN stavka s ON s.idRacun = r.idRacun
            LEFT JOIN korisnik k ON k.idKorisnik = r.idKorisnik";

        internal static Bill MapBill(MySqlDataReader r) => new Bill
        {
            Id = r.GetInt32("idRacun"),
            Date = r.GetDateTime("DatumKupovine"),
            Total = r.Decimal("Ukupno"),
            ItemCount = Convert.ToInt32(r.GetInt64("Stavke")),
            CashierName = r.GetString("Kasir")
        };

        // Saves the bill through InsertRacunWithStavka, which prices the lines and lowers stock in one transaction.
        public async Task<int> CreateBillAsync(int? userId, IEnumerable<BillItem> lines)
        {
            string json = JsonSerializer.Serialize(lines.Select(l => new { l.IdProduct, l.Amount }));
            try
            {
                await using var connection = await Db.OpenAsync();
                await using var command = new MySqlCommand("InsertRacunWithStavka", connection) { CommandType = CommandType.StoredProcedure };
                command.Parameters.AddWithValue("@idKorisnik_param", (object?)userId ?? DBNull.Value);
                command.Parameters.AddWithValue("@DatumKupovine_param", DateTime.Now);
                command.Parameters.AddWithValue("@stavke_list", json);
                return Convert.ToInt32(await command.ExecuteScalarAsync());
            }
            catch (MySqlException ex)
            {
                throw DataErrors.Translate(ex);
            }
        }

        public Task<PagedResult<Bill>> GetPageAsync(DateTime from, DateTime to, PageRequest page)
        {
            const string filter = @"
                WHERE r.DatumKupovine >= @from AND r.DatumKupovine < @to
                  AND (@like IS NULL OR CAST(r.idRacun AS CHAR) LIKE @like OR CONCAT(k.Ime, ' ', k.Prezime) LIKE @like)";
            (DateTime start, DateTime end) = WholeDays(from, to);
            return Db.PageAsync(
                BillSelect + filter + " GROUP BY r.idRacun, r.DatumKupovine, k.Ime, k.Prezime ORDER BY r.DatumKupovine DESC, r.idRacun DESC",
                "SELECT COUNT(*) FROM racun r LEFT JOIN korisnik k ON k.idKorisnik = r.idKorisnik" + filter,
                MapBill, page, ("@from", start), ("@to", end), ("@like", page.Like));
        }

        public Task<Bill?> GetBillAsync(int id)
        {
            return Db.SingleAsync(BillSelect + " WHERE r.idRacun = @id GROUP BY r.idRacun, r.DatumKupovine, k.Ime, k.Prezime", MapBill, ("@id", id));
        }

        public Task<List<BillItem>> GetItemsAsync(int billId)
        {
            return Db.QueryAsync(@"
                SELECT s.idRacun, s.idProizvod, p.Naziv, p.BarKod, s.Kolicina, s.Cijena
                FROM stavka s JOIN proizvod p ON p.idProizvod = s.idProizvod
                WHERE s.idRacun = @id ORDER BY p.Naziv",
                r => new BillItem
                {
                    IdBill = r.GetInt32("idRacun"),
                    IdProduct = r.GetInt32("idProizvod"),
                    Name = r.GetString("Naziv"),
                    BarCode = r.GetString("BarKod"),
                    Amount = r.GetDecimal("Kolicina"),
                    Price = r.GetDecimal("Cijena")
                },
                ("@id", billId));
        }

        // From the start of the first day to the end of the last one; a reversed range is swapped.
        internal static (DateTime Start, DateTime End) WholeDays(DateTime from, DateTime to)
        {
            DateTime start = from.Date, end = to.Date;
            if (end < start) (start, end) = (end, start);
            return (start, end.AddDays(1));
        }
    }

    public class ProcurementRepository
    {
        private const string Select = @"
            SELECT n.idNabavka, n.`idDobavljač` AS idDobavljac, d.Ime AS Dobavljac, n.DatumKupovine,
                   IFNULL(SUM(s.Kolicina * s.Cijena), 0) AS Ukupno, COUNT(s.idProizvod) AS Stavke,
                   IFNULL(CONCAT(k.Ime, ' ', k.Prezime), '') AS Korisnik
            FROM nabavka n
            JOIN dobavljac d ON d.idDobavljac = n.`idDobavljač`
            LEFT JOIN stavka_nabvke s ON s.idNabavka = n.idNabavka AND s.`idDobavljač` = n.`idDobavljač`
            LEFT JOIN korisnik k ON k.idKorisnik = n.idKorisnik";

        private const string Group = " GROUP BY n.idNabavka, n.`idDobavljač`, d.Ime, n.DatumKupovine, k.Ime, k.Prezime";

        private static Procurement Map(MySqlDataReader r) => new Procurement
        {
            Id = r.GetInt32("idNabavka"),
            IdSupplier = r.GetInt32("idDobavljac"),
            SupplierName = r.GetString("Dobavljac"),
            Date = r.GetDateTime("DatumKupovine"),
            Total = r.Decimal("Ukupno"),
            ItemCount = Convert.ToInt32(r.GetInt64("Stavke")),
            UserName = r.GetString("Korisnik")
        };

        public async Task<int> CreateAsync(int supplierId, int? userId, IEnumerable<ProcurementItem> lines)
        {
            string json = JsonSerializer.Serialize(lines.Select(l => new { l.IdProduct, l.Amount, l.Price }));
            try
            {
                await using var connection = await Db.OpenAsync();
                await using var command = new MySqlCommand("InsertNabavka", connection) { CommandType = CommandType.StoredProcedure };
                command.Parameters.AddWithValue("@idDobavljac_param", supplierId);
                command.Parameters.AddWithValue("@idKorisnik_param", (object?)userId ?? DBNull.Value);
                command.Parameters.AddWithValue("@DatumKupovine_param", DateTime.Now);
                command.Parameters.AddWithValue("@stavke_list", json);
                return Convert.ToInt32(await command.ExecuteScalarAsync());
            }
            catch (MySqlException ex)
            {
                throw DataErrors.Translate(ex);
            }
        }

        // The price paid the last time this product was bought, as a suggestion for the next procurement.
        public async Task<decimal?> GetLastPriceAsync(int productId)
        {
            object? price = await Db.ScalarAsync(@"
                SELECT s.Cijena FROM stavka_nabvke s JOIN nabavka n ON n.idNabavka = s.idNabavka AND n.`idDobavljač` = s.`idDobavljač`
                WHERE s.idProizvod = @id ORDER BY n.DatumKupovine DESC LIMIT 1", ("@id", productId));
            return price == null ? null : Convert.ToDecimal(price);
        }

        public Task<PagedResult<Procurement>> GetPageAsync(DateTime from, DateTime to, PageRequest page)
        {
            const string filter = @"
                WHERE n.DatumKupovine >= @from AND n.DatumKupovine < @to
                  AND (@like IS NULL OR CAST(n.idNabavka AS CHAR) LIKE @like OR d.Ime LIKE @like)";
            (DateTime start, DateTime end) = SalesRepository.WholeDays(from, to);
            return Db.PageAsync(
                Select + filter + Group + " ORDER BY n.DatumKupovine DESC, n.idNabavka DESC",
                "SELECT COUNT(*) FROM nabavka n JOIN dobavljac d ON d.idDobavljac = n.`idDobavljač`" + filter,
                Map, page, ("@from", start), ("@to", end), ("@like", page.Like));
        }

        public Task<Procurement?> GetAsync(int id)
        {
            return Db.SingleAsync(Select + " WHERE n.idNabavka = @id" + Group, Map, ("@id", id));
        }

        public Task<List<ProcurementItem>> GetItemsAsync(int procurementId)
        {
            return Db.QueryAsync(@"
                SELECT s.idNabavka, s.idProizvod, p.Naziv, m.Ime AS Proizvodjac, s.Kolicina, s.Cijena
                FROM stavka_nabvke s
                JOIN proizvod p ON p.idProizvod = s.idProizvod
                JOIN proizvodjac m ON m.idProizvodjac = p.idProizvodjac
                WHERE s.idNabavka = @id ORDER BY p.Naziv",
                r => new ProcurementItem
                {
                    IdProcurement = r.GetInt32("idNabavka"),
                    IdProduct = r.GetInt32("idProizvod"),
                    Name = r.GetString("Naziv"),
                    ManufacturerName = r.GetString("Proizvodjac"),
                    Amount = r.GetDecimal("Kolicina"),
                    Price = r.GetDecimal("Cijena")
                },
                ("@id", procurementId));
        }
    }

    public class DashboardRepository
    {
        public const int ChartDays = 14;
        public const int AttentionLimit = 12;

        public async Task<DashboardData> GetAsync(DateTime now)
        {
            DateTime today = now.Date;
            var stock = await Db.QueryAsync(@"
                SELECT COUNT(*) AS Ukupno,
                       IFNULL(SUM(Zalihe > 0 AND Zalihe < @low), 0) AS Nisko,
                       IFNULL(SUM(Zalihe <= 0), 0) AS Nema,
                       IFNULL(SUM(DatumIsteka < @today), 0) AS Isteklo,
                       IFNULL(SUM(DatumIsteka >= @today AND DatumIsteka <= @soon), 0) AS Uskoro,
                       IFNULL(SUM(GREATEST(Zalihe, 0) * Cijena), 0) AS Vrijednost
                FROM proizvod",
                r => (Total: Convert.ToInt32(r.GetInt64("Ukupno")), Low: Convert.ToInt32(r.GetDecimal("Nisko")), Out: Convert.ToInt32(r.GetDecimal("Nema")),
                      Expired: Convert.ToInt32(r.GetDecimal("Isteklo")), Soon: Convert.ToInt32(r.GetDecimal("Uskoro")), Value: r.GetDecimal("Vrijednost")),
                ("@low", Inventory.LowStockThreshold), ("@today", today), ("@soon", today.AddDays(Inventory.ExpiringSoonDays)));

            DateTime chartStart = today.AddDays(-(ChartDays - 1));
            DateTime monthStart = today.AddDays(-29);
            var perDay = await Db.QueryAsync(@"
                SELECT DATE(r.DatumKupovine) AS Dan, SUM(s.Kolicina * s.Cijena) AS Ukupno
                FROM racun r JOIN stavka s ON s.idRacun = r.idRacun
                WHERE r.DatumKupovine >= @from AND r.DatumKupovine < @to
                GROUP BY DATE(r.DatumKupovine)",
                r => new DailySales(r.GetDateTime("Dan"), r.GetDecimal("Ukupno")),
                ("@from", monthStart), ("@to", today.AddDays(1)));
            int billsToday = Convert.ToInt32(await Db.ScalarAsync(
                "SELECT COUNT(*) FROM racun WHERE DatumKupovine >= @from AND DatumKupovine < @to", ("@from", today), ("@to", today.AddDays(1))));

            var chart = Enumerable.Range(0, ChartDays)
                .Select(i => chartStart.AddDays(i))
                .Select(day => new DailySales(day, perDay.Where(d => d.Day.Date == day).Sum(d => d.Total)))
                .ToList();

            List<Product> attention = await Db.QueryAsync(@"
                SELECT p.idProizvod, p.Naziv, p.Cijena, p.Zalihe, p.DatumIsteka, p.BarKod, p.Opis, p.Slika,
                       p.idKategorija, k.Naziv AS Kategorija, p.idProizvodjac, m.Ime AS Proizvodjac
                FROM proizvod p
                JOIN kategorija k ON k.idKategorija = p.idKategorija
                JOIN proizvodjac m ON m.idProizvodjac = p.idProizvodjac
                WHERE p.Zalihe < @low OR p.DatumIsteka <= @soon
                ORDER BY (p.DatumIsteka < @today) DESC, (p.Zalihe <= 0) DESC, p.DatumIsteka, p.Zalihe
                LIMIT @limit",
                ProductRepository.Map,
                ("@low", Inventory.LowStockThreshold), ("@today", today), ("@soon", today.AddDays(Inventory.ExpiringSoonDays)), ("@limit", AttentionLimit));

            var recent = await new SalesRepository().GetPageAsync(today.AddDays(-30), today, new PageRequest(1, 6));
            var s = stock[0];
            return new DashboardData
            {
                ProductCount = s.Total,
                LowStockCount = s.Low,
                OutOfStockCount = s.Out,
                ExpiredCount = s.Expired,
                ExpiringSoonCount = s.Soon,
                StockValue = s.Value,
                BillsToday = billsToday,
                SalesToday = perDay.Where(d => d.Day.Date == today).Sum(d => d.Total),
                SalesLast30Days = perDay.Sum(d => d.Total),
                DailySales = chart,
                NeedsAttention = attention,
                RecentBills = recent.Items
            };
        }
    }
}
