using System.Diagnostics.CodeAnalysis;
using System.ComponentModel;
using System.Globalization;

namespace Prodavnica.UI.Controls
{
    // A rounded text field: optional leading icon, placeholder, password eye and an error state.
    public class UiInput : UiControl
    {
        private readonly TextBox box = new TextBox();
        private readonly string glyph;
        private bool hasError;
        private bool passwordVisible;
        private readonly bool password;
        private readonly bool multiline;

        public UiInput(string placeholder = "", string glyph = "", bool password = false, bool multiline = false)
        {
            this.glyph = glyph;
            this.password = password;
            this.multiline = multiline;
            Cursor = Cursors.IBeam;
            box.BorderStyle = BorderStyle.None;
            box.BackColor = Theme.Surface;
            box.ForeColor = Theme.Text;
            box.Font = Theme.Font(10.5f);
            box.Multiline = multiline;
            box.UseSystemPasswordChar = password;
            if (multiline) box.ScrollBars = ScrollBars.Vertical;
            box.TextChanged += (_, _) => { HasError = false; OnTextChanged(EventArgs.Empty); };
            box.GotFocus += (_, _) => Invalidate();
            box.LostFocus += (_, _) => Invalidate();
            box.KeyDown += (_, e) => OnKeyDown(e);
            box.HandleCreated += (_, _) => { if (!multiline && placeholder.Length > 0) Native.Placeholder(box, placeholder); };
            Controls.Add(box);
            Height = multiline ? S(96) : S(40);
            Width = S(260);
        }

        public TextBox Box => box;

