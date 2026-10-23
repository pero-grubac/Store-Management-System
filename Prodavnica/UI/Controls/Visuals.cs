using Prodavnica.Database.DTO;
using Prodavnica.Services;
using System.ComponentModel;

namespace Prodavnica.UI.Controls
{
    // One product as a card: picture, category, name, manufacturer, price, stock and expiry badges.
    public class ProductCard : UiControl
    {
        private readonly Image? picture;
        private readonly DateTime today;
        private bool selected;

        public ProductCard(Product product, DateTime today)
        {
            Product = product;
            this.today = today;
            picture = Images.Decode(product.Image);
            Text = product.Name; // read by screen readers and UI automation
            Cursor = Cursors.Hand;
            Size = new Size(S(220), S(272));
            Margin = new Padding(S(8));
        }

        public Product Product { get; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Selected { get => selected; set { selected = value; Invalidate(); } }

        protected override void Dispose(bool disposing)
        {
            if (disposing) picture?.Dispose();
            base.Dispose(disposing);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            PaintParentBackground(e);
            Graphics g = e.Graphics;
            Draw.Smooth(g);
            float radius = SF(12);
            var card = new RectangleF(SF(2), SF(1), Width - SF(5), Height - SF(6));
            if (Hovered || selected) Draw.Shadow(g, card, radius, 5);
            Draw.Fill(g, Theme.Surface, card, radius);

            // Picture area, rounded at the top only.
            var photo = new RectangleF(card.X, card.Y, card.Width, SF(124));
            using (var clip = Draw.Rounded(card, radius))
            {
                Region old = g.Clip;
                g.SetClip(clip, System.Drawing.Drawing2D.CombineMode.Intersect);
                if (picture != null)
                {
                    g.DrawImage(picture, Draw.Cover(picture.Size, photo));
                }
                else
                {
                    Color tone = Theme.ColorFor(Product.CategoryName);
                    using (var brush = new SolidBrush(Theme.Mix(tone, Color.White, 0.88)))
                    {
                        g.FillRectangle(brush, photo);
                    }
                    TextRenderer.DrawText(g, Draw.Initials(Product.Name), Theme.Semibold(24f), Rectangle.Round(photo), Theme.Mix(tone, Color.White, 0.25), Draw.Center);
                }
                g.Clip = old;
            }
            Draw.Stroke(g, selected ? Theme.Accent : Hovered ? Theme.BorderStrong : Theme.Border, selected ? SF(2) : 1, card, radius);

            int pad = S(14);
            int x = (int)card.X + pad, width = (int)card.Width - pad * 2;
            ExpiryState expiry = Inventory.GetExpiryState(Product, today);
            if (expiry != ExpiryState.Fresh)
            {
                string badge = T.Get(expiry == ExpiryState.Expired ? "stock.expired" : "stock.expiringSoon");
                Draw.Pill(g, badge, Theme.Semibold(8f), expiry == ExpiryState.Expired ? Theme.Danger : Theme.Warning,
                    new Point(x - S(4), (int)photo.Y + S(10)), solid: true);
            }

            int y = (int)photo.Bottom + S(12);
            TextRenderer.DrawText(g, Product.CategoryName.ToUpperInvariant(), Theme.Semibold(7.75f), new Rectangle(x, y, width, S(16)), Theme.Subtle, Draw.Line);
            y += S(18);
            TextRenderer.DrawText(g, Product.Name, Theme.Semibold(11f), new Rectangle(x, y, width, S(22)), Theme.Text, Draw.Line);
            y += S(23);
            TextRenderer.DrawText(g, Product.ManufacturerName, Theme.Font(9f), new Rectangle(x, y, width, S(18)), Theme.Muted, Draw.Line);
            y += S(18);
            TextRenderer.DrawText(g, T.Format("product.bestBefore", T.Date(Product.ExpirationDate)), Theme.Font(8.75f),
                new Rectangle(x, y, width, S(18)), expiry == ExpiryState.Expired ? Theme.Danger : Theme.Subtle, Draw.Line);

            int bottom = (int)card.Bottom - S(14);
            TextRenderer.DrawText(g, T.Money(Product.Price), Theme.Semibold(13f), new Rectangle(x, bottom - S(26), width, S(26)), Theme.Text, Draw.LeftMiddle);
            StockLevel stock = Inventory.GetStockLevel(Product);
            (string text, Color color) = stock switch
            {
                StockLevel.OutOfStock => (T.Get("stock.out"), Theme.Danger),
                StockLevel.Low => (T.Format("stock.low", T.Number(Product.Supplies)), Theme.Warning),
                _ => (T.Format("stock.in", T.Number(Product.Supplies)), Theme.Success)
            };
            Draw.Pill(g, text, Theme.Semibold(8f), color, new Point(x + width, bottom - S(23)), alignRight: true);
        }
    }

