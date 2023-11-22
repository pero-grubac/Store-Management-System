namespace Prodavnica.Forms.HelperForms.Admin
{
    partial class Store
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
            tcShop = new TabControl();
            tbManufacturer = new TabPage();
            tpProcurement = new TabPage();
            tcShop.SuspendLayout();
            SuspendLayout();
            // 
            // tcShop
            // 
            tcShop.Controls.Add(tpProcurement);
            tcShop.Controls.Add(tbManufacturer);
            tcShop.Dock = DockStyle.Fill;
            tcShop.Location = new Point(0, 0);
            tcShop.Name = "tcShop";
            tcShop.SelectedIndex = 0;
            tcShop.Size = new Size(800, 450);
            tcShop.TabIndex = 0;
            // 
            // tbManufacturer
            // 
            tbManufacturer.Location = new Point(4, 29);
            tbManufacturer.Name = "tbManufacturer";
            tbManufacturer.Padding = new Padding(3);
            tbManufacturer.Size = new Size(792, 417);
            tbManufacturer.TabIndex = 1;
            tbManufacturer.Text = "Manufacturer";
            tbManufacturer.UseVisualStyleBackColor = true;
            // 
            // tpProcurement
            // 
            tpProcurement.Location = new Point(4, 29);
            tpProcurement.Name = "tpProcurement";
            tpProcurement.Padding = new Padding(3);
            tpProcurement.Size = new Size(792, 417);
            tpProcurement.TabIndex = 2;
            tpProcurement.Text = "Procurement";
            tpProcurement.UseVisualStyleBackColor = true;
            // 
            // Store
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tcShop);
            Name = "Store";
            Text = "Store";
            tcShop.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl tcShop;
        private TabPage tbManufacturer;
        private TabPage tpProcurement;
    }
}