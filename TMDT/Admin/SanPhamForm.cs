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
using System.Linq;

namespace TMDT.Admin
{
    public partial class SanPhamForm : Form
    {
        public SanPhamForm()
        {
            InitializeComponent();
            LoadCategories();
            LoadProducts();
        }

        // Load categories to combobox
        private void LoadCategories()
        {
            using (var db = new Model1())
            {
                var cats = db.Categories.OrderBy(c => c.Name).ToList();
                this.cboDanhMuc.DataSource = cats;
                this.cboDanhMuc.DisplayMember = "Name";
                this.cboDanhMuc.ValueMember = "CategoryID";
            }
        }

        // Load products to grid
        private void LoadProducts()
        {
            using (var db = new Model1())
            {
                var data = db.Products
                    .Select(p => new { p.ProductID, p.SKU, p.Name, Category = p.Category.Name, p.IsActive, p.CreatedAt })
                    .OrderByDescending(p => p.ProductID)
                    .ToList();
                this.dgvSanPham.DataSource = data;
            }
        }

        // Fill inputs when selecting a product
        private void dgvSanPham_SelectionChanged(object sender, System.EventArgs e)
        {
            if (this.dgvSanPham.CurrentRow == null) return;
            var name = this.dgvSanPham.CurrentRow.Cells["Name"]?.Value?.ToString();
            if (string.IsNullOrEmpty(name)) return;
            this.txtTen.Text = name;
            this.txtMoTa.Text = string.Empty;
        }

        private void btnThem_Click(object sender, System.EventArgs e)
        {
            this.txtTen.Clear();
            this.txtGia.Clear();
            this.txtSoLuong.Clear();
            this.txtMoTa.Clear();
            if (this.cboDanhMuc.Items.Count > 0) this.cboDanhMuc.SelectedIndex = 0;
        }

        private void btnSua_Click(object sender, System.EventArgs e)
        {
            // For simplicity, editing handled by saving overrides on current selection
        }

        private void btnXoa_Click(object sender, System.EventArgs e)
        {
            if (this.dgvSanPham.CurrentRow == null) return;
            var idObj = this.dgvSanPham.CurrentRow.Cells["ProductID"]?.Value;
            if (idObj == null) return;
            int id = System.Convert.ToInt32(idObj);
            using (var db = new Model1())
            {
                var p = db.Products.FirstOrDefault(x => x.ProductID == id);
                if (p != null)
                {
                    db.Products.Remove(p);
                    db.SaveChanges();
                }
            }
            LoadProducts();
        }

        private void btnLuu_Click(object sender, System.EventArgs e)
        {
            var name = this.txtTen.Text?.Trim();
            if (string.IsNullOrWhiteSpace(name)) { MessageBox.Show("Nhập tên sản phẩm"); return; }

            using (var db = new Model1())
            {
                int? selectedId = this.dgvSanPham.CurrentRow?.Cells["ProductID"]?.Value as int?;
                Product p = null;
                if (selectedId.HasValue)
                {
                    p = db.Products.FirstOrDefault(x => x.ProductID == selectedId.Value);
                }
                if (p == null)
                {
                    p = new Product { CreatedAt = System.DateTime.Now, IsActive = true };
                    db.Products.Add(p);
                }
                p.Name = name;
                p.ShortDescription = this.txtMoTa.Text?.Trim();
                p.Description = this.txtMoTa.Text?.Trim();
                if (this.cboDanhMuc.SelectedValue is int catId) p.DefaultCategoryID = catId;
                db.SaveChanges();
            }
            LoadProducts();
        }

        private void btnLamMoi_Click(object sender, System.EventArgs e)
        {
            LoadProducts();
        }
    }
}
