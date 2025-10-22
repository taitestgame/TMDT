using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TMDT.DAL;

namespace TMDT.Admin
{
    public partial class KhachHangForm : Form
    {
        public KhachHangForm()
        {
            InitializeComponent();
            LoadCustomers();
        }

       
        // Load dữ liệu khách hàng
        
        private void LoadCustomers()
        {
            using (var db = new Model1())
            {
                var data = db.Customers
                    .Select(c => new
                    {
                        c.CustomerID,
                        c.UserName,
                        c.Password,
                        c.FullName,
                        c.Email,
                        c.Phone,
                        c.CreatedAt,
                        c.IsAdmin
                    })
                    .OrderByDescending(c => c.CustomerID)
                    .ToList();

                dgvKhachHang.DataSource = data;
            }
        }

       
        //  Chọn khách hàng trong bảng -> hiện thông tin
       
        private void dgvKhachHang_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvKhachHang.CurrentRow == null) return;

            txtTen.Text = dgvKhachHang.CurrentRow.Cells["FullName"]?.Value?.ToString();
            txtEmail.Text = dgvKhachHang.CurrentRow.Cells["Email"]?.Value?.ToString();
            txtSDT.Text = dgvKhachHang.CurrentRow.Cells["Phone"]?.Value?.ToString();
            txtUser.Text = dgvKhachHang.CurrentRow.Cells["UserName"]?.Value?.ToString();
            txtPassword.Text = dgvKhachHang.CurrentRow.Cells["Password"]?.Value?.ToString();
        }

        

        
        //   Sửa thông tin khách hàng
        
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvKhachHang.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = Convert.ToInt32(dgvKhachHang.CurrentRow.Cells["CustomerID"].Value);

            using (var db = new Model1())
            {
                var c = db.Customers.FirstOrDefault(x => x.CustomerID == id);
                if (c != null)
                {
                    c.FullName = txtTen.Text?.Trim();
                    c.Phone = txtSDT.Text?.Trim();
                    c.Email = txtEmail.Text?.Trim();
                    db.SaveChanges();
                }
            }

            MessageBox.Show("Cập nhật thông tin thành công!", "Thành công",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadCustomers();
        }

        //   Xoá khách hàng
       
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (this.dgvKhachHang.CurrentRow == null)
                return;

            int id = Convert.ToInt32(this.dgvKhachHang.CurrentRow.Cells["CustomerID"].Value);

            var confirm = MessageBox.Show("Bạn có chắc muốn xóa khách hàng này không?",
                                          "Xác nhận xóa", MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes)
                return;

            using (var db = new Model1())
            {
                var customer = db.Customers.FirstOrDefault(x => x.CustomerID == id);
                if (customer == null)
                    return;

                // Xóa các bảng liên quan trước
                var reviews = db.Reviews.Where(r => r.CustomerID == id);
                db.Reviews.RemoveRange(reviews);

                var orders = db.OrderTbls.Where(o => o.CustomerID == id);
                foreach (var order in orders)
                {
                    var payments = db.Payments.Where(p => p.OrderID == order.OrderID);
                    db.Payments.RemoveRange(payments);

                    var shipments = db.Shipments.Where(s => s.OrderID == order.OrderID);
                    db.Shipments.RemoveRange(shipments);

                    var orderItems = db.OrderItems.Where(oi => oi.OrderID == order.OrderID);
                    db.OrderItems.RemoveRange(orderItems);
                }
                db.OrderTbls.RemoveRange(orders);

                var carts = db.Carts.Where(c => c.CustomerID == id);
                foreach (var cart in carts)
                {
                    var cartItems = db.CartItems.Where(ci => ci.CartID == cart.CartID);
                    db.CartItems.RemoveRange(cartItems);
                }
                db.Carts.RemoveRange(carts);

                var addresses = db.Addresses.Where(a => a.CustomerID == id);
                db.Addresses.RemoveRange(addresses);

                // Cuối cùng mới xóa khách hàng
                db.Customers.Remove(customer);

                try
                {
                    db.SaveChanges();
                    MessageBox.Show("Đã xóa khách hàng và toàn bộ dữ liệu liên quan!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message);
                }
            }

            LoadCustomers();
        }


        
        //  Làm mới danh sách
       
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTen.Clear();
            txtEmail.Clear();
            txtSDT.Clear();
            LoadCustomers();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdminMainForm mainForm = new AdminMainForm();
            
            this.Close();
            mainForm.Show();
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            AdminMainForm mainForm = new AdminMainForm();
            mainForm.Show();
            this.Close();
        }
    }
}
