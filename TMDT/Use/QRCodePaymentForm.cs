using System;
using System.Drawing;
using System.Windows.Forms;
using QRCoder;
using TMDT.BUS;
using TMDT.DAL;

namespace TMDT.Use
{
    public partial class QRCodePaymentForm : Form
    {
        private readonly int orderId;
        private readonly decimal totalAmount;
        private readonly PaymentBUS paymentBUS;
        private System.Windows.Forms.PictureBox pictureBoxQR;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.Label lblOrderID;
        private System.Windows.Forms.Label lblInstruction;
        private System.Windows.Forms.Button btnCompleted;
        private System.Windows.Forms.Button btnCancel;

        public QRCodePaymentForm(int orderId, decimal totalAmount)
        {
            InitializeComponent();
            this.orderId = orderId;
            this.totalAmount = totalAmount;
            this.paymentBUS = new PaymentBUS();
            GenerateQRCode();
            SetupUI();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form properties
            this.AutoScaleDimensions = new SizeF(8F, 16F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.White;
            this.ClientSize = new Size(500, 650);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Thanh toán QR Code";

            // lblTitle
            this.lblTitle = new Label();
            this.lblTitle.AutoSize = false;
            this.lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.FromArgb(0, 102, 204);
            this.lblTitle.Text = "Thanh Toán QR Code";
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            this.lblTitle.Location = new Point(0, 20);
            this.lblTitle.Size = new Size(500, 35);

            // lblOrderID
            this.lblOrderID = new Label();
            this.lblOrderID.Font = new Font("Segoe UI", 10F);
            this.lblOrderID.Location = new Point(100, 70);
            this.lblOrderID.Size = new Size(300, 25);
            this.lblOrderID.TextAlign = ContentAlignment.MiddleCenter;

            // pictureBoxQR
            this.pictureBoxQR = new PictureBox();
            this.pictureBoxQR.Location = new Point(100, 110);
            this.pictureBoxQR.Size = new Size(300, 300);
            this.pictureBoxQR.SizeMode = PictureBoxSizeMode.StretchImage;

            // lblAmount
            this.lblAmount = new Label();
            this.lblAmount.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblAmount.ForeColor = Color.Red;
            this.lblAmount.Location = new Point(100, 425);
            this.lblAmount.Size = new Size(300, 30);
            this.lblAmount.TextAlign = ContentAlignment.MiddleCenter;

            // lblInstruction
            this.lblInstruction = new Label();
            this.lblInstruction.Font = new Font("Segoe UI", 9F);
            this.lblInstruction.ForeColor = Color.FromArgb(100, 100, 100);
            this.lblInstruction.Location = new Point(50, 470);
            this.lblInstruction.Size = new Size(400, 60);
            this.lblInstruction.TextAlign = ContentAlignment.TopCenter;
            this.lblInstruction.Text = "Quét mã QR bằng ứng dụng ngân hàng\nhoặc ví điện tử để thanh toán";

            // btnCompleted
            this.btnCompleted = new Button();
            this.btnCompleted.BackColor = Color.FromArgb(0, 153, 51);
            this.btnCompleted.FlatStyle = FlatStyle.Flat;
            this.btnCompleted.FlatAppearance.BorderSize = 0;
            this.btnCompleted.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnCompleted.ForeColor = Color.White;
            this.btnCompleted.Location = new Point(100, 550);
            this.btnCompleted.Size = new Size(145, 40);
            this.btnCompleted.Text = "Đã thanh toán";
            this.btnCompleted.Click += BtnCompleted_Click;

            // btnCancel
            this.btnCancel = new Button();
            this.btnCancel.BackColor = Color.White;
            this.btnCancel.FlatStyle = FlatStyle.Flat;
            this.btnCancel.FlatAppearance.BorderColor = Color.Gray;
            this.btnCancel.FlatAppearance.BorderSize = 1;
            this.btnCancel.Font = new Font("Segoe UI", 10F);
            this.btnCancel.ForeColor = Color.FromArgb(0, 102, 204);
            this.btnCancel.Location = new Point(255, 550);
            this.btnCancel.Size = new Size(145, 40);
            this.btnCancel.Text = "Hủy";
            this.btnCancel.Click += BtnCancel_Click;

            // Add controls
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblOrderID);
            this.Controls.Add(this.pictureBoxQR);
            this.Controls.Add(this.lblAmount);
            this.Controls.Add(this.lblInstruction);
            this.Controls.Add(this.btnCompleted);
            this.Controls.Add(this.btnCancel);

            this.ResumeLayout(false);
        }

        private void GenerateQRCode()
        {
            try
            {
                // Cập nhật text cho labels
                lblOrderID.Text = $"Mã đơn hàng: #{orderId}";
                lblAmount.Text = $"Tổng tiền: {totalAmount:N0} đ";

                // Tạo dữ liệu QR code
                string qrData = paymentBUS.GenerateQRData(orderId, totalAmount);

                // Tạo QR Code
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrData_encoded = qrGenerator.CreateQrCode(qrData, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrData_encoded);

                // Chuyển đổi sang bitmap
                Bitmap qrBitmap = qrCode.GetGraphic(10, Color.Black, Color.White, true);

                // Hiển thị QR code
                pictureBoxQR.Image = qrBitmap;

                // Lưu thông tin thanh toán vào database
                paymentBUS.CreateQRPayment(orderId, totalAmount);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tạo QR Code: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupUI()
        {
            this.BackColor = Color.FromArgb(245, 248, 252);
        }

        private void BtnCompleted_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show(
                    "Bạn đã hoàn tất thanh toán?",
                    "Xác nhận thanh toán",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    if (paymentBUS.ConfirmPayment(orderId))
                    {
                        MessageBox.Show(
                            "Thanh toán thành công!",
                            "Thành công",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(
                            "Không thể xác nhận thanh toán. Vui lòng thử lại.",
                            "Lỗi",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Đã xảy ra lỗi: {ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}

