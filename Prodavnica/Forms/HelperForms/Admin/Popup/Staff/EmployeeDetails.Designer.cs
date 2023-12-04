namespace Prodavnica.Forms.HelperForms.Admin.Popup.Staff
{
    partial class EmployeeDetails
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
            lblUsername = new Label();
            txtUsername = new TextBox();
            txtFirstName = new TextBox();
            lblFirstName = new Label();
            txtLastName = new TextBox();
            lblLastName = new Label();
            txtNumber = new TextBox();
            lblNumber = new Label();
            txtEmail = new TextBox();
            lblEmail = new Label();
            txtPassword = new TextBox();
            lblPassword = new Label();
            btnSave = new Button();
            btnCancel = new Button();
            lblWarrning = new Label();
            SuspendLayout();
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(27, 36);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(75, 20);
            lblUsername.TabIndex = 0;
            lblUsername.Text = "Username";
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(143, 33);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(292, 27);
            txtUsername.TabIndex = 1;
            // 
            // txtFirstName
            // 
            txtFirstName.Location = new Point(143, 83);
            txtFirstName.Name = "txtFirstName";
            txtFirstName.Size = new Size(292, 27);
            txtFirstName.TabIndex = 3;
            // 
            // lblFirstName
            // 
            lblFirstName.AutoSize = true;
            lblFirstName.Location = new Point(27, 86);
            lblFirstName.Name = "lblFirstName";
            lblFirstName.Size = new Size(77, 20);
            lblFirstName.TabIndex = 2;
            lblFirstName.Text = "First name";
            // 
            // txtLastName
            // 
            txtLastName.Location = new Point(143, 133);
            txtLastName.Name = "txtLastName";
            txtLastName.Size = new Size(292, 27);
            txtLastName.TabIndex = 5;
            // 
            // lblLastName
            // 
            lblLastName.AutoSize = true;
            lblLastName.Location = new Point(27, 136);
            lblLastName.Name = "lblLastName";
            lblLastName.Size = new Size(76, 20);
            lblLastName.TabIndex = 4;
            lblLastName.Text = "Last name";
            // 
            // txtNumber
            // 
            txtNumber.Location = new Point(143, 183);
            txtNumber.Name = "txtNumber";
            txtNumber.Size = new Size(292, 27);
            txtNumber.TabIndex = 7;
            // 
            // lblNumber
            // 
            lblNumber.AutoSize = true;
            lblNumber.Location = new Point(27, 186);
            lblNumber.Name = "lblNumber";
            lblNumber.Size = new Size(105, 20);
            lblNumber.TabIndex = 6;
            lblNumber.Text = "Phone number";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(143, 233);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(292, 27);
            txtEmail.TabIndex = 9;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(27, 236);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(46, 20);
            lblEmail.TabIndex = 8;
            lblEmail.Text = "Email";
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(143, 283);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(292, 27);
            txtPassword.TabIndex = 11;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(27, 286);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(70, 20);
            lblPassword.TabIndex = 10;
            lblPassword.Text = "Password";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(212, 374);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(94, 29);
            btnSave.TabIndex = 12;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(341, 374);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(94, 29);
            btnCancel.TabIndex = 13;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // lblWarrning
            // 
            lblWarrning.AutoSize = true;
            lblWarrning.Location = new Point(82, 341);
            lblWarrning.Name = "lblWarrning";
            lblWarrning.Size = new Size(50, 20);
            lblWarrning.TabIndex = 14;
            lblWarrning.Text = "label1";
            // 
            // EmployeeDetails
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(466, 430);
            ControlBox = false;
            Controls.Add(lblWarrning);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(txtPassword);
            Controls.Add(lblPassword);
            Controls.Add(txtEmail);
            Controls.Add(lblEmail);
            Controls.Add(txtNumber);
            Controls.Add(lblNumber);
            Controls.Add(txtLastName);
            Controls.Add(lblLastName);
            Controls.Add(txtFirstName);
            Controls.Add(lblFirstName);
            Controls.Add(txtUsername);
            Controls.Add(lblUsername);
            FormBorderStyle = FormBorderStyle.None;
            Name = "EmployeeDetails";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "EmployeeDetails";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblUsername;
        private TextBox txtUsername;
        private TextBox txtFirstName;
        private Label lblFirstName;
        private TextBox txtLastName;
        private Label lblLastName;
        private TextBox txtNumber;
        private Label lblNumber;
        private TextBox txtEmail;
        private Label lblEmail;
        private TextBox txtPassword;
        private Label lblPassword;
        private Button btnSave;
        private Button btnCancel;
        private Label lblWarrning;
    }
}