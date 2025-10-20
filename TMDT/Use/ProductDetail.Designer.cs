namespace TMDT.Use
{
    partial class ProductDetail
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
            this.picSanPham = new System.Windows.Forms.PictureBox();
            this.lblTenSP = new System.Windows.Forms.Label();
            this.lblGia = new System.Windows.Forms.Label();
            this.lblMoTa = new System.Windows.Forms.Label();
            this.lblLoai = new System.Windows.Forms.Label();
            this.txtSoLuong = new System.Windows.Forms.NumericUpDown();
            this.lblSoLuong = new System.Windows.Forms.Label();
            this.btnThemGioHang = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picSanPham)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoLuong)).BeginInit();
            this.SuspendLayout();
            // 
            // picSanPham
            // 
            this.picSanPham.BackColor = System.Drawing.Color.WhiteSmoke;
            this.picSanPham.Location = new System.Drawing.Point(34, 32);
            this.picSanPham.Name = "picSanPham";
            this.picSanPham.Size = new System.Drawing.Size(286, 336);
            this.picSanPham.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picSanPham.TabIndex = 0;
            this.picSanPham.TabStop = false;
            // 
            // lblTenSP
            // 
            this.lblTenSP.AutoSize = true;
            this.lblTenSP.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTenSP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.lblTenSP.Location = new System.Drawing.Point(366, 43);
            this.lblTenSP.Name = "lblTenSP";
            this.lblTenSP.Size = new System.Drawing.Size(171, 32);
            this.lblTenSP.TabIndex = 1;
            this.lblTenSP.Text = "Tên sản phẩm";
            // 
            // lblGia
            // 
            this.lblGia.AutoSize = true;
            this.lblGia.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblGia.ForeColor = System.Drawing.Color.Red;
            this.lblGia.Location = new System.Drawing.Point(366, 91);
            this.lblGia.Name = "lblGia";
            this.lblGia.Size = new System.Drawing.Size(79, 28);
            this.lblGia.TabIndex = 2;
            this.lblGia.Text = "Giá: 0đ";
            // 
            // lblMoTa
            // 
            this.lblMoTa.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblMoTa.ForeColor = System.Drawing.Color.Black;
            this.lblMoTa.Location = new System.Drawing.Point(366, 171);
            this.lblMoTa.Name = "lblMoTa";
            this.lblMoTa.Size = new System.Drawing.Size(400, 107);
            this.lblMoTa.TabIndex = 4;
            this.lblMoTa.Text = "Mô tả sản phẩm hiển thị tại đây...";
            // 
            // lblLoai
            // 
            this.lblLoai.AutoSize = true;
            this.lblLoai.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblLoai.ForeColor = System.Drawing.Color.Gray;
            this.lblLoai.Location = new System.Drawing.Point(366, 128);
            this.lblLoai.Name = "lblLoai";
            this.lblLoai.Size = new System.Drawing.Size(125, 23);
            this.lblLoai.TabIndex = 3;
            this.lblLoai.Text = "Loại sản phẩm:";
            // 
            // txtSoLuong
            // 
            this.txtSoLuong.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSoLuong.Location = new System.Drawing.Point(457, 286);
            this.txtSoLuong.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtSoLuong.Name = "txtSoLuong";
            this.txtSoLuong.Size = new System.Drawing.Size(69, 30);
            this.txtSoLuong.TabIndex = 6;
            this.txtSoLuong.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblSoLuong
            // 
            this.lblSoLuong.AutoSize = true;
            this.lblSoLuong.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSoLuong.Location = new System.Drawing.Point(366, 288);
            this.lblSoLuong.Name = "lblSoLuong";
            this.lblSoLuong.Size = new System.Drawing.Size(82, 23);
            this.lblSoLuong.TabIndex = 5;
            this.lblSoLuong.Text = "Số lượng:";
            // 
            // btnThemGioHang
            // 
            this.btnThemGioHang.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.btnThemGioHang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemGioHang.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnThemGioHang.ForeColor = System.Drawing.Color.White;
            this.btnThemGioHang.Location = new System.Drawing.Point(366, 331);
            this.btnThemGioHang.Name = "btnThemGioHang";
            this.btnThemGioHang.Size = new System.Drawing.Size(171, 37);
            this.btnThemGioHang.TabIndex = 7;
            this.btnThemGioHang.Text = "Thêm vào giỏ";
            this.btnThemGioHang.UseVisualStyleBackColor = false;
            this.btnThemGioHang.Click += new System.EventHandler(this.btnThemGioHang_Click);
            // 
            // btnDong
            // 
            this.btnDong.BackColor = System.Drawing.Color.White;
            this.btnDong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDong.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnDong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.btnDong.Location = new System.Drawing.Point(560, 331);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(114, 37);
            this.btnDong.TabIndex = 8;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = false;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // ProductDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(800, 427);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.btnThemGioHang);
            this.Controls.Add(this.txtSoLuong);
            this.Controls.Add(this.lblSoLuong);
            this.Controls.Add(this.lblMoTa);
            this.Controls.Add(this.lblLoai);
            this.Controls.Add(this.lblGia);
            this.Controls.Add(this.lblTenSP);
            this.Controls.Add(this.picSanPham);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ProductDetail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chi tiết sản phẩm";
            this.Load += new System.EventHandler(this.ProductDetail_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picSanPham)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoLuong)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picSanPham;
        private System.Windows.Forms.Label lblTenSP;
        private System.Windows.Forms.Label lblGia;
        private System.Windows.Forms.Label lblMoTa;
        private System.Windows.Forms.Label lblLoai;
        private System.Windows.Forms.NumericUpDown txtSoLuong;
        private System.Windows.Forms.Label lblSoLuong;
        private System.Windows.Forms.Button btnThemGioHang;
        private System.Windows.Forms.Button btnDong;
    }
}