        [AllowNull, DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string Text { get => box.Text; set => box.Text = value ?? string.Empty; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool ReadOnly { get => box.ReadOnly; set { box.ReadOnly = value; Invalidate(); } }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool HasError { get => hasError; set { if (hasError != value) { hasError = value; Invalidate(); } } }

        private int IconWidth => string.IsNullOrEmpty(glyph) ? 0 : S(34);
        private int EyeWidth => password ? S(34) : 0;

        public void FocusInput() => box.Focus();

        public bool TryGetDecimal(out decimal value)
        {
            return decimal.TryParse(box.Text.Trim(), NumberStyles.Number, CultureInfo.CurrentCulture, out value);
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            base.OnLayout(e);
            int left = IconWidth > 0 ? IconWidth : S(12);
            int right = EyeWidth > 0 ? EyeWidth : S(12);
            if (multiline)
            {
                box.SetBounds(left, S(10), Width - left - right, Height - S(20));
            }
            else
            {
                int h = box.PreferredHeight;
                box.SetBounds(left, (Height - h) / 2, Width - left - right, h);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (password && e.X > Width - EyeWidth)
            {
                passwordVisible = !passwordVisible;
                box.UseSystemPasswordChar = !passwordVisible;
                Invalidate();
            }
            box.Focus();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            Cursor = password && e.X > Width - EyeWidth ? Cursors.Hand : Cursors.IBeam;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            PaintParentBackground(e);
            Graphics g = e.Graphics;
            Draw.Smooth(g);
            var r = new RectangleF(0.5f, 0.5f, Width - 1.5f, Height - 1.5f);
            float radius = SF(8);
            bool focused = box.Focused;
            Color back = box.ReadOnly ? Theme.Hover : Theme.Surface;
            box.BackColor = back;
            if (focused) Draw.Fill(g, Theme.Mix(hasError ? Theme.Danger : Theme.Accent, Color.White, 0.82), RectangleF.Inflate(r, 0, 0), radius);
            var inner = focused ? RectangleF.Inflate(r, -SF(2.5f), -SF(2.5f)) : r;
            Draw.Fill(g, back, inner, radius - (focused ? 2 : 0));
            Color border = hasError ? Theme.Danger : focused ? Theme.Accent : Hovered ? Theme.Subtle : Theme.BorderStrong;
            Draw.Stroke(g, border, focused ? SF(1.5f) : 1, inner, radius - (focused ? 2 : 0));
            if (IconWidth > 0)
            {
                Draw.Glyph(g, glyph, 10.5f, focused ? Theme.Accent : Theme.Subtle, new Rectangle(0, multiline ? S(4) : 0, IconWidth, multiline ? S(30) : Height));
            }
            if (password)
            {
                Draw.Glyph(g, Glyphs.Eye, 10.5f, passwordVisible ? Theme.Accent : Theme.Subtle, new Rectangle(Width - EyeWidth, 0, EyeWidth, Height));
            }
        }
    }

    // A dropdown drawn like the text fields; the list opens in a styled popup.
    public class UiSelect : UiControl
    {
        private readonly List<(string Text, object? Value)> items = new();
        private int selectedIndex = -1;
        private bool hasError;
        private readonly string placeholder;

        public UiSelect(string placeholder = "")
        {
            this.placeholder = placeholder;
            Cursor = Cursors.Hand;
            TabStop = true;
            Height = S(40);
            Width = S(220);
            Font = Theme.Font(10.5f);
        }

        public event EventHandler? SelectionChanged;

        public int SelectedIndex
        {
            get => selectedIndex;
            set
            {
                if (selectedIndex == value) return;
                selectedIndex = value;
                HasError = false;
                Invalidate();
                SelectionChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public object? SelectedValue => selectedIndex >= 0 && selectedIndex < items.Count ? items[selectedIndex].Value : null;

        public string SelectedText => selectedIndex >= 0 && selectedIndex < items.Count ? items[selectedIndex].Text : string.Empty;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool HasError { get => hasError; set { hasError = value; Invalidate(); } }

        public void SetItems(IEnumerable<(string Text, object? Value)> list, object? select = null)
        {
            items.Clear();
            items.AddRange(list);
            selectedIndex = items.FindIndex(i => Equals(i.Value, select));
            Invalidate();
        }

        public void SelectValue(object? value) => SelectedIndex = items.FindIndex(i => Equals(i.Value, value));

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            Focus();
            OpenList();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode is Keys.Enter or Keys.Space or Keys.F4) OpenList();
            else if (e.KeyCode == Keys.Down && selectedIndex < items.Count - 1) SelectedIndex++;
            else if (e.KeyCode == Keys.Up && selectedIndex > 0) SelectedIndex--;
            base.OnKeyDown(e);
        }

        protected override bool IsInputKey(Keys keyData) => keyData is Keys.Up or Keys.Down || base.IsInputKey(keyData);

        private void OpenList()
        {
            var menu = new ContextMenuStrip
            {
                Renderer = new MenuRenderer(),
                ShowImageMargin = false,
                Font = Theme.Font(10.5f),
                Padding = new Padding(S(4)),
                MaximumSize = new Size(Math.Max(Width, S(200)), S(360))
            };
            for (int i = 0; i < items.Count; i++)
            {
                int index = i;
                var item = new ToolStripMenuItem(items[i].Text)
                {
                    Checked = i == selectedIndex,
                    AutoSize = false,
                    Size = new Size(Math.Max(Width, S(200)) - S(10), S(34)),
                    Padding = new Padding(S(6), 0, 0, 0)
                };
                item.Click += (_, _) => SelectedIndex = index;
                menu.Items.Add(item);
            }
            menu.Closed += (_, _) => BeginInvoke(menu.Dispose);
            menu.Show(this, new Point(0, Height + S(4)));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            PaintParentBackground(e);
            Graphics g = e.Graphics;
            Draw.Smooth(g);
            var r = new RectangleF(0.5f, 0.5f, Width - 1.5f, Height - 1.5f);
            float radius = SF(8);
            Draw.Fill(g, Enabled ? Theme.Surface : Theme.Hover, r, radius);
            Color border = hasError ? Theme.Danger : Focused ? Theme.Accent : Hovered ? Theme.Subtle : Theme.BorderStrong;
            Draw.Stroke(g, border, Focused ? SF(1.5f) : 1, r, radius);
            bool empty = selectedIndex < 0;
            TextRenderer.DrawText(g, empty ? placeholder : SelectedText, Font, new Rectangle(S(12), 0, Width - S(44), Height),
                empty ? Theme.Subtle : Theme.Text, Draw.LeftMiddle);
            Draw.Glyph(g, Glyphs.ChevronDown, 8.5f, Theme.Muted, new Rectangle(Width - S(36), 0, S(28), Height));
        }
    }

    // A date field; the calendar opens in a popup.
    public class UiDate : UiControl
    {
        private DateTime value = DateTime.Today;

        public UiDate()
        {
            Cursor = Cursors.Hand;
            TabStop = true;
            Height = S(40);
            Width = S(170);
            Font = Theme.Font(10.5f);
        }

        public event EventHandler? ValueChanged;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DateTime Value
        {
            get => value;
            set
            {
                if (this.value == value.Date) return;
                this.value = value.Date;
                Invalidate();
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            var calendar = new MonthCalendar { MaxSelectionCount = 1, SelectionStart = value, ShowTodayCircle = true, Font = Theme.Font(10) };
            var host = new ToolStripControlHost(calendar) { Margin = Padding.Empty, Padding = Padding.Empty };
            var drop = new ToolStripDropDown { Padding = new Padding(S(4)), Renderer = new MenuRenderer() };
            drop.Items.Add(host);
            calendar.DateSelected += (_, args) => { Value = args.Start; drop.Close(); };
            drop.Closed += (_, _) => BeginInvoke(drop.Dispose);
            drop.Show(this, new Point(0, Height + S(4)));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            PaintParentBackground(e);
            Graphics g = e.Graphics;
            Draw.Smooth(g);
            var r = new RectangleF(0.5f, 0.5f, Width - 1.5f, Height - 1.5f);
            Draw.Fill(g, Theme.Surface, r, SF(8));
            Draw.Stroke(g, Hovered ? Theme.Subtle : Theme.BorderStrong, 1, r, SF(8));
            Draw.Glyph(g, Glyphs.Calendar, 10.5f, Theme.Subtle, new Rectangle(0, 0, S(36), Height));
            TextRenderer.DrawText(g, T.Date(value), Font, new Rectangle(S(36), 0, Width - S(40), Height), Theme.Text, Draw.LeftMiddle);
        }
    }

    // Light menus with rounded hover items, used by dropdowns and context menus.
    public class MenuRenderer : ToolStripProfessionalRenderer
    {
        public MenuRenderer() : base(new MenuColors()) { RoundedEdges = false; }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            if (!e.Item.Selected && !(e.Item is ToolStripMenuItem { Checked: true })) return;
            Draw.Smooth(e.Graphics);
            var r = new RectangleF(2, 1, e.Item.Width - 4, e.Item.Height - 2);
            Draw.Fill(e.Graphics, e.Item.Selected ? Theme.Hover : Theme.AccentSoft, r, 6);
        }

        protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e) { }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            e.TextColor = e.Item is ToolStripMenuItem { Checked: true } ? Theme.AccentText : e.Item.ForeColor == Theme.Danger ? Theme.Danger : Theme.Text;
            base.OnRenderItemText(e);
        }

        private sealed class MenuColors : ProfessionalColorTable
        {
            public override Color ToolStripDropDownBackground => Theme.Surface;
            public override Color MenuBorder => Theme.Border;
            public override Color ImageMarginGradientBegin => Theme.Surface;
            public override Color ImageMarginGradientMiddle => Theme.Surface;
            public override Color ImageMarginGradientEnd => Theme.Surface;
            public override Color MenuItemBorder => Color.Transparent;
            public override Color MenuItemSelected => Theme.Hover;
            public override Color SeparatorDark => Theme.Border;
            public override Color SeparatorLight => Theme.Surface;
        }
    }

