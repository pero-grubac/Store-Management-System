using Prodavnica.Language;

namespace Prodavnica.Forms
{
    partial class MainFOrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFOrm));
            panelMenu = new Panel();
            btnSettings = new Button();
            btnSupplier = new Button();
            btnProducer = new Button();
            btnLogOut = new Button();
            btnStore = new Button();
            btnStaff = new Button();
            panelLogo = new Panel();
            lbl = new Label();
            panelTitleBar = new Panel();
            btnMin = new Button();
            btnMax = new Button();
            btnClose = new Button();
            btnCloseChldForm = new Button();
            lblTitle = new Label();
            panelDesktop = new Panel();
            pictureBox1 = new PictureBox();
            panelMenu.SuspendLayout();
            panelLogo.SuspendLayout();
            panelTitleBar.SuspendLayout();
            panelDesktop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panelMenu
            // 
            panelMenu.BackColor = Color.FromArgb(51, 51, 76);
            panelMenu.Controls.Add(btnSettings);
            panelMenu.Controls.Add(btnSupplier);
            panelMenu.Controls.Add(btnProducer);
            panelMenu.Controls.Add(btnLogOut);
            panelMenu.Controls.Add(btnStore);
            panelMenu.Controls.Add(btnStaff);
            panelMenu.Controls.Add(panelLogo);
            panelMenu.Dock = DockStyle.Left;
            panelMenu.Location = new Point(0, 0);
            panelMenu.Name = "panelMenu";
            panelMenu.Size = new Size(220, 675);
            panelMenu.TabIndex = 1;
            // 
            // btnSettings
            // 
            btnSettings.Dock = DockStyle.Top;
            btnSettings.FlatAppearance.BorderSize = 0;
            btnSettings.FlatStyle = FlatStyle.Flat;
            btnSettings.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            btnSettings.ForeColor = Color.Gainsboro;
            btnSettings.Image = Properties.Resources.setting;
            btnSettings.ImageAlign = ContentAlignment.MiddleLeft;
            btnSettings.Location = new Point(0, 320);
            btnSettings.Name = "btnSettings";
            btnSettings.Padding = new Padding(12, 0, 0, 0);
            btnSettings.Size = new Size(220, 60);
            btnSettings.TabIndex = 9;
            btnSettings.Text = "   Settings";
            btnSettings.TextAlign = ContentAlignment.MiddleLeft;
            btnSettings.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnSettings.UseVisualStyleBackColor = true;
            btnSettings.Click += btnSettings_Click;
            // 
            // btnSupplier
            // 
            btnSupplier.Dock = DockStyle.Top;
            btnSupplier.FlatAppearance.BorderSize = 0;
            btnSupplier.FlatStyle = FlatStyle.Flat;
            btnSupplier.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            btnSupplier.ForeColor = Color.Gainsboro;
            btnSupplier.Image = Properties.Resources.fast_delivery2;
            btnSupplier.ImageAlign = ContentAlignment.MiddleLeft;
            btnSupplier.Location = new Point(0, 260);
            btnSupplier.Name = "btnSupplier";
            btnSupplier.Padding = new Padding(12, 0, 0, 0);
            btnSupplier.Size = new Size(220, 60);
            btnSupplier.TabIndex = 8;
            btnSupplier.Text = "   Procurement";
            btnSupplier.TextAlign = ContentAlignment.MiddleLeft;
            btnSupplier.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnSupplier.UseVisualStyleBackColor = true;
            btnSupplier.Click += btnProcurement_Click;
            // 
            // btnProducer
            // 
            btnProducer.Dock = DockStyle.Top;
            btnProducer.FlatAppearance.BorderSize = 0;
            btnProducer.FlatStyle = FlatStyle.Flat;
            btnProducer.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            btnProducer.ForeColor = Color.Gainsboro;
            btnProducer.Image = Properties.Resources.product_management;
            btnProducer.ImageAlign = ContentAlignment.MiddleLeft;
            btnProducer.Location = new Point(0, 200);
            btnProducer.Name = "btnProducer";
            btnProducer.Padding = new Padding(12, 0, 0, 0);
            btnProducer.Size = new Size(220, 60);
            btnProducer.TabIndex = 7;
            btnProducer.Text = "  Producer";
            btnProducer.TextAlign = ContentAlignment.MiddleLeft;
            btnProducer.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnProducer.UseVisualStyleBackColor = true;
            btnProducer.Click += btnProducer_Click;
            // 
            // btnLogOut
            // 
            btnLogOut.Dock = DockStyle.Bottom;
            btnLogOut.FlatAppearance.BorderSize = 0;
            btnLogOut.FlatStyle = FlatStyle.Flat;
            btnLogOut.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            btnLogOut.ForeColor = Color.Gainsboro;
            btnLogOut.Image = (Image)resources.GetObject("btnLogOut.Image");
            btnLogOut.ImageAlign = ContentAlignment.MiddleLeft;
            btnLogOut.Location = new Point(0, 615);
            btnLogOut.Name = "btnLogOut";
            btnLogOut.Padding = new Padding(12, 0, 0, 0);
            btnLogOut.Size = new Size(220, 60);
            btnLogOut.TabIndex = 4;
            btnLogOut.Text = "   Log out";
            btnLogOut.TextAlign = ContentAlignment.MiddleLeft;
            btnLogOut.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnLogOut.UseVisualStyleBackColor = true;
            btnLogOut.Click += btnLogOut_Click;
            // 
            // btnStore
            // 
            btnStore.Dock = DockStyle.Top;
            btnStore.FlatAppearance.BorderSize = 0;
            btnStore.FlatStyle = FlatStyle.Flat;
            btnStore.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            btnStore.ForeColor = Color.Gainsboro;
            btnStore.Image = Properties.Resources.store;
            btnStore.ImageAlign = ContentAlignment.MiddleLeft;
            btnStore.Location = new Point(0, 140);
            btnStore.Name = "btnStore";
            btnStore.Padding = new Padding(12, 0, 0, 0);
            btnStore.Size = new Size(220, 60);
            btnStore.TabIndex = 2;
            btnStore.Text = "   Store";
            btnStore.TextAlign = ContentAlignment.MiddleLeft;
            btnStore.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnStore.UseVisualStyleBackColor = true;
            btnStore.Click += btnStore_Click;
            // 
            // btnStaff
            // 
            btnStaff.Dock = DockStyle.Top;
            btnStaff.FlatAppearance.BorderSize = 0;
            btnStaff.FlatStyle = FlatStyle.Flat;
            btnStaff.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            btnStaff.ForeColor = Color.Gainsboro;
            btnStaff.Image = Properties.Resources.user;
            btnStaff.ImageAlign = ContentAlignment.MiddleLeft;
            btnStaff.Location = new Point(0, 80);
            btnStaff.Name = "btnStaff";
            btnStaff.Padding = new Padding(12, 0, 0, 0);
            btnStaff.Size = new Size(220, 60);
            btnStaff.TabIndex = 1;
            btnStaff.Text = "   Employee";
            btnStaff.TextAlign = ContentAlignment.MiddleLeft;
            btnStaff.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnStaff.UseVisualStyleBackColor = true;
            btnStaff.Click += btnStaff_Click;
            // 
            // panelLogo
            // 
            panelLogo.BackColor = Color.FromArgb(39, 39, 58);
            panelLogo.Controls.Add(lbl);
            panelLogo.Dock = DockStyle.Top;
            panelLogo.Location = new Point(0, 0);
            panelLogo.Name = "panelLogo";
            panelLogo.Size = new Size(220, 80);
            panelLogo.TabIndex = 0;
            // 
            // lbl
            // 
            lbl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lbl.AutoSize = true;
            lbl.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lbl.ForeColor = Color.LightGray;
            lbl.Location = new Point(63, 28);
            lbl.Name = "lbl";
            lbl.Size = new Size(59, 25);
            lbl.TabIndex = 0;
            lbl.Text = "Shop";
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelTitleBar
            // 
            panelTitleBar.BackColor = Color.FromArgb(0, 74, 173);
            panelTitleBar.Controls.Add(btnMin);
            panelTitleBar.Controls.Add(btnMax);
            panelTitleBar.Controls.Add(btnClose);
            panelTitleBar.Controls.Add(btnCloseChldForm);
            panelTitleBar.Controls.Add(lblTitle);
            panelTitleBar.Dock = DockStyle.Top;
            panelTitleBar.Location = new Point(220, 0);
            panelTitleBar.Name = "panelTitleBar";
            panelTitleBar.Size = new Size(972, 80);
            panelTitleBar.TabIndex = 2;
            panelTitleBar.MouseDown += mouse_Down;
            panelTitleBar.MouseMove += mouse_Move;
            // 
            // btnMin
            // 
            btnMin.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMin.BackgroundImage = Properties.Resources.rec;
            btnMin.BackgroundImageLayout = ImageLayout.Center;
            btnMin.FlatAppearance.BorderSize = 0;
            btnMin.FlatStyle = FlatStyle.Flat;
            btnMin.Font = new Font("Bookman Old Style", 15F, FontStyle.Regular, GraphicsUnit.Point);
            btnMin.ForeColor = Color.White;
            btnMin.Location = new Point(867, 0);
            btnMin.Name = "btnMin";
            btnMin.Size = new Size(30, 37);
            btnMin.TabIndex = 7;
            btnMin.UseVisualStyleBackColor = true;
            btnMin.Click += btnMin_Click;
            // 
            // btnMax
            // 
            btnMax.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMax.BackgroundImage = Properties.Resources.rec;
            btnMax.BackgroundImageLayout = ImageLayout.Center;
            btnMax.FlatAppearance.BorderSize = 0;
            btnMax.FlatStyle = FlatStyle.Flat;
            btnMax.Font = new Font("Book Antiqua", 15F, FontStyle.Bold, GraphicsUnit.Point);
            btnMax.ForeColor = Color.White;
            btnMax.Location = new Point(903, 0);
            btnMax.Name = "btnMax";
            btnMax.Size = new Size(30, 37);
            btnMax.TabIndex = 6;
            btnMax.UseVisualStyleBackColor = true;
            btnMax.Click += btnMax_Click;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.BackgroundImage = Properties.Resources.rec;
            btnClose.BackgroundImageLayout = ImageLayout.Center;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Book Antiqua", 15F, FontStyle.Bold, GraphicsUnit.Point);
            btnClose.ForeColor = Color.White;
            btnClose.Location = new Point(939, 0);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(30, 37);
            btnClose.TabIndex = 5;
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // btnCloseChldForm
            // 
            btnCloseChldForm.Dock = DockStyle.Left;
            btnCloseChldForm.FlatAppearance.BorderSize = 0;
            btnCloseChldForm.FlatStyle = FlatStyle.Flat;
            btnCloseChldForm.Image = Properties.Resources.cancel;
            btnCloseChldForm.Location = new Point(0, 0);
            btnCloseChldForm.Name = "btnCloseChldForm";
            btnCloseChldForm.Size = new Size(59, 80);
            btnCloseChldForm.TabIndex = 1;
            btnCloseChldForm.UseVisualStyleBackColor = true;
            btnCloseChldForm.Click += btnCloseChldForm_Click_1;
            // 
            // lblTitle
            // 
            lblTitle.Anchor = AnchorStyles.None;
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Modern No. 20", 16.2F, FontStyle.Regular, GraphicsUnit.Point);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(420, 23);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(111, 30);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "  HOME";
            // 
            // panelDesktop
            // 
            panelDesktop.Controls.Add(pictureBox1);
            panelDesktop.Dock = DockStyle.Fill;
            panelDesktop.Location = new Point(220, 80);
            panelDesktop.Name = "panelDesktop";
            panelDesktop.Size = new Size(972, 595);
            panelDesktop.TabIndex = 3;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.None;
            pictureBox1.Image = Properties.Resources.laptop;
            pictureBox1.Location = new Point(176, 56);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(512, 512);
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // MainFOrm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1192, 675);
            ControlBox = false;
            Controls.Add(panelDesktop);
            Controls.Add(panelTitleBar);
            Controls.Add(panelMenu);
            Name = "MainFOrm";
            Text = "Administrator";
            panelMenu.ResumeLayout(false);
            panelLogo.ResumeLayout(false);
            panelLogo.PerformLayout();
            panelTitleBar.ResumeLayout(false);
            panelTitleBar.PerformLayout();
            panelDesktop.ResumeLayout(false);
            panelDesktop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelMenu;
        private Button btnLogOut;
        private Button btnStore;
        private Button btnStaff;
        private Panel panelLogo;
        private Label lbl;
        private Panel panelTitleBar;
        private Button btnCloseChldForm;
        private Label lblTitle;
        private Panel panelDesktop;
        private Button btnMin;
        private Button btnMax;
        private Button btnClose;
        private PictureBox pictureBox1;
        private Button btnProducer;
        private Button btnSettings;
        private Button btnSupplier;
    }
}