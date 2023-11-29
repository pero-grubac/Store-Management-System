namespace Prodavnica.Forms.HelperForms.Admin
{
    partial class Producer
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
            tlpProducer = new TableLayoutPanel();
            dgvProducer = new DataGridView();
            nameColumn = new DataGridViewTextBoxColumn();
            emailColumn = new DataGridViewTextBoxColumn();
            manufacturerBindingSource = new BindingSource(components);
            tlpOptions = new TableLayoutPanel();
            gbCreate = new GroupBox();
            btnCreate = new Button();
            txtEmail = new TextBox();
            lblEmail = new Label();
            txtName = new TextBox();
            lblName = new Label();
            gbUpdate = new GroupBox();
            btnUpdate = new Button();
            txtUpdateEmail = new TextBox();
            lblUpdateEmail = new Label();
            txtUpdateName = new TextBox();
            lblUpdateName = new Label();
            btnDelete = new Button();
            tlpProducer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProducer).BeginInit();
            ((System.ComponentModel.ISupportInitialize)manufacturerBindingSource).BeginInit();
            tlpOptions.SuspendLayout();
            gbCreate.SuspendLayout();
            gbUpdate.SuspendLayout();
            SuspendLayout();
            // 
            // tlpProducer
            // 
            tlpProducer.ColumnCount = 2;
            tlpProducer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpProducer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpProducer.Controls.Add(dgvProducer, 1, 0);
            tlpProducer.Controls.Add(tlpOptions, 0, 0);
            tlpProducer.Dock = DockStyle.Fill;
            tlpProducer.Location = new Point(0, 0);
            tlpProducer.Name = "tlpProducer";
            tlpProducer.RowCount = 1;
            tlpProducer.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpProducer.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlpProducer.Size = new Size(800, 450);
            tlpProducer.TabIndex = 0;
            // 
            // dgvProducer
            // 
            dgvProducer.AllowUserToAddRows = false;
            dgvProducer.AllowUserToDeleteRows = false;
            dgvProducer.AllowUserToResizeRows = false;
            dgvProducer.AutoGenerateColumns = false;
            dgvProducer.BackgroundColor = Color.White;
            dgvProducer.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvProducer.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProducer.Columns.AddRange(new DataGridViewColumn[] { nameColumn, emailColumn });
            dgvProducer.DataSource = manufacturerBindingSource;
            dgvProducer.Dock = DockStyle.Fill;
            dgvProducer.Location = new Point(403, 3);
            dgvProducer.Name = "dgvProducer";
            dgvProducer.ReadOnly = true;
            dgvProducer.RowHeadersVisible = false;
            dgvProducer.RowHeadersWidth = 51;
            dgvProducer.RowTemplate.Height = 29;
            dgvProducer.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducer.Size = new Size(394, 444);
            dgvProducer.TabIndex = 0;
            dgvProducer.RowHeaderMouseClick += dgvProducer_RowHeaderMouseClick;
            // 
            // nameColumn
            // 
            nameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            nameColumn.DataPropertyName = "Name";
            nameColumn.HeaderText = "Name";
            nameColumn.MinimumWidth = 6;
            nameColumn.Name = "nameColumn";
            nameColumn.ReadOnly = true;
            // 
            // emailColumn
            // 
            emailColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            emailColumn.DataPropertyName = "Email";
            emailColumn.HeaderText = "Email";
            emailColumn.MinimumWidth = 6;
            emailColumn.Name = "emailColumn";
            emailColumn.ReadOnly = true;
            // 
            // manufacturerBindingSource
            // 
            manufacturerBindingSource.DataSource = typeof(Database.DTO.Manufacturer);
            // 
            // tlpOptions
            // 
            tlpOptions.ColumnCount = 1;
            tlpOptions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpOptions.Controls.Add(gbCreate, 0, 0);
            tlpOptions.Controls.Add(gbUpdate, 0, 2);
            tlpOptions.Controls.Add(btnDelete, 0, 1);
            tlpOptions.Dock = DockStyle.Fill;
            tlpOptions.Location = new Point(3, 3);
            tlpOptions.Name = "tlpOptions";
            tlpOptions.RowCount = 3;
            tlpOptions.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpOptions.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F));
            tlpOptions.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpOptions.Size = new Size(394, 444);
            tlpOptions.TabIndex = 1;
            // 
            // gbCreate
            // 
            gbCreate.BackColor = Color.White;
            gbCreate.Controls.Add(btnCreate);
            gbCreate.Controls.Add(txtEmail);
            gbCreate.Controls.Add(lblEmail);
            gbCreate.Controls.Add(txtName);
            gbCreate.Controls.Add(lblName);
            gbCreate.Dock = DockStyle.Fill;
            gbCreate.Location = new Point(3, 3);
            gbCreate.Name = "gbCreate";
            gbCreate.Size = new Size(388, 188);
            gbCreate.TabIndex = 0;
            gbCreate.TabStop = false;
            gbCreate.Text = "Create";
            // 
            // btnCreate
            // 
            btnCreate.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCreate.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnCreate.Location = new Point(273, 137);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(94, 32);
            btnCreate.TabIndex = 4;
            btnCreate.Text = "Save";
            btnCreate.UseVisualStyleBackColor = true;
            // 
            // txtEmail
            // 
            txtEmail.BorderStyle = BorderStyle.FixedSingle;
            txtEmail.Location = new Point(84, 104);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(283, 27);
            txtEmail.TabIndex = 3;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(6, 107);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(46, 20);
            lblEmail.TabIndex = 2;
            lblEmail.Text = "Email";
            // 
            // txtName
            // 
            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtName.Location = new Point(84, 46);
            txtName.Name = "txtName";
            txtName.Size = new Size(283, 27);
            txtName.TabIndex = 1;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(6, 49);
            lblName.Name = "lblName";
            lblName.Size = new Size(49, 20);
            lblName.TabIndex = 0;
            lblName.Text = "Name";
            // 
            // gbUpdate
            // 
            gbUpdate.BackColor = Color.White;
            gbUpdate.Controls.Add(btnUpdate);
            gbUpdate.Controls.Add(txtUpdateEmail);
            gbUpdate.Controls.Add(lblUpdateEmail);
            gbUpdate.Controls.Add(txtUpdateName);
            gbUpdate.Controls.Add(lblUpdateName);
            gbUpdate.Dock = DockStyle.Fill;
            gbUpdate.Location = new Point(3, 252);
            gbUpdate.Name = "gbUpdate";
            gbUpdate.Size = new Size(388, 189);
            gbUpdate.TabIndex = 3;
            gbUpdate.TabStop = false;
            gbUpdate.Text = "Update";
            // 
            // btnUpdate
            // 
            btnUpdate.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnUpdate.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnUpdate.Location = new Point(273, 148);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(94, 32);
            btnUpdate.TabIndex = 6;
            btnUpdate.Text = "Save";
            btnUpdate.UseVisualStyleBackColor = true;
            // 
            // txtUpdateEmail
            // 
            txtUpdateEmail.BorderStyle = BorderStyle.FixedSingle;
            txtUpdateEmail.Location = new Point(84, 115);
            txtUpdateEmail.Name = "txtUpdateEmail";
            txtUpdateEmail.Size = new Size(283, 27);
            txtUpdateEmail.TabIndex = 5;
            // 
            // lblUpdateEmail
            // 
            lblUpdateEmail.AutoSize = true;
            lblUpdateEmail.Location = new Point(6, 118);
            lblUpdateEmail.Name = "lblUpdateEmail";
            lblUpdateEmail.Size = new Size(46, 20);
            lblUpdateEmail.TabIndex = 4;
            lblUpdateEmail.Text = "Email";
            // 
            // txtUpdateName
            // 
            txtUpdateName.BorderStyle = BorderStyle.FixedSingle;
            txtUpdateName.Location = new Point(84, 62);
            txtUpdateName.Name = "txtUpdateName";
            txtUpdateName.Size = new Size(283, 27);
            txtUpdateName.TabIndex = 3;
            // 
            // lblUpdateName
            // 
            lblUpdateName.AutoSize = true;
            lblUpdateName.Location = new Point(6, 65);
            lblUpdateName.Name = "lblUpdateName";
            lblUpdateName.Size = new Size(49, 20);
            lblUpdateName.TabIndex = 2;
            lblUpdateName.Text = "Name";
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.White;
            btnDelete.Dock = DockStyle.Fill;
            btnDelete.Location = new Point(3, 197);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(388, 49);
            btnDelete.TabIndex = 4;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = false;
            // 
            // Producer
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            ControlBox = false;
            Controls.Add(tlpProducer);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Producer";
            Text = "Producer";
            tlpProducer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvProducer).EndInit();
            ((System.ComponentModel.ISupportInitialize)manufacturerBindingSource).EndInit();
            tlpOptions.ResumeLayout(false);
            gbCreate.ResumeLayout(false);
            gbCreate.PerformLayout();
            gbUpdate.ResumeLayout(false);
            gbUpdate.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tlpProducer;
        private DataGridView dgvProducer;
        private DataGridViewTextBoxColumn nameColumn;
        private DataGridViewTextBoxColumn emailColumn;
        private BindingSource manufacturerBindingSource;
        private TableLayoutPanel tlpOptions;
        private GroupBox gbCreate;
        private Button btnDelete;
        private GroupBox gbUpdate;
        private TextBox txtName;
        private Label lblName;
        private TextBox txtEmail;
        private Label lblEmail;
        private TextBox txtUpdateEmail;
        private Label lblUpdateEmail;
        private TextBox txtUpdateName;
        private Label lblUpdateName;
        private Button btnCreate;
        private Button btnUpdate;
    }
}