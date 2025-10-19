using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDT.DAL;

namespace TMDT.BUS
{
    internal class OrderBUS
    {


        private Model1 db = new Model1();

        // Tạo đơn hàng mới từ danh sách sản phẩm
        public bool CreateOrder(int customerId, List<OrderItem> orderItems, decimal shippingFee = 0, decimal taxRate = 0.1m, decimal discountAmount = 0)
        {
            try
            {
                // Tính SubTotal
                decimal subTotal = orderItems.Sum(item => item.UnitPrice * item.Quantity);

                // Tính các khoản khác
                decimal taxAmount = subTotal * taxRate;
                decimal totalAmount = subTotal + shippingFee + taxAmount - discountAmount;

                // Tạo đơn hàng
                var order = new OrderTbl
                {
                    CustomerID = customerId,
                    OrderDate = DateTime.Now,
                    Status = "Pending",
                    SubTotal = subTotal,
                    ShippingFee = shippingFee,
                    TaxAmount = taxAmount,
                    DiscountAmount = discountAmount,
                    TotalAmount = totalAmount
                };

                db.OrderTbls.Add(order);
                db.SaveChanges(); // Lưu để có OrderID

                // Gắn OrderID cho từng OrderItem và tính TotalPrice
                foreach (var item in orderItems)
                {
                    item.OrderID = order.OrderID;
                    item.TotalPrice = item.UnitPrice * item.Quantity;
                    db.OrderItems.Add(item);
                }

                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Lấy danh sách đơn hàng của khách
        public List<OrderTbl> GetOrdersByCustomer(int customerId)
        {
            return db.OrderTbls
                     .Where(o => o.CustomerID == customerId)
                     .OrderByDescending(o => o.OrderDate)
                     .ToList();
        }

        // Lấy chi tiết đơn hàng theo OrderID
        public List<OrderItem> GetOrderItems(int orderId)
        {
            return db.OrderItems
                     .Where(oi => oi.OrderID == orderId)
                     .ToList();
        }


    }
}
