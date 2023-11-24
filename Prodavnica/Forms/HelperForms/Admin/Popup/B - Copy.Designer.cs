namespace Prodavnica.Forms.HelperForms.Admin.Popup
{
    partial class SelectSupplier
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
            cbSupplier = new ComboBox();
            btnOK = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // cbSupplier
            // 
            cbSupplier.FormattingEnabled = true;
            cbSupplier.Location = new Point(28, 31);
            cbSupplier.Name = "cbSupplier";
            cbSupplier.Size = new Size(341, 28);
            cbSupplier.TabIndex = 0;
            // 
            // btnOK
            // 
            btnOK.Location = new Point(28, 91);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(94, 29);
            btnOK.TabIndex = 1;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(275, 91);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(94, 29);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // SelectSupplier
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(400, 150);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            Controls.Add(cbSupplier);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SelectSupplier";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SelectSupplier";
            ResumeLayout(false);
        }

        #endregion

        private ComboBox cbSupplier;
        private Button btnOK;
        private Button btnCancel;
    }
}