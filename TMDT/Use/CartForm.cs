using System;
using System.Linq;
using System.Windows.Forms;
using TMDT.BUS;
using TMDT.DAL;

namespace TMDT.Use
{
    public partial class CartForm : Form
    {
        private decimal tongTienHienTai = 0;

        public CartForm()
        {
            InitializeComponent();
            LoadCart();
            CaiDatUI();
        }

        private void CaiDatUI()
        {
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;

            lblTongTien.Font = new System.Drawing.Font("Segoe UI", 11, System.Drawing.FontStyle.Bold);
            btnThanhToan.BackColor = System.Drawing.Color.SteelBlue;
            btnThanhToan.ForeColor = System.Drawing.Color.White;
            btnThanhToan.FlatStyle = FlatStyle.Flat;
            btnThanhToan.FlatAppearance.BorderSize = 0;

            dgvGioHang.BorderStyle = BorderStyle.None;
            dgvGioHang.BackgroundColor = System.Drawing.Color.White;
            dgvGioHang.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10);
        }

        private void LoadCart()
        {
            var user = Session.CurrentCustomer;
            if (user == null) return;

            var cartBus = new CartBUS();
            var items = cartBus.GetCartItems(user.CustomerID)
                .Select(ci => new
                {
                    ci.CartItemID,
                    Product = ci.ProductVariant.Product.Name,
                    SKU = ci.ProductVariant.SKU,
                    ci.Quantity,
                    ci.UnitPrice,
                    Total = ci.UnitPrice * ci.Quantity
                })
                .ToList();

            dgvGioHang.DataSource = items;
            tongTienHienTai = Math.Round(items.Sum(i => i.Total));
            lblTongTien.Text = $"Tổng tiền: {tongTienHienTai:N0} đ";
        }

        private void btnApDung_Click(object sender, EventArgs e)
        {
            var code = txtMaGiamGia.Text?.Trim();
            if (string.IsNullOrWhiteSpace(code)) return;

            if (code.StartsWith("CP", StringComparison.OrdinalIgnoreCase))
            {
                tongTienHienTai *= 0.9m;
                lblTongTien.Text = $"Tổng tiền: {tongTienHienTai:N0} đ";
                MessageBox.Show("Áp dụng mã giảm giá 10% thành công!");
            }
            else
            {
                MessageBox.Show("Mã không hợp lệ hoặc chưa hỗ trợ.");
            }
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            // Truyền trực tiếp tổng tiền sang form Checkout
            using (var checkout = new CheckoutForm(tongTienHienTai))
            {
                checkout.ShowDialog();
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
