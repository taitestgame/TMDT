using System;
using System.Windows.Forms;
using System.Linq;
using TMDT.BUS;
using TMDT.DAL;

namespace TMDT.Use
{
    public partial class CartForm : Form
    {
        // Load the current user's cart when opening the form
        public CartForm()
        {
            InitializeComponent();
            LoadCart();
        }

        // Bind the cart items to the grid and show total
        private void LoadCart()
        {
            var user = Session.CurrentCustomer;
            if (user == null) return;

            var cartBus = new CartBUS();
            var items = cartBus.GetCartItems(user.CustomerID)
                .Select(ci => new
                {
                    CartItemID = ci.CartItemID,
                    Product = ci.ProductVariant.Product.Name,
                    VariantSKU = ci.ProductVariant.SKU,
                    Quantity = ci.Quantity,
                    UnitPrice = ci.UnitPrice,
                    Total = ci.UnitPrice * ci.Quantity
                })
                .ToList();

            this.dgvGioHang.DataSource = items;
            var total = Math.Round(items.Sum(i => i.Total));
            this.lblTongTien.Text = $"Tổng tiền: {total:N0} đ"; // N0: group separators, no decimals
        }

        // Apply a coupon code (optional demo: just reduces 10% if code starts with CP)
        private void btnApDung_Click(object sender, EventArgs e)
        {
            var code = this.txtMaGiamGia.Text?.Trim();
            if (string.IsNullOrWhiteSpace(code)) return;

            if (code.StartsWith("CP", StringComparison.OrdinalIgnoreCase))
            {
                // Simple demo: subtract 10%
                var currentText = this.lblTongTien.Text;
                var digits = new string(currentText.Where(char.IsDigit).ToArray());
                if (decimal.TryParse(digits, out var raw))
                {
                    var discounted = raw * 0.9m;
                    this.lblTongTien.Text = $"Tổng tiền: {discounted:0,0} đ";
                    MessageBox.Show("Áp dụng mã giảm giá 10% (demo)");
                }
            }
            else
            {
                MessageBox.Show("Mã không hợp lệ hoặc chưa hỗ trợ.");
            }
        }

        // Proceed to checkout
        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            using (var checkout = new CheckoutForm())
            {
                checkout.ShowDialog();
            }
        }

        // Close cart form
        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
