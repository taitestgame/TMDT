using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TMDT.DAL;

namespace TMDT.Admin
{
    public partial class AdminMainForm : Form
    {
        public AdminMainForm()
        {
            InitializeComponent();
        }

        // Helper: load a simple grid into panelMain for quick admin views
        private void ShowGrid<T>(IEnumerable<T> data, string title)
        {
            this.panelMain.Controls.Clear();
            var lbl = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 102, 204),
                Left = 10,
                Top = 10,
                AutoSize = true
            };
            var grid = new DataGridView
            {
                Left = 10,
                Top = 50,
                Width = this.panelMain.Width - 20,
                Height = this.panelMain.Height - 60,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None
            };
            grid.DataSource = data.ToList();
            this.panelMain.Controls.Add(lbl);
            this.panelMain.Controls.Add(grid);
        }

        private void btnSanPham_Click(object sender, EventArgs e)
        {
            using (var db = new Model1())
            {
                var products = db.Products
                    .Select(p => new { p.ProductID, p.SKU, p.Name, Category = p.Category.Name, p.IsActive, p.CreatedAt })
                    .OrderByDescending(p => p.ProductID)
                    .ToList();
                ShowGrid(products, "Danh sách sản phẩm");
            }
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            using (var db = new Model1())
            {
                var customers = db.Customers
                    .Select(c => new { c.CustomerID, c.UserName, c.FullName, c.Email, c.Phone, c.CreatedAt, c.IsAdmin })
                    .OrderByDescending(c => c.CustomerID)
                    .ToList();
                ShowGrid(customers, "Danh sách khách hàng");
            }
        }

        private void btnDonHang_Click(object sender, EventArgs e)
        {
            using (var db = new Model1())
            {
                var orders = db.OrderTbls
                    .Select(o => new { o.OrderID, o.Customer.FullName, o.OrderDate, o.Status, o.SubTotal, o.ShippingFee, o.TotalAmount })
                    .OrderByDescending(o => o.OrderID)
                    .ToList();
                ShowGrid(orders, "Danh sách đơn hàng");
            }
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            using (var db = new Model1())
            {
                var today = DateTime.Today;
                var stats = db.OrderTbls
                    .GroupBy(o => new { Year = o.OrderDate.Year, Month = o.OrderDate.Month })
                    .Select(g => new
                    {
                        g.Key.Year,
                        g.Key.Month,
                        Orders = g.Count(),
                        Revenue = g.Sum(x => (decimal?)x.TotalAmount) ?? 0
                    })
                    .OrderBy(g => g.Year).ThenBy(g => g.Month)
                    .ToList();
                ShowGrid(stats, "Thống kê doanh thu theo tháng");
            }
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
