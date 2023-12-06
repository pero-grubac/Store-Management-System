namespace Prodavnica.Forms.HelperForms.Admin.Popup.ProcurementPopup
{
    partial class ProcurementDetails
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
            dgvProcurement = new DataGridView();
            procurementItemBindingSource = new BindingSource(components);
            billItemBindingSource = new BindingSource(components);
            manufacturer = new DataGridViewTextBoxColumn();
            name = new DataGridViewTextBoxColumn();
            amount = new DataGridViewTextBoxColumn();
            price = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvProcurement).BeginInit();
            ((System.ComponentModel.ISupportInitialize)procurementItemBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)billItemBindingSource).BeginInit();
            SuspendLayout();
            // 
            // dgvProcurement
            // 
            dgvProcurement.AllowUserToAddRows = false;
            dgvProcurement.AllowUserToDeleteRows = false;
            dgvProcurement.AutoGenerateColumns = false;
            dgvProcurement.BackgroundColor = Color.White;
            dgvProcurement.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvProcurement.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProcurement.Columns.AddRange(new DataGridViewColumn[] { manufacturer, name, amount, price });
            dgvProcurement.DataSource = procurementItemBindingSource;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Window;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.Format = "N2";
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            dgvProcurement.DefaultCellStyle = dataGridViewCellStyle1;
            dgvProcurement.Dock = DockStyle.Fill;
            dgvProcurement.GridColor = SystemColors.WindowText;
            dgvProcurement.Location = new Point(0, 0);
            dgvProcurement.Name = "dgvProcurement";
            dgvProcurement.ReadOnly = true;
            dgvProcurement.RowHeadersVisible = false;
            dgvProcurement.RowHeadersWidth = 51;
            dgvProcurement.RowTemplate.Height = 29;
            dgvProcurement.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProcurement.Size = new Size(600, 450);
            dgvProcurement.TabIndex = 0;
            // 
            // procurementItemBindingSource
            // 
            procurementItemBindingSource.DataSource = typeof(Database.DTO.ProcurementItem);
            // 
            // billItemBindingSource
            // 
            billItemBindingSource.DataSource = typeof(Database.DTO.BillItem);
            // 
            // manufacturer
            // 
            manufacturer.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            manufacturer.DataPropertyName = "ManufacturerName";
            manufacturer.HeaderText = "ManufacturerName";
            manufacturer.MinimumWidth = 6;
            manufacturer.Name = "manufacturer";
            manufacturer.ReadOnly = true;
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
            // ProcurementDetails
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(600, 450);
            Controls.Add(dgvProcurement);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ProcurementDetails";
            StartPosition = FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)dgvProcurement).EndInit();
            ((System.ComponentModel.ISupportInitialize)procurementItemBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)billItemBindingSource).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvProcurement;
        private BindingSource billItemBindingSource;
        private BindingSource procurementItemBindingSource;
        private DataGridViewTextBoxColumn manufacturer;
        private DataGridViewTextBoxColumn name;
        private DataGridViewTextBoxColumn amount;
        private DataGridViewTextBoxColumn price;
    }
}