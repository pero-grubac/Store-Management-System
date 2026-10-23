using Prodavnica.App;
using Prodavnica.Database.DTO;
using Prodavnica.Services;
using Prodavnica.Shell;
using Prodavnica.UI;
using Prodavnica.UI.Controls;

using Theme = Prodavnica.UI.Theme;
namespace Prodavnica.Pages
{
    // Profile, password, appearance and language, as cards in a centred column.
    public class SettingsPage : Page
    {
        private readonly MainForm main;
        private readonly Panel column = new Panel { BackColor = Color.Transparent };

        private readonly ImagePicker picture;
        private readonly UiInput first = new UiInput();
        private readonly UiInput last = new UiInput();
        private readonly UiInput userName = new UiInput(string.Empty, Glyphs.Person);
        private readonly UiInput email = new UiInput(string.Empty, Glyphs.Mail);
        private readonly UiInput phone = new UiInput(string.Empty, Glyphs.Phone);
        private readonly Dictionary<UserField, Field> profileFields = new();

        private readonly UiInput current = new UiInput(string.Empty, Glyphs.Lock, password: true);
        private readonly UiInput newPassword = new UiInput(string.Empty, Glyphs.Lock, password: true);
        private readonly UiInput confirm = new UiInput(string.Empty, Glyphs.Lock, password: true);
        private readonly Field currentField, newField, confirmField;

        private readonly Swatches swatches = new Swatches();
        private readonly UiSelect font = new UiSelect();
        private readonly Segmented size = new Segmented((T.Get("settings.sizeSmall"), string.Empty), (T.Get("settings.sizeNormal"), string.Empty), (T.Get("settings.sizeLarge"), string.Empty));
        private readonly Segmented language = new Segmented(("Srpski (latinica)", string.Empty), ("Српски (ћирилица)", string.Empty), ("English", string.Empty));
        private bool loading = true;

        public SettingsPage(MainForm main)
        {
            this.main = main;
            AutoScroll = true;
            User user = Session.User;

            // Profile
            picture = new ImagePicker(round: true, user.FullName) { Width = S(160) };
            var profileGrid = new FieldGrid(2);
            profileFields[UserField.FirstName] = profileGrid.Add(new Field(T.Get("employees.firstName"), first));
            profileFields[UserField.LastName] = profileGrid.Add(new Field(T.Get("employees.lastName"), last));
            profileFields[UserField.UserName] = profileGrid.Add(new Field(T.Get("employees.userName"), userName));
            profileFields[UserField.Email] = profileGrid.Add(new Field(T.Get("employees.email"), email));
            profileFields[UserField.Phone] = profileGrid.Add(new Field(T.Get("employees.phone"), phone));
            var saveProfile = new UiButton(T.Get("settings.saveProfile"), Glyphs.Check, ButtonKind.Primary);
            saveProfile.Click += async (_, _) => await SaveProfileAsync();
            var profile = new Card { Title = T.Get("settings.profile"), Subtitle = T.Get("settings.profileSubtitle") };
            profile.Controls.AddRange(new Control[] { picture, profileGrid, saveProfile });
            profile.Layout += (_, _) =>
            {
                Rectangle area = profile.DisplayRectangle;
                picture.Location = new Point(area.X, area.Y + S(4));
                profileGrid.SetBounds(picture.Right + S(32), area.Y, area.Right - picture.Right - S(32), profileGrid.Height);
                saveProfile.Location = new Point(area.Right - saveProfile.Width, Math.Max(profileGrid.Bottom, picture.Bottom) + S(8));
                profile.Height = saveProfile.Bottom + S(20);
            };

            // Password
            currentField = new Field(T.Get("settings.currentPassword"), current);
            newField = new Field(T.Get("settings.newPassword"), newPassword);
            confirmField = new Field(T.Get("employees.confirmPassword"), confirm);
            var passwordGrid = new FieldGrid(3);
            passwordGrid.Add(currentField);
            passwordGrid.Add(newField);
            passwordGrid.Add(confirmField);
            var changePassword = new UiButton(T.Get("settings.changePassword"), Glyphs.Lock, ButtonKind.Secondary);
            changePassword.Click += async (_, _) => await ChangePasswordAsync();
            var security = new Card { Title = T.Get("settings.security"), Subtitle = T.Get("settings.securitySubtitle") };
            security.Controls.AddRange(new Control[] { passwordGrid, changePassword });
            security.Layout += (_, _) =>
            {
                Rectangle area = security.DisplayRectangle;
                passwordGrid.SetBounds(area.X, area.Y, area.Width, passwordGrid.Height);
                changePassword.Location = new Point(area.Right - changePassword.Width, passwordGrid.Bottom + S(8));
                security.Height = changePassword.Bottom + S(20);
            };

            // Appearance
            font.SetItems(Theme.FontFamilies.Select(f => (f, (object?)f)), Session.ThemeSettings.FontName);
            if (font.SelectedIndex < 0) font.SelectValue("Segoe UI");
            size.SelectedIndex = Session.ThemeSettings.Size <= 10 ? 0 : Session.ThemeSettings.Size >= 12 ? 2 : 1;
            swatches.Selected = Theme.Accent;
            var colorCaption = new UiLabel(T.Get("settings.accent"), 9.25f, semibold: true, Theme.Muted);
            var appearanceGrid = new FieldGrid(2);
            appearanceGrid.Add(new Field(T.Get("settings.font"), font));
            appearanceGrid.Add(new Field(T.Get("settings.textSize"), size));
            var appearance = new Card { Title = T.Get("settings.appearance"), Subtitle = T.Get("settings.appearanceSubtitle") };
            appearance.Controls.AddRange(new Control[] { colorCaption, swatches, appearanceGrid });
            appearance.Layout += (_, _) =>
            {
                Rectangle area = appearance.DisplayRectangle;
                colorCaption.Location = new Point(area.X, area.Y);
                swatches.SetBounds(area.X, colorCaption.Bottom + S(8), area.Width, swatches.Height);
                appearanceGrid.SetBounds(area.X, swatches.Bottom + S(20), area.Width, appearanceGrid.Height);
                appearance.Height = appearanceGrid.Bottom + S(8);
            };
            swatches.SelectedChanged += async (_, _) => await SaveAppearanceAsync();
            font.SelectionChanged += async (_, _) => await SaveAppearanceAsync();
            size.SelectedIndexChanged += async (_, _) => await SaveAppearanceAsync();

            // Language
            language.SelectedIndex = Math.Clamp(user.IdLanguage == 3 ? 1 : user.IdLanguage == 2 ? 2 : 0, 0, 2);
            language.SelectedIndexChanged += async (_, _) => await SaveLanguageAsync();
            var languageCard = new Card { Title = T.Get("settings.language"), Subtitle = T.Get("settings.languageSubtitle") };
            languageCard.Controls.Add(language);
            languageCard.Layout += (_, _) =>
            {
                Rectangle area = languageCard.DisplayRectangle;
                language.Location = new Point(area.X, area.Y);
                languageCard.Height = language.Bottom + S(24);
            };

            column.Controls.AddRange(new Control[] { profile, security, appearance, languageCard });
            Controls.Add(column);

            first.Text = user.FirstName;
            last.Text = user.LastName;
            userName.Text = user.UserName;
            email.Text = user.Email;
            phone.Text = user.PhoneNumber;
            picture.ImageData = user.Image;
            loading = false;
        }

