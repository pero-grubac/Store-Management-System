namespace Prodavnica.Forms.HelperForms.Admin.Popup.Products
{
    partial class ProductsDetails
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
            lblManufacturer = new Label();
            txtSupplies = new TextBox();
            lblSupplies = new Label();
            txtPrice = new TextBox();
            lblPrice = new Label();
            txtBarcode = new TextBox();
            lblBarCode = new Label();
            lblCategory = new Label();
            cbManufacturer = new ComboBox();
            cbCategory = new ComboBox();
            lblDate = new Label();
            dtpDate = new DateTimePicker();
            btnSave = new Button();
            btnCancel = new Button();
            rtbMessage = new RichTextBox();
            txtDescription = new TextBox();
            lblDescription = new Label();
            SuspendLayout();
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(39, 40);
            lblName.Name = "lblName";
            lblName.Size = new Size(49, 20);
            lblName.TabIndex = 0;
            lblName.Text = "Name";
            // 
            // txtName
            // 
            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtName.Location = new Point(173, 33);
            txtName.Name = "txtName";
            txtName.Size = new Size(283, 27);
            txtName.TabIndex = 1;
            // 
            // lblManufacturer
            // 
            lblManufacturer.AutoSize = true;
            lblManufacturer.Location = new Point(39, 92);
            lblManufacturer.Name = "lblManufacturer";
            lblManufacturer.Size = new Size(97, 20);
            lblManufacturer.TabIndex = 2;
            lblManufacturer.Text = "Manufacturer";
            // 
            // txtSupplies
            // 
            txtSupplies.BorderStyle = BorderStyle.FixedSingle;
            txtSupplies.Location = new Point(173, 139);
            txtSupplies.Name = "txtSupplies";
            txtSupplies.Size = new Size(283, 27);
            txtSupplies.TabIndex = 5;
            txtSupplies.TextChanged += txtSupplies_TextChanged;
            // 
            // lblSupplies
            // 
            lblSupplies.AutoSize = true;
            lblSupplies.Location = new Point(39, 146);
            lblSupplies.Name = "lblSupplies";
            lblSupplies.Size = new Size(65, 20);
            lblSupplies.TabIndex = 4;
            lblSupplies.Text = "Supplies";
            // 
            // txtPrice
            // 
            txtPrice.BorderStyle = BorderStyle.FixedSingle;
            txtPrice.Location = new Point(173, 191);
            txtPrice.Name = "txtPrice";
            txtPrice.Size = new Size(283, 27);
            txtPrice.TabIndex = 7;
            txtPrice.TextChanged += txtPrice_TextChanged;
            // 
            // lblPrice
            // 
            lblPrice.AutoSize = true;
            lblPrice.Location = new Point(39, 198);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(41, 20);
            lblPrice.TabIndex = 6;
            lblPrice.Text = "Price";
            // 
            // txtBarcode
            // 
            txtBarcode.BorderStyle = BorderStyle.FixedSingle;
            txtBarcode.Location = new Point(173, 246);
            txtBarcode.Name = "txtBarcode";
            txtBarcode.Size = new Size(283, 27);
            txtBarcode.TabIndex = 9;
            txtBarcode.TextChanged += txtBarcode_TextChanged;
            // 
            // lblBarCode
            // 
            lblBarCode.AutoSize = true;
            lblBarCode.Location = new Point(39, 253);
            lblBarCode.Name = "lblBarCode";
            lblBarCode.Size = new Size(68, 20);
            lblBarCode.TabIndex = 8;
            lblBarCode.Text = "Bar code";
            // 
            // lblCategory
            // 
            lblCategory.AutoSize = true;
            lblCategory.Location = new Point(39, 305);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(69, 20);
            lblCategory.TabIndex = 10;
            lblCategory.Text = "Category";
            // 
            // cbManufacturer
            // 
            cbManufacturer.FormattingEnabled = true;
            cbManufacturer.Location = new Point(173, 84);
            cbManufacturer.Name = "cbManufacturer";
            cbManufacturer.Size = new Size(283, 28);
            cbManufacturer.TabIndex = 12;
            // 
            // cbCategory
            // 
            cbCategory.FormattingEnabled = true;
            cbCategory.Location = new Point(173, 302);
            cbCategory.Name = "cbCategory";
            cbCategory.Size = new Size(283, 28);
            cbCategory.TabIndex = 13;
            // 
            // lblDate
            // 
            lblDate.AutoSize = true;
            lblDate.Location = new Point(39, 363);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(110, 20);
            lblDate.TabIndex = 14;
            lblDate.Text = "Expiration date";
            // 
            // dtpDate
            // 
            dtpDate.Location = new Point(173, 356);
            dtpDate.Name = "dtpDate";
            dtpDate.Size = new Size(283, 27);
            dtpDate.TabIndex = 15;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(237, 513);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(94, 29);
            btnSave.TabIndex = 17;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(362, 513);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(94, 29);
            btnCancel.TabIndex = 18;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCencel_Click;
            // 
            // rtbMessage
            // 
            rtbMessage.BorderStyle = BorderStyle.None;
            rtbMessage.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            rtbMessage.ForeColor = Color.Red;
            rtbMessage.Location = new Point(39, 463);
            rtbMessage.Name = "rtbMessage";
            rtbMessage.ReadOnly = true;
            rtbMessage.Size = new Size(417, 28);
            rtbMessage.TabIndex = 19;
            rtbMessage.Text = "";
            // 
            // txtDescription
            // 
            txtDescription.BorderStyle = BorderStyle.FixedSingle;
            txtDescription.Location = new Point(174, 412);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(283, 27);
            txtDescription.TabIndex = 21;
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(40, 419);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(85, 20);
            lblDescription.TabIndex = 20;
            lblDescription.Text = "Description";
            // 
            // ProductsDetails
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(484, 576);
            Controls.Add(txtDescription);
            Controls.Add(lblDescription);
            Controls.Add(rtbMessage);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(dtpDate);
            Controls.Add(lblDate);
            Controls.Add(cbCategory);
            Controls.Add(cbManufacturer);
            Controls.Add(lblCategory);
            Controls.Add(txtBarcode);
            Controls.Add(lblBarCode);
            Controls.Add(txtPrice);
            Controls.Add(lblPrice);
            Controls.Add(txtSupplies);
            Controls.Add(lblSupplies);
            Controls.Add(lblManufacturer);
            Controls.Add(txtName);
            Controls.Add(lblName);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ProductsDetails";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ProductsDetails";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblName;
        private TextBox txtName;
        private Label lblManufacturer;
        private TextBox txtSupplies;
        private Label lblSupplies;
        private TextBox txtPrice;
        private Label lblPrice;
        private TextBox txtBarcode;
        private Label lblBarCode;
        private Label lblCategory;
        private ComboBox cbManufacturer;
        private ComboBox cbCategory;
        private Label lblDate;
        private DateTimePicker dtpDate;
        private Button btnSave;
        private Button btnCancel;
        private RichTextBox rtbMessage;
        private TextBox txtDescription;
        private Label lblDescription;
    }
}