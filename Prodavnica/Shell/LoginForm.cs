using Prodavnica.App;
using Prodavnica.Database.DTO;
using Prodavnica.UI;
using Prodavnica.UI.Controls;
using System.Drawing.Drawing2D;

using Theme = Prodavnica.UI.Theme;
namespace Prodavnica.Shell
{
    // Sign-in window: brand panel on the left, the form on the right.
    public class LoginForm : Form
    {
        private readonly BrandPanel brand = new BrandPanel();
        private readonly Panel form = new Panel { BackColor = Theme.Surface };
        private readonly UiInput userName = new UiInput(T.Get("login.userPlaceholder"), Glyphs.Person);
        private readonly UiInput password = new UiInput(T.Get("login.passwordPlaceholder"), Glyphs.Lock, password: true);
        private readonly ErrorBanner error = new ErrorBanner();
        private readonly UiButton signIn = new UiButton(T.Get("login.signIn"), string.Empty, ButtonKind.Primary);

        public LoginForm()
        {
            Text = T.Get("app.name");
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Theme.Surface;
            ClientSize = new Size(LogicalToDeviceUnits(980), LogicalToDeviceUnits(620));
            Font = Theme.Font(10);
            KeyPreview = true;

            var title = new UiLabel(T.Get("login.title"), 21f, semibold: true);
            var subtitle = new UiLabel(T.Get("login.subtitle"), 10.5f, color: Theme.Muted);
            var userField = new Field(T.Get("login.userName"), userName);
            var passwordField = new Field(T.Get("login.password"), password);
            var close = new UiButton(string.Empty, Glyphs.Close, ButtonKind.Ghost) { TabStop = false };
            var minimize = new UiButton(string.Empty, Glyphs.Minimize, ButtonKind.Ghost) { TabStop = false };
            close.Click += (_, _) => Close();
            minimize.Click += (_, _) => WindowState = FormWindowState.Minimized;
            signIn.AutoWidth = false;
            signIn.Height = LogicalToDeviceUnits(46);
            signIn.Font = Theme.Semibold(10.5f);
            signIn.Click += async (_, _) => await SignInAsync();
            AcceptButton = signIn;
            error.Visible = false;
            userName.Box.Name = "txtUserName";
            password.Box.Name = "txtPassword";

            form.Controls.AddRange(new Control[] { title, subtitle, userField, passwordField, error, signIn, close, minimize });
            form.Layout += (_, _) =>
            {
                int left = LogicalToDeviceUnits(64), width = form.Width - left * 2, y = LogicalToDeviceUnits(118);
                close.Location = new Point(form.Width - close.Width - LogicalToDeviceUnits(12), LogicalToDeviceUnits(12));
                minimize.Location = new Point(close.Left - minimize.Width - LogicalToDeviceUnits(4), close.Top);
                title.Location = new Point(left - LogicalToDeviceUnits(2), y);
                subtitle.Location = new Point(left, title.Bottom + LogicalToDeviceUnits(6));
                y = subtitle.Bottom + LogicalToDeviceUnits(34);
                userField.SetBounds(left, y, width, userField.Height);
                passwordField.SetBounds(left, userField.Bottom + LogicalToDeviceUnits(4), width, passwordField.Height);
                error.SetBounds(left, passwordField.Bottom + LogicalToDeviceUnits(2), width, LogicalToDeviceUnits(42));
                signIn.SetBounds(left, (error.Visible ? error.Bottom : passwordField.Bottom) + LogicalToDeviceUnits(14), width, signIn.Height);
            };
            form.MouseDown += (_, e) => { if (e.Button == MouseButtons.Left) Native.DragWindow(this); };
            brand.MouseDown += (_, e) => { if (e.Button == MouseButtons.Left) Native.DragWindow(this); };

            brand.Dock = DockStyle.Left;
            brand.Width = LogicalToDeviceUnits(430);
            form.Dock = DockStyle.Fill;
            Controls.Add(form);
            Controls.Add(brand);
            userName.TextChanged += (_, _) => HideError();
            password.TextChanged += (_, _) => HideError();
            Shown += (_, _) => userName.FocusInput();
        }

        public event EventHandler? SignedIn;

        public bool Succeeded { get; private set; }

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

        private void HideError()
        {
            if (!error.Visible) return;
            error.Visible = false;
            form.PerformLayout();
        }

        private void ShowError(string message)
        {
            error.Message = message;
            error.Visible = true;
            form.PerformLayout();
        }

