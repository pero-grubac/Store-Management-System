using Prodavnica.Database;
using Prodavnica.Language;
using System.Globalization;

namespace Prodavnica.UI
{
    // Translations and number/date formatting in one place.
    public static class T
    {
        public static string Get(string key) => LanguageHelper.GetString(key) ?? key;

        public static string Format(string key, params object[] args) => LanguageHelper.Format(key, args);

        public static string Money(decimal value) => value.ToString("N2", CultureInfo.CurrentCulture) + " KM";

        public static string Number(decimal value) => value.ToString("0.##", CultureInfo.CurrentCulture);

        public static string Date(DateTime value) => value.ToString("dd.MM.yyyy.", CultureInfo.CurrentCulture);

        public static string DateTime(DateTime value) => value.ToString("dd.MM.yyyy. HH:mm", CultureInfo.CurrentCulture);

        public static string DocumentNumber(int id) => "#" + id.ToString("D6", CultureInfo.InvariantCulture);

        // A message the user can understand for a database failure.
        public static string Error(Exception exception)
        {
            if (exception is StoreDataException data)
            {
                string? text = LanguageHelper.GetString("err." + data.Code);
                if (text != null) return text;
            }
            if (exception is MySqlConnector.MySqlException sql && sql.Number is 0 or 1042 or 2003)
            {
                return Get("err.CONNECTION");
            }
            return Format("err.UNEXPECTED", exception.Message);
        }
    }
}
