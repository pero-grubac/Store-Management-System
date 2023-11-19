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
            pnlOldPass2 = new Panel();
            lblNewPass = new Label();
            txtOldPass2 = new TextBox();
            pnlOldPass1 = new Panel();
            txtOldPass = new TextBox();
            lblOldPass2 = new Label();
            lblOldPass1 = new Label();
            gbColor = new GroupBox();
            btnSaveTheme = new Button();
            lblNewTheme = new Label();
            btnFont = new Button();
            lblOldTheme = new Label();
            btnColor = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            gbPassword.SuspendLayout();
            gbColor.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // gbPassword
            // 
            gbPassword.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            gbPassword.BackColor = Color.White;
            gbPassword.Controls.Add(btnChangePass);
            gbPassword.Controls.Add(pnlNewPass);
            gbPassword.Controls.Add(txtNewPass);
            gbPassword.Controls.Add(pnlOldPass2);
            gbPassword.Controls.Add(lblNewPass);
            gbPassword.Controls.Add(txtOldPass2);
            gbPassword.Controls.Add(pnlOldPass1);
            gbPassword.Controls.Add(txtOldPass);
            gbPassword.Controls.Add(lblOldPass2);
            gbPassword.Controls.Add(lblOldPass1);
            gbPassword.Dock = DockStyle.Fill;
            gbPassword.Location = new Point(3, 3);
            gbPassword.Name = "gbPassword";
            gbPassword.Size = new Size(394, 219);
            gbPassword.TabIndex = 0;
            gbPassword.TabStop = false;
            gbPassword.Text = "groupBox1";
            // 
            // btnChangePass
            // 
            btnChangePass.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnChangePass.AutoSize = true;
            btnChangePass.FlatAppearance.BorderSize = 0;
            btnChangePass.FlatStyle = FlatStyle.Flat;
            btnChangePass.Location = new Point(280, 173);
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
            pnlNewPass.Location = new Point(9, 134);
            pnlNewPass.Name = "pnlNewPass";
            pnlNewPass.Size = new Size(365, 1);
            pnlNewPass.TabIndex = 15;
            // 
            // txtNewPass
            // 
            txtNewPass.BorderStyle = BorderStyle.None;
            txtNewPass.Font = new Font("Microsoft Sans Serif", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            txtNewPass.ForeColor = Color.FromArgb(0, 74, 173);
            txtNewPass.Location = new Point(112, 106);
            txtNewPass.Multiline = true;
            txtNewPass.Name = "txtNewPass";
            txtNewPass.PasswordChar = '*';
            txtNewPass.Size = new Size(262, 24);
            txtNewPass.TabIndex = 14;
            // 
            // pnlOldPass2
            // 
            pnlOldPass2.BackColor = Color.FromArgb(0, 74, 173);
            pnlOldPass2.ForeColor = Color.FromArgb(0, 74, 173);
            pnlOldPass2.Location = new Point(9, 89);
            pnlOldPass2.Name = "pnlOldPass2";
            pnlOldPass2.Size = new Size(365, 1);
            pnlOldPass2.TabIndex = 12;
            // 
            // lblNewPass
            // 
            lblNewPass.AutoSize = true;
            lblNewPass.Location = new Point(6, 108);
            lblNewPass.Name = "lblNewPass";
            lblNewPass.Size = new Size(83, 20);
            lblNewPass.TabIndex = 13;
            lblNewPass.Text = "lblNewPass";
            // 
            // txtOldPass2
            // 
            txtOldPass2.BorderStyle = BorderStyle.None;
            txtOldPass2.Font = new Font("Microsoft Sans Serif", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            txtOldPass2.ForeColor = Color.FromArgb(0, 74, 173);
            txtOldPass2.Location = new Point(112, 70);
            txtOldPass2.Multiline = true;
            txtOldPass2.Name = "txtOldPass2";
            txtOldPass2.PasswordChar = '*';
            txtOldPass2.Size = new Size(262, 24);
            txtOldPass2.TabIndex = 12;
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
            txtOldPass.Location = new Point(112, 34);
            txtOldPass.Multiline = true;
            txtOldPass.Name = "txtOldPass";
            txtOldPass.PasswordChar = '*';
            txtOldPass.Size = new Size(262, 24);
            txtOldPass.TabIndex = 10;
            // 
            // lblOldPass2
            // 
            lblOldPass2.AutoSize = true;
            lblOldPass2.Location = new Point(6, 70);
            lblOldPass2.Name = "lblOldPass2";
            lblOldPass2.Size = new Size(77, 20);
            lblOldPass2.TabIndex = 2;
            lblOldPass2.Text = "lblOldPass";
            // 
            // lblOldPass1
            // 
            lblOldPass1.AutoSize = true;
            lblOldPass1.Location = new Point(6, 36);
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
            gbColor.Controls.Add(lblOldTheme);
            gbColor.Controls.Add(btnColor);
            gbColor.Dock = DockStyle.Fill;
            gbColor.Location = new Point(3, 228);
            gbColor.MinimumSize = new Size(306, 190);
            gbColor.Name = "gbColor";
            gbColor.Size = new Size(394, 219);
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
            btnSaveTheme.Location = new Point(280, 180);
            btnSaveTheme.Name = "btnSaveTheme";
            btnSaveTheme.Size = new Size(94, 30);
            btnSaveTheme.TabIndex = 4;
            btnSaveTheme.Text = "button1";
            btnSaveTheme.UseVisualStyleBackColor = true;
            btnSaveTheme.Click += btnSaveTheme_Click;
            // 
            // lblNewTheme
            // 
            lblNewTheme.AutoSize = true;
            lblNewTheme.Location = new Point(9, 154);
            lblNewTheme.Name = "lblNewTheme";
            lblNewTheme.Size = new Size(50, 20);
            lblNewTheme.TabIndex = 3;
            lblNewTheme.Text = "label1";
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
            // lblOldTheme
            // 
            lblOldTheme.AutoSize = true;
            lblOldTheme.Location = new Point(9, 97);
            lblOldTheme.MinimumSize = new Size(77, 30);
            lblOldTheme.Name = "lblOldTheme";
            lblOldTheme.Size = new Size(77, 30);
            lblOldTheme.TabIndex = 1;
            lblOldTheme.Text = "label1";
            // 
            // btnColor
            // 
            btnColor.AutoSize = true;
            btnColor.FlatAppearance.BorderSize = 0;
            btnColor.FlatStyle = FlatStyle.Flat;
            btnColor.Location = new Point(9, 41);
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
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(800, 450);
            tableLayoutPanel1.TabIndex = 18;
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
            ResumeLayout(false);
        }

        #endregion

        private ColorDialog colorDialog;
        private FontDialog fontDialog;
        private GroupBox gbPassword;
        private Label lblOldPass1;
        private Label lblOldPass2;
        private TextBox txtOldPass;
        private Panel pnlOldPass1;
        private Panel pnlOldPass2;
        private TextBox txtOldPass2;
        private Panel pnlNewPass;
        private TextBox txtNewPass;
        private Label lblNewPass;
        private Button btnChangePass;
        private GroupBox gbColor;
        private Button btnColor;
        private Label lblOldTheme;
        private Button btnFont;
        private TableLayoutPanel tableLayoutPanel1;
        private Label lblNewTheme;
        private Button btnSaveTheme;
    }
}