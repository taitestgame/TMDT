using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDT.DAL;

namespace TMDT.BUS
{
    internal class CustomerBUS
    {

        private Model1 db = new Model1(); // Kết nối database

        // Đăng nhập: kiểm tra username và password
        public bool Login(string username, string password)
        {
            return db.Customers.Any(c => c.UserName == username && c.Password == password);
        }

        // Đăng ký: kiểm tra trùng username/email trước khi thêm
        public bool Register(Customer newCustomer)
        {
            if (db.Customers.Any(c => c.UserName == newCustomer.UserName || c.Email == newCustomer.Email))
                return false;

            db.Customers.Add(newCustomer);
            db.SaveChanges();
            return true;
        }

        // Lấy thông tin khách hàng theo ID
        public Customer GetCustomerById(int id)
        {
            return db.Customers.FirstOrDefault(c => c.CustomerID == id);
        }

    }
}
