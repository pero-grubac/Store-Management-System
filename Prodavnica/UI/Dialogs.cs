using Prodavnica.UI.Controls;

namespace Prodavnica.UI
{
    // Base for every dialog: borderless with rounded corners and a shadow, a header, a body and a footer with buttons.
    public class DialogForm : Form
    {
        private readonly Panel header = new Panel();
        private readonly FlowLayoutPanel footer = new FlowLayoutPanel();
        private readonly UiLabel titleLabel;
        private readonly UiLabel subtitleLabel;

        public DialogForm(string title, string? subtitle, int width)
        {
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterParent;
            ShowInTaskbar = false;
            KeyPreview = true;
            BackColor = Theme.Surface;
            Font = Theme.Font(10);
            AutoScaleMode = AutoScaleMode.None;
            Width = LogicalToDeviceUnits(width);

            titleLabel = new UiLabel(title, 14f, semibold: true);
            subtitleLabel = new UiLabel(subtitle ?? string.Empty, 9.5f, color: Theme.Muted) { Visible = !string.IsNullOrEmpty(subtitle) };
            var close = new UiButton(string.Empty, Glyphs.Close, ButtonKind.Ghost) { TabStop = false };
            close.Click += (_, _) => { DialogResult = DialogResult.Cancel; Close(); };
            header.Controls.AddRange(new Control[] { titleLabel, subtitleLabel, close });
            header.Dock = DockStyle.Top;
            header.Height = LogicalToDeviceUnits(string.IsNullOrEmpty(subtitle) ? 68 : 84);
            header.Layout += (_, _) =>
            {
                int p = LogicalToDeviceUnits(24);
                titleLabel.Location = new Point(p, LogicalToDeviceUnits(22));
                subtitleLabel.Location = new Point(p, titleLabel.Bottom + LogicalToDeviceUnits(2));
                close.Location = new Point(header.Width - close.Width - LogicalToDeviceUnits(14), LogicalToDeviceUnits(14));
            };
            header.MouseDown += (_, e) => { if (e.Button == MouseButtons.Left) Native.DragWindow(this); };
            titleLabel.MouseDown += (_, e) => { if (e.Button == MouseButtons.Left) Native.DragWindow(this); };

            footer.Dock = DockStyle.Bottom;
            footer.FlowDirection = FlowDirection.RightToLeft;
            footer.Height = LogicalToDeviceUnits(72);
            footer.Padding = new Padding(LogicalToDeviceUnits(18), LogicalToDeviceUnits(16), LogicalToDeviceUnits(18), 0);
            footer.BackColor = Theme.Background;
            footer.Paint += (_, e) => { using var pen = new Pen(Theme.Border); e.Graphics.DrawLine(pen, 0, 0, footer.Width, 0); };

            Body = new Panel { Dock = DockStyle.Fill, BackColor = Theme.Surface, Padding = new Padding(LogicalToDeviceUnits(24), LogicalToDeviceUnits(4), LogicalToDeviceUnits(24), LogicalToDeviceUnits(24)) };
            Controls.Add(Body);
            Controls.Add(footer);
            Controls.Add(header);
        }

        protected Panel Body { get; }

        protected void SetSubtitle(string text)
        {
            subtitleLabel.Text = text;
            subtitleLabel.Visible = text.Trim().Length > 0;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= Native.CsDropShadow;
                return cp;
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            Native.RoundCorners(this);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) { DialogResult = DialogResult.Cancel; Close(); }
            base.OnKeyDown(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using var pen = new Pen(Theme.BorderStrong);
            e.Graphics.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);
        }

        // Buttons are added right to left: the first one ends up on the far right.
        protected UiButton AddButton(string text, ButtonKind kind, string glyph = "", DialogResult result = DialogResult.None)
        {
            var button = new UiButton(text, glyph, kind) { DialogResult = result, Margin = new Padding(LogicalToDeviceUnits(10), 0, 0, 0) };
            if (button.Width < LogicalToDeviceUnits(110)) { button.AutoWidth = false; button.Width = LogicalToDeviceUnits(110); }
            footer.Controls.Add(button);
            return button;
        }

        protected void SetBodyHeight(int logicalHeight)
        {
            ClientSize = new Size(ClientSize.Width, header.Height + footer.Height + LogicalToDeviceUnits(logicalHeight));
        }

