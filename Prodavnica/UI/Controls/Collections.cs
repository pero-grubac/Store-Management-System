using System.ComponentModel;

namespace Prodavnica.UI.Controls
{
    // Pill-shaped switch between a few options (views, date ranges, roles, languages).
    public class Segmented : UiControl
    {
        private readonly List<(string Text, string Glyph)> segments = new();
        private int selected;
        private int hover = -1;

        public Segmented(params (string Text, string Glyph)[] options)
        {
            segments.AddRange(options);
            Text = string.Join(" / ", options.Select(o => o.Text)); // read by screen readers and UI automation
            Height = S(38);
            Font = Theme.Font(9.75f);
            Cursor = Cursors.Hand;
            Width = segments.Sum(SegmentWidth) + S(8);
        }

        public event EventHandler? SelectedIndexChanged;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectedIndex
        {
            get => selected;
            set
            {
                if (selected == value) return;
                selected = value;
                Invalidate();
                SelectedIndexChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private int SegmentWidth((string Text, string Glyph) s)
        {
            int text = string.IsNullOrEmpty(s.Text) ? 0 : TextRenderer.MeasureText(s.Text, Theme.Semibold(9.75f)).Width;
            int icon = string.IsNullOrEmpty(s.Glyph) ? 0 : S(18) + (text > 0 ? S(6) : 0);
            return text + icon + S(text > 0 ? 28 : 22);
        }

        private Rectangle SegmentBounds(int index)
        {
            int x = S(4);
            for (int i = 0; i < index; i++) x += SegmentWidth(segments[i]);
            return new Rectangle(x, S(4), SegmentWidth(segments[index]), Height - S(8));
        }

        private int HitTest(Point p)
        {
            for (int i = 0; i < segments.Count; i++) if (SegmentBounds(i).Contains(p)) return i;
            return -1;
        }

        protected override void OnMouseMove(MouseEventArgs e) { int h = HitTest(e.Location); if (h != hover) { hover = h; Invalidate(); } base.OnMouseMove(e); }

        protected override void OnMouseLeave(EventArgs e) { hover = -1; base.OnMouseLeave(e); }

        protected override void OnMouseClick(MouseEventArgs e) { int h = HitTest(e.Location); if (h >= 0) SelectedIndex = h; base.OnMouseClick(e); }

        protected override void OnPaint(PaintEventArgs e)
        {
            PaintParentBackground(e);
            Graphics g = e.Graphics;
            Draw.Smooth(g);
            Draw.Fill(g, Theme.Mix(Theme.Hover, Theme.Border, 0.4), new RectangleF(0, 0, Width - 1, Height - 1), SF(10));
            for (int i = 0; i < segments.Count; i++)
            {
                Rectangle r = SegmentBounds(i);
                bool on = i == selected;
                if (on)
                {
                    Draw.Shadow(g, r, SF(7), 2);
                    Draw.Fill(g, Theme.Surface, r, SF(7));
                }
                else if (i == hover)
                {
                    Draw.Fill(g, Color.FromArgb(120, Theme.Surface), r, SF(7));
                }
                Color fore = on ? Theme.Text : Theme.Muted;
                Font font = on ? Theme.Semibold(9.75f) : Theme.Font(9.75f);
                var (text, glyph) = segments[i];
                int textWidth = string.IsNullOrEmpty(text) ? 0 : TextRenderer.MeasureText(g, text, font).Width;
                int iconWidth = string.IsNullOrEmpty(glyph) ? 0 : S(18);
                int gap = iconWidth > 0 && textWidth > 0 ? S(6) : 0;
                int x = r.X + (r.Width - textWidth - iconWidth - gap) / 2;
                if (iconWidth > 0) { Draw.Glyph(g, glyph, 10f, on ? Theme.Accent : Theme.Muted, new Rectangle(x, r.Y, iconWidth, r.Height)); x += iconWidth + gap; }
                if (textWidth > 0) TextRenderer.DrawText(g, text, font, new Rectangle(x, r.Y, textWidth + 2, r.Height), fore, Draw.LeftMiddle);
            }
        }
    }

    // "Showing 21–40 of 243" with previous/next buttons.
    public class Pager : Panel
    {
        private readonly UiLabel summary = new UiLabel(string.Empty, 9.5f, color: Theme.Muted);
        private readonly UiLabel position = new UiLabel(string.Empty, 9.5f, semibold: true);
        private readonly UiButton previous = new UiButton(string.Empty, Glyphs.ChevronLeft);
        private readonly UiButton next = new UiButton(string.Empty, Glyphs.ChevronRight);

        public Pager()
        {
            Height = LogicalToDeviceUnits(52);
            BackColor = Color.Transparent;
            Controls.AddRange(new Control[] { summary, position, previous, next });
            previous.Click += (_, _) => PageRequested?.Invoke(this, Page - 1);
            next.Click += (_, _) => PageRequested?.Invoke(this, Page + 1);
        }

        public event EventHandler<int>? PageRequested;

        public int Page { get; private set; } = 1;

        public void Show<TItem>(Database.PagedResult<TItem> result)
        {
            Page = result.Page;
            summary.Text = result.TotalCount == 0 ? T.Get("pager.empty") : T.Format("pager.summary", result.FirstIndex, result.LastIndex, result.TotalCount);
            position.Text = T.Format("pager.position", result.Page, result.PageCount);
            previous.Enabled = result.HasPrevious;
            next.Enabled = result.HasNext;
            PerformLayout();
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            int y = (Height - previous.Height) / 2;
            next.Location = new Point(Width - next.Width, y);
            previous.Location = new Point(next.Left - previous.Width - LogicalToDeviceUnits(8), y);
            position.Location = new Point(previous.Left - position.PreferredWidth - LogicalToDeviceUnits(14), (Height - position.PreferredHeight) / 2);
            summary.Location = new Point(0, (Height - summary.PreferredHeight) / 2);
        }
    }

    // A list styled as a modern table: no grid lines, roomy rows, hover highlight, custom-painted cells.
    public class DataTable : DataGridView
    {
        private int hoverRow = -1;
        private readonly Dictionary<string, Action<Graphics, Rectangle, object, bool>> painters = new();
        private readonly Dictionary<object, Image?> imageCache = new();

        public DataTable()
        {
            DoubleBuffered = true;
            BorderStyle = BorderStyle.None;
            BackgroundColor = Theme.Surface;
            GridColor = Theme.Border;
            CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            EnableHeadersVisualStyles = false;
            RowHeadersVisible = false;
            SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            MultiSelect = false;
            ReadOnly = true;
            AllowUserToAddRows = false;
            AllowUserToDeleteRows = false;
            AllowUserToResizeRows = false;
            AllowUserToResizeColumns = false;
            AllowUserToOrderColumns = false;
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            ColumnHeadersHeight = LogicalToDeviceUnits(44);
            RowTemplate.Height = LogicalToDeviceUnits(52);
            StandardTab = true;
            ShowCellToolTips = false;

            ColumnHeadersDefaultCellStyle.BackColor = Theme.Surface;
            ColumnHeadersDefaultCellStyle.ForeColor = Theme.Muted;
            ColumnHeadersDefaultCellStyle.SelectionBackColor = Theme.Surface;
            ColumnHeadersDefaultCellStyle.SelectionForeColor = Theme.Muted;
            ColumnHeadersDefaultCellStyle.Font = Theme.Semibold(9f);
            ColumnHeadersDefaultCellStyle.Padding = new Padding(LogicalToDeviceUnits(12), 0, LogicalToDeviceUnits(12), 0);
            DefaultCellStyle.BackColor = Color.Transparent;
            DefaultCellStyle.ForeColor = Theme.Text;
            DefaultCellStyle.Font = Theme.Font(10f);
            DefaultCellStyle.SelectionBackColor = Color.Transparent;
            DefaultCellStyle.SelectionForeColor = Theme.Text;
            DefaultCellStyle.Padding = new Padding(LogicalToDeviceUnits(12), 0, LogicalToDeviceUnits(12), 0);
        }

        public event EventHandler<object>? ItemActivated;

        public string EmptyText { get; set; } = string.Empty;

        public object? SelectedItem => CurrentRow?.Tag;

        public DataGridViewColumn AddText(string header, float weight = 1f, bool right = false)
        {
            var column = new DataGridViewTextBoxColumn
            {
                HeaderText = header,
                FillWeight = weight * 100,
                SortMode = DataGridViewColumnSortMode.NotSortable
            };
            if (right)
            {
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            Columns.Add(column);
            return column;
        }

        // A column painted by the caller, e.g. an avatar with two lines of text or a status pill.
        public DataGridViewColumn AddCustom(string header, float weight, Action<Graphics, Rectangle, object, bool> paint)
        {
            DataGridViewColumn column = AddText(header, weight);
            column.Name = "custom" + Columns.Count;
            painters[column.Name] = paint;
            return column;
        }

        public void SetRows<TItem>(IEnumerable<TItem> items, Func<TItem, object?[]> cells) where TItem : notnull
        {
            foreach (Image? image in imageCache.Values) image?.Dispose();
            imageCache.Clear();
            SuspendLayout();
            Rows.Clear();
            foreach (TItem item in items)
            {
                int index = Rows.Add(cells(item) as object[]);
                Rows[index].Tag = item;
            }
            ResumeLayout();
            ClearSelection();
            Invalidate();
        }

        // Decoded pictures are kept per row item so repainting does not decode them again.
        public Image? CachedImage(object item, byte[]? data)
        {
            if (!imageCache.TryGetValue(item, out Image? image))
            {
                image = Images.Decode(data);
                imageCache[item] = image;
            }
            return image;
        }

        protected override void OnCellMouseEnter(DataGridViewCellEventArgs e)
        {
            base.OnCellMouseEnter(e);
            if (e.RowIndex != hoverRow) { int old = hoverRow; hoverRow = e.RowIndex; InvalidateRowSafe(old); InvalidateRowSafe(hoverRow); }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            int old = hoverRow; hoverRow = -1; InvalidateRowSafe(old);
        }

        private void InvalidateRowSafe(int row)
        {
            if (row >= 0 && row < Rows.Count) InvalidateRow(row);
        }

        protected override void OnCellMouseDown(DataGridViewCellMouseEventArgs e)
        {
            // Right-click selects the row first, so the context menu acts on it.
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0) CurrentCell = Rows[e.RowIndex].Cells[0];
            base.OnCellMouseDown(e);
        }

        protected override void OnCellDoubleClick(DataGridViewCellEventArgs e)
        {
            base.OnCellDoubleClick(e);
            if (e.RowIndex >= 0 && Rows[e.RowIndex].Tag is { } item) ItemActivated?.Invoke(this, item);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && SelectedItem is { } item)
            {
                ItemActivated?.Invoke(this, item);
                e.Handled = true;
                return;
            }
            base.OnKeyDown(e);
        }

        private Color RowBackground(int rowIndex)
        {
            if (Rows[rowIndex].Selected) return Theme.AccentSoft;
            return rowIndex == hoverRow ? Theme.Hover : Theme.Surface;
        }

        // The row background (hover/selection) is painted here; cells only paint their content.
        protected override void OnRowPrePaint(DataGridViewRowPrePaintEventArgs e)
        {
            base.OnRowPrePaint(e);
            using (var brush = new SolidBrush(RowBackground(e.RowIndex)))
            {
                e.Graphics.FillRectangle(brush, e.RowBounds);
            }
            e.PaintParts &= ~(DataGridViewPaintParts.Background | DataGridViewPaintParts.SelectionBackground | DataGridViewPaintParts.Focus);
        }

        protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
        {
            base.OnCellPainting(e);
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || e.Graphics == null) return;
            string name = Columns[e.ColumnIndex].Name;
            if (!painters.TryGetValue(name, out var paint) || Rows[e.RowIndex].Tag is not { } item) return;
            using (var back = new SolidBrush(RowBackground(e.RowIndex)))
            {
                e.Graphics.FillRectangle(back, e.CellBounds);
            }
            e.Paint(e.CellBounds, DataGridViewPaintParts.Border);
            Draw.Smooth(e.Graphics);
            var r = Rectangle.Inflate(e.CellBounds, -LogicalToDeviceUnits(12), 0);
            paint(e.Graphics, r, item, Rows[e.RowIndex].Selected);
            e.Handled = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (Rows.Count == 0 && !string.IsNullOrEmpty(EmptyText))
            {
                var r = new Rectangle(0, ColumnHeadersHeight, Width, Math.Max(LogicalToDeviceUnits(120), Height - ColumnHeadersHeight));
                Draw.Glyph(e.Graphics, Glyphs.Search, 20, Theme.Subtle, new Rectangle(r.X, r.Y + r.Height / 2 - LogicalToDeviceUnits(46), r.Width, LogicalToDeviceUnits(40)));
                TextRenderer.DrawText(e.Graphics, EmptyText, Theme.Font(10.5f), new Rectangle(r.X, r.Y + r.Height / 2, r.Width, LogicalToDeviceUnits(24)), Theme.Muted, Draw.Center);
            }
        }

