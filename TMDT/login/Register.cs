using System;
using System.Windows.Forms;
using TMDT.BUS;
using TMDT.DAL;
using System.Linq;

namespace TMDT
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            var username = this.txtUser.Text?.Trim();
            var password = this.txtPass.Text;
            var repassword = this.txtRePass.Text;
            var email = this.txtEmail.Text?.Trim();
            var fullName = this.txtFullName.Text?.Trim();
            var phone = this.txtPhone.Text?.Trim();
            var street = this.txtStreet.Text?.Trim();
            var city = this.txtCity.Text?.Trim();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(repassword) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(street) || string.IsNullOrWhiteSpace(city))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ các trường bắt buộc (bao gồm địa chỉ)", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!string.Equals(password, repassword))
            {
                MessageBox.Show("Mật khẩu nhập lại không khớp", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var newCustomer = new Customer
            {
                UserName = username,
                Password = password,
                Email = email,
                FullName = string.IsNullOrWhiteSpace(fullName) ? null : fullName,
                Phone = string.IsNullOrWhiteSpace(phone) ? null : phone,
                CreatedAt = DateTime.Now,
                IsAdmin = false
            };

            var customerBus = new CustomerBUS();
            var ok = customerBus.Register(newCustomer);
            if (!ok)
            {
                MessageBox.Show("Tên đăng nhập hoặc email đã tồn tại", "Đăng ký thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Tạo địa chỉ mặc định cho khách hàng mới
            using (var db = new Model1())
            {
                var created = db.Customers.FirstOrDefault(c => c.UserName == username);
                if (created != null)
                {
                    db.Addresses.Add(new Address
                    {
                        CustomerID = created.CustomerID,
                        Type = "Shipping",
                        Street = street,
                        City = city,
                        State = null,
                        PostalCode = null,
                        Country = "Việt Nam",
                        IsDefault = true
                    });
                    db.SaveChanges();
                }
            }

            MessageBox.Show("Đăng ký thành công! Vui lòng đăng nhập.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
