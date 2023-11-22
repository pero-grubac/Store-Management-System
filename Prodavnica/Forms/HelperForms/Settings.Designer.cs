namespace Prodavnica.Forms.HelperForms
{
    partial class Settings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            colorDialog = new ColorDialog();
            fontDialog = new FontDialog();
            gbPassword = new GroupBox();
            btnChangePass = new Button();
            pnlNewPass = new Panel();
            txtNewPass = new TextBox();
            pnlNewPass2 = new Panel();
            lblNewPass = new Label();
            txtNewPass2 = new TextBox();
            pnlOldPass1 = new Panel();
            txtOldPass = new TextBox();
            lblNewPass2 = new Label();
            lblOldPass1 = new Label();
            gbColor = new GroupBox();
            btnSaveTheme = new Button();
            lblNewTheme = new Label();
            btnFont = new Button();
            btnColor = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            gbLanguage = new GroupBox();
            cbLanguage = new ComboBox();
            gbProfile = new GroupBox();
            pnlLastName = new Panel();
            txtLastName = new TextBox();
            lblLastName = new Label();
            pnlName = new Panel();
            txtName = new TextBox();
            lblName = new Label();
            btnSaveProfile = new Button();
            pnlPhone = new Panel();
            txtPhone = new TextBox();
            lblPhone = new Label();
            pblEmail = new Panel();
            txtEmail = new TextBox();
            lblEmail = new Label();
            pnlUsername = new Panel();
            txtUsername = new TextBox();
            lblUsername = new Label();
            gbPassword.SuspendLayout();
            gbColor.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            gbLanguage.SuspendLayout();
            gbProfile.SuspendLayout();
            SuspendLayout();
            // 
            // gbPassword
            // 
            gbPassword.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            gbPassword.BackColor = Color.White;
            gbPassword.Controls.Add(btnChangePass);
            gbPassword.Controls.Add(pnlNewPass);
            gbPassword.Controls.Add(txtNewPass);
            gbPassword.Controls.Add(pnlNewPass2);
            gbPassword.Controls.Add(lblNewPass);
            gbPassword.Controls.Add(txtNewPass2);
            gbPassword.Controls.Add(pnlOldPass1);
            gbPassword.Controls.Add(txtOldPass);
            gbPassword.Controls.Add(lblNewPass2);
            gbPassword.Controls.Add(lblOldPass1);
            gbPassword.Dock = DockStyle.Fill;
            gbPassword.Location = new Point(3, 3);
            gbPassword.Name = "gbPassword";
            gbPassword.Size = new Size(394, 264);
            gbPassword.TabIndex = 0;
            gbPassword.TabStop = false;
            gbPassword.Text = "gbPassword";
            // 
            // btnChangePass
            // 
            btnChangePass.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnChangePass.AutoSize = true;
            btnChangePass.FlatAppearance.BorderSize = 0;
            btnChangePass.FlatStyle = FlatStyle.Flat;
            btnChangePass.Location = new Point(280, 218);
            btnChangePass.Name = "btnChangePass";
            btnChangePass.Size = new Size(94, 30);
            btnChangePass.TabIndex = 16;
            btnChangePass.Text = "button1";
            btnChangePass.UseVisualStyleBackColor = true;
            btnChangePass.Click += btnChangePass_Click;
            // 
            // pnlNewPass
            // 
            pnlNewPass.BackColor = Color.FromArgb(0, 74, 173);
            pnlNewPass.ForeColor = Color.FromArgb(0, 74, 173);
            pnlNewPass.Location = new Point(9, 139);
            pnlNewPass.Name = "pnlNewPass";
            pnlNewPass.Size = new Size(365, 1);
            pnlNewPass.TabIndex = 15;
            // 
            // txtNewPass
            // 
            txtNewPass.BorderStyle = BorderStyle.None;
            txtNewPass.Font = new Font("Microsoft Sans Serif", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            txtNewPass.ForeColor = Color.FromArgb(0, 74, 173);
            txtNewPass.Location = new Point(177, 114);
            txtNewPass.Multiline = true;
            txtNewPass.Name = "txtNewPass";
            txtNewPass.PasswordChar = '*';
            txtNewPass.Size = new Size(200, 24);
            txtNewPass.TabIndex = 14;
            // 
            // pnlNewPass2
            // 
            pnlNewPass2.BackColor = Color.FromArgb(0, 74, 173);
            pnlNewPass2.ForeColor = Color.FromArgb(0, 74, 173);
            pnlNewPass2.Location = new Point(9, 99);
            pnlNewPass2.Name = "pnlNewPass2";
            pnlNewPass2.Size = new Size(365, 1);
            pnlNewPass2.TabIndex = 12;
            // 
            // lblNewPass
            // 
            lblNewPass.AutoSize = true;
            lblNewPass.Location = new Point(9, 114);
            lblNewPass.Name = "lblNewPass";
            lblNewPass.Size = new Size(83, 20);
            lblNewPass.TabIndex = 13;
            lblNewPass.Text = "lblNewPass";
            // 
            // txtNewPass2
            // 
            txtNewPass2.BorderStyle = BorderStyle.None;
            txtNewPass2.Font = new Font("Microsoft Sans Serif", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            txtNewPass2.ForeColor = Color.FromArgb(0, 74, 173);
            txtNewPass2.Location = new Point(177, 73);
            txtNewPass2.Multiline = true;
            txtNewPass2.Name = "txtNewPass2";
            txtNewPass2.PasswordChar = '*';
            txtNewPass2.Size = new Size(197, 24);
            txtNewPass2.TabIndex = 12;
            // 
            // pnlOldPass1
            // 
            pnlOldPass1.BackColor = Color.FromArgb(0, 74, 173);
            pnlOldPass1.ForeColor = Color.FromArgb(0, 74, 173);
            pnlOldPass1.Location = new Point(9, 59);
            pnlOldPass1.Name = "pnlOldPass1";
            pnlOldPass1.Size = new Size(365, 1);
            pnlOldPass1.TabIndex = 11;
            // 
            // txtOldPass
            // 
            txtOldPass.BorderStyle = BorderStyle.None;
            txtOldPass.Font = new Font("Microsoft Sans Serif", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            txtOldPass.ForeColor = Color.FromArgb(0, 74, 173);
            txtOldPass.Location = new Point(177, 34);
            txtOldPass.Multiline = true;
            txtOldPass.Name = "txtOldPass";
            txtOldPass.PasswordChar = '*';
            txtOldPass.Size = new Size(197, 24);
            txtOldPass.TabIndex = 10;
            // 
            // lblNewPass2
            // 
            lblNewPass2.AutoSize = true;
            lblNewPass2.Location = new Point(9, 77);
            lblNewPass2.Name = "lblNewPass2";
            lblNewPass2.Size = new Size(83, 20);
            lblNewPass2.TabIndex = 2;
            lblNewPass2.Text = "lblNewPass";
            // 
            // lblOldPass1
            // 
            lblOldPass1.AutoSize = true;
            lblOldPass1.Location = new Point(9, 36);
            lblOldPass1.Name = "lblOldPass1";
            lblOldPass1.Size = new Size(100, 20);
            lblOldPass1.TabIndex = 1;
            lblOldPass1.Text = "Old password";
            // 
            // gbColor
            // 
            gbColor.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            gbColor.BackColor = Color.White;
            gbColor.Controls.Add(btnSaveTheme);
            gbColor.Controls.Add(lblNewTheme);
            gbColor.Controls.Add(btnFont);
            gbColor.Controls.Add(btnColor);
            gbColor.Dock = DockStyle.Fill;
            gbColor.Location = new Point(3, 273);
            gbColor.MinimumSize = new Size(306, 190);
            gbColor.Name = "gbColor";
            gbColor.Size = new Size(394, 190);
            gbColor.TabIndex = 17;
            gbColor.TabStop = false;
            gbColor.Text = "gbColor";
            // 
            // btnSaveTheme
            // 
            btnSaveTheme.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSaveTheme.AutoSize = true;
            btnSaveTheme.FlatAppearance.BorderSize = 0;
            btnSaveTheme.FlatStyle = FlatStyle.Flat;
            btnSaveTheme.Location = new Point(280, 138);
            btnSaveTheme.Name = "btnSaveTheme";
            btnSaveTheme.Size = new Size(94, 30);
            btnSaveTheme.TabIndex = 4;
            btnSaveTheme.Text = "button1";
            btnSaveTheme.UseVisualStyleBackColor = true;
            btnSaveTheme.Click += btnSaveTheme_Click;
            // 
            // lblNewTheme
            // 
            lblNewTheme.Location = new Point(129, 91);
            lblNewTheme.Name = "lblNewTheme";
            lblNewTheme.Size = new Size(115, 20);
            lblNewTheme.TabIndex = 3;
            lblNewTheme.Text = "New theme";
            lblNewTheme.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnFont
            // 
            btnFont.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnFont.AutoSize = true;
            btnFont.FlatAppearance.BorderSize = 0;
            btnFont.FlatStyle = FlatStyle.Flat;
            btnFont.Location = new Point(280, 41);
            btnFont.Name = "btnFont";
            btnFont.Size = new Size(94, 30);
            btnFont.TabIndex = 2;
            btnFont.Text = "button1";
            btnFont.UseVisualStyleBackColor = true;
            btnFont.Click += btnFont_Click;
            // 
            // btnColor
            // 
            btnColor.AutoSize = true;
            btnColor.FlatAppearance.BorderSize = 0;
            btnColor.FlatStyle = FlatStyle.Flat;
            btnColor.Location = new Point(6, 41);
            btnColor.Name = "btnColor";
            btnColor.Size = new Size(94, 30);
            btnColor.TabIndex = 0;
            btnColor.Text = "btnColor";
            btnColor.UseVisualStyleBackColor = true;
            btnColor.Click += btnColor_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(gbColor, 0, 1);
            tableLayoutPanel1.Controls.Add(gbPassword, 0, 0);
            tableLayoutPanel1.Controls.Add(gbLanguage, 1, 1);
            tableLayoutPanel1.Controls.Add(gbProfile, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            tableLayoutPanel1.Size = new Size(800, 450);
            tableLayoutPanel1.TabIndex = 18;
            // 
            // gbLanguage
            // 
            gbLanguage.BackColor = Color.White;
            gbLanguage.Controls.Add(cbLanguage);
            gbLanguage.Dock = DockStyle.Fill;
            gbLanguage.Location = new Point(403, 273);
            gbLanguage.Name = "gbLanguage";
            gbLanguage.Size = new Size(394, 174);
            gbLanguage.TabIndex = 18;
            gbLanguage.TabStop = false;
            gbLanguage.Text = "gbLanguage";
            // 
            // cbLanguage
            // 
            cbLanguage.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            cbLanguage.BackColor = Color.White;
            cbLanguage.FormattingEnabled = true;
            cbLanguage.Location = new Point(60, 54);
            cbLanguage.Name = "cbLanguage";
            cbLanguage.Size = new Size(271, 28);
            cbLanguage.TabIndex = 0;
            cbLanguage.SelectedIndexChanged += cbLanguage_SelectedIndexChanged;
            // 
            // gbProfile
            // 
            gbProfile.BackColor = Color.White;
            gbProfile.Controls.Add(pnlLastName);
            gbProfile.Controls.Add(txtLastName);
            gbProfile.Controls.Add(lblLastName);
            gbProfile.Controls.Add(pnlName);
            gbProfile.Controls.Add(txtName);
            gbProfile.Controls.Add(lblName);
            gbProfile.Controls.Add(btnSaveProfile);
            gbProfile.Controls.Add(pnlPhone);
            gbProfile.Controls.Add(txtPhone);
            gbProfile.Controls.Add(lblPhone);
            gbProfile.Controls.Add(pblEmail);
            gbProfile.Controls.Add(txtEmail);
            gbProfile.Controls.Add(lblEmail);
            gbProfile.Controls.Add(pnlUsername);
            gbProfile.Controls.Add(txtUsername);
            gbProfile.Controls.Add(lblUsername);
            gbProfile.Dock = DockStyle.Fill;
            gbProfile.ForeColor = Color.Black;
            gbProfile.Location = new Point(403, 3);
            gbProfile.Name = "gbProfile";
            gbProfile.Size = new Size(394, 264);
            gbProfile.TabIndex = 19;
            gbProfile.TabStop = false;
            gbProfile.Text = "gbProfile";
            // 
            // pnlLastName
            // 
            pnlLastName.BackColor = Color.FromArgb(0, 74, 173);
            pnlLastName.ForeColor = Color.FromArgb(0, 74, 173);
            pnlLastName.Location = new Point(6, 198);
            pnlLastName.Name = "pnlLastName";
            pnlLastName.Size = new Size(365, 1);
            pnlLastName.TabIndex = 27;
            // 
            // txtLastName
            // 
            txtLastName.BorderStyle = BorderStyle.None;
            txtLastName.Font = new Font("Microsoft Sans Serif", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            txtLastName.ForeColor = Color.FromArgb(0, 74, 173);
            txtLastName.Location = new Point(160, 171);
            txtLastName.Multiline = true;
            txtLastName.Name = "txtLastName";
            txtLastName.Size = new Size(229, 24);
            txtLastName.TabIndex = 26;
            // 
            // lblLastName
            // 
            lblLastName.AutoSize = true;
            lblLastName.Location = new Point(3, 175);
            lblLastName.Name = "lblLastName";
            lblLastName.Size = new Size(76, 20);
            lblLastName.TabIndex = 25;
            lblLastName.Text = "Last name";
            // 
            // pnlName
            // 
            pnlName.BackColor = Color.FromArgb(0, 74, 173);
            pnlName.ForeColor = Color.FromArgb(0, 74, 173);
            pnlName.Location = new Point(6, 165);
            pnlName.Name = "pnlName";
            pnlName.Size = new Size(365, 1);
            pnlName.TabIndex = 24;
            // 
            // txtName
            // 
            txtName.BorderStyle = BorderStyle.None;
            txtName.Font = new Font("Microsoft Sans Serif", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            txtName.ForeColor = Color.FromArgb(0, 74, 173);
            txtName.Location = new Point(160, 140);
            txtName.Multiline = true;
            txtName.Name = "txtName";
            txtName.Size = new Size(211, 24);
            txtName.TabIndex = 23;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(3, 142);
            lblName.Name = "lblName";
            lblName.Size = new Size(77, 20);
            lblName.TabIndex = 22;
            lblName.Text = "First name";
            // 
            // btnSaveProfile
            // 
            btnSaveProfile.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSaveProfile.AutoSize = true;
            btnSaveProfile.FlatAppearance.BorderSize = 0;
            btnSaveProfile.FlatStyle = FlatStyle.Flat;
            btnSaveProfile.Location = new Point(274, 218);
            btnSaveProfile.Name = "btnSaveProfile";
            btnSaveProfile.Size = new Size(97, 30);
            btnSaveProfile.TabIndex = 21;
            btnSaveProfile.Text = "Save Profile";
            btnSaveProfile.UseVisualStyleBackColor = true;
            btnSaveProfile.Click += btnSaveProfile_Click;
            // 
            // pnlPhone
            // 
            pnlPhone.BackColor = Color.FromArgb(0, 74, 173);
            pnlPhone.ForeColor = Color.FromArgb(0, 74, 173);
            pnlPhone.Location = new Point(6, 127);
            pnlPhone.Name = "pnlPhone";
            pnlPhone.Size = new Size(365, 1);
            pnlPhone.TabIndex = 20;
            // 
            // txtPhone
            // 
            txtPhone.BorderStyle = BorderStyle.None;
            txtPhone.Font = new Font("Microsoft Sans Serif", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            txtPhone.ForeColor = Color.FromArgb(0, 74, 173);
            txtPhone.Location = new Point(160, 102);
            txtPhone.Multiline = true;
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(211, 24);
            txtPhone.TabIndex = 19;
            // 
            // lblPhone
            // 
            lblPhone.AutoSize = true;
            lblPhone.Location = new Point(3, 104);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(105, 20);
            lblPhone.TabIndex = 18;
            lblPhone.Text = "Phone number";
            // 
            // pblEmail
            // 
            pblEmail.BackColor = Color.FromArgb(0, 74, 173);
            pblEmail.ForeColor = Color.FromArgb(0, 74, 173);
            pblEmail.Location = new Point(6, 89);
            pblEmail.Name = "pblEmail";
            pblEmail.Size = new Size(365, 1);
            pblEmail.TabIndex = 17;
            // 
            // txtEmail
            // 
            txtEmail.BorderStyle = BorderStyle.None;
            txtEmail.Font = new Font("Microsoft Sans Serif", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            txtEmail.ForeColor = Color.FromArgb(0, 74, 173);
            txtEmail.Location = new Point(160, 64);
            txtEmail.Multiline = true;
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(211, 24);
            txtEmail.TabIndex = 16;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(3, 66);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(46, 20);
            lblEmail.TabIndex = 15;
            lblEmail.Text = "Email";
            // 
            // pnlUsername
            // 
            pnlUsername.BackColor = Color.FromArgb(0, 74, 173);
            pnlUsername.ForeColor = Color.FromArgb(0, 74, 173);
            pnlUsername.Location = new Point(6, 59);
            pnlUsername.Name = "pnlUsername";
            pnlUsername.Size = new Size(365, 1);
            pnlUsername.TabIndex = 14;
            // 
            // txtUsername
            // 
            txtUsername.BorderStyle = BorderStyle.None;
            txtUsername.Font = new Font("Microsoft Sans Serif", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            txtUsername.ForeColor = Color.FromArgb(0, 74, 173);
            txtUsername.Location = new Point(160, 34);
            txtUsername.Multiline = true;
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(211, 24);
            txtUsername.TabIndex = 13;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(3, 36);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(75, 20);
            lblUsername.TabIndex = 12;
            lblUsername.Text = "Username";
            // 
            // Settings
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tableLayoutPanel1);
            Name = "Settings";
            Text = "Settings";
            gbPassword.ResumeLayout(false);
            gbPassword.PerformLayout();
            gbColor.ResumeLayout(false);
            gbColor.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            gbLanguage.ResumeLayout(false);
            gbProfile.ResumeLayout(false);
            gbProfile.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ColorDialog colorDialog;
        private FontDialog fontDialog;
        private GroupBox gbPassword;
        private Label lblOldPass1;
        private Label lblNewPass2;
        private TextBox txtOldPass;
        private Panel pnlOldPass1;
        private Panel pnlNewPass2;
        private TextBox txtNewPass2;
        private Panel pnlNewPass;
        private TextBox txtNewPass;
        private Label lblNewPass;
        private Button btnChangePass;
        private GroupBox gbColor;
        private Button btnColor;
        private Button btnFont;
        private TableLayoutPanel tableLayoutPanel1;
        private Label lblNewTheme;
        private Button btnSaveTheme;
        private GroupBox gbLanguage;
        private ComboBox cbLanguage;
        private GroupBox gbProfile;
        private Panel pnlUsername;
        private TextBox txtUsername;
        private Label lblUsername;
        private Panel pnlPhone;
        private TextBox txtPhone;
        private Label lblPhone;
        private Panel pblEmail;
        private TextBox txtEmail;
        private Label lblEmail;
        private Button btnSaveProfile;
        private Panel pnlName;
        private TextBox txtName;
        private Label lblName;
        private Panel pnlLastName;
        private TextBox txtLastName;
        private Label lblLastName;
    }
}