using OnlineKupovinaGUI;
using Prodavnica.Database.DTO;
using Prodavnica.Database.Repository;
using Prodavnica.Language;
using Prodavnica.Util;
using System.Runtime.InteropServices;

namespace Prodavnica.Forms
{
    public partial class MainFOrm : Form
    {
        private Button currentButton;
        private Random random;
        private int tempIndex;
        private Form activeForm;
        private User user;
        private UserDAOImpl userDAO = new UserDAOImpl();
        private Point mouseLocation;
        public MainFOrm(ref User user)
        {
            InitializeComponent();
            random = new Random();
            btnCloseChldForm.Visible = false;
            this.Text = string.Empty;
            MaximizedBounds = Screen.FromHandle(Handle).WorkingArea;
            this.user = user;
            ChangeText();
            /*   btnProducer.Enabled = false;
               btnProducer.Visible = false;
               AKO NISI ADMIN
             */
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        public void ChangeText()
        {
            LoadSettings.ApplySettins(user, this);
            panelTitleBar.BackColor = btnClose.BackColor;
            lblTitle.Text = LanguageHelper.GetString("lblTitle");
            lbl.Text = LanguageHelper.GetString("lbl");
            btnStore.Text = LanguageHelper.GetString("btnStore");
            btnStaff.Text = LanguageHelper.GetString("btnStaff");
            btnLogOut.Text = LanguageHelper.GetString("btnLogOut");
            btnSettings.Text = LanguageHelper.GetString("btnSettings");
            btnSupplier.Text = LanguageHelper.GetString("btnSupplier");
            btnProducer.Text = LanguageHelper.GetString("btnProducer");

        }

        private void OpenChildForm(System.Windows.Forms.Form childForm, object btnSender, Button button)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            btnCloseChldForm.Visible = true;
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelDesktop.Controls.Add(childForm);
            panelDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            lblTitle.Text = button.Text;
        }

        private void Reset()
        {
            lblTitle.Text = LanguageHelper.GetString("lblTitle");
            currentButton = null;
            btnCloseChldForm.Visible = false;
            user = userDAO.FindById(user.Id);
            ChangeText();
            panelTitleBar.BackColor = btnClose.BackColor;
        }
        private void btnCloseChldForm_Click(object sender, EventArgs e)
        {
            if (activeForm != null)
                activeForm.Close();
            Reset();
        }
        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState |= FormWindowState.Minimized;

        }

        private void btnMax_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                this.WindowState = FormWindowState.Maximized;
            else
                this.WindowState = FormWindowState.Normal;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnStore_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.HelperForms.Admin.StoreTable(user), sender, btnStore);
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.HelperForms.Admin.Employee(user), sender, btnStaff);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            MainFOrm parent = this;
            OpenChildForm(new Forms.HelperForms.Settings(ref user, ref parent), sender, btnSettings);
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            GUIUtil.ShowNextForm(this, new Login());
        }

        private void btnCloseChldForm_Click_1(object sender, EventArgs e)
        {
            if (activeForm != null)
                activeForm.Close();
            Reset();
        }

        private void mouse_Down(object sender, MouseEventArgs e)
        {
            mouseLocation = new Point(-e.X, -e.Y);
        }

        private void mouse_Move(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mousePosition = Control.MousePosition;
                mousePosition.Offset(mouseLocation.X, mouseLocation.Y);
                Location = mousePosition;
            }
        }

        private void btnProducer_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.HelperForms.Admin.Producer(user, true), sender, btnProducer);
        }

        private void btnProcurement_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.HelperForms.Admin.Producer(user, false), sender, btnSupplier);
        }
    }
}
