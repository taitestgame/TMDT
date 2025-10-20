using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDT.DAL;

namespace TMDT.BUS
{
    public class ProductBUS
    {

        private Model1 db = new Model1(); // Kết nối database

        // Lấy tất cả sản phẩm
        public List<Product> GetAllProducts()
        {
            return db.Products.ToList();
        }

        // Lấy sản phẩm theo ID
        public Product GetProductById(int id)
        {
            return db.Products.FirstOrDefault(p => p.ProductID == id);
        }

        // Tìm kiếm sản phẩm theo tên
        public List<Product> SearchProducts(string keyword)
        {
            return db.Products
                     .Where(p => p.Name.Contains(keyword))
                     .ToList();
        }

        // Lấy sản phẩm theo danh mục
        public List<Product> GetProductsByCategory(int categoryId)
        {
            return db.Products
                     .Where(p => p.DefaultCategoryID == categoryId)
                     .ToList();
        }

    }
}
