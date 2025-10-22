using System;
using System.Windows.Forms;
using System.Linq;
using TMDT.DAL;

namespace TMDT.Use
{
    public partial class OrderHistoryForm : Form
    {
        public OrderHistoryForm()
        {
            InitializeComponent();
            LoadOrders();
        }

        // Load orders for current user
        private void LoadOrders()
        {
            var user = Session.CurrentCustomer;
            if (user == null) return;

            using (var db = new Model1())
            {
                var orders = db.OrderTbls
                    .Where(o => o.CustomerID == user.CustomerID)
                    .OrderByDescending(o => o.OrderDate)
                    .Select(o => new
                    {
                        o.OrderID,
                        o.OrderDate,
                        o.Status,
                        SubTotal = o.SubTotal,
                        ShippingFee = o.ShippingFee,
                        Total = o.TotalAmount
                    })
                    .ToList();

                this.dgvDonHang.DataSource = orders;
                this.dgvChiTiet.DataSource = null;
            }
        }

        // When selecting an order, show its items
        private void dgvDonHang_SelectionChanged(object sender, EventArgs e)
        {
            if (this.dgvDonHang.CurrentRow == null) return;
            var orderIdObj = this.dgvDonHang.CurrentRow.Cells["OrderID"]?.Value;
            if (orderIdObj == null) return;
            int orderId = Convert.ToInt32(orderIdObj);

            using (var db = new Model1())
            {
                var items = db.OrderItems
                    .Where(oi => oi.OrderID == orderId)
                    .Select(oi => new
                    {
                        oi.SKU,
                        oi.ProductName,
                        oi.Quantity,
                        UnitPrice = oi.UnitPrice,
                        Total = oi.TotalPrice
                    })
                    .ToList();
                this.dgvChiTiet.DataSource = items;
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void FocusOrder(int orderId)
        {
            try
            {
                foreach (DataGridViewRow row in dgvDonHang.Rows)
                {
                    if (row.Cells["OrderID"].Value != null &&
                        Convert.ToInt32(row.Cells["OrderID"].Value) == orderId)
                    {
                        row.Selected = true;
                        dgvDonHang.FirstDisplayedScrollingRowIndex = row.Index;
                        break;
                    }
                }
            }
            catch
            {
                // Ignore errors if grid is empty or not ready
            }
        }


    }
}
