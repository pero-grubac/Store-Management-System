using Prodavnica.Services;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace Prodavnica.Language
{
    public static class LanguageHelper
    {
        private static ResourceManager manager;

        static LanguageHelper()
        {
            manager = new ResourceManager("Prodavnica.Language.string", Assembly.GetExecutingAssembly());
        }
        public static string? GetString(string name)
        {
            return manager.GetString(name);
        }
        public static string Format(string name, params object[] args)
        {
            return string.Format(CultureInfo.CurrentCulture, manager.GetString(name) ?? name, args);
        }
        public static void ChngLang(string abbreviation)
        {
            var cultureInfo = new CultureInfo(abbreviation);
            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;
        }
        public static void ChangeLanguage(string language)
        {
            ChngLang(LanguageCodes.FromName(language));
        }
    }
}