        // Keep the scroll position when a control inside gets focus.
        protected override Point ScrollToControl(Control activeControl) => DisplayRectangle.Location;

        public override string Title => T.Get("nav.settings");
        public override string Subtitle => T.Get("settings.subtitle");

        protected override void OnLayout(LayoutEventArgs e)
        {
            base.OnLayout(e);
            int width = Math.Min(S(960), ClientSize.Width - Padding.Horizontal);
            int x = Math.Max(Padding.Left, (ClientSize.Width - width) / 2);
            int y = 0;
            column.SuspendLayout();
            foreach (Control card in column.Controls)
            {
                card.SetBounds(0, y, width, card.Height);
                card.PerformLayout();
                y = card.Bottom + S(18);
            }
            column.ResumeLayout();
            column.SetBounds(x, AutoScrollPosition.Y, width, y);
        }

        private async Task SaveProfileAsync()
        {
            foreach (Field f in profileFields.Values) f.SetError(null);
            var input = new UserInput { FirstName = first.Text, LastName = last.Text, UserName = userName.Text, Email = email.Text, Phone = phone.Text };
            bool taken = false;
            if (!await UI.Dialogs.RunAsync(this, async () => taken = await Session.Users.UserNameExistsAsync(userName.Text, Session.User.Id))) return;
            List<UserValidationError> errors = UserValidator.Validate(input, passwordRequired: false, userNameTaken: taken);
            foreach (var error in errors.GroupBy(e => e.Field).Select(g => g.First()))
            {
                if (profileFields.TryGetValue(error.Field, out Field? field)) field.SetError(T.Get("val.user." + error.Error));
            }
            if (errors.Count > 0) return;
            User user = Session.User;
            user.FirstName = first.Text.Trim();
            user.LastName = last.Text.Trim();
            user.UserName = userName.Text.Trim();
            user.Email = email.Text.Trim();
            user.PhoneNumber = phone.Text.Trim();
            user.Image = picture.ImageData;
            if (!await UI.Dialogs.RunAsync(this, () => Session.Users.UpdateAsync(user))) return;
            await Session.LogAsync("PROFILE_UPDATE", user.FullName);
            main.UpdateUserBadge();
            Toast.Show(FindForm(), T.Get("settings.profileSaved"));
        }

