using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using TMDT.DAL;

namespace TMDT.Admin
{
    public partial class DonHangForm : Form
    {
        public DonHangForm()
        {
            InitializeComponent();
            LoadOrders();
        }

        private void LoadOrders()
        {
            using (var db = new Model1())
            {
                var data = db.OrderTbls
                    .Select(o => new { o.OrderID, Customer = o.Customer.FullName, o.OrderDate, o.Status, o.SubTotal, o.ShippingFee, o.TotalAmount })
                    .OrderByDescending(o => o.OrderID)
                    .ToList();
                this.dgvDonHang.DataSource = data;
                this.dgvChiTiet.DataSource = null;
            }
        }

        private void dgvDonHang_SelectionChanged(object sender, System.EventArgs e)
        {
            if (this.dgvDonHang.CurrentRow == null) return;
            var idObj = this.dgvDonHang.CurrentRow.Cells["OrderID"]?.Value;
            if (idObj == null) return;
            int id = System.Convert.ToInt32(idObj);
            using (var db = new Model1())
            {
                var items = db.OrderItems
                    .Where(oi => oi.OrderID == id)
                    .Select(oi => new { oi.SKU, oi.ProductName, oi.Quantity, oi.UnitPrice, oi.TotalPrice })
                    .ToList();
                this.dgvChiTiet.DataSource = items;
            }
        }

        private void btnCapNhat_Click(object sender, System.EventArgs e)
        {
            if (this.dgvDonHang.CurrentRow == null) return;
            int id = System.Convert.ToInt32(this.dgvDonHang.CurrentRow.Cells["OrderID"].Value);
            var status = this.cboTrangThai.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(status)) return;
            using (var db = new Model1())
            {
                var order = db.OrderTbls.FirstOrDefault(o => o.OrderID == id);
                if (order != null)
                {
                    order.Status = status;
                    db.SaveChanges();
                }
            }
            LoadOrders();
        }

        private void btnXoa_Click(object sender, System.EventArgs e)
        {
            if (this.dgvDonHang.CurrentRow == null) return;
            int id = System.Convert.ToInt32(this.dgvDonHang.CurrentRow.Cells["OrderID"].Value);
            using (var db = new Model1())
            {
                var order = db.OrderTbls.FirstOrDefault(o => o.OrderID == id);
                if (order != null)
                {
                    var details = db.OrderItems.Where(oi => oi.OrderID == id);
                    db.OrderItems.RemoveRange(details);
                    db.OrderTbls.Remove(order);
                    db.SaveChanges();
                }
            }
            LoadOrders();
        }

        private void btnLamMoi_Click(object sender, System.EventArgs e)
        {
            LoadOrders();
        }
    }
}
