using System;
using System.Windows.Forms;
using System.Linq;
using TMDT.DAL;
using TMDT.BUS;

namespace TMDT.Use
{
    public partial class CheckoutForm : Form
    {
        public CheckoutForm()
        {
            InitializeComponent();
            LoadDefaultInfo();
        }

        // Load default customer info and total amount into the form
        private void LoadDefaultInfo()
        {
            var user = Session.CurrentCustomer;
            if (user == null) return;

            this.txtTenKH.Text = user.FullName ?? user.UserName;
            this.txtSDT.Text = user.Phone ?? string.Empty;

            using (var db = new Model1())
            {
                var shipping = db.Addresses.FirstOrDefault(a => a.CustomerID == user.CustomerID && a.IsDefault);
                if (shipping != null)
                {
                    this.txtDiaChi.Text = $"{shipping.Street}, {shipping.City}, {shipping.State}, {shipping.Country}";
                }

                var total = db.CartItems
                    .Where(ci => ci.Cart.CustomerID == user.CustomerID)
                    .Select(ci => ci.UnitPrice * ci.Quantity)
                    .DefaultIfEmpty(0)
                    .Sum();
                var rounded = Math.Round(total);
                this.lblTongTien.Text = $"Tổng tiền: {rounded:N0} đ";
            }
        }

        // Confirm checkout: create OrderTbl + OrderItems, then clear cart
        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            var user = Session.CurrentCustomer;
            if (user == null)
            {
                MessageBox.Show("Bạn cần đăng nhập.");
                return;
            }

            using (var db = new Model1())
            {
                // Create order
                var order = new OrderTbl
                {
                    CustomerID = user.CustomerID,
                    OrderDate = DateTime.Now,
                    Status = "Paid", // demo: giả sử đã thanh toán
                    SubTotal = 0,
                    ShippingFee = 30000,
                    TaxAmount = 0,
                    DiscountAmount = 0,
                    TotalAmount = 0
                };
                db.OrderTbls.Add(order);
                db.SaveChanges();

                var items = db.CartItems.Where(ci => ci.Cart.CustomerID == user.CustomerID).ToList();
                decimal subTotal = 0;
                foreach (var ci in items)
                {
                    var totalPrice = ci.UnitPrice * ci.Quantity;
                    subTotal += totalPrice;
                    db.OrderItems.Add(new OrderItem
                    {
                        OrderID = order.OrderID,
                        VariantID = ci.VariantID,
                        SKU = ci.ProductVariant.SKU,
                        ProductName = ci.ProductVariant.Product.Name,
                        Quantity = ci.Quantity,
                        UnitPrice = ci.UnitPrice,
                        TotalPrice = totalPrice
                    });
                }

                order.SubTotal = Math.Round(subTotal);
                order.TotalAmount = Math.Round(subTotal + order.ShippingFee - order.DiscountAmount + order.TaxAmount);
                db.SaveChanges();

                // Clear cart
                db.CartItems.RemoveRange(items);
                db.SaveChanges();
            }

            MessageBox.Show("Đặt hàng thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            using (var history = new OrderHistoryForm())
            {
                history.ShowDialog();
            }
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
