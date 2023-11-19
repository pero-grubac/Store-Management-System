﻿using OnlineKupovinaGUI;
using Prodavnica.Database.DTO;
using Prodavnica.Database.Repository;
using Prodavnica.Language;
using Prodavnica.Util;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Prodavnica.Forms
{
    public partial class AdministratorForm : Form
    {
        private Button currentButton;
        private Random random;
        private int tempIndex;
        private Form activeForm;
        private User user;
        private UserDAOImpl userDAO = new UserDAOImpl();
        public AdministratorForm(ref User user)
        {
            InitializeComponent();
            random = new Random();
            btnCloseChldForm.Visible = false;
            this.Text = string.Empty;
            this.ControlBox = false;
            MaximizedBounds = Screen.FromHandle(Handle).WorkingArea;
            this.user = user;
            ChangeText();
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
            user = userDAO.FindById(user.id);
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
            OpenChildForm(new Forms.HelperForms.Admin.Store(), sender, btnStore);
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.HelperForms.Admin.Employee(), sender, btnStaff);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            AdministratorForm parent = this;
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
    }
}