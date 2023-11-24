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
            tpProducts = new TabPage();
            tlpProducts = new TableLayoutPanel();
            dgvProducts = new DataGridView();
            barCodeDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            nameDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            priceDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            suppliesDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            expirationDateDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            descriptionDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
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
            tlpBasketAndProc = new TableLayoutPanel();
            tlpProcurement = new TableLayoutPanel();
            gbOptions = new GroupBox();
            btnAddBasket = new Button();
            gbCategoryName = new GroupBox();
            txtCategoryName = new TextBox();
            gbExpirationDate = new GroupBox();
            dateTimePicker1 = new DateTimePicker();
            gbDescription = new GroupBox();
            txtDescription = new TextBox();
            gbSupplies = new GroupBox();
            txtSupplies = new TextBox();
            gbPurchasePrice = new GroupBox();
            txtPurchasePrice = new TextBox();
            gbPrice = new GroupBox();
            txtPrice = new TextBox();
            gbBarcode = new GroupBox();
            txtBarCode = new TextBox();
            gbProductName = new GroupBox();
            txtName = new TextBox();
            gbManufactuer = new GroupBox();
            txtManufactuer = new TextBox();
            btnOldMan = new Button();
            btnNewMan = new Button();
            gbSupplier = new GroupBox();
            txtSupplier = new TextBox();
            btnOldSup = new Button();
            btnNewSup = new Button();
            tlpBasket = new TableLayoutPanel();
            gbRemove = new GroupBox();
            btnBuy = new Button();
            btnBskRemove = new Button();
            txt = new TextBox();
            tcShop.SuspendLayout();
            tpProducts.SuspendLayout();
            tlpProducts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
            ((System.ComponentModel.ISupportInitialize)productsBindingSource).BeginInit();
            gbProducts.SuspendLayout();
            tbProcurement.SuspendLayout();
            tlpBasketAndProc.SuspendLayout();
            tlpProcurement.SuspendLayout();
            gbOptions.SuspendLayout();
            gbCategoryName.SuspendLayout();
            gbExpirationDate.SuspendLayout();
            gbDescription.SuspendLayout();
            gbSupplies.SuspendLayout();
            gbPurchasePrice.SuspendLayout();
            gbPrice.SuspendLayout();
            gbBarcode.SuspendLayout();
            gbProductName.SuspendLayout();
            gbManufactuer.SuspendLayout();
            gbSupplier.SuspendLayout();
            tlpBasket.SuspendLayout();
            gbRemove.SuspendLayout();
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
            tcShop.Size = new Size(952, 800);
            tcShop.TabIndex = 0;
            tcShop.SelectedIndexChanged += tcShop_SelectedIndexChanged;
            // 
            // tpProducts
            // 
            tpProducts.Controls.Add(tlpProducts);
            tpProducts.Location = new Point(4, 29);
            tpProducts.Name = "tpProducts";
            tpProducts.Padding = new Padding(3);
            tpProducts.Size = new Size(944, 767);
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
            tlpProducts.Size = new Size(938, 761);
            tlpProducts.TabIndex = 1;
            // 
            // dgvProducts
            // 
            dgvProducts.AllowUserToAddRows = false;
            dgvProducts.AllowUserToDeleteRows = false;
            dgvProducts.AutoGenerateColumns = false;
            dgvProducts.BackgroundColor = Color.White;
            dgvProducts.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProducts.Columns.AddRange(new DataGridViewColumn[] { barCodeDataGridViewTextBoxColumn, nameDataGridViewTextBoxColumn, priceDataGridViewTextBoxColumn, suppliesDataGridViewTextBoxColumn, expirationDateDataGridViewTextBoxColumn, descriptionDataGridViewTextBoxColumn, CategoryName, ManufacturerName });
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
            // barCodeDataGridViewTextBoxColumn
            // 
            barCodeDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            barCodeDataGridViewTextBoxColumn.DataPropertyName = "BarCode";
            barCodeDataGridViewTextBoxColumn.HeaderText = "BarCode";
            barCodeDataGridViewTextBoxColumn.MinimumWidth = 6;
            barCodeDataGridViewTextBoxColumn.Name = "barCodeDataGridViewTextBoxColumn";
            barCodeDataGridViewTextBoxColumn.ReadOnly = true;
            barCodeDataGridViewTextBoxColumn.Resizable = DataGridViewTriState.False;
            barCodeDataGridViewTextBoxColumn.Width = 125;
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
            // CategoryName
            // 
            CategoryName.DataPropertyName = "CategoryName";
            CategoryName.HeaderText = "Category";
            CategoryName.MinimumWidth = 6;
            CategoryName.Name = "CategoryName";
            CategoryName.ReadOnly = true;
            CategoryName.Resizable = DataGridViewTriState.False;
            CategoryName.Width = 125;
            // 
            // ManufacturerName
            // 
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
            tbProcurement.Controls.Add(tlpBasketAndProc);
            tbProcurement.Location = new Point(4, 29);
            tbProcurement.Name = "tbProcurement";
            tbProcurement.Padding = new Padding(3);
            tbProcurement.Size = new Size(944, 767);
            tbProcurement.TabIndex = 1;
            tbProcurement.Text = "Procurement";
            tbProcurement.UseVisualStyleBackColor = true;
            // 
            // tlpBasketAndProc
            // 
            tlpBasketAndProc.AutoScroll = true;
            tlpBasketAndProc.ColumnCount = 2;
            tlpBasketAndProc.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            tlpBasketAndProc.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tlpBasketAndProc.Controls.Add(tlpProcurement, 0, 0);
            tlpBasketAndProc.Controls.Add(tlpBasket, 1, 0);
            tlpBasketAndProc.Dock = DockStyle.Fill;
            tlpBasketAndProc.Location = new Point(3, 3);
            tlpBasketAndProc.Name = "tlpBasketAndProc";
            tlpBasketAndProc.RowCount = 1;
            tlpBasketAndProc.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpBasketAndProc.Size = new Size(938, 761);
            tlpBasketAndProc.TabIndex = 1;
            // 
            // tlpProcurement
            // 
            tlpProcurement.AutoScroll = true;
            tlpProcurement.ColumnCount = 1;
            tlpProcurement.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpProcurement.Controls.Add(gbOptions, 0, 10);
            tlpProcurement.Controls.Add(gbCategoryName, 0, 9);
            tlpProcurement.Controls.Add(gbExpirationDate, 0, 8);
            tlpProcurement.Controls.Add(gbDescription, 0, 7);
            tlpProcurement.Controls.Add(gbSupplies, 0, 6);
            tlpProcurement.Controls.Add(gbPurchasePrice, 0, 5);
            tlpProcurement.Controls.Add(gbPrice, 0, 4);
            tlpProcurement.Controls.Add(gbBarcode, 0, 3);
            tlpProcurement.Controls.Add(gbProductName, 0, 2);
            tlpProcurement.Controls.Add(gbManufactuer, 0, 1);
            tlpProcurement.Controls.Add(gbSupplier, 0, 0);
            tlpProcurement.Dock = DockStyle.Fill;
            tlpProcurement.Location = new Point(3, 3);
            tlpProcurement.Name = "tlpProcurement";
            tlpProcurement.RowCount = 11;
            tlpProcurement.RowStyles.Add(new RowStyle(SizeType.Absolute, 77F));
            tlpProcurement.RowStyles.Add(new RowStyle(SizeType.Absolute, 77F));
            tlpProcurement.RowStyles.Add(new RowStyle(SizeType.Absolute, 77F));
            tlpProcurement.RowStyles.Add(new RowStyle(SizeType.Absolute, 77F));
            tlpProcurement.RowStyles.Add(new RowStyle(SizeType.Absolute, 77F));
            tlpProcurement.RowStyles.Add(new RowStyle(SizeType.Absolute, 77F));
            tlpProcurement.RowStyles.Add(new RowStyle(SizeType.Absolute, 77F));
            tlpProcurement.RowStyles.Add(new RowStyle(SizeType.Absolute, 77F));
            tlpProcurement.RowStyles.Add(new RowStyle(SizeType.Absolute, 77F));
            tlpProcurement.RowStyles.Add(new RowStyle(SizeType.Absolute, 77F));
            tlpProcurement.RowStyles.Add(new RowStyle(SizeType.Absolute, 77F));
            tlpProcurement.Size = new Size(650, 755);
            tlpProcurement.TabIndex = 0;
            // 
            // gbOptions
            // 
            gbOptions.BackColor = Color.White;
            gbOptions.Controls.Add(btnAddBasket);
            gbOptions.Dock = DockStyle.Fill;
            gbOptions.Location = new Point(3, 773);
            gbOptions.Name = "gbOptions";
            gbOptions.Size = new Size(644, 71);
            gbOptions.TabIndex = 10;
            gbOptions.TabStop = false;
            gbOptions.Text = "Options";
            // 
            // btnAddBasket
            // 
            btnAddBasket.Anchor = AnchorStyles.Right;
            btnAddBasket.Location = new Point(496, 36);
            btnAddBasket.Name = "btnAddBasket";
            btnAddBasket.Size = new Size(126, 29);
            btnAddBasket.TabIndex = 3;
            btnAddBasket.Text = "Add to basket";
            btnAddBasket.UseVisualStyleBackColor = true;
            btnAddBasket.Click += btnAddBasket_Click;
            // 
            // gbCategoryName
            // 
            gbCategoryName.BackColor = Color.White;
            gbCategoryName.Controls.Add(txtCategoryName);
            gbCategoryName.Dock = DockStyle.Fill;
            gbCategoryName.Location = new Point(3, 696);
            gbCategoryName.Name = "gbCategoryName";
            gbCategoryName.Size = new Size(644, 71);
            gbCategoryName.TabIndex = 9;
            gbCategoryName.TabStop = false;
            gbCategoryName.Text = "Category";
            // 
            // txtCategoryName
            // 
            txtCategoryName.BorderStyle = BorderStyle.FixedSingle;
            txtCategoryName.Location = new Point(6, 31);
            txtCategoryName.Name = "txtCategoryName";
            txtCategoryName.Size = new Size(125, 27);
            txtCategoryName.TabIndex = 3;
            // 
            // gbExpirationDate
            // 
            gbExpirationDate.BackColor = Color.White;
            gbExpirationDate.Controls.Add(dateTimePicker1);
            gbExpirationDate.Dock = DockStyle.Fill;
            gbExpirationDate.Location = new Point(3, 619);
            gbExpirationDate.Name = "gbExpirationDate";
            gbExpirationDate.Size = new Size(644, 71);
            gbExpirationDate.TabIndex = 8;
            gbExpirationDate.TabStop = false;
            gbExpirationDate.Text = "ExpirationDate";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(6, 26);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(289, 27);
            dateTimePicker1.TabIndex = 0;
            // 
            // gbDescription
            // 
            gbDescription.BackColor = Color.White;
            gbDescription.Controls.Add(txtDescription);
            gbDescription.Dock = DockStyle.Fill;
            gbDescription.Location = new Point(3, 542);
            gbDescription.Name = "gbDescription";
            gbDescription.Size = new Size(644, 71);
            gbDescription.TabIndex = 7;
            gbDescription.TabStop = false;
            gbDescription.Text = "Description";
            // 
            // txtDescription
            // 
            txtDescription.BorderStyle = BorderStyle.FixedSingle;
            txtDescription.Location = new Point(6, 26);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(125, 27);
            txtDescription.TabIndex = 2;
            // 
            // gbSupplies
            // 
            gbSupplies.BackColor = Color.White;
            gbSupplies.Controls.Add(txtSupplies);
            gbSupplies.Dock = DockStyle.Fill;
            gbSupplies.Location = new Point(3, 465);
            gbSupplies.Name = "gbSupplies";
            gbSupplies.Size = new Size(644, 71);
            gbSupplies.TabIndex = 6;
            gbSupplies.TabStop = false;
            gbSupplies.Text = "Supplies";
            // 
            // txtSupplies
            // 
            txtSupplies.BorderStyle = BorderStyle.FixedSingle;
            txtSupplies.Location = new Point(6, 26);
            txtSupplies.Name = "txtSupplies";
            txtSupplies.Size = new Size(125, 27);
            txtSupplies.TabIndex = 2;
            // 
            // gbPurchasePrice
            // 
            gbPurchasePrice.BackColor = Color.White;
            gbPurchasePrice.Controls.Add(txtPurchasePrice);
            gbPurchasePrice.Dock = DockStyle.Fill;
            gbPurchasePrice.Location = new Point(3, 388);
            gbPurchasePrice.Name = "gbPurchasePrice";
            gbPurchasePrice.Size = new Size(644, 71);
            gbPurchasePrice.TabIndex = 5;
            gbPurchasePrice.TabStop = false;
            gbPurchasePrice.Text = "Purchase price";
            // 
            // txtPurchasePrice
            // 
            txtPurchasePrice.BorderStyle = BorderStyle.FixedSingle;
            txtPurchasePrice.Location = new Point(6, 31);
            txtPurchasePrice.Name = "txtPurchasePrice";
            txtPurchasePrice.Size = new Size(125, 27);
            txtPurchasePrice.TabIndex = 2;
            // 
            // gbPrice
            // 
            gbPrice.BackColor = Color.White;
            gbPrice.Controls.Add(txtPrice);
            gbPrice.Dock = DockStyle.Fill;
            gbPrice.Location = new Point(3, 311);
            gbPrice.Name = "gbPrice";
            gbPrice.Size = new Size(644, 71);
            gbPrice.TabIndex = 4;
            gbPrice.TabStop = false;
            gbPrice.Text = "Price";
            // 
            // txtPrice
            // 
            txtPrice.BorderStyle = BorderStyle.FixedSingle;
            txtPrice.Location = new Point(6, 26);
            txtPrice.Name = "txtPrice";
            txtPrice.Size = new Size(125, 27);
            txtPrice.TabIndex = 2;
            // 
            // gbBarcode
            // 
            gbBarcode.BackColor = Color.White;
            gbBarcode.Controls.Add(txtBarCode);
            gbBarcode.Dock = DockStyle.Fill;
            gbBarcode.Location = new Point(3, 234);
            gbBarcode.Name = "gbBarcode";
            gbBarcode.Size = new Size(644, 71);
            gbBarcode.TabIndex = 3;
            gbBarcode.TabStop = false;
            gbBarcode.Text = "Bar code";
            // 
            // txtBarCode
            // 
            txtBarCode.BorderStyle = BorderStyle.FixedSingle;
            txtBarCode.Location = new Point(6, 26);
            txtBarCode.Name = "txtBarCode";
            txtBarCode.Size = new Size(125, 27);
            txtBarCode.TabIndex = 2;
            // 
            // gbProductName
            // 
            gbProductName.BackColor = Color.White;
            gbProductName.Controls.Add(txtName);
            gbProductName.Dock = DockStyle.Fill;
            gbProductName.Location = new Point(3, 157);
            gbProductName.Name = "gbProductName";
            gbProductName.Size = new Size(644, 71);
            gbProductName.TabIndex = 2;
            gbProductName.TabStop = false;
            gbProductName.Text = "Name";
            // 
            // txtName
            // 
            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtName.Location = new Point(6, 26);
            txtName.Name = "txtName";
            txtName.Size = new Size(125, 27);
            txtName.TabIndex = 1;
            // 
            // gbManufactuer
            // 
            gbManufactuer.BackColor = Color.White;
            gbManufactuer.Controls.Add(txtManufactuer);
            gbManufactuer.Controls.Add(btnOldMan);
            gbManufactuer.Controls.Add(btnNewMan);
            gbManufactuer.Dock = DockStyle.Fill;
            gbManufactuer.Location = new Point(3, 80);
            gbManufactuer.Name = "gbManufactuer";
            gbManufactuer.Size = new Size(644, 71);
            gbManufactuer.TabIndex = 1;
            gbManufactuer.TabStop = false;
            gbManufactuer.Text = "Manufactuer";
            // 
            // txtManufactuer
            // 
            txtManufactuer.BorderStyle = BorderStyle.FixedSingle;
            txtManufactuer.Location = new Point(206, 27);
            txtManufactuer.Name = "txtManufactuer";
            txtManufactuer.ReadOnly = true;
            txtManufactuer.Size = new Size(153, 27);
            txtManufactuer.TabIndex = 5;
            // 
            // btnOldMan
            // 
            btnOldMan.Location = new Point(106, 26);
            btnOldMan.Name = "btnOldMan";
            btnOldMan.Size = new Size(94, 32);
            btnOldMan.TabIndex = 4;
            btnOldMan.Text = "Old";
            btnOldMan.UseVisualStyleBackColor = true;
            // 
            // btnNewMan
            // 
            btnNewMan.Location = new Point(6, 26);
            btnNewMan.Name = "btnNewMan";
            btnNewMan.Size = new Size(94, 32);
            btnNewMan.TabIndex = 3;
            btnNewMan.Text = "New";
            btnNewMan.UseVisualStyleBackColor = true;
            // 
            // gbSupplier
            // 
            gbSupplier.BackColor = Color.White;
            gbSupplier.Controls.Add(txtSupplier);
            gbSupplier.Controls.Add(btnOldSup);
            gbSupplier.Controls.Add(btnNewSup);
            gbSupplier.Dock = DockStyle.Fill;
            gbSupplier.Location = new Point(3, 3);
            gbSupplier.Name = "gbSupplier";
            gbSupplier.Size = new Size(644, 71);
            gbSupplier.TabIndex = 0;
            gbSupplier.TabStop = false;
            gbSupplier.Text = "Supplier";
            // 
            // txtSupplier
            // 
            txtSupplier.BorderStyle = BorderStyle.FixedSingle;
            txtSupplier.Location = new Point(206, 24);
            txtSupplier.Name = "txtSupplier";
            txtSupplier.ReadOnly = true;
            txtSupplier.Size = new Size(416, 27);
            txtSupplier.TabIndex = 2;
            txtSupplier.TextChanged += txtSupplier_TextChanged;
            // 
            // btnOldSup
            // 
            btnOldSup.Location = new Point(106, 23);
            btnOldSup.Name = "btnOldSup";
            btnOldSup.Size = new Size(94, 32);
            btnOldSup.TabIndex = 1;
            btnOldSup.Text = "Old";
            btnOldSup.UseVisualStyleBackColor = true;
            btnOldSup.Click += btnSuppliers_Click;
            // 
            // btnNewSup
            // 
            btnNewSup.Location = new Point(6, 23);
            btnNewSup.Name = "btnNewSup";
            btnNewSup.Size = new Size(94, 32);
            btnNewSup.TabIndex = 0;
            btnNewSup.Text = "New";
            btnNewSup.UseVisualStyleBackColor = true;
            btnNewSup.Click += btnNew_Click;
            // 
            // tlpBasket
            // 
            tlpBasket.AutoScroll = true;
            tlpBasket.ColumnCount = 1;
            tlpBasket.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpBasket.Controls.Add(gbRemove, 0, 2);
            tlpBasket.Controls.Add(txt, 0, 0);
            tlpBasket.Dock = DockStyle.Fill;
            tlpBasket.Location = new Point(659, 3);
            tlpBasket.Name = "tlpBasket";
            tlpBasket.RowCount = 3;
            tlpBasket.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tlpBasket.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpBasket.RowStyles.Add(new RowStyle());
            tlpBasket.Size = new Size(276, 755);
            tlpBasket.TabIndex = 1;
            // 
            // gbRemove
            // 
            gbRemove.BackColor = Color.White;
            gbRemove.Controls.Add(btnBuy);
            gbRemove.Controls.Add(btnBskRemove);
            gbRemove.Dock = DockStyle.Fill;
            gbRemove.Location = new Point(3, 542);
            gbRemove.Name = "gbRemove";
            gbRemove.Size = new Size(270, 210);
            gbRemove.TabIndex = 1;
            gbRemove.TabStop = false;
            gbRemove.Text = "Remove";
            // 
            // btnBuy
            // 
            btnBuy.Anchor = AnchorStyles.Right;
            btnBuy.Location = new Point(170, 154);
            btnBuy.Name = "btnBuy";
            btnBuy.Size = new Size(94, 29);
            btnBuy.TabIndex = 5;
            btnBuy.Text = "Buy";
            btnBuy.UseVisualStyleBackColor = true;
            // 
            // btnBskRemove
            // 
            btnBskRemove.Anchor = AnchorStyles.Right;
            btnBskRemove.Location = new Point(50, 154);
            btnBskRemove.Name = "btnBskRemove";
            btnBskRemove.Size = new Size(94, 29);
            btnBskRemove.TabIndex = 4;
            btnBskRemove.Text = "Remove";
            btnBskRemove.UseVisualStyleBackColor = true;
            // 
            // txt
            // 
            txt.BackColor = Color.White;
            txt.BorderStyle = BorderStyle.None;
            txt.Dock = DockStyle.Fill;
            txt.Location = new Point(3, 3);
            txt.Name = "txt";
            txt.ReadOnly = true;
            txt.Size = new Size(270, 20);
            txt.TabIndex = 2;
            txt.Text = "Procurement";
            txt.TextAlign = HorizontalAlignment.Center;
            // 
            // Store
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(952, 800);
            Controls.Add(tcShop);
            Name = "Store";
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
            tlpBasketAndProc.ResumeLayout(false);
            tlpProcurement.ResumeLayout(false);
            gbOptions.ResumeLayout(false);
            gbCategoryName.ResumeLayout(false);
            gbCategoryName.PerformLayout();
            gbExpirationDate.ResumeLayout(false);
            gbDescription.ResumeLayout(false);
            gbDescription.PerformLayout();
            gbSupplies.ResumeLayout(false);
            gbSupplies.PerformLayout();
            gbPurchasePrice.ResumeLayout(false);
            gbPurchasePrice.PerformLayout();
            gbPrice.ResumeLayout(false);
            gbPrice.PerformLayout();
            gbBarcode.ResumeLayout(false);
            gbBarcode.PerformLayout();
            gbProductName.ResumeLayout(false);
            gbProductName.PerformLayout();
            gbManufactuer.ResumeLayout(false);
            gbManufactuer.PerformLayout();
            gbSupplier.ResumeLayout(false);
            gbSupplier.PerformLayout();
            tlpBasket.ResumeLayout(false);
            tlpBasket.PerformLayout();
            gbRemove.ResumeLayout(false);
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
        private DataGridViewTextBoxColumn barCodeDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn priceDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn suppliesDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn expirationDateDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn CategoryName;
        private DataGridViewTextBoxColumn ManufacturerName;
        private TableLayoutPanel tlpProcurement;
        private GroupBox gbOptions;
        private GroupBox gbCategoryName;
        private GroupBox gbExpirationDate;
        private GroupBox gbDescription;
        private GroupBox gbSupplies;
        private GroupBox gbPurchasePrice;
        private GroupBox gbPrice;
        private GroupBox gbBarcode;
        private GroupBox gbProductName;
        private GroupBox gbManufactuer;
        private GroupBox gbSupplier;
        private TextBox txtSupplier;
        private Button btnOldSup;
        private Button btnNewSup;
        private TextBox txtDescription;
        private TextBox txtSupplies;
        private TextBox txtPurchasePrice;
        private TextBox txtPrice;
        private TextBox txtBarCode;
        private TextBox txtName;
        private TextBox txtCategoryName;
        private TextBox txtManufactuer;
        private Button btnOldMan;
        private Button btnNewMan;
        private DateTimePicker dateTimePicker1;
        private Button btnBuy;
        private Button btnBskRemove;
        private Button btnAddBasket;
        private TableLayoutPanel tlpBasketAndProc;
        private TableLayoutPanel tlpBasket;
        private GroupBox gbRemove;
        private TextBox txt;
    }
}