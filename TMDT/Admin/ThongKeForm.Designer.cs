namespace TMDT.Admin
{
    partial class ThongKeForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.lblTongDoanhThu = new System.Windows.Forms.Label();
            this.lblTongDonHang = new System.Windows.Forms.Label();
            this.lblTongSanPham = new System.Windows.Forms.Label();
            this.lblTongKhachHang = new System.Windows.Forms.Label();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();
            this.pnlContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.lblTitle.Location = new System.Drawing.Point(23, 11);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(400, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "THỐNG KÊ HỆ THỐNG";
            // 
            // pnlContainer
            // 
            this.pnlContainer.BackColor = System.Drawing.Color.White;
            this.pnlContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlContainer.Controls.Add(this.lblTongDoanhThu);
            this.pnlContainer.Controls.Add(this.lblTongDonHang);
            this.pnlContainer.Controls.Add(this.lblTongSanPham);
            this.pnlContainer.Controls.Add(this.lblTongKhachHang);
            this.pnlContainer.Location = new System.Drawing.Point(23, 64);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(640, 267);
            this.pnlContainer.TabIndex = 1;
            // 
            // lblTongDoanhThu
            // 
            this.lblTongDoanhThu.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTongDoanhThu.ForeColor = System.Drawing.Color.Red;
            this.lblTongDoanhThu.Location = new System.Drawing.Point(34, 149);
            this.lblTongDoanhThu.Name = "lblTongDoanhThu";
            this.lblTongDoanhThu.Size = new System.Drawing.Size(343, 27);
            this.lblTongDoanhThu.TabIndex = 3;
            this.lblTongDoanhThu.Text = "Tổng doanh thu: 0đ";
            // 
            // lblTongDonHang
            // 
            this.lblTongDonHang.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTongDonHang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.lblTongDonHang.Location = new System.Drawing.Point(34, 107);
            this.lblTongDonHang.Name = "lblTongDonHang";
            this.lblTongDonHang.Size = new System.Drawing.Size(343, 27);
            this.lblTongDonHang.TabIndex = 2;
            this.lblTongDonHang.Text = "Tổng đơn hàng: 0";
            // 
            // lblTongSanPham
            // 
            this.lblTongSanPham.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTongSanPham.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.lblTongSanPham.Location = new System.Drawing.Point(34, 64);
            this.lblTongSanPham.Name = "lblTongSanPham";
            this.lblTongSanPham.Size = new System.Drawing.Size(343, 27);
            this.lblTongSanPham.TabIndex = 1;
            this.lblTongSanPham.Text = "Tổng sản phẩm: 0";
            // 
            // lblTongKhachHang
            // 
            this.lblTongKhachHang.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTongKhachHang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.lblTongKhachHang.Location = new System.Drawing.Point(34, 21);
            this.lblTongKhachHang.Name = "lblTongKhachHang";
            this.lblTongKhachHang.Size = new System.Drawing.Size(343, 27);
            this.lblTongKhachHang.TabIndex = 0;
            this.lblTongKhachHang.Text = "Tổng khách hàng: 0";
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(526, 352);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(137, 37);
            this.btnLamMoi.TabIndex = 2;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnDong
            // 
            this.btnDong.BackColor = System.Drawing.Color.Gray;
            this.btnDong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDong.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDong.ForeColor = System.Drawing.Color.White;
            this.btnDong.Location = new System.Drawing.Point(366, 352);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(137, 37);
            this.btnDong.TabIndex = 3;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = false;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // ThongKeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(686, 427);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.btnLamMoi);
            this.Controls.Add(this.pnlContainer);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ThongKeForm";
            this.Text = "Thống kê hệ thống";
            this.pnlContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.Label lblTongDoanhThu;
        private System.Windows.Forms.Label lblTongDonHang;
        private System.Windows.Forms.Label lblTongSanPham;
        private System.Windows.Forms.Label lblTongKhachHang;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnDong;
    }
}
