namespace Prodavnica.Forms.HelperForms.Admin.Popup.Manufactuer
{
    partial class NewManufactuer
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
            lblName = new Label();
            txtName = new TextBox();
            btnOK = new Button();
            btnCencel = new Button();
            txtEmail = new TextBox();
            lblEmail = new Label();
            SuspendLayout();
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(12, 30);
            lblName.Name = "lblName";
            lblName.Size = new Size(49, 20);
            lblName.TabIndex = 0;
            lblName.Text = "Name";
            // 
            // txtName
            // 
            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtName.Location = new Point(87, 28);
            txtName.Name = "txtName";
            txtName.Size = new Size(301, 27);
            txtName.TabIndex = 2;
            // 
            // btnOK
            // 
            btnOK.Location = new Point(12, 97);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(94, 29);
            btnOK.TabIndex = 4;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // btnCencel
            // 
            btnCencel.Location = new Point(294, 97);
            btnCencel.Name = "btnCencel";
            btnCencel.Size = new Size(94, 29);
            btnCencel.TabIndex = 5;
            btnCencel.Text = "Cencel";
            btnCencel.UseVisualStyleBackColor = true;
            btnCencel.Click += btnCencel_Click;
            // 
            // txtEmail
            // 
            txtEmail.BorderStyle = BorderStyle.FixedSingle;
            txtEmail.Location = new Point(87, 62);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(301, 27);
            txtEmail.TabIndex = 7;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(12, 64);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(46, 20);
            lblEmail.TabIndex = 6;
            lblEmail.Text = "Email";
            // 
            // NewManufactuer
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(400, 150);
            ControlBox = false;
            Controls.Add(txtEmail);
            Controls.Add(lblEmail);
            Controls.Add(btnCencel);
            Controls.Add(btnOK);
            Controls.Add(txtName);
            Controls.Add(lblName);
            FormBorderStyle = FormBorderStyle.None;
            Name = "NewManufactuer";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "NewSupplier";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblName;
        private TextBox txtName;
        private Button btnOK;
        private Button btnCencel;
        private TextBox txtEmail;
        private Label lblEmail;
    }
}