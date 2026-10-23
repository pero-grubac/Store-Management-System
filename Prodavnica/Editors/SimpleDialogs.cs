using Prodavnica.App;
using Prodavnica.Database.DTO;
using Prodavnica.Services;
using Prodavnica.UI;
using Prodavnica.UI.Controls;

using Theme = Prodavnica.UI.Theme;
namespace Prodavnica.Editors
{
    // Name (and optionally e-mail) for categories, manufacturers and suppliers.
    public class NameDialog : DialogForm
    {
        private readonly UiInput name = new UiInput(T.Get("common.namePlaceholder"));
        private readonly UiInput email = new UiInput("ime@firma.ba", Glyphs.Mail);
        private readonly Field nameField;
        private readonly Field emailField;
        private readonly bool withEmail;

        public NameDialog(string title, string currentName, string? currentEmail, bool withEmail) : base(title, null, 480)
        {
            this.withEmail = withEmail;
            name.Text = currentName;
            email.Text = currentEmail ?? string.Empty;
            nameField = new Field(T.Get("common.name"), name);
            emailField = new Field(T.Get("employees.email"), email) { Visible = withEmail };
            var grid = new FieldGrid(1) { Dock = DockStyle.Top };
            grid.Add(nameField);
            grid.Add(emailField);
            Body.Controls.Add(grid);
            AcceptButton = AddButton(T.Get("common.save"), ButtonKind.Primary, Glyphs.Check);
            CancelButton = AddButton(T.Get("common.cancel"), ButtonKind.Secondary, result: DialogResult.Cancel);
            ((UiButton)AcceptButton).Click += (_, _) => Save();
            SetBodyHeight(withEmail ? 190 : 100);
            Shown += (_, _) => name.FocusInput();
        }

        public string NameValue => name.Text.Trim();
        public string EmailValue => email.Text.Trim();

        private void Save()
        {
            bool ok = true;
            nameField.SetError(null);
            emailField.SetError(null);
            if (NameValue.Length == 0) { nameField.SetError(T.Get("val.required")); ok = false; }
            else if (NameValue.Length > 45) { nameField.SetError(T.Get("val.tooLong")); ok = false; }
            if (withEmail && !UserValidator.IsValidEmail(EmailValue)) { emailField.SetError(T.Get("val.email")); ok = false; }
            if (ok) DialogResult = DialogResult.OK;
        }
    }

    public class EmployeeDialog : DialogForm
    {
        private readonly User? existing;
        private readonly ImagePicker picture;
        private readonly UiInput first = new UiInput();
        private readonly UiInput last = new UiInput();
        private readonly UiInput userName = new UiInput(string.Empty, Glyphs.Person);
        private readonly UiInput email = new UiInput(string.Empty, Glyphs.Mail);
        private readonly UiInput phone = new UiInput(string.Empty, Glyphs.Phone);
        private readonly Segmented role = new Segmented((T.Get("role.cashier"), string.Empty), (T.Get("role.admin"), string.Empty));
        private readonly UiInput password = new UiInput(string.Empty, Glyphs.Lock, password: true);
        private readonly UiInput confirm = new UiInput(string.Empty, Glyphs.Lock, password: true);
        private readonly Dictionary<UserField, Field> fields = new();

        public EmployeeDialog(User? user) : base(T.Get(user == null ? "employees.add" : "employees.edit"),
            T.Get(user == null ? "employees.addSubtitle" : "employees.editSubtitle"), 860)
        {
            existing = user;
            picture = new ImagePicker(round: true, user?.FullName ?? "?") { Location = new Point(LogicalToDeviceUnits(24), LogicalToDeviceUnits(8)) };
            var grid = new FieldGrid(2) { Location = new Point(LogicalToDeviceUnits(240), LogicalToDeviceUnits(8)), Width = LogicalToDeviceUnits(570) };
            fields[UserField.FirstName] = grid.Add(new Field(T.Get("employees.firstName"), first));
            fields[UserField.LastName] = grid.Add(new Field(T.Get("employees.lastName"), last));
            fields[UserField.UserName] = grid.Add(new Field(T.Get("employees.userName"), userName));
            fields[UserField.Email] = grid.Add(new Field(T.Get("employees.email"), email));
            fields[UserField.Phone] = grid.Add(new Field(T.Get("employees.phone"), phone));
            grid.Add(new Field(T.Get("employees.role"), role));
            string? hint = user == null ? null : T.Get("employees.passwordHint");
            fields[UserField.Password] = grid.Add(new Field(T.Get("employees.password"), password, hint));
            fields[UserField.ConfirmPassword] = grid.Add(new Field(T.Get("employees.confirmPassword"), confirm));
            Body.Controls.Add(picture);
            Body.Controls.Add(grid);
            first.TextChanged += (_, _) => picture.PlaceholderName = $"{first.Text} {last.Text}";
            last.TextChanged += (_, _) => picture.PlaceholderName = $"{first.Text} {last.Text}";

            if (user != null)
            {
                first.Text = user.FirstName;
                last.Text = user.LastName;
                userName.Text = user.UserName;
                email.Text = user.Email;
                phone.Text = user.PhoneNumber;
                role.SelectedIndex = user.IsAdmin ? 1 : 0;
                picture.ImageData = user.Image;
                // Nobody can take away their own admin rights by accident.
                role.Enabled = user.Id != Session.User.Id;
            }
            else
            {
                picture.ImageData = null;
            }

            var save = AddButton(T.Get("common.save"), ButtonKind.Primary, Glyphs.Check);
            save.Click += async (_, _) => await SaveAsync();
            AcceptButton = save;
            CancelButton = AddButton(T.Get("common.cancel"), ButtonKind.Secondary, result: DialogResult.Cancel);
            SetBodyHeight(372);
            Shown += (_, _) => first.FocusInput();
        }

        public User? Result { get; private set; }

        private async Task SaveAsync()
        {
            foreach (Field f in fields.Values) f.SetError(null);
            var input = new UserInput
            {
                FirstName = first.Text,
                LastName = last.Text,
                UserName = userName.Text,
                Email = email.Text,
                Phone = phone.Text,
                Password = password.Text,
                ConfirmPassword = confirm.Text
            };
            bool taken = false;
            if (!await UI.Dialogs.RunAsync(this, async () => taken = await Session.Users.UserNameExistsAsync(userName.Text, existing?.Id))) return;
            List<UserValidationError> errors = UserValidator.Validate(input, passwordRequired: existing == null, userNameTaken: taken);
            foreach (var error in errors.GroupBy(e => e.Field).Select(g => g.First()))
            {
                fields[error.Field].SetError(T.Get("val.user." + error.Error));
            }
            if (errors.Count > 0) return;

            var user = existing ?? new User { IdLanguage = 1 };
            user.FirstName = input.FirstName.Trim();
            user.LastName = input.LastName.Trim();
            user.UserName = input.UserName.Trim();
            user.Email = input.Email.Trim();
            user.PhoneNumber = input.Phone.Trim();
            user.IsAdmin = role.SelectedIndex == 1;
            user.Image = picture.ImageData;
            bool ok = await UI.Dialogs.RunAsync(this, async () =>
            {
                if (existing == null) await Session.Users.CreateAsync(user, input.Password);
                else await Session.Users.UpdateAsync(user, input.Password.Length > 0 ? input.Password : null);
            });
            if (!ok) return;
            await Session.LogAsync(existing == null ? "USER_CREATE" : "USER_UPDATE", user.FullName);
            Result = user;
            DialogResult = DialogResult.OK;
        }
    }
}
