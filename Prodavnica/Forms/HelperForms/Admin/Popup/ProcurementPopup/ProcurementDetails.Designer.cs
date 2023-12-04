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
            manufacturerNameDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            nameDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            amountDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            priceDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            procurementItemBindingSource = new BindingSource(components);
            billItemBindingSource = new BindingSource(components);
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
            dgvProcurement.Columns.AddRange(new DataGridViewColumn[] { manufacturerNameDataGridViewTextBoxColumn, nameDataGridViewTextBoxColumn, amountDataGridViewTextBoxColumn, priceDataGridViewTextBoxColumn });
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
            // manufacturerNameDataGridViewTextBoxColumn
            // 
            manufacturerNameDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            manufacturerNameDataGridViewTextBoxColumn.DataPropertyName = "ManufacturerName";
            manufacturerNameDataGridViewTextBoxColumn.HeaderText = "ManufacturerName";
            manufacturerNameDataGridViewTextBoxColumn.MinimumWidth = 6;
            manufacturerNameDataGridViewTextBoxColumn.Name = "manufacturerNameDataGridViewTextBoxColumn";
            manufacturerNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            nameDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            nameDataGridViewTextBoxColumn.HeaderText = "Name";
            nameDataGridViewTextBoxColumn.MinimumWidth = 6;
            nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            nameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // amountDataGridViewTextBoxColumn
            // 
            amountDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            amountDataGridViewTextBoxColumn.DataPropertyName = "Amount";
            amountDataGridViewTextBoxColumn.HeaderText = "Amount";
            amountDataGridViewTextBoxColumn.MinimumWidth = 6;
            amountDataGridViewTextBoxColumn.Name = "amountDataGridViewTextBoxColumn";
            amountDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // priceDataGridViewTextBoxColumn
            // 
            priceDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            priceDataGridViewTextBoxColumn.DataPropertyName = "Price";
            priceDataGridViewTextBoxColumn.HeaderText = "Price";
            priceDataGridViewTextBoxColumn.MinimumWidth = 6;
            priceDataGridViewTextBoxColumn.Name = "priceDataGridViewTextBoxColumn";
            priceDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // procurementItemBindingSource
            // 
            procurementItemBindingSource.DataSource = typeof(Database.DTO.ProcurementItem);
            // 
            // billItemBindingSource
            // 
            billItemBindingSource.DataSource = typeof(Database.DTO.BillItem);
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
        private DataGridViewTextBoxColumn manufacturerNameDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn amountDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn priceDataGridViewTextBoxColumn;
        private BindingSource procurementItemBindingSource;


    }
}