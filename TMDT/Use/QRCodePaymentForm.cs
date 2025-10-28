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

        public QRCodePaymentForm(int orderId, decimal totalAmount)
        {
            InitializeComponent();
            this.orderId = orderId;
            this.totalAmount = totalAmount;
            this.paymentBUS = new PaymentBUS();
            GenerateQRCode();
            SetupUI();
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

