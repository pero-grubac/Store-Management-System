using Google.Protobuf.WellKnownTypes;
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
        private List<User> users;

        private ThemeDAOImpl themeDAO = new ThemeDAOImpl();
        private UserDAOImpl userDAO = new UserDAOImpl();
        public EmployeeDetails(User user, User selectedUser, bool update)
        {
            InitializeComponent();
            this.user = user;
            this.selectedUser = selectedUser;
            this.update = update;
            LoadSettings.ApplySettins(user, this);
            ChangeText();
            if (update)
            {
                SetData();
            }
            lblWarrning.Visible = false;
            users = userDAO.GetAll(user.Id);
        }
        private void ChangeText()
        {
            LoadSettings.ApplySettins(user, this);
            lblUsername.Text = LanguageHelper.GetString("lblUsername");
            lblFirstName.Text = LanguageHelper.GetString("lblName");
            lblLastName.Text = LanguageHelper.GetString("lblLastName");
            lblNumber.Text = LanguageHelper.GetString("lblPhone");
            lblEmail.Text = LanguageHelper.GetString("lblEmail");
            lblPassword.Text = LanguageHelper.GetString("password");
            btnSave.Text = LanguageHelper.GetString("btnSave");
            btnCancel.Text = LanguageHelper.GetString("btnCancel");
            lblWarrning.Text = LanguageHelper.GetString("userExists");
            lblWarrning.ForeColor = Color.Red;
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
                    if (txtUsername.Text != selectedUser.UserName ||
                        txtFirstName.Text != selectedUser.FirstName ||
                        txtLastName.Text != selectedUser.LastName ||
                        txtEmail.Text != selectedUser.Email ||
                        txtNumber.Text != selectedUser.PhoneNumber ||
                        txtPassword.Text != "*" )
                    {
                        if (users.Any(u => u.UserName == newUser.UserName) && newUser.UserName != selectedUser.UserName)
                        {
                            lblWarrning.Visible = true;
                        }
                        else
                        {

                            lblWarrning.Visible = false;
                            string storedPassword = selectedUser.Password;
                            User updatedUser = new User
                            {
                                UserName = txtUsername.Text,
                                FirstName = txtFirstName.Text,
                                LastName = txtLastName.Text,
                                Email = txtEmail.Text,
                                Password = txtPassword.Text,
                                PhoneNumber = txtNumber.Text,
                                Id = selectedUser.Id
                            };
                            userDAO.SaveUser(updatedUser);
                            bool newPassword = Password.Verify(updatedUser.Password, storedPassword);
                            if (!newPassword)
                            {
                                userDAO.ChangePassword(ref updatedUser, updatedUser.Password);
                            }
                        }

                    }
                }
                else
                {
                    if (users.Any(u => u.UserName == newUser.UserName))
                    {
                        lblWarrning.Visible = true;
                    }
                    else
                    {
                        lblWarrning.Visible = false;

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
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    

                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void SetData()
        {
            txtEmail.Text = selectedUser.Email;
            txtFirstName.Text = selectedUser.FirstName;
            txtLastName.Text = selectedUser.LastName;
            txtNumber.Text = selectedUser.PhoneNumber;
            txtUsername.Text = selectedUser.UserName;
            txtPassword.Text = "*";
        }
    }
}
