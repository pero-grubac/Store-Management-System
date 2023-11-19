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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prodavnica.Forms.HelperForms
{
    public partial class Settings : Form
    {
        private UserDAOImpl userDAO = new UserDAOImpl();
        private ThemeDAOImpl themeDAO = new ThemeDAOImpl();
        private User user;
        private Theme theme;
        private Color userColor;
        private Font userFont;
        public Settings(User user)
        {
            InitializeComponent();
            ChangeText();
            VisiblePassSettings(false);
            txtOldPass.TextChanged += TextBox_TextChanged;
            txtOldPass2.TextChanged += TextBox_TextChanged;
            gbPassword.Resize += gbPass_Resize;
            gbColor.Resize += gbColor_Resize;
            this.user = user;
            theme = themeDAO.FindById(user.idTheme);
            userColor = themeDAO.GetColor(theme);
            userFont = themeDAO.GetFont(theme);
            lblOldTheme.BackColor = userColor;
            lblOldTheme.Font = userFont;
            LoadSettings.ApplySettins(user, this);
        }

        private void ChangeText()
        {
            gbColor.Text = LanguageHelper.GetString("gbColor");
            btnColor.Text = LanguageHelper.GetString("btnColor");
            lblOldTheme.Text = LanguageHelper.GetString("lblOldTheme");
            gbPassword.Text = LanguageHelper.GetString("gbPassword");
            lblNewPass.Text = LanguageHelper.GetString("lblNewPass");
            lblOldPass1.Text = LanguageHelper.GetString("lblOldPass");
            lblOldPass2.Text = LanguageHelper.GetString("lblOldPass");
            btnChangePass.Text = LanguageHelper.GetString("btnChangePass");
            btnFont.Text = LanguageHelper.GetString("btnFont");
            lblNewTheme.Text = LanguageHelper.GetString("lblNewTheme");
            btnSaveTheme.Text = LanguageHelper.GetString("btnSaveTheme");
        }
        private void VisiblePassSettings(bool hide)
        {
            btnChangePass.Visible = hide;
            lblNewPass.Visible = hide;
            txtNewPass.Visible = hide;
            txtNewPass.Clear();
            pnlNewPass.Visible = hide;
        }
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (txtOldPass2 != null && txtOldPass != null && txtOldPass.Text == txtOldPass2.Text && Password.Verify(txtOldPass.Text, user.password))
            {
                VisiblePassSettings(true);
            }
            else
            {
                VisiblePassSettings(false);
            }
        }

        private void btnChangePass_Click(object sender, EventArgs e)
        {
            if (txtNewPass.Text != txtOldPass.Text)
            {
                userDAO.changePassword(user, txtNewPass.Text);
            }
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                lblNewTheme.BackColor = colorDialog.Color;
                userColor = colorDialog.Color;
            }
        }

        private void btnFont_Click(object sender, EventArgs e)
        {
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                lblNewTheme.Font = fontDialog.Font;
                userFont = fontDialog.Font;
            }
        }
        private void gbPass_Resize(object sender, EventArgs e)
        {
            txtOldPass.Width = gbPassword.ClientSize.Width - txtOldPass.Margin.Horizontal - lblOldPass1.Width - 32;
            txtOldPass2.Width = gbPassword.ClientSize.Width - txtOldPass2.Margin.Horizontal - lblOldPass2.Width - 32;
            txtNewPass.Width = gbPassword.ClientSize.Width - txtNewPass.Margin.Horizontal - lblNewPass.Width - 32;

            pnlOldPass1.Width = txtOldPass.Width + lblNewPass.Width;
            pnlNewPass.Width = txtOldPass.Width + lblNewPass.Width;
            pnlOldPass2.Width = txtOldPass.Width + lblNewPass.Width;
        }
        private void gbColor_Resize(object sender, EventArgs e)
        {
            lblOldTheme.Width = gbColor.ClientSize.Width;
        }

        private void btnSaveTheme_Click(object sender, EventArgs e)
        {
            theme.colorName = userColor.Name;
            theme.fontStyle = userFont.Style.ToString();
            theme.isUnderline = userFont.Underline;
            theme.isStrikeout = userFont.Strikeout;
            theme.fontName = userFont.Name;
            theme.size = (int)userFont.Size + 1;
            themeDAO.ChangeTheme(theme);
            LoadSettings.ApplySettins(user, this);
        }
    }
}
