using Prodavnica.Database.DTO;
using Prodavnica.Database.Repository;
using Prodavnica.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prodavnica.Forms.HelperForms.Admin
{
    public partial class Store : Form
    {
        private User user;
        private List<Product> products;
        private ProductsDAOImpl productsDAO = new ProductsDAOImpl();
        public Store(User user)
        {
            InitializeComponent();
            this.user = user;
            LoadSettings.ApplySettins(user,this);
            this.Resize += Store_Resize;
            GetProduts();
            dgvProducts.DataSource = products;

        }
        private void GetProduts()
        {
            products = productsDAO.GetAll();
        }
        private void Store_Resize(object? sender, EventArgs e)
        {

            if (dgvProducts.AutoSizeColumnsMode == DataGridViewAutoSizeColumnsMode.None)
            {
                int totalWidth = dgvProducts.ClientSize.Width - (dgvProducts.RowHeadersVisible ? dgvProducts.RowHeadersWidth : 0);

                int visibleColumnCount = dgvProducts.Columns.GetColumnCount(DataGridViewElementStates.Visible);
                int resizableColumnCount = visibleColumnCount; 
                int totalFixedWidth = 0;

                foreach (DataGridViewColumn column in dgvProducts.Columns)
                {
                    if (column.Visible)
                    {
                        if (column.Resizable == DataGridViewTriState.False) 
                        {
                            totalFixedWidth += column.Width; 
                            resizableColumnCount--; 
                        }
                    }
                }

                int resizableColumnWidth = (totalWidth - totalFixedWidth) / resizableColumnCount;

                foreach (DataGridViewColumn column in dgvProducts.Columns)
                {
                    if (column.Visible && column.Resizable != DataGridViewTriState.False)
                    {
                        column.Width = resizableColumnWidth; 
                    }
                }
            }
        }
    }
}