        private async Task SignInAsync()
        {
            if (string.IsNullOrWhiteSpace(userName.Text) || string.IsNullOrEmpty(password.Text))
            {
                ShowError(T.Get("login.missing"));
                return;
            }
            signIn.Enabled = false;
            signIn.Text = T.Get("login.signingIn");
            try
            {
                User? user = await Session.Users.AuthenticateAsync(userName.Text, password.Text);
                if (user == null)
                {
                    ShowError(T.Get("login.wrong"));
                    password.Text = string.Empty;
                    password.FocusInput();
                    return;
                }
                await Session.StartAsync(user);
                await Session.LogAsync("LOGIN", T.Get("auditText.login"));
                Succeeded = true;
                SignedIn?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                ShowError(T.Error(ex));
            }
            finally
            {
                if (!IsDisposed)
                {
                    signIn.Enabled = true;
                    signIn.Text = T.Get("login.signIn");
                }
            }
        }

        private sealed class ErrorBanner : UiControl
        {
            public string Message { get; set; } = string.Empty;

            protected override void OnPaint(PaintEventArgs e)
            {
                PaintParentBackground(e);
                Draw.Smooth(e.Graphics);
                var r = new RectangleF(0.5f, 0.5f, Width - 1.5f, Height - 1.5f);
                Draw.Fill(e.Graphics, Theme.Mix(Theme.Danger, Color.White, 0.9), r, SF(8));
                Draw.Glyph(e.Graphics, Glyphs.Warning, 10.5f, Theme.Danger, new Rectangle(S(4), 0, S(36), Height));
                TextRenderer.DrawText(e.Graphics, Message, Theme.Font(9.5f), new Rectangle(S(40), 0, Width - S(48), Height), Theme.Mix(Theme.Danger, Color.Black, 0.25), Draw.LeftMiddle);
            }
        }

        // Accent gradient with the app name, a tagline and what the app does.
        private sealed class BrandPanel : Panel
        {
            public BrandPanel()
            {
                SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true);
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                Graphics g = e.Graphics;
                Draw.Smooth(g);
                Color start = Theme.Mix(Theme.Accent, Color.Black, 0.15), end = Theme.Mix(Theme.Accent, Color.FromArgb(30, 27, 75), 0.55);
                using (var gradient = new LinearGradientBrush(ClientRectangle, start, end, 60f)) g.FillRectangle(gradient, ClientRectangle);
                using (var circle = new SolidBrush(Color.FromArgb(22, Color.White)))
                {
                    g.FillEllipse(circle, Width - LogicalToDeviceUnits(220), -LogicalToDeviceUnits(120), LogicalToDeviceUnits(360), LogicalToDeviceUnits(360));
                    g.FillEllipse(circle, -LogicalToDeviceUnits(140), Height - LogicalToDeviceUnits(240), LogicalToDeviceUnits(320), LogicalToDeviceUnits(320));
                }
                int left = LogicalToDeviceUnits(48);
                var logo = new Rectangle(left, LogicalToDeviceUnits(48), LogicalToDeviceUnits(48), LogicalToDeviceUnits(48));
                Draw.Fill(g, Color.FromArgb(40, Color.White), logo, LogicalToDeviceUnits(12));
                Draw.Glyph(g, Glyphs.Shop, 17, Color.White, logo);
                TextRenderer.DrawText(g, T.Get("app.name"), Theme.Semibold(14f), new Point(logo.Right + LogicalToDeviceUnits(14), logo.Y + LogicalToDeviceUnits(12)), Color.White, TextFormatFlags.NoPadding);

                var headline = new Rectangle(left, LogicalToDeviceUnits(200), Width - left * 2, LogicalToDeviceUnits(120));
                TextRenderer.DrawText(g, T.Get("login.headline"), Theme.Semibold(21f), headline, Color.White, TextFormatFlags.WordBreak | TextFormatFlags.NoPadding);
                int y = LogicalToDeviceUnits(350);
                foreach (string key in new[] { "login.feature1", "login.feature2", "login.feature3" })
                {
                    var dot = new Rectangle(left, y, LogicalToDeviceUnits(24), LogicalToDeviceUnits(24));
                    using (var brush = new SolidBrush(Color.FromArgb(48, Color.White))) g.FillEllipse(brush, dot);
                    Draw.Glyph(g, Glyphs.Check, 8, Color.White, dot);
                    TextRenderer.DrawText(g, T.Get(key), Theme.Font(10.5f), new Point(dot.Right + LogicalToDeviceUnits(12), y + LogicalToDeviceUnits(3)), Color.FromArgb(235, Color.White), TextFormatFlags.NoPadding);
                    y += LogicalToDeviceUnits(40);
                }
                TextRenderer.DrawText(g, "© 2026 Store Management System", Theme.Font(8.5f), new Point(left, Height - LogicalToDeviceUnits(44)), Color.FromArgb(170, Color.White), TextFormatFlags.NoPadding);
            }
        }
    }
}
