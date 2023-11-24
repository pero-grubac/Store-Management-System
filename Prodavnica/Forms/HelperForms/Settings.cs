using Prodavnica.Database.DTO;
using Prodavnica.Database.Repository;
using Prodavnica.Language;
using Prodavnica.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Prodavnica.Forms.HelperForms
{
    public partial class Settings : Form
    {
        private UserDAOImpl userDAO = new UserDAOImpl();
        private ThemeDAOImpl themeDAO = new ThemeDAOImpl();
        private LanguageDAOImpl languageDAO = new LanguageDAOImpl();
        private User user;
        private Theme theme;
        private Color userColor;
        private Font userFont;
        private List<string> languages;
        private string selectedLanguage;
        MainFOrm form;
        private bool isInitializing = true;

        public Settings(ref User user, ref MainFOrm form)
        {
            InitializeComponent();
            VisiblePassSettings(false);
            VisibleProfileSettings(false);
            btnChangePass.Visible = false;
            this.form = form;

            txtOldPass.TextChanged += TextBox_TextChanged;
            txtNewPass2.TextChanged += TextBox_TextChanged;
            gbPassword.Resize += gbPass_Resize;

            txtUsername.Text = user.UserName;
            txtUsername.TextChanged += TextBox_TextChanged;
            txtEmail.Text = user.Email;
            txtEmail.TextChanged += TextBox_TextChanged;
            txtPhone.Text = user.PhoneNumber;
            txtPhone.TextChanged += TextBox_TextChanged;
            txtName.Text = user.FirstName;
            txtName.TextChanged += TextBox_TextChanged;
            txtLastName.Text = user.LastName;
            txtLastName.TextChanged += TextBox_TextChanged;
            gbProfile.Resize += gbProfile_Resize;

            txtNewPass.TextChanged += TextBoxNewPass_TextChanged;
            txtNewPass2.TextChanged += TextBoxNewPass_TextChanged;

            this.user = user;
            theme = themeDAO.FindById(user.IdTheme);
            userColor = themeDAO.GetColor(theme);
            userFont = themeDAO.GetFont(theme);
            languages = languageDAO.GetLanguages();
            cbLanguage.DataSource = languages;
            cbLanguage.SelectedItem = languageDAO.GetSelectedLanguageName(user.IdLangugae);
            isInitializing = false;

            lblNewTheme.Font = userFont;

            LoadSettings.ApplySettins(user, this);
            ChangeText();

        }

        private void TextBoxNewPass_TextChanged(object? sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNewPass.Text) && !string.IsNullOrWhiteSpace(txtNewPass2.Text) &&
                txtNewPass.Text != txtOldPass.Text && txtNewPass2.Text == txtNewPass.Text)
            {
                btnChangePass.Visible = true;
            }
        }

        private void ChangeText()
        {
            gbColor.Text = LanguageHelper.GetString("gbColor");
            btnColor.Text = LanguageHelper.GetString("btnColor");
            gbPassword.Text = LanguageHelper.GetString("gbPassword");
            lblNewPass.Text = LanguageHelper.GetString("lblNewPass");
            lblNewPass2.Text = LanguageHelper.GetString("lblNewPass");
            lblOldPass1.Text = LanguageHelper.GetString("lblOldPass");
            btnChangePass.Text = LanguageHelper.GetString("btnChangePass");
            btnFont.Text = LanguageHelper.GetString("btnFont");
            lblNewTheme.Text = LanguageHelper.GetString("lblNewTheme");
            btnSaveTheme.Text = LanguageHelper.GetString("btnSaveTheme");
            gbLanguage.Text = LanguageHelper.GetString("gbLanguage");
            gbProfile.Text = LanguageHelper.GetString("gbProfile");
            lblUsername.Text = LanguageHelper.GetString("lblUsername");
            lblEmail.Text = LanguageHelper.GetString("lblEmail");
            lblPhone.Text = LanguageHelper.GetString("lblPhone");
            btnSaveProfile.Text = LanguageHelper.GetString("btnSaveProfile");
            lblLastName.Text = LanguageHelper.GetString("lblLastName");
            lblName.Text = LanguageHelper.GetString("lblName");
        }
        private void VisiblePassSettings(bool hide)
        {

            lblNewPass.Visible = hide;
            txtNewPass.Visible = hide;
            lblNewPass2.Visible = hide;
            txtNewPass.Clear();
            pnlNewPass.Visible = hide;
            pnlNewPass2.Visible = hide;
            txtNewPass2.Visible = hide;
        }
        private void VisibleProfileSettings(bool hide)
        {
            btnSaveProfile.Visible = hide;
        }
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtOldPass.Text) && Password.Verify(txtOldPass.Text, user.Password))
            {
                VisiblePassSettings(true);
            }
            else
            {
                VisiblePassSettings(false);
            }

            if (txtUsername.Text != user.UserName || txtEmail.Text != user.Email || txtPhone.Text != user.PhoneNumber
                || txtName.Text != user.FirstName || txtLastName.Text != user.LastName)
            {
                if (!string.IsNullOrWhiteSpace(txtUsername.Text) && !string.IsNullOrWhiteSpace(txtEmail.Text)
                    && !string.IsNullOrWhiteSpace(txtPhone.Text) && !string.IsNullOrWhiteSpace(txtName.Text)
                    && !string.IsNullOrWhiteSpace(txtLastName.Text))
                {
                    VisibleProfileSettings(true);
                }
            }
            else
            {
                VisibleProfileSettings(false);
            }
        }

        private void btnChangePass_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNewPass.Text) && !string.IsNullOrWhiteSpace(txtNewPass2.Text) &&
              txtNewPass.Text != txtOldPass.Text && txtNewPass2.Text == txtNewPass.Text)
            {
                userDAO.ChangePassword(ref user, txtNewPass.Text);
                VisiblePassSettings(false);
                txtOldPass.Clear();
                txtNewPass2.Clear();
                txtNewPass.Clear();
                btnChangePass.Visible = false;
            }
            else
            {
                btnChangePass.Visible = true;
            }
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                lblNewTheme.AutoSize = false;
                lblNewTheme.BackColor = colorDialog.Color;
                userColor = colorDialog.Color;
                lblNewTheme.Font = userFont;
            }
        }

        private void btnFont_Click(object sender, EventArgs e)
        {
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                lblNewTheme.AutoSize = true;
                lblNewTheme.Font = fontDialog.Font;
                userFont = fontDialog.Font;
            }
        }
        private void gbPass_Resize(object sender, EventArgs e)
        {
            txtOldPass.Width = gbPassword.ClientSize.Width - txtOldPass.Margin.Horizontal - lblOldPass1.Width - 32;
            txtNewPass2.Width = gbPassword.ClientSize.Width - txtNewPass2.Margin.Horizontal - lblNewPass2.Width - 32;
            txtNewPass.Width = gbPassword.ClientSize.Width - txtNewPass.Margin.Horizontal - lblNewPass.Width - 32;

            pnlOldPass1.Width = txtOldPass.Width + lblNewPass.Width;
            pnlNewPass.Width = txtOldPass.Width + lblNewPass.Width;
            pnlNewPass2.Width = txtOldPass.Width + lblNewPass.Width;
        }
        private void gbProfile_Resize(object sender, EventArgs e)
        {
            txtUsername.Width = gbProfile.ClientSize.Width - txtUsername.Margin.Horizontal - lblUsername.Width - 32;
            txtEmail.Width = gbProfile.ClientSize.Width - txtEmail.Margin.Horizontal - lblEmail.Width - 32;
            txtPhone.Width = gbProfile.ClientSize.Width - txtPhone.Margin.Horizontal - lblPhone.Width - 32;
            txtName.Width = gbProfile.ClientSize.Width - txtName.Margin.Horizontal - lblName.Width - 32;
            txtLastName.Width = gbProfile.ClientSize.Width - txtLastName.Margin.Horizontal - lblLastName.Width - 32;

            pnlUsername.Width = txtUsername.Width + lblUsername.Width;
            pblEmail.Width = txtEmail.Width + lblEmail.Width;
            pnlPhone.Width = txtPhone.Width + lblPhone.Width;
            pnlLastName.Width = txtLastName.Width + lblLastName.Width;
            pnlName.Width = txtName.Width + lblName.Width;

        }


        private void btnSaveTheme_Click(object sender, EventArgs e)
        {
            theme.ColorName = $"{userColor.A},{userColor.R},{userColor.G},{userColor.B}";

            theme.FontStyle = userFont.Style.ToString();
            theme.IsUnderline = userFont.Underline;
            theme.IsStrikeout = userFont.Strikeout;
            theme.FontName = userFont.Name;
            lblNewTheme.Font = userFont;
            theme.Size = (int)userFont.Size + 1;
            themeDAO.ChangeTheme(theme);
            LoadSettings.ApplySettins(user, this);
            form.ChangeText();
        }

        private void cbLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                string newLanguage = cbLanguage.SelectedItem.ToString();
                List<Database.DTO.Language> list = languageDAO.GetAll();
                int newId = list.FirstOrDefault(lang => lang.Name == newLanguage)?.Id ?? 1;
                userDAO.ChangeLanguage(ref user, newId);
                LoadSettings.ApplySettins(user, this);
                ChangeText();
                form.ChangeText();
            }
        }

        private void btnSaveProfile_Click(object sender, EventArgs e)
        {

            user.UserName = txtUsername.Text;
            user.Email = txtEmail.Text;
            user.PhoneNumber = txtPhone.Text;
            user.FirstName = txtName.Text;
            user.LastName = txtLastName.Text;

            userDAO.SaveUser(user);
            txtUsername.Text = user.UserName;
            txtEmail.Text = user.Email;
            txtPhone.Text = user.PhoneNumber;
            txtName.Text = user.FirstName;
            txtLastName.Text = user.LastName;
            VisibleProfileSettings(false);

        }
    }
}
