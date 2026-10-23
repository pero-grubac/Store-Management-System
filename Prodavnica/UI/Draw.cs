using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Prodavnica.UI
{
    public static class Draw
    {
        public const TextFormatFlags Line = TextFormatFlags.NoPadding | TextFormatFlags.NoPrefix | TextFormatFlags.SingleLine | TextFormatFlags.EndEllipsis;
        public const TextFormatFlags Center = TextFormatFlags.NoPadding | TextFormatFlags.NoPrefix | TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine;
        public const TextFormatFlags LeftMiddle = Line | TextFormatFlags.VerticalCenter;

        public static GraphicsPath Rounded(RectangleF r, float radius)
        {
            var path = new GraphicsPath();
            float d = Math.Min(radius * 2, Math.Min(r.Width, r.Height));
            if (d <= 0.5f)
            {
                path.AddRectangle(r);
                return path;
            }
            path.AddArc(r.X, r.Y, d, d, 180, 90);
            path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        public static void Fill(Graphics g, Color color, RectangleF r, float radius)
        {
            using var path = Rounded(r, radius);
            using var brush = new SolidBrush(color);
            g.FillPath(brush, path);
        }

        public static void Stroke(Graphics g, Color color, float width, RectangleF r, float radius)
        {
            using var path = Rounded(r, radius);
            using var pen = new Pen(color, width);
            g.DrawPath(pen, path);
        }

        // A soft drop shadow made of a few translucent rounded layers.
        public static void Shadow(Graphics g, RectangleF r, float radius, int depth = 4)
        {
            for (int i = depth; i >= 1; i--)
            {
                var layer = new RectangleF(r.X - i * 0.5f, r.Y + i * 0.8f, r.Width + i, r.Height + i * 0.6f);
                Fill(g, Color.FromArgb(7, 15, 23, 42), layer, radius + i);
            }
        }

        public static void Smooth(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        }

        // Small rounded label such as "In stock". Returns its size so callers can lay out around it.
        public static Size Pill(Graphics g, string text, Font font, Color color, Point at, bool alignRight = false, bool solid = false)
        {
            Size size = TextRenderer.MeasureText(g, text, font, Size.Empty, TextFormatFlags.NoPadding | TextFormatFlags.NoPrefix);
            int padX = Scale(g, 8), padY = Scale(g, 3);
            var r = new Rectangle(at, new Size(size.Width + padX * 2, size.Height + padY * 2));
            if (alignRight) r.X -= r.Width;
            Fill(g, solid ? color : Theme.Mix(color, Color.White, 0.86), r, r.Height / 2f);
            TextRenderer.DrawText(g, text, font, new Point(r.X + padX, r.Y + padY), solid ? Color.White : Theme.Mix(color, Color.Black, 0.18),
                TextFormatFlags.NoPadding | TextFormatFlags.NoPrefix);
            return r.Size;
        }

        public static void Glyph(Graphics g, string glyph, float size, Color color, Rectangle bounds)
        {
            TextRenderer.DrawText(g, glyph, Theme.Icons(size), bounds, color, Center);
        }

        // A circular picture, or initials on a colour derived from the name when there is no picture.
        public static void Avatar(Graphics g, Rectangle r, string name, Image? image)
        {
            Smooth(g);
            if (image != null)
            {
                using var path = new GraphicsPath();
                path.AddEllipse(r);
                Region old = g.Clip;
                g.SetClip(path, CombineMode.Intersect);
                g.DrawImage(image, Cover(image.Size, r));
                g.Clip = old;
                return;
            }
            using (var brush = new SolidBrush(Theme.ColorFor(name)))
            {
                g.FillEllipse(brush, r);
            }
            string initials = Initials(name);
            using var font = new Font(Theme.FontFamilyName, Math.Max(7f, r.Height * 0.36f * 72f / g.DpiY), FontStyle.Bold);
            TextRenderer.DrawText(g, initials, font, r, Color.White, Center);
        }

        public static string Initials(string? name)
        {
            var parts = (name ?? string.Empty).Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return parts.Length switch
            {
                0 => "?",
                1 => parts[0][..Math.Min(2, parts[0].Length)].ToUpperInvariant(),
                _ => $"{char.ToUpperInvariant(parts[0][0])}{char.ToUpperInvariant(parts[1][0])}"
            };
        }

        // The source rectangle drawn into target so the image fills it without distortion.
        public static RectangleF Cover(Size image, RectangleF target)
        {
            float scale = Math.Max(target.Width / image.Width, target.Height / image.Height);
            float w = image.Width * scale, h = image.Height * scale;
            return new RectangleF(target.X + (target.Width - w) / 2, target.Y + (target.Height - h) / 2, w, h);
        }

        public static int Scale(Graphics g, int value) => (int)Math.Round(value * g.DpiX / 96f);
    }

    public static class Images
    {
        public const int MaxSide = 480;

        public static Image? Decode(byte[]? data)
        {
            if (data == null || data.Length == 0) return null;
            try
            {
                using var stream = new MemoryStream(data);
                using var image = Image.FromStream(stream);
                return new Bitmap(image);
            }
            catch (ArgumentException)
            {
                return null;
            }
        }

        // Pictures are stored small: the longest side at most MaxSide pixels, as JPEG.
        public static byte[] Prepare(string path)
        {
            using var source = Image.FromFile(path);
            float scale = Math.Min(1f, MaxSide / (float)Math.Max(source.Width, source.Height));
            int w = Math.Max(1, (int)(source.Width * scale)), h = Math.Max(1, (int)(source.Height * scale));
            using var bitmap = new Bitmap(w, h);
            using (var g = Graphics.FromImage(bitmap))
            {
                Draw.Smooth(g);
                g.Clear(Color.White);
                g.DrawImage(source, 0, 0, w, h);
            }
            using var output = new MemoryStream();
            ImageCodecInfo jpeg = ImageCodecInfo.GetImageEncoders().First(c => c.FormatID == ImageFormat.Jpeg.Guid);
            using var parameters = new EncoderParameters(1);
            parameters.Param[0] = new EncoderParameter(Encoder.Quality, 85L);
            bitmap.Save(output, jpeg, parameters);
            return output.ToArray();
        }
    }

    internal static class Native
    {
        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attribute, ref int value, int size);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, string lParam);

        public const int CsDropShadow = 0x20000;

        // Windows 11 rounded corners for borderless windows (ignored on Windows 10).
        public static void RoundCorners(Form form)
        {
            int round = 2;
            DwmSetWindowAttribute(form.Handle, 33, ref round, sizeof(int));
        }

        // A white title bar with dark text on Windows 11, matching the app.
        public static void LightCaption(Form form)
        {
            int caption = ColorTranslator.ToWin32(Color.White);
            int text = ColorTranslator.ToWin32(Theme.Text);
            DwmSetWindowAttribute(form.Handle, 35, ref caption, sizeof(int));
            DwmSetWindowAttribute(form.Handle, 36, ref text, sizeof(int));
        }

        // Lets the user drag a borderless window by any control.
        public static void DragWindow(Form form)
        {
            ReleaseCapture();
            SendMessage(form.Handle, 0xA1, (IntPtr)2, IntPtr.Zero);
        }

        public static void Placeholder(TextBox box, string text)
        {
            SendMessage(box.Handle, 0x1501, (IntPtr)1, text);
        }
    }
}
