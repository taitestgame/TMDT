using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using TMDT.BUS;
using TMDT.DAL;

namespace TMDT.Use
{
    public partial class ProductDetail : Form
    {
        public int ProductId { get; set; }

        public ProductDetail()
        {
            InitializeComponent();
        }

        private void ProductDetail_Load(object sender, EventArgs e)
        {
            // Allow passing via Tag or property
            if (this.ProductId == 0 && this.Tag is int idFromTag)
            {
                this.ProductId = idFromTag;
            }
            if (this.ProductId == 0) { return; }

            using (var db = new Model1())
            {
                var product = db.Products.FirstOrDefault(p => p.ProductID == this.ProductId);
                if (product == null) return;

                this.lblTenSP.Text = product.Name;
                this.lblMoTa.Text = product.Description ?? product.ShortDescription;

                var category = product.DefaultCategoryID != null ? db.Categories.FirstOrDefault(c => c.CategoryID == product.DefaultCategoryID) : null;
                this.lblLoai.Text = $"Loại sản phẩm: {category?.Name ?? "N/A"}";

                var variant = db.ProductVariants.Where(v => v.ProductID == product.ProductID).OrderBy(v => v.VariantID).FirstOrDefault();
                if (variant != null)
                {
                    this.lblGia.Text = $"Giá: {Math.Round(variant.Price):N0} đ";
                }

                var img = db.ProductImages.FirstOrDefault(pi => pi.ProductID == product.ProductID);
                if (img != null && !string.IsNullOrWhiteSpace(img.Url))
                {
                    try
                    {
                        // Lấy đường dẫn thư mục chứa ảnh (nằm cùng project)
                        string imageFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");
                        string imagePath = Path.Combine(imageFolder, img.Url);

                        if (File.Exists(imagePath))
                        {
                            picSanPham.Image = Image.FromFile(imagePath);
                        }
                        else
                        {
                            // Có thể hiển thị ảnh mặc định nếu không tìm thấy
                            // picture.Image = Image.FromFile(Path.Combine(imageFolder, "no_image.jpg"));
                        }
                    }
                    catch
                    {
                        // ignore load errors
                    }
                }
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Add selected quantity of the product's first variant to current user's cart
        private void btnThemGioHang_Click(object sender, EventArgs e)
        {
            var user = Session.CurrentCustomer;
            if (user == null)
            {
                MessageBox.Show("Bạn cần đăng nhập để thêm vào giỏ hàng.");
                return;
            }

            if (this.ProductId == 0 && this.Tag is int idFromTag)
            {
                this.ProductId = idFromTag;
            }
            if (this.ProductId == 0)
            {
                MessageBox.Show("Không xác định được sản phẩm.");
                return;
            }

            int qty = 1;
            // try to read NumericUpDown if exists
            var qtyControl = this.Controls.Find("txtSoLuong", true).FirstOrDefault() as NumericUpDown;
            if (qtyControl != null) qty = (int)qtyControl.Value;
            if (qty <= 0) qty = 1;

            using (var db = new Model1())
            {
                var variant = db.ProductVariants
                    .Where(v => v.ProductID == this.ProductId)
                    .OrderBy(v => v.VariantID)
                    .FirstOrDefault();
                if (variant == null)
                {
                    MessageBox.Show("Sản phẩm chưa có phiên bản để bán.");
                    return;
                }

                var cartBus = new CartBUS();
                cartBus.AddToCart(user.CustomerID, variant.VariantID, qty);
            }

            MessageBox.Show("Đã thêm vào giỏ hàng.");
        }
    }
}
