using System;
using System.Linq;
using System.Windows.Forms;
using TMDT.DAL;

namespace TMDT.Use
{
    public partial class CheckoutForm : Form
    {
        private readonly decimal tongTienNhanDuoc;

        public CheckoutForm(decimal tongTien)
        {
            InitializeComponent();
            tongTienNhanDuoc = tongTien;
            LoadDefaultInfo();
            CaiDatUI();
        }

        private void CaiDatUI()
        {
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;

            lblTongTien.Font = new System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Bold);
            btnXacNhan.BackColor = System.Drawing.Color.MediumSeaGreen;
            btnXacNhan.ForeColor = System.Drawing.Color.White;
            btnXacNhan.FlatStyle = FlatStyle.Flat;
            btnXacNhan.FlatAppearance.BorderSize = 0;

            btnHuy.BackColor = System.Drawing.Color.LightGray;
            btnHuy.FlatStyle = FlatStyle.Flat;
            btnHuy.FlatAppearance.BorderSize = 0;
        }

        private void LoadDefaultInfo()
        {
            var user = Session.CurrentCustomer;
            if (user == null) return;

            txtTenKH.Text = user.FullName ?? user.UserName;
            txtSDT.Text = user.Phone ?? string.Empty;

            using (var db = new Model1())
            {
                var shipping = db.Addresses
                    .FirstOrDefault(a => a.CustomerID == user.CustomerID && a.IsDefault);

                if (shipping != null)
                {
                    txtDiaChi.Text = $"{shipping.Street}, {shipping.City}, {shipping.State}, {shipping.Country}";
                }
            }

            lblTongTien.Text = $"Tổng tiền: {tongTienNhanDuoc:N0} đ";
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            var user = Session.CurrentCustomer;
            if (user == null)
            {
                MessageBox.Show("Bạn cần đăng nhập trước khi thanh toán.");
                return;
            }

            try
            {
                int orderIdVuaTao; // để lưu lại OrderID vừa tạo

                using (var db = new Model1())
                {
                    var order = new OrderTbl
                    {
                        CustomerID = user.CustomerID,
                        OrderDate = DateTime.Now,
                        Status = "Paid",
                        SubTotal = tongTienNhanDuoc,
                        ShippingFee = 30000,
                        TaxAmount = 0,
                        DiscountAmount = 0,
                        TotalAmount = tongTienNhanDuoc + 30000
                    };

                    db.OrderTbls.Add(order);
                    db.SaveChanges(); // cần lưu để lấy OrderID
                    orderIdVuaTao = order.OrderID;

                    var items = db.CartItems.Where(ci => ci.Cart.CustomerID == user.CustomerID).ToList();
                    foreach (var ci in items)
                    {
                        db.OrderItems.Add(new OrderItem
                        {
                            OrderID = order.OrderID,
                            VariantID = ci.VariantID,
                            SKU = ci.ProductVariant.SKU,
                            ProductName = ci.ProductVariant.Product.Name,
                            Quantity = ci.Quantity,
                            UnitPrice = ci.UnitPrice,
                            TotalPrice = ci.UnitPrice * ci.Quantity
                        });
                    }

                    db.CartItems.RemoveRange(items);
                    db.SaveChanges();
                }

                MessageBox.Show("Đặt hàng thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //  Mở form lịch sử đơn hàng
                using (var history = new OrderHistoryForm())
                {
                    // tuỳ chọn: nếu muốn focus vào đơn hàng vừa đặt
                    history.Shown += (s, ev) => history.FocusOrder(orderIdVuaTao);


                    history.ShowDialog();
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi đặt hàng:\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
