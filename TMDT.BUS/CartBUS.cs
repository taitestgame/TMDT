using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDT.DAL;

namespace TMDT.BUS
{
    internal class CartBUS
    {

        private Model1 db = new Model1();


        // Thêm sản phẩm vào giỏ hàng
        public void AddToCart(int customerId, int variantId, int quantity)
        {
            // Tìm giỏ hàng của khách
            var cart = db.Carts.FirstOrDefault(c => c.CustomerID == customerId);
            if (cart == null)
            {
                cart = new Cart
                {
                    CustomerID = customerId,
                    CreatedAt = DateTime.Now
                };
                db.Carts.Add(cart);
                db.SaveChanges(); // Lưu để có CartID
            }

            // Kiểm tra sản phẩm đã có trong giỏ chưa
            var existingItem = db.CartItems.FirstOrDefault(ci =>
                ci.CartID == cart.CartID && ci.VariantID == variantId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                var newItem = new CartItem
                {
                    CartID = cart.CartID,
                    VariantID = variantId,
                    Quantity = quantity,
                    UnitPrice = db.ProductVariants
                                 .Where(v => v.VariantID == variantId)
                                 .Select(v => v.Price)
                                 .FirstOrDefault()
                };
                db.CartItems.Add(newItem);
            }

            db.SaveChanges();
        }

        // Xóa sản phẩm khỏi giỏ hàng
        public void RemoveFromCart(int cartItemId)
        {
            var item = db.CartItems.Find(cartItemId);
            if (item != null)
            {
                db.CartItems.Remove(item);
                db.SaveChanges();
            }
        }

        // Lấy danh sách sản phẩm trong giỏ hàng của khách
        public List<CartItem> GetCartItems(int customerId)
        {
            return db.CartItems
                     .Where(ci => ci.Cart.CustomerID == customerId)
                     .ToList();
        }

        // Tính tổng tiền giỏ hàng
        public decimal CalculateTotal(int customerId)
        {
            var cartItems = db.CartItems
                              .Where(ci => ci.Cart.CustomerID == customerId)
                              .ToList();

            decimal total = 0;

            foreach (var item in cartItems)
            {
                total += item.UnitPrice * item.Quantity;
            }

            return total;
        }


    }
}
