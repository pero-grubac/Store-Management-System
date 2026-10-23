using System.Globalization;

namespace Prodavnica.Services
{
    // tema.boja stores a colour as "A,R,G,B", e.g. "255,0,128,128".
    public static class ThemeFormat
    {
        public const string DefaultColor = "255,0,128,128";

        public static (int A, int R, int G, int B)? ParseArgb(string? value)
        {
            if (string.IsNullOrWhiteSpace(value)) return null;

            string[] parts = value.Split(',');
            if (parts.Length != 4) return null;

            var channels = new int[4];
            for (int i = 0; i < 4; i++)
            {
                if (!int.TryParse(parts[i].Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out channels[i])
                    || channels[i] < 0 || channels[i] > 255)
                {
                    return null;
                }
            }

            return (channels[0], channels[1], channels[2], channels[3]);
        }

        public static string FormatArgb(int a, int r, int g, int b)
        {
            return string.Create(CultureInfo.InvariantCulture, $"{a},{r},{g},{b}");
        }
    }
}
