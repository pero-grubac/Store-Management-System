namespace Prodavnica.Forms.HelperForms.Admin.Popup.BillPopup
{
    partial class BillDetails
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            dgvBillItems = new DataGridView();
            name = new DataGridViewTextBoxColumn();
            amount = new DataGridViewTextBoxColumn();
            price = new DataGridViewTextBoxColumn();
            billItemBindingSource = new BindingSource(components);
            ((System.ComponentModel.ISupportInitialize)dgvBillItems).BeginInit();
            ((System.ComponentModel.ISupportInitialize)billItemBindingSource).BeginInit();
            SuspendLayout();
            // 
            // dgvBillItems
            // 
            dgvBillItems.AllowUserToAddRows = false;
            dgvBillItems.AllowUserToDeleteRows = false;
            dgvBillItems.AutoGenerateColumns = false;
            dgvBillItems.BackgroundColor = SystemColors.Control;
            dgvBillItems.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvBillItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBillItems.Columns.AddRange(new DataGridViewColumn[] { name, amount, price });
            dgvBillItems.DataSource = billItemBindingSource;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Window;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.Format = "N2";
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            dgvBillItems.DefaultCellStyle = dataGridViewCellStyle1;
            dgvBillItems.Dock = DockStyle.Fill;
            dgvBillItems.GridColor = SystemColors.WindowText;
            dgvBillItems.Location = new Point(0, 0);
            dgvBillItems.Name = "dgvBillItems";
            dgvBillItems.ReadOnly = true;
            dgvBillItems.RowHeadersVisible = false;
            dgvBillItems.RowHeadersWidth = 51;
            dgvBillItems.RowTemplate.Height = 29;
            dgvBillItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBillItems.Size = new Size(539, 450);
            dgvBillItems.TabIndex = 0;
            // 
            // name
            // 
            name.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            name.DataPropertyName = "Name";
            name.HeaderText = "Name";
            name.MinimumWidth = 6;
            name.Name = "name";
            name.ReadOnly = true;
            // 
            // amount
            // 
            amount.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            amount.DataPropertyName = "Amount";
            amount.HeaderText = "Amount";
            amount.MinimumWidth = 6;
            amount.Name = "amount";
            amount.ReadOnly = true;
            // 
            // price
            // 
            price.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            price.DataPropertyName = "Price";
            price.HeaderText = "Price";
            price.MinimumWidth = 6;
            price.Name = "price";
            price.ReadOnly = true;
            // 
            // billItemBindingSource
            // 
            billItemBindingSource.DataSource = typeof(Database.DTO.BillItem);
            // 
            // BillDetails
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(539, 450);
            Controls.Add(dgvBillItems);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "BillDetails";
            StartPosition = FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)dgvBillItems).EndInit();
            ((System.ComponentModel.ISupportInitialize)billItemBindingSource).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvBillItems;
        private DataGridViewTextBoxColumn name;
        private DataGridViewTextBoxColumn amount;
        private DataGridViewTextBoxColumn price;
        private BindingSource billItemBindingSource;
    }
}