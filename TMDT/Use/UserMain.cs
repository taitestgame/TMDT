using System;
using System.Windows.Forms;
using TMDT.DAL;
using TMDT.BUS;
using System.Net;
using System.Drawing;
using System.Linq;

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
            this.pnlContent.Controls.Clear();
            this.pnlContent.AutoScroll = true;

            using (var db = new Model1())
            {
                var categories = db.Categories.OrderBy(c => c.CategoryID).ToList();

                int cardWidth = 180;
                int cardHeight = 80;
                int padding = 16;
                int colCount = Math.Max(1, this.pnlContent.Width / (cardWidth + padding));

                int x = padding;
                int y = padding;
                int col = 0;

                foreach (var cat in categories)
                {
                    var panel = new Panel
                    {
                        Width = cardWidth,
                        Height = cardHeight,
                        BackColor = Color.White,
                        BorderStyle = BorderStyle.FixedSingle,
                        Cursor = Cursors.Hand,
                        Tag = cat.CategoryID,
                        Left = x,
                        Top = y
                    };

                    var label = new Label
                    {
                        Text = cat.Name,
                        AutoSize = false,
                        Width = cardWidth - 16,
                        Height = cardHeight - 16,
                        Left = 8,
                        Top = 8
                    };

                    panel.Controls.Add(label);
                    panel.Click += CategoryCard_Click;
                    label.Click += CategoryCard_Click;

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

            var bus = new ProductBUS();
            var products = categoryId.HasValue ? bus.GetProductsByCategory(categoryId.Value) : bus.GetAllProducts();

            int cardWidth = 180;
            int cardHeight = 220;
            int padding = 16;
            int colCount = Math.Max(1, this.pnlContent.Width / (cardWidth + padding));

            int x = padding;
            int y = padding;
            int col = 0;

            foreach (var p in products)
            {
                var panel = new Panel
                {
                    Width = cardWidth,
                    Height = cardHeight,
                    BackColor = Color.White,
                    BorderStyle = BorderStyle.FixedSingle,
                    Cursor = Cursors.Hand,
                    Tag = p.ProductID
                };

                var picture = new PictureBox
                {
                    Width = cardWidth,
                    Height = 140,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Top = 0,
                    Left = 0
                };

                // Load image from first ProductImage if any
                using (var db = new Model1())
                {
                    var img = db.ProductImages.FirstOrDefault(pi => pi.ProductID == p.ProductID);
                    if (img != null && !string.IsNullOrWhiteSpace(img.Url))
                    {
                        try
                        {
                            var request = WebRequest.Create(img.Url);
                            (request as HttpWebRequest).UserAgent = "Mozilla/5.0";
                            using (var response = request.GetResponse())
                            using (var stream = response.GetResponseStream())
                            {
                                picture.Image = Bitmap.FromStream(stream);
                            }
                        }
                        catch
                        {
                            // ignore load errors
                        }
                    }
                }

                var label = new Label
                {
                    Text = p.Name,
                    AutoSize = false,
                    Width = cardWidth - 8,
                    Height = 60,
                    Left = 4,
                    Top = 150
                };

                panel.Left = x;
                panel.Top = y;
                panel.Controls.Add(picture);
                panel.Controls.Add(label);
                panel.Click += ProductCard_Click;
                picture.Click += ProductCard_Click;
                label.Click += ProductCard_Click;

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

            // Ensure scrollbars appear when content exceeds view
            this.pnlContent.AutoScrollMinSize = new Size(0, y + cardHeight + padding);
        }

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