        // Two lines of text next to an avatar or thumbnail.
        public static void PaintIdentity(Graphics g, Rectangle r, string title, string subtitle, Image? picture, bool round, Control owner)
        {
            int size = owner.LogicalToDeviceUnits(34);
            var pic = new Rectangle(r.X, r.Y + (r.Height - size) / 2, size, size);
            if (round)
            {
                Draw.Avatar(g, pic, title, picture);
            }
            else if (picture != null)
            {
                using var path = Draw.Rounded(pic, owner.LogicalToDeviceUnits(6));
                Region old = g.Clip;
                g.SetClip(path, System.Drawing.Drawing2D.CombineMode.Intersect);
                g.DrawImage(picture, Draw.Cover(picture.Size, pic));
                g.Clip = old;
            }
            else
            {
                Draw.Fill(g, Theme.Mix(Theme.ColorFor(title), Color.White, 0.85f), pic, owner.LogicalToDeviceUnits(6));
                TextRenderer.DrawText(g, Draw.Initials(title), Theme.Semibold(9f), pic, Theme.ColorFor(title), Draw.Center);
            }
            int x = pic.Right + owner.LogicalToDeviceUnits(12);
            int width = r.Right - x;
            bool two = !string.IsNullOrEmpty(subtitle);
            int lineHeight = owner.LogicalToDeviceUnits(19);
            int y = r.Y + (r.Height - (two ? lineHeight * 2 : lineHeight)) / 2;
            TextRenderer.DrawText(g, title, Theme.Semibold(10f), new Rectangle(x, y, width, lineHeight), Theme.Text, Draw.LeftMiddle);
            if (two) TextRenderer.DrawText(g, subtitle, Theme.Font(9f), new Rectangle(x, y + lineHeight, width, lineHeight), Theme.Muted, Draw.LeftMiddle);
        }

        public static void PaintPill(Graphics g, Rectangle r, string text, Color color, bool right = false)
        {
            Size size = TextRenderer.MeasureText(g, text, Theme.Semibold(8.5f), Size.Empty, TextFormatFlags.NoPadding);
            int h = size.Height + Draw.Scale(g, 6);
            Draw.Pill(g, text, Theme.Semibold(8.5f), color, new Point(right ? r.Right : r.X, r.Y + (r.Height - h) / 2), right);
        }
    }
}
