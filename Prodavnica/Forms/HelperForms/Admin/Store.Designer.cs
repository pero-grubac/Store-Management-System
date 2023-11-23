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
            components = new System.ComponentModel.Container();
            tcShop = new TabControl();
            tpProcurement = new TabPage();
            dgvProducts = new DataGridView();
            productsBindingSource = new BindingSource(components);
            tbManufacturer = new TabPage();
            barCodeDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            nameDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            priceDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            suppliesDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            expirationDateDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            descriptionDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            Category = new DataGridViewTextBoxColumn();
            tcShop.SuspendLayout();
            tpProcurement.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
            ((System.ComponentModel.ISupportInitialize)productsBindingSource).BeginInit();
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
            tcShop.Size = new Size(701, 450);
            tcShop.TabIndex = 0;
            // 
            // tpProcurement
            // 
            tpProcurement.Controls.Add(dgvProducts);
            tpProcurement.Location = new Point(4, 29);
            tpProcurement.Name = "tpProcurement";
            tpProcurement.Padding = new Padding(3);
            tpProcurement.Size = new Size(693, 417);
            tpProcurement.TabIndex = 2;
            tpProcurement.Text = "Procurement";
            tpProcurement.UseVisualStyleBackColor = true;
            // 
            // dgvProducts
            // 
            dgvProducts.AllowUserToAddRows = false;
            dgvProducts.AllowUserToDeleteRows = false;
            dgvProducts.AutoGenerateColumns = false;
            dgvProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProducts.Columns.AddRange(new DataGridViewColumn[] { barCodeDataGridViewTextBoxColumn, nameDataGridViewTextBoxColumn, priceDataGridViewTextBoxColumn, suppliesDataGridViewTextBoxColumn, expirationDateDataGridViewTextBoxColumn, descriptionDataGridViewTextBoxColumn, Category });
            dgvProducts.DataSource = productsBindingSource;
            dgvProducts.Dock = DockStyle.Fill;
            dgvProducts.Location = new Point(3, 3);
            dgvProducts.Name = "dgvProducts";
            dgvProducts.ReadOnly = true;
            dgvProducts.RowHeadersVisible = false;
            dgvProducts.RowHeadersWidth = 51;
            dgvProducts.RowTemplate.Height = 29;
            dgvProducts.Size = new Size(687, 411);
            dgvProducts.TabIndex = 0;
            // 
            // productsBindingSource
            // 
            productsBindingSource.DataSource = typeof(Database.DTO.Product);
            // 
            // tbManufacturer
            // 
            tbManufacturer.Location = new Point(4, 29);
            tbManufacturer.Name = "tbManufacturer";
            tbManufacturer.Padding = new Padding(3);
            tbManufacturer.Size = new Size(693, 417);
            tbManufacturer.TabIndex = 1;
            tbManufacturer.Text = "Manufacturer";
            tbManufacturer.UseVisualStyleBackColor = true;
            // 
            // barCodeDataGridViewTextBoxColumn
            // 
            barCodeDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            barCodeDataGridViewTextBoxColumn.DataPropertyName = "BarCode";
            barCodeDataGridViewTextBoxColumn.HeaderText = "BarCode";
            barCodeDataGridViewTextBoxColumn.MinimumWidth = 6;
            barCodeDataGridViewTextBoxColumn.Name = "barCodeDataGridViewTextBoxColumn";
            barCodeDataGridViewTextBoxColumn.ReadOnly = true;
            barCodeDataGridViewTextBoxColumn.Resizable = DataGridViewTriState.False;
            barCodeDataGridViewTextBoxColumn.Width = 90;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            nameDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            nameDataGridViewTextBoxColumn.HeaderText = "Name";
            nameDataGridViewTextBoxColumn.MinimumWidth = 6;
            nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            nameDataGridViewTextBoxColumn.ReadOnly = true;
            nameDataGridViewTextBoxColumn.Width = 78;
            // 
            // priceDataGridViewTextBoxColumn
            // 
            priceDataGridViewTextBoxColumn.DataPropertyName = "Price";
            priceDataGridViewTextBoxColumn.HeaderText = "Price";
            priceDataGridViewTextBoxColumn.MinimumWidth = 6;
            priceDataGridViewTextBoxColumn.Name = "priceDataGridViewTextBoxColumn";
            priceDataGridViewTextBoxColumn.ReadOnly = true;
            priceDataGridViewTextBoxColumn.Resizable = DataGridViewTriState.False;
            priceDataGridViewTextBoxColumn.Width = 70;
            // 
            // suppliesDataGridViewTextBoxColumn
            // 
            suppliesDataGridViewTextBoxColumn.DataPropertyName = "Supplies";
            suppliesDataGridViewTextBoxColumn.HeaderText = "Supplies";
            suppliesDataGridViewTextBoxColumn.MinimumWidth = 6;
            suppliesDataGridViewTextBoxColumn.Name = "suppliesDataGridViewTextBoxColumn";
            suppliesDataGridViewTextBoxColumn.ReadOnly = true;
            suppliesDataGridViewTextBoxColumn.Resizable = DataGridViewTriState.False;
            suppliesDataGridViewTextBoxColumn.Width = 94;
            // 
            // expirationDateDataGridViewTextBoxColumn
            // 
            expirationDateDataGridViewTextBoxColumn.DataPropertyName = "ExpirationDate";
            expirationDateDataGridViewTextBoxColumn.HeaderText = "ExpirationDate";
            expirationDateDataGridViewTextBoxColumn.MinimumWidth = 6;
            expirationDateDataGridViewTextBoxColumn.Name = "expirationDateDataGridViewTextBoxColumn";
            expirationDateDataGridViewTextBoxColumn.ReadOnly = true;
            expirationDateDataGridViewTextBoxColumn.Resizable = DataGridViewTriState.False;
            expirationDateDataGridViewTextBoxColumn.Width = 137;
            // 
            // descriptionDataGridViewTextBoxColumn
            // 
            descriptionDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            descriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
            descriptionDataGridViewTextBoxColumn.HeaderText = "Description";
            descriptionDataGridViewTextBoxColumn.MinimumWidth = 6;
            descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
            descriptionDataGridViewTextBoxColumn.ReadOnly = true;
            descriptionDataGridViewTextBoxColumn.Width = 114;
            // 
            // Category
            // 
            Category.HeaderText = "Category";
            Category.MinimumWidth = 6;
            Category.Name = "Category";
            Category.ReadOnly = true;
            Category.Resizable = DataGridViewTriState.False;
            Category.Width = 98;
            // 
            // Store
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(701, 450);
            Controls.Add(tcShop);
            Name = "Store";
            Text = "Store";
            tcShop.ResumeLayout(false);
            tpProcurement.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
            ((System.ComponentModel.ISupportInitialize)productsBindingSource).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tcShop;
        private TabPage tbManufacturer;
        private TabPage tpProcurement;
        private DataGridView dgvProducts;
        private BindingSource productsBindingSource;
        private DataGridViewTextBoxColumn barCodeDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn priceDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn suppliesDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn expirationDateDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn Category;
    }
}