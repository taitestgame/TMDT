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
    public partial class UserMain : Form
    {
        public UserMain()
        {
            InitializeComponent();
        }

        private void UserMain_Load(object sender, EventArgs e)
        {
            var user = Session.CurrentCustomer;
            if (user != null)
            {
                this.Text = $"Xin chào, {user.FullName ?? user.UserName}";
                LoadProductsGrid();
            }
        }

        private void LblTitle_Click(object sender, EventArgs e)
        {

        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            LoadProductsGrid();
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            LoadCategoriesGrid();
        }

        private void LoadCategoriesGrid()
        {
            pnlContent.Controls.Clear();
            pnlContent.AutoScroll = true;

            using (var db = new Model1())
            {
                var categories = db.Categories.OrderBy(c => c.CategoryID).ToList();

                int cardWidth = 175;
                
                int cardHeight = 130;
                int padding = 20;
                
                int colCount = Math.Max(1, pnlContent.Width / (cardWidth + padding));

                int x = padding;
                int y = padding;
                int col = 0;

                string imageFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");
                Image defaultImg = SystemIcons.Question.ToBitmap(); // ảnh mặc định nếu thiếu logo

                foreach (var cat in categories)
                {
                    // Tạo panel card
                    Panel panel = new Panel
                    {
                        Width = cardWidth,
                        
                        Height = cardHeight,
                        BackColor = Color.White,
                        BorderStyle = BorderStyle.None,
                        Cursor = Cursors.Hand,
                        Tag = cat.CategoryID,
                        Left = x,
                        Top = y,
                        Margin = new Padding(padding)
                    };

                    // Hiệu ứng bo góc & đổ bóng
                    panel.Paint += (s, e) =>
                    {
                        var g = e.Graphics;
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                        Rectangle rect = new Rectangle(0, 0, panel.Width - 1, panel.Height - 1);
                        using (var path = RoundedRect(rect, 15))
                        {
                            using (var brush = new SolidBrush(panel.BackColor))
                                g.FillPath(brush, path);

                            using (var pen = new Pen(Color.LightGray, 1))
                                g.DrawPath(pen, path);
                        }
                    };

                    // Hover effect
                    panel.MouseEnter += (s, e) => panel.BackColor = Color.FromArgb(245, 248, 255);
                    panel.MouseLeave += (s, e) => panel.BackColor = Color.White;

                    // Ảnh logo
                    PictureBox picture = new PictureBox
                    {
                        Width = cardWidth - 20,
                        Height = 70,
                        Top = 10,
                        Left = 10,
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Cursor = Cursors.Hand,
                        Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
                    };

                    string imagePath = Path.Combine(imageFolder, $"{cat.CategoryID}.jpg");
                    if (File.Exists(imagePath))
                        picture.Image = Image.FromFile(imagePath);
                    else
                        picture.Image = defaultImg;

                    // Tên danh mục
                    Label label = new Label
                    {
                        Text = cat.Name,
                        AutoSize = false,
                        Width = cardWidth,
                        Height = 35,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Top = picture.Bottom + 5,
                        Font = new Font("Segoe UI", 10, FontStyle.Bold),
                        ForeColor = Color.FromArgb(40, 40, 40),
                        Cursor = Cursors.Hand
                    };

                    // Gắn sự kiện click cho toàn bộ
                    panel.Click += CategoryCard_Click;
                    label.Click += CategoryCard_Click;
                    picture.Click += CategoryCard_Click;

                    panel.Controls.Add(picture);
                    panel.Controls.Add(label);
                    pnlContent.Controls.Add(panel);

                    // Cập nhật vị trí lưới
                    col++;
                    if (col >= colCount)
                    {
                        col = 0;
                        x = padding;
                        y += cardHeight + padding;
                    }
                    else
                    {
                        x += cardWidth + padding;
                    }
                }

                pnlContent.AutoScrollMinSize = new Size(0, y + cardHeight + padding);
            }
        }

        /// <summary>
        /// Hàm bo góc cho Panel (tạo path hình chữ nhật bo tròn)
        /// </summary>
        private System.Drawing.Drawing2D.GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int d = radius * 2;
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddArc(bounds.X, bounds.Y, d, d, 180, 90);
            path.AddArc(bounds.Right - d, bounds.Y, d, d, 270, 90);
            path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }


        private void CategoryCard_Click(object sender, EventArgs e)
        {
            int categoryId;
            if (sender is Control c && c.Tag is int id)
            {
                categoryId = id;
            }
            else if ((sender as Control)?.Parent?.Tag is int pid)
            {
                categoryId = pid;
            }
            else
            {
                return;
            }

            LoadProductsGrid(categoryId);
        }

        private void LoadProductsGrid(int? categoryId = null)
        {
            this.pnlContent.Controls.Clear();
            this.pnlContent.AutoScroll = true;
            var db = new Model1();

            var bus = new ProductBUS();
            var products = categoryId.HasValue ? bus.GetProductsByCategory(categoryId.Value) : bus.GetAllProducts();
            

            int cardWidth = 180;
            int cardHeight = 260;
            int padding = 16;
            int colCount = Math.Max(1, this.pnlContent.Width / (cardWidth + padding));

            int x = padding;
            int y = padding;
            int col = 0;

            string imageFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");

            foreach (var p in products)
            {
                // ========== PANEL CHA (CARD) ==========
                var panel = new Panel
                {
                    Width = cardWidth,
                    Height = cardHeight,
                    BackColor = Color.White,
                    BorderStyle = BorderStyle.None,
                    Cursor = Cursors.Hand,
                    Tag = p.ProductID
                };

                // Bo góc + đổ bóng nhẹ
                panel.Paint += (s, e) =>
                {
                    var g = e.Graphics;
                    var rect = new Rectangle(0, 0, panel.Width - 1, panel.Height - 1);
                    using (var path = RoundedRect(rect, 12))
                    using (var brush = new SolidBrush(panel.BackColor))
                    using (var pen = new Pen(Color.LightGray))
                    {
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        g.FillPath(brush, path);
                        g.DrawPath(pen, path);
                    }
                };

                // ========== HÌNH ẢNH SẢN PHẨM ==========
                var picture = new PictureBox
                {
                    Width = cardWidth - 10,
                    Height = 150,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Top = 5,
                    Left = 5,
                    Cursor = Cursors.Hand
                };

                var img = db.ProductImages.FirstOrDefault(pi => pi.ProductID == p.ProductID);
                string imagePath = Path.Combine(imageFolder, img.Url);
                if (File.Exists(imagePath))
                {
                    picture.Image = Image.FromFile(imagePath);
                }
                else
                {
                    // Có thể hiển thị ảnh mặc định nếu không tìm thấy
                    // picture.Image = Image.FromFile(Path.Combine(imageFolder, "no_image.jpg"));
                }


                // ========== TÊN SẢN PHẨM ==========
                var lblName = new Label
                {
                    Text = p.Name,
                    AutoSize = false,
                    Width = cardWidth - 10,
                    Height = 40,
                    Left = 5,
                    Top = picture.Bottom + 5,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    ForeColor = Color.FromArgb(30, 30, 30),
                    TextAlign = ContentAlignment.TopCenter
                };

                // ========== GIÁ SẢN PHẨM ==========
                var lblPrice = new Label
                {   


                    Text = $"{p.Description:N0} ₫",
                    AutoSize = false,
                    Width = cardWidth - 10,
                    Height = 25,
                    Left = 5,
                    Top = lblName.Bottom,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    ForeColor = Color.DarkOrange,
                    TextAlign = ContentAlignment.MiddleCenter
                };

                // ========== HIỆU ỨNG HOVER ==========
                panel.MouseEnter += (s, e) =>
                {
                    panel.BackColor = Color.FromArgb(250, 250, 250);
                    panel.Invalidate(); // vẽ lại border
                };
                panel.MouseLeave += (s, e) =>
                {
                    panel.BackColor = Color.White;
                    panel.Invalidate();
                };

                // Gắn sự kiện click
                panel.Click += ProductCard_Click;
                lblName.Click += ProductCard_Click;
                lblPrice.Click += ProductCard_Click;
                picture.Click += ProductCard_Click;

                // ========== THÊM CONTROL ==========
                panel.Controls.Add(picture);
                panel.Controls.Add(lblName);
                panel.Controls.Add(lblPrice);

                // ========== VỊ TRÍ ==========
                panel.Left = x;
                panel.Top = y;

                this.pnlContent.Controls.Add(panel);

                col++;
                if (col >= colCount)
                {
                    col = 0;
                    x = padding;
                    y += cardHeight + padding;
                }
                else
                {
                    x += cardWidth + padding;
                }
            }

            this.pnlContent.AutoScrollMinSize = new Size(0, y + cardHeight + padding);
        }

        /// <summary>
        /// Hàm vẽ khung bo góc (giúp card nhìn mềm mại hơn)
        /// </summary>
        


        private void ProductCard_Click(object sender, EventArgs e)
        {
            int productId;
            if (sender is Control c && c.Tag is int id)
            {
                productId = id;
            }
            else if ((sender as Control)?.Parent?.Tag is int pid)
            {
                productId = pid;
            }
            else
            {
                return;
            }

            using (var detail = new ProductDetail())
            {
                detail.Tag = productId; // pass id via Tag for now
                detail.ShowDialog();
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Session.CurrentCustomer = null;
            var login = new Login();
            login.FormClosed += (s, args) => this.Close();
            this.Hide();
            login.Show();
        }

        private void btnCart_Click(object sender, EventArgs e)
        {
            using (var form = new CartForm())
            {
                form.ShowDialog();
            }
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            using (var form = new AccountForm())
            {
                form.ShowDialog();
            }
        }
    }
}
