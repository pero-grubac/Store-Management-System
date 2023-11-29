namespace Prodavnica.Forms.HelperForms.Admin
{
    partial class StoreTable
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
            dgvProducts = new DataGridView();
            BarCode = new DataGridViewTextBoxColumn();
            NameColumn = new DataGridViewTextBoxColumn();
            Price = new DataGridViewTextBoxColumn();
            Supplies = new DataGridViewTextBoxColumn();
            ExpirationDate = new DataGridViewTextBoxColumn();
            Description = new DataGridViewTextBoxColumn();
            CategoryName = new DataGridViewTextBoxColumn();
            ManufacturerName = new DataGridViewTextBoxColumn();
            productsBindingSource = new BindingSource(components);
            gbProducts = new GroupBox();
            txtSearch = new TextBox();
            lblSearch = new Label();
            btnDelete = new Button();
            btnUpdate = new Button();
            btnAdd = new Button();
            tbProcurement = new TabPage();
            tlpProcurement = new TableLayoutPanel();
            tlpBill = new TableLayoutPanel();
            gbBuy = new GroupBox();
            txtTotal = new TextBox();
            lblTotal = new Label();
            btnBuy = new Button();
            dgvBill = new DataGridView();
            productBIllBindingSource = new BindingSource(components);
            btnDeleteItem = new Button();
            tlypSelect = new TableLayoutPanel();
            gbSearch = new GroupBox();
            txtSearchBarCode = new TextBox();
            lblSearchProduct = new Label();
            gbAdd = new GroupBox();
            txtAmount = new TextBox();
            lblAmount = new Label();
            txtPrice = new TextBox();
            btnAddToCart = new Button();
            lblPrice = new Label();
            dgvProductsBill = new DataGridView();
            barCodeColumn = new DataGridViewTextBoxColumn();
            nameColumnBill = new DataGridViewTextBoxColumn();
            tbBills = new TabPage();
            nameColumBill = new DataGridViewTextBoxColumn();
            amountColumnBill = new DataGridViewTextBoxColumn();
            priceColumnBill = new DataGridViewTextBoxColumn();
            tcShop.SuspendLayout();
            tpProducts.SuspendLayout();
            tlpProducts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
            ((System.ComponentModel.ISupportInitialize)productsBindingSource).BeginInit();
            gbProducts.SuspendLayout();
            tbProcurement.SuspendLayout();
            tlpProcurement.SuspendLayout();
            tlpBill.SuspendLayout();
            gbBuy.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBill).BeginInit();
            ((System.ComponentModel.ISupportInitialize)productBIllBindingSource).BeginInit();
            tlypSelect.SuspendLayout();
            gbSearch.SuspendLayout();
            gbAdd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProductsBill).BeginInit();
            SuspendLayout();
            // 
            // tcShop
            // 
            tcShop.Controls.Add(tpProducts);
            tcShop.Controls.Add(tbProcurement);
            tcShop.Controls.Add(tbBills);
            tcShop.Dock = DockStyle.Fill;
            tcShop.Location = new Point(0, 0);
            tcShop.Name = "tcShop";
            tcShop.SelectedIndex = 0;
            tcShop.Size = new Size(952, 509);
            tcShop.TabIndex = 0;
            tcShop.SelectedIndexChanged += tcShop_SelectedIndexChanged;
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
            tlpProducts.Controls.Add(dgvProducts, 0, 1);
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
            // dgvProducts
            // 
            dgvProducts.AllowUserToAddRows = false;
            dgvProducts.AllowUserToDeleteRows = false;
            dgvProducts.AllowUserToResizeRows = false;
            dgvProducts.AutoGenerateColumns = false;
            dgvProducts.BackgroundColor = Color.White;
            dgvProducts.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProducts.Columns.AddRange(new DataGridViewColumn[] { BarCode, NameColumn, Price, Supplies, ExpirationDate, Description, CategoryName, ManufacturerName });
            dgvProducts.DataSource = productsBindingSource;
            dgvProducts.Dock = DockStyle.Fill;
            dgvProducts.Location = new Point(3, 78);
            dgvProducts.Name = "dgvProducts";
            dgvProducts.ReadOnly = true;
            dgvProducts.RowHeadersVisible = false;
            dgvProducts.RowHeadersWidth = 51;
            dgvProducts.RowTemplate.Height = 29;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.Size = new Size(932, 885);
            dgvProducts.TabIndex = 0;
            dgvProducts.RowHeaderMouseClick += dgvProducts_RowHeaderMouseClick;
            // 
            // BarCode
            // 
            BarCode.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            BarCode.DataPropertyName = "BarCode";
            BarCode.HeaderText = "BarCode";
            BarCode.MinimumWidth = 6;
            BarCode.Name = "BarCode";
            BarCode.ReadOnly = true;
            BarCode.Width = 125;
            // 
            // NameColumn
            // 
            NameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            NameColumn.DataPropertyName = "Name";
            NameColumn.HeaderText = "Name";
            NameColumn.MinimumWidth = 6;
            NameColumn.Name = "NameColumn";
            NameColumn.ReadOnly = true;
            NameColumn.Width = 125;
            // 
            // Price
            // 
            Price.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            Price.DataPropertyName = "Price";
            Price.HeaderText = "Price";
            Price.MinimumWidth = 6;
            Price.Name = "Price";
            Price.ReadOnly = true;
            Price.Width = 70;
            // 
            // Supplies
            // 
            Supplies.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            Supplies.DataPropertyName = "Supplies";
            Supplies.HeaderText = "Supplies";
            Supplies.MinimumWidth = 6;
            Supplies.Name = "Supplies";
            Supplies.ReadOnly = true;
            Supplies.Resizable = DataGridViewTriState.True;
            Supplies.Width = 94;
            // 
            // ExpirationDate
            // 
            ExpirationDate.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            ExpirationDate.DataPropertyName = "ExpirationDate";
            ExpirationDate.HeaderText = "ExpirationDate";
            ExpirationDate.MinimumWidth = 6;
            ExpirationDate.Name = "ExpirationDate";
            ExpirationDate.ReadOnly = true;
            ExpirationDate.Width = 175;
            // 
            // Description
            // 
            Description.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            Description.DataPropertyName = "Description";
            Description.HeaderText = "Description";
            Description.MinimumWidth = 6;
            Description.Name = "Description";
            Description.ReadOnly = true;
            Description.Width = 114;
            // 
            // CategoryName
            // 
            CategoryName.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            CategoryName.DataPropertyName = "CategoryName";
            CategoryName.HeaderText = "Category";
            CategoryName.MinimumWidth = 6;
            CategoryName.Name = "CategoryName";
            CategoryName.ReadOnly = true;
            CategoryName.Width = 125;
            // 
            // ManufacturerName
            // 
            ManufacturerName.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            ManufacturerName.DataPropertyName = "ManufacturerName";
            ManufacturerName.HeaderText = "Manufacturer";
            ManufacturerName.MinimumWidth = 6;
            ManufacturerName.Name = "ManufacturerName";
            ManufacturerName.ReadOnly = true;
            ManufacturerName.Width = 125;
            // 
            // productsBindingSource
            // 
            productsBindingSource.DataSource = typeof(Database.DTO.Product);
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
            txtSearch.KeyPress += txtSeatch_KeyPress;
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
            btnDelete.Click += btnDelete_Click;
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
            btnUpdate.Click += btnUpdate_Click;
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
            btnAdd.Click += btnAdd_Click;
            // 
            // tbProcurement
            // 
            tbProcurement.Controls.Add(tlpProcurement);
            tbProcurement.Location = new Point(4, 29);
            tbProcurement.Name = "tbProcurement";
            tbProcurement.Padding = new Padding(3);
            tbProcurement.Size = new Size(944, 476);
            tbProcurement.TabIndex = 1;
            tbProcurement.Text = "Procurement";
            tbProcurement.UseVisualStyleBackColor = true;
            // 
            // tlpProcurement
            // 
            tlpProcurement.ColumnCount = 2;
            tlpProcurement.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpProcurement.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpProcurement.Controls.Add(tlpBill, 1, 0);
            tlpProcurement.Controls.Add(tlypSelect, 0, 0);
            tlpProcurement.Dock = DockStyle.Fill;
            tlpProcurement.Location = new Point(3, 3);
            tlpProcurement.Name = "tlpProcurement";
            tlpProcurement.RowCount = 1;
            tlpProcurement.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpProcurement.Size = new Size(938, 470);
            tlpProcurement.TabIndex = 0;
            // 
            // tlpBill
            // 
            tlpBill.ColumnCount = 1;
            tlpBill.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpBill.Controls.Add(gbBuy, 0, 2);
            tlpBill.Controls.Add(dgvBill, 0, 0);
            tlpBill.Controls.Add(btnDeleteItem, 0, 1);
            tlpBill.Dock = DockStyle.Fill;
            tlpBill.Location = new Point(472, 3);
            tlpBill.Name = "tlpBill";
            tlpBill.RowCount = 3;
            tlpBill.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpBill.RowStyles.Add(new RowStyle(SizeType.Absolute, 75F));
            tlpBill.RowStyles.Add(new RowStyle(SizeType.Absolute, 150F));
            tlpBill.Size = new Size(463, 464);
            tlpBill.TabIndex = 0;
            // 
            // gbBuy
            // 
            gbBuy.BackColor = Color.White;
            gbBuy.Controls.Add(txtTotal);
            gbBuy.Controls.Add(lblTotal);
            gbBuy.Controls.Add(btnBuy);
            gbBuy.Dock = DockStyle.Fill;
            gbBuy.Location = new Point(3, 317);
            gbBuy.Name = "gbBuy";
            gbBuy.Size = new Size(457, 144);
            gbBuy.TabIndex = 0;
            gbBuy.TabStop = false;
            // 
            // txtTotal
            // 
            txtTotal.BorderStyle = BorderStyle.FixedSingle;
            txtTotal.Location = new Point(102, 59);
            txtTotal.Name = "txtTotal";
            txtTotal.Size = new Size(340, 27);
            txtTotal.TabIndex = 7;
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Location = new Point(6, 62);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(42, 20);
            lblTotal.TabIndex = 6;
            lblTotal.Text = "Total";
            // 
            // btnBuy
            // 
            btnBuy.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnBuy.Location = new Point(357, 106);
            btnBuy.Name = "btnBuy";
            btnBuy.Size = new Size(94, 32);
            btnBuy.TabIndex = 4;
            btnBuy.Text = "Buy";
            btnBuy.UseVisualStyleBackColor = true;
            // 
            // dgvBill
            // 
            dgvBill.AllowUserToAddRows = false;
            dgvBill.AllowUserToDeleteRows = false;
            dgvBill.AllowUserToResizeRows = false;
            dgvBill.AutoGenerateColumns = false;
            dgvBill.BackgroundColor = Color.White;
            dgvBill.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBill.Columns.AddRange(new DataGridViewColumn[] { nameColumBill, amountColumnBill, priceColumnBill });
            dgvBill.DataSource = productBIllBindingSource;
            dgvBill.Dock = DockStyle.Fill;
            dgvBill.Location = new Point(3, 3);
            dgvBill.Name = "dgvBill";
            dgvBill.ReadOnly = true;
            dgvBill.RowHeadersVisible = false;
            dgvBill.RowHeadersWidth = 51;
            dgvBill.RowTemplate.Height = 29;
            dgvBill.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBill.Size = new Size(457, 233);
            dgvBill.TabIndex = 1;
            // 
            // productBIllBindingSource
            // 
            productBIllBindingSource.DataSource = typeof(ProductBIll);
            // 
            // btnDeleteItem
            // 
            btnDeleteItem.BackColor = Color.White;
            btnDeleteItem.Dock = DockStyle.Fill;
            btnDeleteItem.Location = new Point(3, 242);
            btnDeleteItem.Name = "btnDeleteItem";
            btnDeleteItem.Size = new Size(457, 69);
            btnDeleteItem.TabIndex = 2;
            btnDeleteItem.Text = "Delete item";
            btnDeleteItem.UseVisualStyleBackColor = false;
            // 
            // tlypSelect
            // 
            tlypSelect.ColumnCount = 1;
            tlypSelect.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlypSelect.Controls.Add(gbSearch, 0, 0);
            tlypSelect.Controls.Add(gbAdd, 0, 2);
            tlypSelect.Controls.Add(dgvProductsBill, 0, 1);
            tlypSelect.Dock = DockStyle.Fill;
            tlypSelect.Location = new Point(3, 3);
            tlypSelect.Name = "tlypSelect";
            tlypSelect.RowCount = 3;
            tlypSelect.RowStyles.Add(new RowStyle(SizeType.Absolute, 75F));
            tlypSelect.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlypSelect.RowStyles.Add(new RowStyle(SizeType.Absolute, 225F));
            tlypSelect.Size = new Size(463, 464);
            tlypSelect.TabIndex = 1;
            // 
            // gbSearch
            // 
            gbSearch.BackColor = Color.White;
            gbSearch.Controls.Add(txtSearchBarCode);
            gbSearch.Controls.Add(lblSearchProduct);
            gbSearch.Dock = DockStyle.Fill;
            gbSearch.Location = new Point(3, 3);
            gbSearch.Name = "gbSearch";
            gbSearch.Size = new Size(457, 69);
            gbSearch.TabIndex = 0;
            gbSearch.TabStop = false;
            // 
            // txtSearchBarCode
            // 
            txtSearchBarCode.BorderStyle = BorderStyle.FixedSingle;
            txtSearchBarCode.Location = new Point(102, 26);
            txtSearchBarCode.Name = "txtSearchBarCode";
            txtSearchBarCode.Size = new Size(340, 27);
            txtSearchBarCode.TabIndex = 1;
            // 
            // lblSearchProduct
            // 
            lblSearchProduct.AutoSize = true;
            lblSearchProduct.Location = new Point(6, 29);
            lblSearchProduct.Name = "lblSearchProduct";
            lblSearchProduct.Size = new Size(53, 20);
            lblSearchProduct.TabIndex = 0;
            lblSearchProduct.Text = "Search";
            // 
            // gbAdd
            // 
            gbAdd.BackColor = Color.White;
            gbAdd.Controls.Add(txtAmount);
            gbAdd.Controls.Add(lblAmount);
            gbAdd.Controls.Add(txtPrice);
            gbAdd.Controls.Add(btnAddToCart);
            gbAdd.Controls.Add(lblPrice);
            gbAdd.Dock = DockStyle.Fill;
            gbAdd.Location = new Point(3, 242);
            gbAdd.Name = "gbAdd";
            gbAdd.Size = new Size(457, 219);
            gbAdd.TabIndex = 1;
            gbAdd.TabStop = false;
            // 
            // txtAmount
            // 
            txtAmount.BorderStyle = BorderStyle.FixedSingle;
            txtAmount.Location = new Point(102, 115);
            txtAmount.Name = "txtAmount";
            txtAmount.Size = new Size(340, 27);
            txtAmount.TabIndex = 5;
            // 
            // lblAmount
            // 
            lblAmount.AutoSize = true;
            lblAmount.Location = new Point(6, 118);
            lblAmount.Name = "lblAmount";
            lblAmount.Size = new Size(62, 20);
            lblAmount.TabIndex = 4;
            lblAmount.Text = "Amount";
            // 
            // txtPrice
            // 
            txtPrice.BorderStyle = BorderStyle.FixedSingle;
            txtPrice.Location = new Point(102, 55);
            txtPrice.Name = "txtPrice";
            txtPrice.Size = new Size(340, 27);
            txtPrice.TabIndex = 3;
            // 
            // btnAddToCart
            // 
            btnAddToCart.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnAddToCart.Location = new Point(348, 181);
            btnAddToCart.Name = "btnAddToCart";
            btnAddToCart.Size = new Size(94, 32);
            btnAddToCart.TabIndex = 0;
            btnAddToCart.Text = "Add";
            btnAddToCart.UseVisualStyleBackColor = true;
            // 
            // lblPrice
            // 
            lblPrice.AutoSize = true;
            lblPrice.Location = new Point(6, 58);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(41, 20);
            lblPrice.TabIndex = 2;
            lblPrice.Text = "Price";
            // 
            // dgvProductsBill
            // 
            dgvProductsBill.AllowUserToAddRows = false;
            dgvProductsBill.AllowUserToDeleteRows = false;
            dgvProductsBill.AllowUserToResizeRows = false;
            dgvProductsBill.AutoGenerateColumns = false;
            dgvProductsBill.BackgroundColor = Color.White;
            dgvProductsBill.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProductsBill.Columns.AddRange(new DataGridViewColumn[] { barCodeColumn, nameColumnBill });
            dgvProductsBill.DataSource = productsBindingSource;
            dgvProductsBill.Dock = DockStyle.Fill;
            dgvProductsBill.Location = new Point(3, 78);
            dgvProductsBill.Name = "dgvProductsBill";
            dgvProductsBill.ReadOnly = true;
            dgvProductsBill.RowHeadersVisible = false;
            dgvProductsBill.RowHeadersWidth = 51;
            dgvProductsBill.RowTemplate.Height = 29;
            dgvProductsBill.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductsBill.Size = new Size(457, 158);
            dgvProductsBill.TabIndex = 2;
            // 
            // barCodeColumn
            // 
            barCodeColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            barCodeColumn.DataPropertyName = "BarCode";
            barCodeColumn.HeaderText = "BarCode";
            barCodeColumn.MinimumWidth = 6;
            barCodeColumn.Name = "barCodeColumn";
            barCodeColumn.ReadOnly = true;
            barCodeColumn.Width = 150;
            // 
            // nameColumnBill
            // 
            nameColumnBill.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            nameColumnBill.DataPropertyName = "Name";
            nameColumnBill.HeaderText = "Name";
            nameColumnBill.MinimumWidth = 6;
            nameColumnBill.Name = "nameColumnBill";
            nameColumnBill.ReadOnly = true;
            // 
            // tbBills
            // 
            tbBills.Location = new Point(4, 29);
            tbBills.Name = "tbBills";
            tbBills.Padding = new Padding(3);
            tbBills.Size = new Size(944, 476);
            tbBills.TabIndex = 3;
            tbBills.Text = "Bills";
            tbBills.UseVisualStyleBackColor = true;
            // 
            // nameColumBill
            // 
            nameColumBill.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            nameColumBill.DataPropertyName = "Name";
            nameColumBill.FillWeight = 32.4324341F;
            nameColumBill.HeaderText = "Name";
            nameColumBill.MinimumWidth = 6;
            nameColumBill.Name = "nameColumBill";
            nameColumBill.ReadOnly = true;
            // 
            // amountColumnBill
            // 
            amountColumnBill.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            amountColumnBill.DataPropertyName = "Amount";
            amountColumnBill.HeaderText = "Amount";
            amountColumnBill.MinimumWidth = 6;
            amountColumnBill.Name = "amountColumnBill";
            amountColumnBill.ReadOnly = true;
            amountColumnBill.Width = 150;
            // 
            // priceColumnBill
            // 
            priceColumnBill.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            priceColumnBill.DataPropertyName = "Price";
            priceColumnBill.FillWeight = 167.567566F;
            priceColumnBill.HeaderText = "Price";
            priceColumnBill.MinimumWidth = 6;
            priceColumnBill.Name = "priceColumnBill";
            priceColumnBill.ReadOnly = true;
            priceColumnBill.Width = 150;
            // 
            // StoreTable
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(952, 509);
            Controls.Add(tcShop);
            FormBorderStyle = FormBorderStyle.None;
            Name = "StoreTable";
            Text = "Store";
            tcShop.ResumeLayout(false);
            tpProducts.ResumeLayout(false);
            tlpProducts.ResumeLayout(false);
            tlpProducts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
            ((System.ComponentModel.ISupportInitialize)productsBindingSource).EndInit();
            gbProducts.ResumeLayout(false);
            gbProducts.PerformLayout();
            tbProcurement.ResumeLayout(false);
            tlpProcurement.ResumeLayout(false);
            tlpBill.ResumeLayout(false);
            gbBuy.ResumeLayout(false);
            gbBuy.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBill).EndInit();
            ((System.ComponentModel.ISupportInitialize)productBIllBindingSource).EndInit();
            tlypSelect.ResumeLayout(false);
            gbSearch.ResumeLayout(false);
            gbSearch.PerformLayout();
            gbAdd.ResumeLayout(false);
            gbAdd.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProductsBill).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tcShop;
        private TabPage tbProcurement;
        private TabPage tpProducts;
        private DataGridView dgvProducts;
        private BindingSource productsBindingSource;
        private TableLayoutPanel tlpProducts;
        private GroupBox gbProducts;
        private Button btnAdd;
        private Button btnDelete;
        private Button btnUpdate;
        private TextBox txtSearch;
        private Label lblSearch;
        private DataGridViewTextBoxColumn NameCol;
        private DataGridViewTextBoxColumn BarCode;
        private DataGridViewTextBoxColumn NameColumn;
        private DataGridViewTextBoxColumn Price;
        private DataGridViewTextBoxColumn Supplies;
        private DataGridViewTextBoxColumn ExpirationDate;
        private DataGridViewTextBoxColumn Description;
        private DataGridViewTextBoxColumn CategoryName;
        private DataGridViewTextBoxColumn ManufacturerName;
        private TabPage tbBills;
        private TableLayoutPanel tlpProcurement;
        private TableLayoutPanel tlpBill;
        private TableLayoutPanel tlypSelect;
        private GroupBox gbBuy;
        private GroupBox gbSearch;
        private GroupBox gbAdd;
        private TextBox txtSearchBarCode;
        private Label lblSearchProduct;
        private Button btnAddToCart;
        private Button btnBuy;
        private TextBox txtAmount;
        private Label lblAmount;
        private TextBox txtPrice;
        private Label lblPrice;
        private TextBox txtTotal;
        private Label lblTotal;
        private DataGridView dgvProductsBill;
        private DataGridView dgvBill;
        private BindingSource productBIllBindingSource;
        private Button btnDeleteItem;
        private DataGridViewTextBoxColumn barCodeColumn;
        private DataGridViewTextBoxColumn nameColumnBill;
        private DataGridViewTextBoxColumn nameColumBill;
        private DataGridViewTextBoxColumn amountColumnBill;
        private DataGridViewTextBoxColumn priceColumnBill;
    }
}