    // Responsive grid of product cards: card width stretches so the columns fill the row.
    public class ProductGrid : FlowLayoutPanel
    {
        private ProductCard? selected;

        public ProductGrid()
        {
            AutoScroll = true;
            BackColor = Theme.Background;
            Padding = new Padding(LogicalToDeviceUnits(2));
            DoubleBuffered = true;
        }

        public event EventHandler<Product>? ItemClicked;
        public event EventHandler<Product>? ItemActivated;

        public ContextMenuStrip? CardMenu { get; set; }

        public Product? SelectedProduct => selected?.Product;

        public string EmptyText { get; set; } = string.Empty;

        public void SetProducts(IEnumerable<Product> products, DateTime today)
        {
            SuspendLayout();
            foreach (Control old in Controls.Cast<Control>().ToList()) old.Dispose();
            Controls.Clear();
            selected = null;
            foreach (Product product in products)
            {
                var card = new ProductCard(product, today) { ContextMenuStrip = CardMenu };
                card.MouseDown += (_, e) => { if (e.Button == MouseButtons.Right) Select(card); };
                card.Click += (_, _) => { Select(card); ItemClicked?.Invoke(this, card.Product); };
                card.DoubleClick += (_, _) => ItemActivated?.Invoke(this, card.Product);
                Controls.Add(card);
            }
            ResumeLayout();
            ResizeCards();
            AutoScrollPosition = Point.Empty;
            Invalidate();
        }

        private void Select(ProductCard card)
        {
            if (selected != null && !selected.IsDisposed) selected.Selected = false;
            selected = card;
            card.Selected = true;
        }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            ResizeCards();
        }

        private void ResizeCards()
        {
            if (Controls.Count == 0) return;
            int margin = LogicalToDeviceUnits(8);
            int available = ClientSize.Width - Padding.Horizontal - SystemInformation.VerticalScrollBarWidth;
            int min = LogicalToDeviceUnits(210);
            int columns = Math.Max(1, available / (min + margin * 2));
            int width = Math.Min(LogicalToDeviceUnits(300), available / columns - margin * 2);
            SuspendLayout();
            foreach (Control card in Controls) card.Width = width;
            ResumeLayout();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (Controls.Count == 0 && !string.IsNullOrEmpty(EmptyText))
            {
                Draw.Glyph(e.Graphics, Glyphs.Package, 24, Theme.Subtle, new Rectangle(0, Height / 2 - LogicalToDeviceUnits(60), Width, LogicalToDeviceUnits(48)));
                TextRenderer.DrawText(e.Graphics, EmptyText, Theme.Font(10.5f), new Rectangle(0, Height / 2, Width, LogicalToDeviceUnits(24)), Theme.Muted, Draw.Center);
            }
        }
    }

    // A headline number with a coloured icon and one line of context.
    public class StatTile : UiControl
    {
        private string title = string.Empty, value = string.Empty, caption = string.Empty, glyph = Glyphs.Info;
        private Color color = Color.Teal;

        public StatTile()
        {
            Height = S(132);
        }

