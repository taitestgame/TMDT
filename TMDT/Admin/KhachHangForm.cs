using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using TMDT.DAL;

namespace TMDT.Admin
{
    public partial class KhachHangForm : Form
    {
        public KhachHangForm()
        {
            InitializeComponent();
            LoadCustomers();
        }

        private void LoadCustomers()
        {
            using (var db = new Model1())
            {
                var data = db.Customers
                    .Select(c => new { c.CustomerID, c.UserName, c.FullName, c.Email, c.Phone, c.CreatedAt, c.IsAdmin })
                    .OrderByDescending(c => c.CustomerID)
                    .ToList();
                this.dgvKhachHang.DataSource = data;
            }
        }

        private void dgvKhachHang_SelectionChanged(object sender, System.EventArgs e)
        {
            if (this.dgvKhachHang.CurrentRow == null) return;
            this.txtTen.Text = this.dgvKhachHang.CurrentRow.Cells["FullName"]?.Value?.ToString();
            this.txtEmail.Text = this.dgvKhachHang.CurrentRow.Cells["Email"]?.Value?.ToString();
            this.txtSDT.Text = this.dgvKhachHang.CurrentRow.Cells["Phone"]?.Value?.ToString();
        }

        private void btnThem_Click(object sender, System.EventArgs e)
        {
            this.txtTen.Clear();
            this.txtEmail.Clear();
            this.txtSDT.Clear();
            this.txtDiaChi.Clear();
        }

        private void btnSua_Click(object sender, System.EventArgs e)
        {
            if (this.dgvKhachHang.CurrentRow == null) return;
            int id = System.Convert.ToInt32(this.dgvKhachHang.CurrentRow.Cells["CustomerID"].Value);
            using (var db = new Model1())
            {
                var c = db.Customers.FirstOrDefault(x => x.CustomerID == id);
                if (c != null)
                {
                    c.FullName = this.txtTen.Text?.Trim();
                    c.Phone = this.txtSDT.Text?.Trim();
                    db.SaveChanges();
                }
            }
            LoadCustomers();
        }

        private void btnXoa_Click(object sender, System.EventArgs e)
        {
            if (this.dgvKhachHang.CurrentRow == null) return;
            int id = System.Convert.ToInt32(this.dgvKhachHang.CurrentRow.Cells["CustomerID"].Value);
            using (var db = new Model1())
            {
                var c = db.Customers.FirstOrDefault(x => x.CustomerID == id);
                if (c != null)
                {
                    db.Customers.Remove(c);
                    db.SaveChanges();
                }
            }
            LoadCustomers();
        }

        private void btnLamMoi_Click(object sender, System.EventArgs e)
        {
            LoadCustomers();
        }
    }
}