    // A labelled form field: caption above, input, and a short error message below.
    public class Field : Panel
    {
        private readonly UiLabel caption;
        private readonly UiLabel error;

        public Field(string label, Control input, string? hint = null)
        {
            Input = input;
            caption = new UiLabel(label, 9.25f, semibold: true, Theme.Muted);
            error = new UiLabel(hint ?? string.Empty, 8.75f, color: hint == null ? Theme.Danger : Theme.Subtle);
            Hint = hint;
            BackColor = Color.Transparent;
            Controls.Add(caption);
            Controls.Add(input);
            Controls.Add(error);
            Height = LogicalToDeviceUnits(22) + input.Height + LogicalToDeviceUnits(24);
        }

        public Control Input { get; }

        private string? Hint { get; }

        public void SetError(string? message)
        {
            error.Text = message ?? Hint ?? string.Empty;
            error.ForeColor = message != null ? Theme.Danger : Theme.Subtle;
            switch (Input)
            {
                case UiInput input: input.HasError = message != null; break;
                case UiSelect select: select.HasError = message != null; break;
            }
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            caption.Location = new Point(0, 0);
            Input.SetBounds(0, LogicalToDeviceUnits(22), Width, Input.Height);
            error.Location = new Point(0, Input.Bottom + LogicalToDeviceUnits(4));
        }
    }

    // Lays out fields in equal columns; a field can span several columns.
    public class FieldGrid : Panel
    {
        private readonly int columns;
        private readonly Dictionary<Control, int> spans = new();

        public FieldGrid(int columns)
        {
            this.columns = columns;
            BackColor = Color.Transparent;
        }

        public T Add<T>(T control, int span = 1) where T : Control
        {
            spans[control] = Math.Min(span, columns);
            Controls.Add(control);
            PerformLayout();
            return control;
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            int gap = LogicalToDeviceUnits(16);
            float columnWidth = (Width - gap * (columns - 1)) / (float)columns;
            int col = 0, y = 0, rowHeight = 0;
            foreach (Control c in Controls)
            {
                if (!c.Visible) continue;
                int span = spans.TryGetValue(c, out int s) ? s : 1;
                if (col + span > columns) { col = 0; y += rowHeight + LogicalToDeviceUnits(6); rowHeight = 0; }
                int x = (int)(col * (columnWidth + gap));
                c.SetBounds(x, y, (int)(columnWidth * span + gap * (span - 1)), c.Height);
                rowHeight = Math.Max(rowHeight, c.Height);
                col += span;
            }
            int height = y + rowHeight;
            if (Height != height) Height = height;
        }
    }
}
