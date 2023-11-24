using Prodavnica;
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
        }
        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {

            string userName = txtUserName.Text;
            string password = txtPassword.Text;
            User user = userDAOImpl.GetUser("admin", "a");
            if (user != null)
            {
                if (user.IsAdmin)
                {
                    GUIUtil.ShowNextForm(this, new MainFOrm(ref user));
                }
            }
            else
                MessageBox.Show("fy");
        }

        private void lblClearFields_Click(object sender, EventArgs e)
        {

            txtPassword.Clear();
            txtUserName.Clear();
            txtUserName.Focus();
        }
    }
}