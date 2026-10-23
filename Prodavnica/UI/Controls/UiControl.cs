using System.ComponentModel;

namespace Prodavnica.UI.Controls
{
    // Base for custom-drawn controls: double buffered, transparent corners, hover and press tracking.
    public abstract class UiControl : Control
    {
        protected bool Hovered { get; private set; }
        protected bool Pressed { get; private set; }

        protected UiControl()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint |
                     ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
        }

        protected int S(int value) => LogicalToDeviceUnits(value);

        protected float SF(float value) => value * DeviceDpi / 96f;

        protected override void OnMouseEnter(EventArgs e) { Hovered = true; Invalidate(); base.OnMouseEnter(e); }

        protected override void OnMouseLeave(EventArgs e) { Hovered = false; Pressed = false; Invalidate(); base.OnMouseLeave(e); }

        protected override void OnMouseDown(MouseEventArgs e) { if (e.Button == MouseButtons.Left) { Pressed = true; Invalidate(); } base.OnMouseDown(e); }

        protected override void OnMouseUp(MouseEventArgs e) { Pressed = false; Invalidate(); base.OnMouseUp(e); }

        protected override void OnEnabledChanged(EventArgs e) { Invalidate(); base.OnEnabledChanged(e); }

        protected override void OnGotFocus(EventArgs e) { Invalidate(); base.OnGotFocus(e); }

        protected override void OnLostFocus(EventArgs e) { Invalidate(); base.OnLostFocus(e); }

        // Paints whatever is behind the control, so rounded corners blend into any parent.
        protected void PaintParentBackground(PaintEventArgs e)
        {
            if (Parent == null) return;
            using var brush = new SolidBrush(EffectiveBackColor(Parent));
            e.Graphics.FillRectangle(brush, ClientRectangle);
        }

