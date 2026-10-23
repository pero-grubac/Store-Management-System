namespace Prodavnica.Services
{
    // Maps jezik.Naziv to the culture of the matching .resx file.
    public static class LanguageCodes
    {
        public const string Default = "en";

        private static readonly Dictionary<string, string> codes = new Dictionary<string, string>
        {
            { "English", "en" },
            { "Srpski(Latinica)", "sr-Latn-RS" },
            { "Српски(Ћирилица)", "sr-Cyrl-RS" }
        };

        public static string FromName(string? languageName)
        {
            return languageName is not null && codes.TryGetValue(languageName, out string? code) ? code : Default;
        }
    }
}
