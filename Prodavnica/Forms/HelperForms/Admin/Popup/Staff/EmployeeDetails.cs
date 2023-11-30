using Prodavnica.Database.DTO;
using Prodavnica.Database.Repository;
using Prodavnica.Language;
using Prodavnica.Util;

namespace Prodavnica.Forms.HelperForms.Admin.Popup.Staff
{
    public partial class EmployeeDetails : Form
    {
        private User user;
        private User selectedUser;
        private bool update;
        private ThemeDAOImpl themeDAO = new ThemeDAOImpl();
        private UserDAOImpl userDAO = new UserDAOImpl();
        public EmployeeDetails(User user, User selectedUser, bool update)
        {
            InitializeComponent();
            this.user = user;
            this.selectedUser = selectedUser;
            this.update = update;
            if (update)
            {
                // upisi u txt product
                EnableSaveButton(false);
            }
            else
            {
                EnableSaveButton(true);
            }
            ChangeText();
        }
        private void ChangeText()
        {
            LoadSettings.ApplySettins(user, this);
            lblUsername.Text = LanguageHelper.GetString("lblUsername");
            lblFirstName.Text = LanguageHelper.GetString("lblName");
            lblLastName.Text = LanguageHelper.GetString("lblLastName");
            lblNumber.Text = LanguageHelper.GetString("lblPhone");
            lblEmail.Text = LanguageHelper.GetString("lblEmail");
            lblPassword.Text = LanguageHelper.GetString("gbPassword");

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUsername.Text) &&
                !string.IsNullOrEmpty(txtFirstName.Text) &&
                !string.IsNullOrEmpty(txtLastName.Text) &&
                !string.IsNullOrEmpty(txtEmail.Text) &&
                !string.IsNullOrEmpty(txtNumber.Text) &&
                !string.IsNullOrEmpty(txtPassword.Text))
            {

                User newUser = new User
                {
                    UserName = txtUsername.Text,
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    Email = txtEmail.Text,
                    Password = txtPassword.Text,
                    PhoneNumber = txtNumber.Text,
                    IsAdmin = false
                };
                if (update)
                {

                }
                else
                {
                    Theme theme = new Theme
                    {
                        ColorName = "255,0,128,128",
                        FontName = "Segoe UI",
                        IsStrikeout = false,
                        IsUnderline = false,
                        Size = 11,
                        FontStyle = "Regular"
                    };
                    themeDAO.CreateTheme(ref theme);
                    newUser.IdLanguage = 2;
                    newUser.IdTheme = theme.Id;
                    userDAO.CreateUser(newUser);
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        private void EnableSaveButton(bool enable)
        {
            btnSave.Enabled = enable;
            btnSave.Visible = enable;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
