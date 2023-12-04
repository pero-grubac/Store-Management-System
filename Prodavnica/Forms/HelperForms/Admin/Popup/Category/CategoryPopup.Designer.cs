namespace Prodavnica.Forms.HelperForms.Admin.Popup.Category
{
    partial class CategoryPopup
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
            btnCancel = new Button();
            btnSave = new Button();
            lblWarrning = new Label();
            SuspendLayout();
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(37, 51);
            lblName.Name = "lblName";
            lblName.Size = new Size(49, 20);
            lblName.TabIndex = 0;
            lblName.Text = "Name";
            // 
            // txtName
            // 
            txtName.BackColor = SystemColors.Control;
            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtName.Location = new Point(119, 48);
            txtName.Name = "txtName";
            txtName.Size = new Size(283, 27);
            txtName.TabIndex = 1;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(308, 112);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(94, 29);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(184, 112);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(94, 29);
            btnSave.TabIndex = 3;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // lblWarrning
            // 
            lblWarrning.AutoSize = true;
            lblWarrning.Location = new Point(119, 88);
            lblWarrning.Name = "lblWarrning";
            lblWarrning.Size = new Size(50, 20);
            lblWarrning.TabIndex = 4;
            lblWarrning.Text = "label1";
            // 
            // CategoryPopup
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(431, 169);
            ControlBox = false;
            Controls.Add(lblWarrning);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            Controls.Add(txtName);
            Controls.Add(lblName);
            FormBorderStyle = FormBorderStyle.None;
            Name = "CategoryPopup";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "CategoryPopup";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblName;
        private TextBox txtName;
        private Button btnCancel;
        private Button btnSave;
        private Label lblWarrning;
    }
}