        public void SetContent(string title, string value, string caption, string glyph, Color color)
        {
            (this.title, this.value, this.caption, this.glyph, this.color) = (title, value, caption, glyph, color);
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            PaintParentBackground(e);
            Graphics g = e.Graphics;
            Draw.Smooth(g);
            var r = new RectangleF(0.5f, 0.5f, Width - 1.5f, Height - 1.5f);
            Draw.Fill(g, Theme.Surface, r, SF(12));
            Draw.Stroke(g, Theme.Border, 1, r, SF(12));
            int pad = S(20);
            var icon = new Rectangle(Width - pad - S(40), pad, S(40), S(40));
            Draw.Fill(g, Theme.Mix(color, Color.White, 0.88), icon, SF(10));
            Draw.Glyph(g, glyph, 13, color, icon);
            int width = icon.Left - pad - S(8);
            TextRenderer.DrawText(g, title, Theme.Font(9.5f), new Rectangle(pad, pad, width, S(20)), Theme.Muted, Draw.Line);
            TextRenderer.DrawText(g, value, Theme.Semibold(19f), new Rectangle(pad, pad + S(24), Width - pad * 2, S(40)), Theme.Text, Draw.LeftMiddle);
            TextRenderer.DrawText(g, caption, Theme.Font(9f), new Rectangle(pad, Height - pad - S(18), Width - pad * 2, S(18)), Theme.Subtle, Draw.Line);
        }
    }

    // Daily totals as rounded bars with a value tooltip on hover.
    public class BarChart : UiControl
    {
        private IReadOnlyList<DailySales> data = Array.Empty<DailySales>();
        private int hover = -1;

        public BarChart()
        {
            Height = S(260);
        }

        public void SetData(IReadOnlyList<DailySales> sales)
        {
            data = sales;
            Invalidate();
        }

        private Rectangle Plot => new Rectangle(S(56), S(12), Width - S(64), Height - S(44));

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            int index = data.Count == 0 ? -1 : (int)((e.X - Plot.X) / (Plot.Width / (float)data.Count));
            if (index < 0 || index >= data.Count || !Plot.Contains(e.Location)) index = -1;
            if (index != hover) { hover = index; Invalidate(); }
        }

        protected override void OnMouseLeave(EventArgs e) { hover = -1; base.OnMouseLeave(e); }

