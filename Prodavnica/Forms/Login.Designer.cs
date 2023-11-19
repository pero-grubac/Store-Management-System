namespace OnlineKupovinaGUI
{
    partial class Login
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pictureBox1 = new PictureBox();
            label1 = new Label();
            pictureBox2 = new PictureBox();
            panel1 = new Panel();
            panel2 = new Panel();
            pictureBox3 = new PictureBox();
            btnLogIn = new Button();
            lblClearFields = new Label();
            lblExit = new Label();
            txtUserName = new TextBox();
            txtPassword = new TextBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Prodavnica.Properties.Resources.open_;
            pictureBox1.Location = new Point(101, 54);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(87, 71);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Bauhaus 93", 24F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.FromArgb(0, 74, 173);
            label1.Location = new Point(78, 138);
            label1.Name = "label1";
            label1.Size = new Size(141, 45);
            label1.TabIndex = 1;
            label1.Text = "LOG IN";
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Prodavnica.Properties.Resources.Profile_icon;
            pictureBox2.Location = new Point(35, 203);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(25, 25);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 2;
            pictureBox2.TabStop = false;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(0, 74, 173);
            panel1.ForeColor = Color.FromArgb(0, 74, 173);
            panel1.Location = new Point(35, 234);
            panel1.Name = "panel1";
            panel1.Size = new Size(236, 1);
            panel1.TabIndex = 3;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(0, 74, 173);
            panel2.ForeColor = Color.FromArgb(0, 74, 173);
            panel2.Location = new Point(35, 272);
            panel2.Name = "panel2";
            panel2.Size = new Size(236, 1);
            panel2.TabIndex = 5;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = Prodavnica.Properties.Resources.lock_232x256;
            pictureBox3.Location = new Point(35, 241);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(25, 25);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 4;
            pictureBox3.TabStop = false;
            // 
            // btnLogIn
            // 
            btnLogIn.BackColor = Color.FromArgb(0, 74, 173);
            btnLogIn.FlatAppearance.BorderSize = 0;
            btnLogIn.FlatStyle = FlatStyle.Flat;
            btnLogIn.Font = new Font("Bahnschrift", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            btnLogIn.ForeColor = Color.White;
            btnLogIn.Location = new Point(35, 301);
            btnLogIn.Name = "btnLogIn";
            btnLogIn.Size = new Size(236, 33);
            btnLogIn.TabIndex = 6;
            btnLogIn.Text = "LOG IN";
            btnLogIn.UseVisualStyleBackColor = false;
            btnLogIn.Click += btnLogIn_Click;
            // 
            // lblClearFields
            // 
            lblClearFields.AutoSize = true;
            lblClearFields.BackColor = Color.FromArgb(0, 74, 173);
            lblClearFields.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            lblClearFields.ForeColor = Color.White;
            lblClearFields.Location = new Point(160, 353);
            lblClearFields.Name = "lblClearFields";
            lblClearFields.Size = new Size(111, 20);
            lblClearFields.TabIndex = 7;
            lblClearFields.Text = "Clear Fields";
            lblClearFields.Click += lblClearFields_Click;
            // 
            // lblExit
            // 
            lblExit.AutoSize = true;
            lblExit.BackColor = Color.FromArgb(0, 74, 173);
            lblExit.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            lblExit.ForeColor = Color.White;
            lblExit.Location = new Point(35, 353);
            lblExit.Name = "lblExit";
            lblExit.Size = new Size(41, 20);
            lblExit.TabIndex = 8;
            lblExit.Text = "Exit";
            lblExit.Click += label3_Click;
            // 
            // txtUserName
            // 
            txtUserName.BorderStyle = BorderStyle.None;
            txtUserName.Font = new Font("Microsoft Sans Serif", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            txtUserName.ForeColor = Color.FromArgb(0, 74, 173);
            txtUserName.Location = new Point(66, 203);
            txtUserName.Multiline = true;
            txtUserName.Name = "txtUserName";
            txtUserName.Size = new Size(205, 24);
            txtUserName.TabIndex = 9;
            // 
            // txtPassword
            // 
            txtPassword.BorderStyle = BorderStyle.None;
            txtPassword.Font = new Font("Microsoft Sans Serif", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            txtPassword.ForeColor = Color.FromArgb(0, 74, 173);
            txtPassword.Location = new Point(66, 241);
            txtPassword.Multiline = true;
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(205, 24);
            txtPassword.TabIndex = 10;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(302, 421);
            Controls.Add(txtPassword);
            Controls.Add(txtUserName);
            Controls.Add(lblExit);
            Controls.Add(lblClearFields);
            Controls.Add(btnLogIn);
            Controls.Add(panel2);
            Controls.Add(pictureBox3);
            Controls.Add(panel1);
            Controls.Add(pictureBox2);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Login";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Label label1;
        private PictureBox pictureBox2;
        private Panel panel1;
        private Panel panel2;
        private PictureBox pictureBox3;
        private Button btnLogIn;
        private Label lblClearFields;
        private Label lblExit;
        private TextBox txtUserName;
        private TextBox txtPassword;
    }
}