        private async Task ChangePasswordAsync()
        {
            currentField.SetError(null);
            newField.SetError(null);
            confirmField.SetError(null);
            if (current.Text.Length == 0) { currentField.SetError(T.Get("val.required")); return; }
            if (newPassword.Text.Length < UserValidator.MinPasswordLength) { newField.SetError(T.Get("val.user.PasswordTooShort")); return; }
            if (confirm.Text != newPassword.Text) { confirmField.SetError(T.Get("val.user.PasswordsDoNotMatch")); return; }
            bool changed = false;
            if (!await UI.Dialogs.RunAsync(this, async () => changed = await Session.Users.ChangePasswordAsync(Session.User.Id, current.Text, newPassword.Text))) return;
            if (!changed) { currentField.SetError(T.Get("settings.wrongPassword")); return; }
            current.Text = newPassword.Text = confirm.Text = string.Empty;
            await Session.LogAsync("PASSWORD_CHANGE", Session.User.FullName);
            Toast.Show(FindForm(), T.Get("settings.passwordChanged"));
        }

        private async Task SaveAppearanceAsync()
        {
            if (loading) return;
            Prodavnica.Database.DTO.Theme theme = Session.ThemeSettings;
            theme.ColorName = ThemeFormat.FormatArgb(255, swatches.Selected.R, swatches.Selected.G, swatches.Selected.B);
            theme.FontName = font.SelectedValue as string ?? "Segoe UI";
            theme.Size = new[] { 10, 11, 12 }[size.SelectedIndex];
            theme.FontStyle = "Regular";
            if (!await UI.Dialogs.RunAsync(this, () => Session.Themes.UpdateAsync(theme))) return;
            await Session.ReloadPreferencesAsync();
            await main.ReloadAppearanceAsync();
        }

        private async Task SaveLanguageAsync()
        {
            if (loading) return;
            int id = new[] { 1, 3, 2 }[language.SelectedIndex];
            if (!await UI.Dialogs.RunAsync(this, () => Session.Users.SetLanguageAsync(Session.User.Id, id))) return;
            Session.User.IdLanguage = id;
            await Session.ReloadPreferencesAsync();
            await main.ReloadAppearanceAsync();
        }

        // Accent colour choices as circles, plus a custom colour.
        private sealed class Swatches : UiControl
        {
            private Color selected;

            public Swatches()
            {
                Height = S(44);
                Cursor = Cursors.Hand;
            }

            public event EventHandler? SelectedChanged;

            public Color Selected { get => selected; set { selected = value; Invalidate(); } }

            private Rectangle SwatchBounds(int i) => new Rectangle(i * S(52), S(2), S(40), S(40));

            protected override void OnMouseClick(MouseEventArgs e)
            {
                base.OnMouseClick(e);
                for (int i = 0; i <= Theme.Palette.Length; i++)
                {
                    if (!SwatchBounds(i).Contains(e.Location)) continue;
                    if (i == Theme.Palette.Length)
                    {
                        using var dialog = new ColorDialog { Color = selected, FullOpen = true };
                        if (dialog.ShowDialog(FindForm()) != DialogResult.OK) return;
                        selected = dialog.Color;
                    }
                    else
                    {
                        selected = Theme.Palette[i];
                    }
                    Invalidate();
                    SelectedChanged?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                PaintParentBackground(e);
                Graphics g = e.Graphics;
                Draw.Smooth(g);
                bool custom = !Theme.Palette.Any(c => c.ToArgb() == selected.ToArgb());
                for (int i = 0; i < Theme.Palette.Length; i++)
                {
                    Rectangle r = SwatchBounds(i);
                    bool on = Theme.Palette[i].ToArgb() == selected.ToArgb();
                    if (on) { using var ring = new Pen(Theme.Palette[i], SF(2)); g.DrawEllipse(ring, r); }
                    var inner = Rectangle.Inflate(r, -S(5), -S(5));
                    using (var brush = new SolidBrush(Theme.Palette[i])) g.FillEllipse(brush, inner);
                    if (on) Draw.Glyph(g, Glyphs.Check, 9, Color.White, inner);
                }
                Rectangle plus = SwatchBounds(Theme.Palette.Length);
                var plusInner = Rectangle.Inflate(plus, -S(5), -S(5));
                using (var brush = new SolidBrush(custom ? selected : Theme.Hover)) g.FillEllipse(brush, plusInner);
                using (var pen = new Pen(custom ? selected : Theme.BorderStrong, custom ? SF(2) : 1)) g.DrawEllipse(pen, custom ? plus : plusInner);
                Draw.Glyph(g, custom ? Glyphs.Check : Glyphs.Add, 9, custom ? Color.White : Theme.Muted, plusInner);
            }
        }
    }
}
