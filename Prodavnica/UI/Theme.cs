
using Prodavnica.Services;

namespace Prodavnica.UI
{
    // The look of the whole app. The user's colour is the accent; everything else is a calm neutral palette.
    public static class Theme
    {
        private static readonly Dictionary<(string, float, FontStyle), Font> fonts = new();

        public static Color Accent { get; private set; } = Color.FromArgb(0, 128, 128);
        public static string FontFamilyName { get; private set; } = "Segoe UI";
        public static float Scale { get; private set; } = 1f;

        public static readonly Color Background = Color.FromArgb(244, 246, 250);
        public static readonly Color Surface = Color.White;
        public static readonly Color Border = Color.FromArgb(228, 232, 240);
        public static readonly Color BorderStrong = Color.FromArgb(203, 211, 224);
        public static readonly Color Text = Color.FromArgb(17, 24, 39);
        public static readonly Color Muted = Color.FromArgb(100, 112, 133);
        public static readonly Color Subtle = Color.FromArgb(152, 162, 179);
        public static readonly Color Hover = Color.FromArgb(243, 245, 249);
        public static readonly Color Success = Color.FromArgb(22, 163, 74);
        public static readonly Color Warning = Color.FromArgb(217, 119, 6);
        public static readonly Color Danger = Color.FromArgb(220, 38, 38);
        public static readonly Color Info = Color.FromArgb(37, 99, 235);
        public static readonly Color Sidebar = Color.FromArgb(21, 25, 38);
        public static readonly Color SidebarHover = Color.FromArgb(34, 39, 56);
        public static readonly Color SidebarText = Color.FromArgb(176, 184, 201);

        public static Color AccentSoft => Mix(Accent, Color.White, 0.88);
        public static Color AccentHover => Mix(Accent, Color.Black, 0.1);
        public static Color AccentText => Mix(Accent, Color.Black, 0.25);
        public static Color OnAccent => Luminance(Accent) > 0.62 ? Text : Color.White;

        // Swatches offered in Settings.
        public static readonly Color[] Palette =
        {
            Color.FromArgb(0, 128, 128), Color.FromArgb(37, 99, 235), Color.FromArgb(79, 70, 229), Color.FromArgb(124, 58, 237),
            Color.FromArgb(219, 39, 119), Color.FromArgb(220, 38, 38), Color.FromArgb(234, 88, 12), Color.FromArgb(5, 150, 105)
        };

        public static readonly string[] FontFamilies = { "Segoe UI", "Calibri", "Arial", "Verdana", "Tahoma" };

        public static void Apply(Prodavnica.Database.DTO.Theme? theme)
        {
            var argb = ThemeFormat.ParseArgb(theme?.ColorName);
            Accent = argb is { } c ? Color.FromArgb(255, c.R, c.G, c.B) : Color.FromArgb(0, 128, 128);
            string family = theme?.FontName ?? "Segoe UI";
            FontFamilyName = System.Drawing.FontFamily.Families.Any(f => f.Name == family) ? family : "Segoe UI";
            Scale = Math.Clamp((theme?.Size ?? 11) / 11f, 0.85f, 1.25f);
            foreach (Font font in fonts.Values) font.Dispose();
            fonts.Clear();
        }

        public static Font Font(float size, FontStyle style = FontStyle.Regular)
        {
            var key = (FontFamilyName, size, style);
            if (!fonts.TryGetValue(key, out Font? font))
            {
                font = new Font(FontFamilyName, size * Scale, style, GraphicsUnit.Point);
                fonts[key] = font;
            }
            return font;
        }

        public static Font Semibold(float size) => FontWithFamily(FontFamilyName == "Segoe UI" ? "Segoe UI Semibold" : FontFamilyName, size, FontFamilyName == "Segoe UI" ? FontStyle.Regular : FontStyle.Bold);

        public static Font Icons(float size) => FontWithFamily(Glyphs.FontName, size, FontStyle.Regular, scale: false);

        private static Font FontWithFamily(string family, float size, FontStyle style, bool scale = true)
        {
            var key = (family, size, style);
            if (!fonts.TryGetValue(key, out Font? font))
            {
                font = new Font(family, size * (scale ? Scale : 1f), style, GraphicsUnit.Point);
                fonts[key] = font;
            }
            return font;
        }

        public static Color Mix(Color from, Color to, double amount)
        {
            int Channel(int a, int b) => (int)Math.Round(a + (b - a) * amount);
            return Color.FromArgb(255, Channel(from.R, to.R), Channel(from.G, to.G), Channel(from.B, to.B));
        }

        public static double Luminance(Color c) => (0.299 * c.R + 0.587 * c.G + 0.114 * c.B) / 255;

        // A stable, pleasant colour for an avatar or a placeholder, derived from a name.
        public static Color ColorFor(string? text)
        {
            Color[] colors =
            {
                Color.FromArgb(14, 116, 144), Color.FromArgb(37, 99, 235), Color.FromArgb(124, 58, 237), Color.FromArgb(190, 24, 93),
                Color.FromArgb(194, 65, 12), Color.FromArgb(21, 128, 61), Color.FromArgb(71, 85, 105), Color.FromArgb(161, 98, 7)
            };
            int hash = 17;
            foreach (char ch in text ?? string.Empty) hash = unchecked(hash * 31 + ch);
            return colors[Math.Abs(hash % colors.Length)];
        }
    }

    // Segoe MDL2 / Fluent icon code points.
    public static class Glyphs
    {
        public static readonly string FontName =
            System.Drawing.FontFamily.Families.Any(f => f.Name == "Segoe Fluent Icons") ? "Segoe Fluent Icons" : "Segoe MDL2 Assets";

        public const string Home = "";
        public const string Shop = "";
        public const string Package = "";
        public const string Cart = "";
        public const string Import = "";
        public const string Receipt = "";
        public const string People = "";
        public const string Person = "";
        public const string Factory = "";
        public const string Truck = "";
        public const string Tag = "";
        public const string History = "";
        public const string Settings = "";
        public const string SignOut = "";
        public const string Add = "";
        public const string Edit = "";
        public const string Delete = "";
        public const string Search = "";
        public const string Calendar = "";
        public const string Print = "";
        public const string Close = "";
        public const string Minimize = "";
        public const string ChevronLeft = "";
        public const string ChevronRight = "";
        public const string ChevronDown = "";
        public const string Check = "";
        public const string Warning = "";
        public const string Error = "";
        public const string Info = "";
        public const string Lock = "";
        public const string Eye = "";
        public const string Image = "";
        public const string Mail = "";
        public const string Phone = "";
        public const string Money = "";
        public const string Grid = "";
        public const string List = "";
        public const string Barcode = "";
        public const string Minus = "";
        public const string Clock = "";
        public const string Chart = "";
        public const string Shield = "";
        public const string Palette = "";
        public const string Globe = "";
        public const string Font = "";
    }
}
