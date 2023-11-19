using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

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
        public static void ChngLang(string abbreviation)
        {
            var cultureInfo = new CultureInfo(abbreviation);
            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;
        }
        public static void ChangeLanguage(string language)
        {
            string abbreviation = GetLangugaeCode(language);
            var cultureInfo = new CultureInfo(abbreviation);
            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;
        }

        private static Dictionary<String, String> languageMap = new Dictionary<String, String>
        {
            {"English","en" },
            {"Srpski(Latinica)","sr" },
            {"Српски(Ћирилица)","sr-Cyrl-RS" }
        };
        private static string GetLangugaeCode(string language)
        {
            if (languageMap.TryGetValue(language, out string languageCode))
            {
                return languageCode;
            }
            return "en";
        }
    }
}
