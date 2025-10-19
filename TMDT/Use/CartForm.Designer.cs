namespace TMDT.Use
{
    partial class CartForm
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
            this.dgvGioHang = new System.Windows.Forms.DataGridView();
            this.lblTongTien = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMaGiamGia = new System.Windows.Forms.TextBox();
            this.btnApDung = new System.Windows.Forms.Button();
            this.btnThanhToan = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGioHang)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvGioHang
            // 
            this.dgvGioHang.AllowUserToAddRows = false;
            this.dgvGioHang.AllowUserToDeleteRows = false;
            this.dgvGioHang.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvGioHang.BackgroundColor = System.Drawing.Color.White;
            this.dgvGioHang.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvGioHang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGioHang.Location = new System.Drawing.Point(30, 30);
            this.dgvGioHang.Name = "dgvGioHang";
            this.dgvGioHang.ReadOnly = true;
            this.dgvGioHang.RowHeadersVisible = false;
            this.dgvGioHang.RowTemplate.Height = 25;
            this.dgvGioHang.Size = new System.Drawing.Size(620, 250);
            this.dgvGioHang.TabIndex = 0;
            // 
            // lblTongTien
            // 
            this.lblTongTien.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTongTien.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this.lblTongTien.Location = new System.Drawing.Point(30, 290);
            this.lblTongTien.Name = "lblTongTien";
            this.lblTongTien.Size = new System.Drawing.Size(250, 30);
            this.lblTongTien.TabIndex = 1;
            this.lblTongTien.Text = "Tổng tiền: 0đ";
            this.lblTongTien.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label1.Location = new System.Drawing.Point(30, 340);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "Mã giảm giá:";
            // 
            // txtMaGiamGia
            // 
            this.txtMaGiamGia.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMaGiamGia.Location = new System.Drawing.Point(130, 337);
            this.txtMaGiamGia.Name = "txtMaGiamGia";
            this.txtMaGiamGia.Size = new System.Drawing.Size(150, 25);
            this.txtMaGiamGia.TabIndex = 3;
            // 
            // btnApDung
            // 
            this.btnApDung.BackColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this.btnApDung.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApDung.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnApDung.ForeColor = System.Drawing.Color.White;
            this.btnApDung.Location = new System.Drawing.Point(300, 335);
            this.btnApDung.Name = "btnApDung";
            this.btnApDung.Size = new System.Drawing.Size(100, 30);
            this.btnApDung.TabIndex = 4;
            this.btnApDung.Text = "Áp dụng";
            this.btnApDung.UseVisualStyleBackColor = false;
            // 
            // btnThanhToan
            // 
            this.btnThanhToan.BackColor = System.Drawing.Color.FromArgb(0, 153, 51);
            this.btnThanhToan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThanhToan.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnThanhToan.ForeColor = System.Drawing.Color.White;
            this.btnThanhToan.Location = new System.Drawing.Point(470, 335);
            this.btnThanhToan.Name = "btnThanhToan";
            this.btnThanhToan.Size = new System.Drawing.Size(120, 35);
            this.btnThanhToan.TabIndex = 5;
            this.btnThanhToan.Text = "Thanh toán";
            this.btnThanhToan.UseVisualStyleBackColor = false;
            // 
            // btnDong
            // 
            this.btnDong.BackColor = System.Drawing.Color.White;
            this.btnDong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDong.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnDong.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this.btnDong.Location = new System.Drawing.Point(600, 335);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(80, 35);
            this.btnDong.TabIndex = 6;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = false;
            // 
            // CartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(230, 243, 255);
            this.ClientSize = new System.Drawing.Size(720, 400);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.btnThanhToan);
            this.Controls.Add(this.btnApDung);
            this.Controls.Add(this.txtMaGiamGia);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTongTien);
            this.Controls.Add(this.dgvGioHang);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "CartForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Giỏ hàng";
            ((System.ComponentModel.ISupportInitialize)(this.dgvGioHang)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvGioHang;
        private System.Windows.Forms.Label lblTongTien;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMaGiamGia;
        private System.Windows.Forms.Button btnApDung;
        private System.Windows.Forms.Button btnThanhToan;
        private System.Windows.Forms.Button btnDong;
    }
}
