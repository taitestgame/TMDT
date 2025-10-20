using System;
using System.Windows.Forms;
using System.Linq;
using TMDT.DAL;

namespace TMDT.Admin
{
    public partial class ThongKeForm : Form
    {
        public ThongKeForm()
        {
            InitializeComponent();
            LoadStats();
        }

        private void LoadStats()
        {
            using (var db = new Model1())
            {
                this.lblTongKhachHang.Text = $"Tổng khách hàng: {db.Customers.Count():N0}";
                this.lblTongSanPham.Text = $"Tổng sản phẩm: {db.Products.Count():N0}";
                this.lblTongDonHang.Text = $"Tổng đơn hàng: {db.OrderTbls.Count():N0}";
                var revenue = db.OrderTbls.Select(o => (decimal?)o.TotalAmount).DefaultIfEmpty(0).Sum();
                this.lblTongDoanhThu.Text = $"Tổng doanh thu: {Math.Round(revenue):N0} đ";
                this.lblTongMaGiam.Text = $"Tổng mã giảm giá: {db.Coupons.Count():N0}";
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadStats();
        }
    }
}
