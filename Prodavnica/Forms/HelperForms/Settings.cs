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

            txtUsername.Text = user.userName;
            txtUsername.TextChanged += TextBox_TextChanged;
            txtEmail.Text = user.email;
            txtEmail.TextChanged += TextBox_TextChanged;
            txtPhone.Text = user.phoneNumber;
            txtPhone.TextChanged += TextBox_TextChanged;
            txtName.Text = user.firstName;
            txtName.TextChanged += TextBox_TextChanged;
            txtLastName.Text = user.lastName;
            txtLastName.TextChanged += TextBox_TextChanged;
            gbProfile.Resize += gbProfile_Resize;

            txtNewPass.TextChanged += TextBoxNewPass_TextChanged;
            txtNewPass2.TextChanged += TextBoxNewPass_TextChanged;

            this.user = user;
            theme = themeDAO.FindById(user.idTheme);
            userColor = themeDAO.GetColor(theme);
            userFont = themeDAO.GetFont(theme);
            languages = languageDAO.GetLanguages();
            cbLanguage.DataSource = languages;
            cbLanguage.SelectedItem = languageDAO.GetSelectedLanguageName(user.idLangugae);
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
            if (!string.IsNullOrWhiteSpace(txtOldPass.Text) && Password.Verify(txtOldPass.Text, user.password))
            {
                VisiblePassSettings(true);
            }
            else
            {
                VisiblePassSettings(false);
            }

            if (txtUsername.Text != user.userName || txtEmail.Text != user.email || txtPhone.Text != user.phoneNumber
                || txtName.Text != user.firstName || txtLastName.Text != user.lastName)
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
            theme.colorName = $"{userColor.A},{userColor.R},{userColor.G},{userColor.B}";

            theme.fontStyle = userFont.Style.ToString();
            theme.isUnderline = userFont.Underline;
            theme.isStrikeout = userFont.Strikeout;
            theme.fontName = userFont.Name;
            lblNewTheme.Font = userFont;
            theme.size = (int)userFont.Size + 1;
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
                int newId = list.FirstOrDefault(lang => lang.name == newLanguage)?.id ?? 1;
                userDAO.ChangeLanguage(ref user, newId);
                LoadSettings.ApplySettins(user, this);
                ChangeText();
                form.ChangeText();
            }
        }

        private void btnSaveProfile_Click(object sender, EventArgs e)
        {

            user.userName = txtUsername.Text;
            user.email = txtEmail.Text;
            user.phoneNumber = txtPhone.Text;
            user.firstName = txtName.Text;
            user.lastName = txtLastName.Text;

            userDAO.SaveUser(user);
            txtUsername.Text = user.userName;
            txtEmail.Text = user.email;
            txtPhone.Text = user.phoneNumber;
            txtName.Text = user.firstName;
            txtLastName.Text = user.lastName;
            VisibleProfileSettings(false);

        }
    }
}
