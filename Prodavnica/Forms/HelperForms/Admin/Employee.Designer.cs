namespace Prodavnica.Forms.HelperForms.Admin
{
    partial class Employee
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
            components = new System.ComponentModel.Container();
            tcShop = new TabControl();
            tpProducts = new TabPage();
            tlpProducts = new TableLayoutPanel();
            gbProducts = new GroupBox();
            txtSearch = new TextBox();
            lblSearch = new Label();
            btnDelete = new Button();
            btnUpdate = new Button();
            btnAdd = new Button();
            tbProcurement = new TabPage();
            productsBindingSource = new BindingSource(components);
            tcShop.SuspendLayout();
            tpProducts.SuspendLayout();
            tlpProducts.SuspendLayout();
            gbProducts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)productsBindingSource).BeginInit();
            SuspendLayout();
            // 
            // tcShop
            // 
            tcShop.Controls.Add(tpProducts);
            tcShop.Controls.Add(tbProcurement);
            tcShop.Dock = DockStyle.Fill;
            tcShop.Location = new Point(0, 0);
            tcShop.Name = "tcShop";
            tcShop.SelectedIndex = 0;
            tcShop.Size = new Size(952, 509);
            tcShop.TabIndex = 0;
            // 
            // tpProducts
            // 
            tpProducts.Controls.Add(tlpProducts);
            tpProducts.Location = new Point(4, 29);
            tpProducts.Name = "tpProducts";
            tpProducts.Padding = new Padding(3);
            tpProducts.Size = new Size(944, 476);
            tpProducts.TabIndex = 2;
            tpProducts.Text = "Products";
            tpProducts.UseVisualStyleBackColor = true;
            // 
            // tlpProducts
            // 
            tlpProducts.ColumnCount = 1;
            tlpProducts.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpProducts.Controls.Add(gbProducts, 0, 0);
            tlpProducts.Dock = DockStyle.Fill;
            tlpProducts.Location = new Point(3, 3);
            tlpProducts.Name = "tlpProducts";
            tlpProducts.RowCount = 2;
            tlpProducts.RowStyles.Add(new RowStyle(SizeType.Absolute, 75F));
            tlpProducts.RowStyles.Add(new RowStyle());
            tlpProducts.Size = new Size(938, 470);
            tlpProducts.TabIndex = 1;
            // 
            // gbProducts
            // 
            gbProducts.AutoSize = true;
            gbProducts.BackColor = Color.White;
            gbProducts.Controls.Add(txtSearch);
            gbProducts.Controls.Add(lblSearch);
            gbProducts.Controls.Add(btnDelete);
            gbProducts.Controls.Add(btnUpdate);
            gbProducts.Controls.Add(btnAdd);
            gbProducts.Dock = DockStyle.Fill;
            gbProducts.ForeColor = Color.Black;
            gbProducts.Location = new Point(3, 3);
            gbProducts.Name = "gbProducts";
            gbProducts.Size = new Size(932, 69);
            gbProducts.TabIndex = 1;
            gbProducts.TabStop = false;
            // 
            // txtSearch
            // 
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.Location = new Point(356, 26);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(308, 27);
            txtSearch.TabIndex = 4;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Location = new Point(281, 31);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(53, 20);
            lblSearch.TabIndex = 3;
            lblSearch.Text = "Search";
            // 
            // btnDelete
            // 
            btnDelete.AutoSize = true;
            btnDelete.Location = new Point(206, 26);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(69, 30);
            btnDelete.TabIndex = 2;
            btnDelete.Text = "DELETE";
            btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnUpdate
            // 
            btnUpdate.AutoSize = true;
            btnUpdate.Location = new Point(106, 26);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(94, 30);
            btnUpdate.TabIndex = 1;
            btnUpdate.Text = "UPDATE";
            btnUpdate.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            btnAdd.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnAdd.Location = new Point(6, 26);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(94, 29);
            btnAdd.TabIndex = 0;
            btnAdd.Text = "ADD";
            btnAdd.UseVisualStyleBackColor = true;
            // 
            // tbProcurement
            // 
            tbProcurement.Location = new Point(4, 29);
            tbProcurement.Name = "tbProcurement";
            tbProcurement.Padding = new Padding(3);
            tbProcurement.Size = new Size(944, 476);
            tbProcurement.TabIndex = 1;
            tbProcurement.Text = "Procurement";
            tbProcurement.UseVisualStyleBackColor = true;
            // 
            // productsBindingSource
            // 
            productsBindingSource.DataSource = typeof(Database.DTO.Product);
            // 
            // Employee
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(952, 509);
            Controls.Add(tcShop);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Employee";
            Text = "Store";
            tcShop.ResumeLayout(false);
            tpProducts.ResumeLayout(false);
            tlpProducts.ResumeLayout(false);
            tlpProducts.PerformLayout();
            gbProducts.ResumeLayout(false);
            gbProducts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)productsBindingSource).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tcShop;
        private TabPage tbProcurement;
        private TabPage tpProducts;
        private BindingSource productsBindingSource;
        private TableLayoutPanel tlpProducts;
        private GroupBox gbProducts;
        private Button btnAdd;
        private Button btnDelete;
        private Button btnUpdate;
        private TextBox txtSearch;
        private Label lblSearch;
        private DataGridViewTextBoxColumn NameCol;
    }
}