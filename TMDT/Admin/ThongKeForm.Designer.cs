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
            this.lblTongMaGiam = new System.Windows.Forms.Label();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.pnlContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this.lblTitle.Location = new System.Drawing.Point(20, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(350, 30);
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
            this.pnlContainer.Controls.Add(this.lblTongMaGiam);
            this.pnlContainer.Location = new System.Drawing.Point(20, 60);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(560, 250);
            this.pnlContainer.TabIndex = 1;
            // 
            // lblTongKhachHang
            // 
            this.lblTongKhachHang.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTongKhachHang.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this.lblTongKhachHang.Location = new System.Drawing.Point(30, 20);
            this.lblTongKhachHang.Name = "lblTongKhachHang";
            this.lblTongKhachHang.Size = new System.Drawing.Size(300, 25);
            this.lblTongKhachHang.TabIndex = 0;
            this.lblTongKhachHang.Text = "Tổng khách hàng: 0";
            // 
            // lblTongSanPham
            // 
            this.lblTongSanPham.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTongSanPham.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this.lblTongSanPham.Location = new System.Drawing.Point(30, 60);
            this.lblTongSanPham.Name = "lblTongSanPham";
            this.lblTongSanPham.Size = new System.Drawing.Size(300, 25);
            this.lblTongSanPham.TabIndex = 1;
            this.lblTongSanPham.Text = "Tổng sản phẩm: 0";
            // 
            // lblTongDonHang
            // 
            this.lblTongDonHang.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTongDonHang.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this.lblTongDonHang.Location = new System.Drawing.Point(30, 100);
            this.lblTongDonHang.Name = "lblTongDonHang";
            this.lblTongDonHang.Size = new System.Drawing.Size(300, 25);
            this.lblTongDonHang.TabIndex = 2;
            this.lblTongDonHang.Text = "Tổng đơn hàng: 0";
            // 
            // lblTongDoanhThu
            // 
            this.lblTongDoanhThu.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTongDoanhThu.ForeColor = System.Drawing.Color.Red;
            this.lblTongDoanhThu.Location = new System.Drawing.Point(30, 140);
            this.lblTongDoanhThu.Name = "lblTongDoanhThu";
            this.lblTongDoanhThu.Size = new System.Drawing.Size(300, 25);
            this.lblTongDoanhThu.TabIndex = 3;
            this.lblTongDoanhThu.Text = "Tổng doanh thu: 0đ";
            // 
            // lblTongMaGiam
            // 
            this.lblTongMaGiam.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTongMaGiam.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this.lblTongMaGiam.Location = new System.Drawing.Point(30, 180);
            this.lblTongMaGiam.Name = "lblTongMaGiam";
            this.lblTongMaGiam.Size = new System.Drawing.Size(300, 25);
            this.lblTongMaGiam.TabIndex = 4;
            this.lblTongMaGiam.Text = "Tổng mã giảm giá: 0";
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BackColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(460, 330);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(120, 35);
            this.btnLamMoi.TabIndex = 2;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // ThongKeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(230, 243, 255);
            this.ClientSize = new System.Drawing.Size(600, 400);
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
        private System.Windows.Forms.Label lblTongMaGiam;
        private System.Windows.Forms.Button btnLamMoi;
    }
}
