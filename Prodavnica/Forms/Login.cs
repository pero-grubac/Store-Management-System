﻿using Prodavnica;
using Prodavnica.Database.DTO;
using Prodavnica.Database.Repository;
using Prodavnica.Forms;
using Prodavnica.Language;
using Prodavnica.Util;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace OnlineKupovinaGUI
{
    public partial class Login : System.Windows.Forms.Form
    {
        private UserDAOImpl userDAOImpl = new UserDAOImpl();

        public Login()
        {
            InitializeComponent();
            lblExit.Height = lblClearFields.Height;
        }
        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(txtPassword.Text) && !string.IsNullOrEmpty(txtUserName.Text))
            {
                string userName = txtUserName.Text;
                string password = txtPassword.Text;
                User user = userDAOImpl.GetUser(userName, password);
                if (user != null)
                {
                    GUIUtil.ShowNextForm(this, new MainFOrm(ref user));
                }
                else
                {
                    txtPassword.Clear();
                    txtUserName.Clear();
                    txtUserName.Focus();
                }
            }
        }

        private void lblClearFields_Click(object sender, EventArgs e)
        {

            txtPassword.Clear();
            txtUserName.Clear();
            txtUserName.Focus();
        }
    }
}