        // Shows the dialog over a dimmed copy of its owner.
        public DialogResult ShowOver(IWin32Window? owner)
        {
            Form? ownerForm = owner as Form ?? (owner as Control)?.FindForm();
            if (ownerForm == null || !ownerForm.Visible) return ShowDialog(owner);
            using var shade = new Form
            {
                FormBorderStyle = FormBorderStyle.None,
                ShowInTaskbar = false,
                StartPosition = FormStartPosition.Manual,
                BackColor = Color.FromArgb(15, 23, 42),
                Opacity = 0.32,
                Bounds = ownerForm.WindowState == FormWindowState.Maximized ? ownerForm.RectangleToScreen(ownerForm.ClientRectangle) : ownerForm.Bounds
            };
            shade.Show(ownerForm);
            try
            {
                return ShowDialog(shade);
            }
            finally
            {
                shade.Close();
            }
        }
    }

    // Confirmations, errors and information, styled like the rest of the app.
    public static class Dialogs
    {
        public static bool Confirm(IWin32Window? owner, string title, string message, string confirmText, bool danger = false)
        {
            using var dialog = new MessageDialog(title, message, danger ? Glyphs.Delete : Glyphs.Info, danger ? Theme.Danger : Theme.Accent);
            dialog.AddButtons(confirmText, danger ? ButtonKind.Danger : ButtonKind.Primary, T.Get("common.cancel"));
            return dialog.ShowOver(owner) == DialogResult.OK;
        }

        public static void Error(IWin32Window? owner, string message)
        {
            using var dialog = new MessageDialog(T.Get("common.error"), message, Glyphs.Warning, Theme.Danger);
            dialog.AddButtons(T.Get("common.ok"), ButtonKind.Primary, null);
            dialog.ShowOver(owner);
        }

        // Runs a database action; a failure is shown to the user instead of crashing the window.
        public static async Task<bool> RunAsync(Control owner, Func<Task> action)
        {
            Cursor? previous = owner.FindForm()?.Cursor;
            try
            {
                if (owner.FindForm() is Form form) form.Cursor = Cursors.AppStarting;
                await action();
                return true;
            }
            catch (Exception ex)
            {
                Error(owner.FindForm(), T.Error(ex));
                return false;
            }
            finally
            {
                if (owner.FindForm() is Form form && previous != null) form.Cursor = previous;
            }
        }

        private sealed class MessageDialog : DialogForm
        {
            public MessageDialog(string title, string message, string glyph, Color color) : base(title, null, 460)
            {
                var icon = new IconBadge(glyph, color) { Location = new Point(LogicalToDeviceUnits(24), LogicalToDeviceUnits(4)) };
                var text = new Label
                {
                    Text = message,
                    Font = Theme.Font(10.5f),
                    ForeColor = Theme.Text,
                    Location = new Point(LogicalToDeviceUnits(80), LogicalToDeviceUnits(6)),
                    MaximumSize = new Size(LogicalToDeviceUnits(340), 0),
                    AutoSize = true,
                    UseMnemonic = false
                };
                Body.Controls.Add(icon);
                Body.Controls.Add(text);
                SetBodyHeight(Math.Max(76, (int)(text.PreferredHeight * 96f / DeviceDpi) + 34));
            }

            public void AddButtons(string confirm, ButtonKind kind, string? cancel)
            {
                AcceptButton = AddButton(confirm, kind, result: DialogResult.OK);
                if (cancel != null) CancelButton = AddButton(cancel, ButtonKind.Secondary, result: DialogResult.Cancel);
            }
        }
    }

    public class IconBadge : UiControl
    {
        private readonly string glyph;
        private readonly Color color;

        public IconBadge(string glyph, Color color)
        {
            this.glyph = glyph;
            this.color = color;
            Size = new Size(S(44), S(44));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            PaintParentBackground(e);
            Draw.Smooth(e.Graphics);
            using (var brush = new SolidBrush(Theme.Mix(color, Color.White, 0.86))) e.Graphics.FillEllipse(brush, 0, 0, Width - 1, Height - 1);
            Draw.Glyph(e.Graphics, glyph, 13, color, ClientRectangle);
        }
    }

    public enum ToastKind
    {
        Success,
        Error,
        Info
    }

    // A short message in the corner of the main window that fades out by itself.
    public sealed class Toast : Form
    {
        private readonly string message;
        private readonly ToastKind kind;
        private readonly System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer { Interval = 30 };
        private int ticks;

        private Toast(string message, ToastKind kind)
        {
            this.message = message;
            this.kind = kind;
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            BackColor = Theme.Text;
            AutoScaleMode = AutoScaleMode.None;
            Size textSize = TextRenderer.MeasureText(message, Theme.Font(10f));
            ClientSize = new Size(textSize.Width + LogicalToDeviceUnits(76), LogicalToDeviceUnits(52));
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            timer.Tick += (_, _) =>
            {
                ticks++;
                if (ticks > 90) Opacity -= 0.08;
                if (Opacity <= 0.05) { timer.Stop(); Close(); }
            };
        }

        protected override bool ShowWithoutActivation => true;

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            Native.RoundCorners(this);
        }

        public static void Show(Form? owner, string message, ToastKind kind = ToastKind.Success)
        {
            if (owner == null || owner.IsDisposed) return;
            var toast = new Toast(message, kind);
            Rectangle area = owner.RectangleToScreen(owner.ClientRectangle);
            toast.Location = new Point(area.Right - toast.Width - toast.LogicalToDeviceUnits(28), area.Bottom - toast.Height - toast.LogicalToDeviceUnits(28));
            toast.Show(owner);
            toast.timer.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Draw.Smooth(g);
            Color color = kind switch { ToastKind.Error => Theme.Danger, ToastKind.Info => Theme.Info, _ => Theme.Success };
            string glyph = kind switch { ToastKind.Error => Glyphs.Warning, ToastKind.Info => Glyphs.Info, _ => Glyphs.Check };
            var icon = new Rectangle(LogicalToDeviceUnits(14), (Height - LogicalToDeviceUnits(26)) / 2, LogicalToDeviceUnits(26), LogicalToDeviceUnits(26));
            using (var brush = new SolidBrush(color)) g.FillEllipse(brush, icon);
            Draw.Glyph(g, glyph, 9, Color.White, icon);
            TextRenderer.DrawText(g, message, Theme.Font(10f), new Rectangle(icon.Right + LogicalToDeviceUnits(12), 0, Width, Height), Color.White, Draw.LeftMiddle);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) timer.Dispose();
            base.Dispose(disposing);
        }
    }

    // Waits until typing pauses before running the search.
    public sealed class Debouncer : IDisposable
    {
        private readonly System.Windows.Forms.Timer timer;

        public Debouncer(int milliseconds, Action action)
        {
            timer = new System.Windows.Forms.Timer { Interval = milliseconds };
            timer.Tick += (_, _) => { timer.Stop(); action(); };
        }

        public void Trigger()
        {
            timer.Stop();
            timer.Start();
        }

        public void Dispose() => timer.Dispose();
    }
}
