using System;
using System.Linq;
using TMDT.DAL;

namespace TMDT.BUS
{
    public class PaymentBUS
    {
        private Model1 db = new Model1();

        // Tạo thanh toán QR code
        public bool CreateQRPayment(int orderId, decimal amount)
        {
            try
            {
                var payment = new Payment
                {
                    OrderID = orderId,
                    PaymentMethod = "QR Code",
                    Amount = amount,
                    Status = "Pending",
                    TransactionRef = GenerateTransactionRef(),
                    PaidAt = null
                };

                db.Payments.Add(payment);
                db.SaveChanges();

                // Cập nhật trạng thái đơn hàng
                var order = db.OrderTbls.FirstOrDefault(o => o.OrderID == orderId);
                if (order != null)
                {
                    order.Status = "Awaiting Payment";
                    db.SaveChanges();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        // Xác nhận thanh toán (khi người dùng scan QR và thanh toán thành công)
        public bool ConfirmPayment(int orderId)
        {
            try
            {
                var payment = db.Payments.FirstOrDefault(p => p.OrderID == orderId);
                if (payment != null)
                {
                    payment.Status = "Paid";
                    payment.PaidAt = DateTime.Now;

                    var order = db.OrderTbls.FirstOrDefault(o => o.OrderID == orderId);
                    if (order != null)
                    {
                        order.Status = "Paid";
                    }

                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        // Lấy thông tin thanh toán
        public Payment GetPaymentByOrderId(int orderId)
        {
            return db.Payments.FirstOrDefault(p => p.OrderID == orderId);
        }

        // Tạo mã giao dịch duy nhất
        private string GenerateTransactionRef()
        {
            return $"QRPAY_{DateTime.Now:yyyyMMddHHmmss}_{new Random().Next(1000, 9999)}";
        }

        // Tạo dữ liệu để gen QR code
        public string GenerateQRData(int orderId, decimal amount)
        {
            var transactionRef = GenerateTransactionRef();
            var order = db.OrderTbls.FirstOrDefault(o => o.OrderID == orderId);
            
            // Định dạng: Số tiền, mã đơn hàng, thông tin khách hàng
            return $"TMĐT|{amount}|{orderId}|{DateTime.Now:yyyyMMddHHmmss}|{transactionRef}";
        }
    }
}