        public static Color EffectiveBackColor(Control control)
        {
            Control? c = control;
            while (c != null && (c.BackColor == Color.Transparent || c.BackColor.A == 0)) c = c.Parent;
            return c?.BackColor ?? Theme.Background;
        }
    }

    public enum ButtonKind
    {
        Primary,
        Secondary,
        Ghost,
        Danger,
        Subtle
    }

    public class UiButton : UiControl, IButtonControl
    {
        private ButtonKind kind = ButtonKind.Secondary;
        private string glyph = string.Empty;
        private bool autoWidth = true;

        public UiButton(string text = "", string glyph = "", ButtonKind kind = ButtonKind.Secondary)
        {
            this.glyph = glyph;
            this.kind = kind;
            Text = text;
            Cursor = Cursors.Hand;
            TabStop = true;
            Height = S(38);
            Font = Theme.Semibold(9.75f);
            UpdateWidth();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ButtonKind Kind { get => kind; set { kind = value; Invalidate(); } }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Glyph { get => glyph; set { glyph = value; UpdateWidth(); Invalidate(); } }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool AutoWidth { get => autoWidth; set { autoWidth = value; UpdateWidth(); } }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DialogResult DialogResult { get; set; }

        public void NotifyDefault(bool value) { }

        public void PerformClick()
        {
            if (Enabled && Visible) OnClick(EventArgs.Empty);
        }

        protected override void OnTextChanged(EventArgs e) { UpdateWidth(); Invalidate(); base.OnTextChanged(e); }

        protected override void OnFontChanged(EventArgs e) { UpdateWidth(); base.OnFontChanged(e); }

        private void UpdateWidth()
        {
            if (!autoWidth) return;
            int text = string.IsNullOrEmpty(Text) ? 0 : TextRenderer.MeasureText(Text, Font).Width;
            int icon = string.IsNullOrEmpty(glyph) ? 0 : S(18) + (text > 0 ? S(8) : 0);
            Width = string.IsNullOrEmpty(Text) ? Height : text + icon + S(32);
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (DialogResult != DialogResult.None && FindForm() is Form form)
            {
                form.DialogResult = DialogResult;
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode is Keys.Enter or Keys.Space) { PerformClick(); e.Handled = true; }
            base.OnKeyDown(e);
        }

        protected override bool IsInputKey(Keys keyData) => keyData is Keys.Enter or Keys.Space || base.IsInputKey(keyData);

        protected override void OnPaint(PaintEventArgs e)
        {
            PaintParentBackground(e);
            Graphics g = e.Graphics;
            Draw.Smooth(g);
            var r = new RectangleF(0.5f, 0.5f, Width - 1.5f, Height - 1.5f);
            float radius = SF(8);

            (Color back, Color fore, Color border) = kind switch
            {
                ButtonKind.Primary => (Pressed ? Theme.Mix(Theme.Accent, Color.Black, 0.18) : Hovered ? Theme.AccentHover : Theme.Accent, Theme.OnAccent, Color.Empty),
                ButtonKind.Danger => (Pressed ? Theme.Mix(Theme.Danger, Color.Black, 0.18) : Hovered ? Theme.Mix(Theme.Danger, Color.Black, 0.08) : Theme.Danger, Color.White, Color.Empty),
                ButtonKind.Ghost => (Pressed ? Theme.Border : Hovered ? Theme.Hover : Color.Empty, Hovered ? Theme.Text : Theme.Muted, Color.Empty),
                ButtonKind.Subtle => (Pressed ? Theme.Mix(Theme.AccentSoft, Theme.Accent, 0.15) : Hovered ? Theme.Mix(Theme.AccentSoft, Theme.Accent, 0.08) : Theme.AccentSoft, Theme.AccentText, Color.Empty),
                _ => (Pressed ? Theme.Border : Hovered ? Theme.Hover : Theme.Surface, Theme.Text, Hovered ? Theme.BorderStrong : Theme.Border)
            };
            if (!Enabled)
            {
                back = back.IsEmpty ? back : Theme.Mix(back, Color.White, 0.55);
                fore = Theme.Mix(fore, Color.White, 0.5);
            }
            if (!back.IsEmpty) Draw.Fill(g, back, r, radius);
            if (!border.IsEmpty) Draw.Stroke(g, border, 1, r, radius);
            if (Focused && ShowFocusCues) Draw.Stroke(g, Theme.Mix(Theme.Accent, Color.White, 0.4), SF(2), RectangleF.Inflate(r, -SF(1.5f), -SF(1.5f)), radius - 1);

            int textWidth = string.IsNullOrEmpty(Text) ? 0 : TextRenderer.MeasureText(g, Text, Font).Width;
            int iconWidth = string.IsNullOrEmpty(glyph) ? 0 : S(18);
            int gap = iconWidth > 0 && textWidth > 0 ? S(8) : 0;
            int x = (Width - textWidth - iconWidth - gap) / 2;
            if (iconWidth > 0)
            {
                Draw.Glyph(g, glyph, 10.5f, fore, new Rectangle(x, 0, iconWidth, Height));
                x += iconWidth + gap;
            }
            if (textWidth > 0)
            {
                TextRenderer.DrawText(g, Text, Font, new Rectangle(x, 0, textWidth + 2, Height), fore, Draw.LeftMiddle);
            }
        }
    }

    // A rounded white panel with a border, optionally titled.
    public class Card : Panel
    {
        private string title = string.Empty;
        private string subtitle = string.Empty;

        public Card()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true);
            BackColor = Theme.Surface;
            Padding = new Padding(LogicalToDeviceUnits(20));
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Title { get => title; set { title = value; UpdatePadding(); Invalidate(); } }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Subtitle { get => subtitle; set { subtitle = value; UpdatePadding(); Invalidate(); } }

        private int HeaderHeight => string.IsNullOrEmpty(title) ? 0 : LogicalToDeviceUnits(string.IsNullOrEmpty(subtitle) ? 36 : 56);

        private void UpdatePadding()
        {
            int p = LogicalToDeviceUnits(20);
            Padding = new Padding(p, p + HeaderHeight, p, p);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            using (var back = new SolidBrush(Parent != null ? UiControl.EffectiveBackColor(Parent) : Theme.Background))
            {
                g.FillRectangle(back, ClientRectangle);
            }
            Draw.Smooth(g);
            var r = new RectangleF(0.5f, 0.5f, Width - 1.5f, Height - 1.5f);
            float radius = LogicalToDeviceUnits(12);
            Draw.Fill(g, Theme.Surface, r, radius);
            Draw.Stroke(g, Theme.Border, 1, r, radius);
            if (!string.IsNullOrEmpty(title))
            {
                int p = LogicalToDeviceUnits(20);
                TextRenderer.DrawText(g, title, Theme.Semibold(11.5f), new Point(p, p), Theme.Text, TextFormatFlags.NoPadding | TextFormatFlags.NoPrefix);
                if (!string.IsNullOrEmpty(subtitle))
                {
                    TextRenderer.DrawText(g, subtitle, Theme.Font(9), new Point(p, p + LogicalToDeviceUnits(24)), Theme.Muted, TextFormatFlags.NoPadding | TextFormatFlags.NoPrefix);
                }
            }
        }
    }

    // Label for headings and helper text, with the theme font.
    public class UiLabel : Label
    {
        public UiLabel(string text = "", float size = 10f, bool semibold = false, Color? color = null)
        {
            Text = text;
            AutoSize = true;
            Font = semibold ? Theme.Semibold(size) : Theme.Font(size);
            ForeColor = color ?? Theme.Text;
            BackColor = Color.Transparent;
            UseMnemonic = false;
        }
    }
}