        protected override void OnPaint(PaintEventArgs e)
        {
            PaintParentBackground(e);
            Graphics g = e.Graphics;
            Draw.Smooth(g);
            Rectangle plot = Plot;
            if (data.Count == 0) return;
            decimal max = Math.Max(1, data.Max(d => d.Total));
            decimal step = NiceStep(max / 4);
            decimal top = Math.Ceiling(max / step) * step;
            int ticks = (int)(top / step);
            using var gridPen = new Pen(Theme.Border) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dot };
            for (int i = 0; i <= ticks; i++)
            {
                decimal v = step * i;
                int y = plot.Bottom - (int)(plot.Height * (double)(v / top));
                g.DrawLine(gridPen, plot.X, y, plot.Right, y);
                TextRenderer.DrawText(g, v.ToString("N0"), Theme.Font(8.25f), new Rectangle(0, y - S(9), plot.X - S(10), S(18)), Theme.Subtle,
                    TextFormatFlags.Right | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);
            }
            float slot = plot.Width / (float)data.Count;
            float barWidth = Math.Min(slot * 0.56f, SF(34));
            for (int i = 0; i < data.Count; i++)
            {
                float h = (float)(plot.Height * (double)(data[i].Total / top));
                float x = plot.X + slot * i + (slot - barWidth) / 2;
                bool last = i == data.Count - 1;
                Color color = i == hover ? Theme.AccentHover : last ? Theme.Accent : Theme.Mix(Theme.Accent, Color.White, 0.45);
                if (h > 0.5f) Draw.Fill(g, color, new RectangleF(x, plot.Bottom - h, barWidth, h), Math.Min(SF(6), h / 2));
                if (i % 2 == (data.Count - 1) % 2 || data.Count <= 8)
                {
                    TextRenderer.DrawText(g, data[i].Day.ToString("dd.MM."), Theme.Font(8.25f), new Rectangle((int)(plot.X + slot * i), plot.Bottom + S(8), (int)slot, S(18)),
                        last ? Theme.Text : Theme.Subtle, Draw.Center);
                }
            }
            if (hover >= 0)
            {
                string text = $"{T.Date(data[hover].Day)}  ·  {T.Money(data[hover].Total)}";
                Size size = TextRenderer.MeasureText(g, text, Theme.Semibold(9f));
                float cx = plot.X + slot * hover + slot / 2;
                var tip = new RectangleF(Math.Clamp(cx - size.Width / 2f - S(10), 0, Width - size.Width - S(20)), plot.Y, size.Width + S(20), size.Height + S(12));
                Draw.Fill(g, Theme.Text, tip, SF(6));
                TextRenderer.DrawText(g, text, Theme.Semibold(9f), Rectangle.Round(tip), Color.White, Draw.Center);
            }
        }

        private static decimal NiceStep(decimal raw)
        {
            if (raw <= 0) return 1;
            double magnitude = Math.Pow(10, Math.Floor(Math.Log10((double)raw)));
            double residual = (double)raw / magnitude;
            double nice = residual > 5 ? 10 : residual > 2 ? 5 : residual > 1 ? 2 : 1;
            return (decimal)(nice * magnitude);
        }
    }

    // Shows a picture (or a placeholder) with buttons to choose or remove it.
    public class ImagePicker : Panel
    {
        private readonly PictureFrame frame;
        private readonly UiButton choose = new UiButton(string.Empty, Glyphs.Image, ButtonKind.Secondary);
        private readonly UiButton remove = new UiButton(string.Empty, Glyphs.Delete, ButtonKind.Ghost);

        public ImagePicker(bool round, string placeholderName)
        {
            frame = new PictureFrame(round, placeholderName);
            BackColor = Color.Transparent;
            choose.Text = T.Get("image.choose");
            remove.Text = T.Get("image.remove");
            Controls.AddRange(new Control[] { frame, choose, remove });
            choose.Click += (_, _) => Choose();
            remove.Click += (_, _) => { ImageData = null; };
            Size = new Size(LogicalToDeviceUnits(180), LogicalToDeviceUnits(round ? 260 : 250));
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public byte[]? ImageData
        {
            get => frame.Data;
            set { frame.Data = value; remove.Visible = value != null; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string PlaceholderName { get => frame.Name2; set => frame.Name2 = value; }

        private void Choose()
        {
            using var dialog = new OpenFileDialog { Filter = T.Get("image.filter") + "|*.jpg;*.jpeg;*.png;*.bmp;*.gif", Title = T.Get("image.choose") };
            if (dialog.ShowDialog(FindForm()) != DialogResult.OK) return;
            try
            {
                ImageData = Images.Prepare(dialog.FileName);
            }
            catch (Exception ex) when (ex is OutOfMemoryException or ArgumentException or IOException)
            {
                Dialogs.Error(FindForm(), T.Get("image.invalid"));
            }
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            int size = Width;
            frame.SetBounds(0, 0, size, frame.Round ? size : (int)(size * 0.8));
            choose.AutoWidth = false;
            choose.SetBounds(0, frame.Bottom + LogicalToDeviceUnits(12), Width, choose.Height);
            remove.AutoWidth = false;
            remove.SetBounds(0, choose.Bottom + LogicalToDeviceUnits(4), Width, remove.Height);
        }

        private sealed class PictureFrame : UiControl
        {
            private byte[]? data;
            private Image? image;

            public PictureFrame(bool round, string name)
            {
                Round = round;
                Name2 = name;
            }

            public bool Round { get; }

            public string Name2 { get; set; }

            public byte[]? Data
            {
                get => data;
                set { data = value; image?.Dispose(); image = Images.Decode(value); Invalidate(); }
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                PaintParentBackground(e);
                Graphics g = e.Graphics;
                Draw.Smooth(g);
                var r = new Rectangle(0, 0, Width - 1, Height - 1);
                if (Round)
                {
                    Draw.Avatar(g, r, Name2, image);
                    return;
                }
                Draw.Fill(g, Theme.Hover, r, SF(12));
                if (image != null)
                {
                    using var path = Draw.Rounded(r, SF(12));
                    g.SetClip(path);
                    g.DrawImage(image, Draw.Cover(image.Size, r));
                    g.ResetClip();
                }
                else
                {
                    Draw.Glyph(g, Glyphs.Image, 26, Theme.Subtle, r);
                }
                Draw.Stroke(g, Theme.Border, 1, r, SF(12));
            }
        }
    }
}
