using Prodavnica.Database.DTO;
using Prodavnica.Database.Repository;
using Prodavnica.Language;
using Prodavnica.Util;

namespace Prodavnica.Forms.HelperForms.Admin
{
    public partial class Employee : Form
    {
        private User user;
        private List<User> users;
        private UserDAOImpl userDAO = new UserDAOImpl();
        public Employee(User user)
        {
            InitializeComponent();
            this.user = user;
            gbUsers.Resize += gbUsers_Resize;
            SetDataToDGV();
            LoadSettings.ApplySettins(user, this);
            ChangeText();
        }
        private void SetDataToDGV()
        {
            users = userDAO.GetAll(user.Id);
            dgvUsers.DataSource = null;
            dgvUsers.DataSource = users;
            dgvUsers.Refresh();
        }
        private void gbUsers_Resize(object? sender, EventArgs e)
        {
            int padding = 5;
            int margin = 10;
            int totalWidth = gbUsers.ClientSize.Width;
            int usedWidth = margin;

            List<System.Windows.Forms.Button> buttons = gbUsers.Controls.OfType<System.Windows.Forms.Button>().OrderBy(b => b.Location.X).ToList();
            Label labelControl = gbUsers.Controls.OfType<Label>().FirstOrDefault();
            System.Windows.Forms.TextBox textBox = gbUsers.Controls.OfType<System.Windows.Forms.TextBox>().FirstOrDefault();

            foreach (System.Windows.Forms.Button btn in buttons)
            {
                int btnWidth = TextRenderer.MeasureText(btn.Text, btn.Font).Width + padding * 2;
                btn.Width = btnWidth;
                btn.Location = new Point(usedWidth, btn.Location.Y);
                usedWidth += btnWidth + padding;
            }

            if (labelControl != null)
            {
                int labelWidth = TextRenderer.MeasureText(labelControl.Text, labelControl.Font).Width + padding * 2;
                labelControl.Width = labelWidth;
                labelControl.Location = new Point(usedWidth, labelControl.Location.Y);
                usedWidth += labelWidth + padding;
            }

            if (textBox != null)
            {
                int textBoxWidth = totalWidth - usedWidth - margin;
                textBox.Width = textBoxWidth >= 0 ? textBoxWidth : 0;
                textBox.Location = new Point(usedWidth, textBox.Location.Y);
            }
            btnAdd.Height = btnUpdate.Height;
        }

        private void ChangeText()
        {
            btnAdd.Text = LanguageHelper.GetString("btnAdd");
            btnDelete.Text = LanguageHelper.GetString("btnDelete");
            btnUpdate.Text = LanguageHelper.GetString("btnUpdate");
            lblSearch.Text = LanguageHelper.GetString("lblSearch");

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            User u = new User();

            using (Popup.Staff.EmployeeDetails employeeDetails = new Popup.Staff.EmployeeDetails(user, u, false))
            {
                if (employeeDetails.ShowDialog() == DialogResult.OK)
                {
                    SetDataToDGV();
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }
    }
}
