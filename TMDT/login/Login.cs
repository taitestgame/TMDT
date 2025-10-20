using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TMDT.Admin;
using TMDT.BUS;
using TMDT.Use;
using TMDT.DAL;
namespace TMDT
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var username = this.txtUser.Text?.Trim();
            var password = this.txtPass.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập và mật khẩu", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var customerBus = new CustomerBUS();
            var customer = customerBus.Authenticate(username, password);

            if (customer == null)
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng", "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Lưu session người dùng hiện tại
            Session.CurrentCustomer = customer;

            // Phân quyền theo cột IsAdmin trong bảng Customer
            Form nextForm = customer.IsAdmin
                ? (Form)new AdminMainForm()
                : (Form)new UserMain();

            nextForm.FormClosed += (s, args) => this.Show();
            this.Hide();
            nextForm.Show();
        }

        private void chkShowPass_CheckedChanged(object sender, EventArgs e)
        {
            this.txtPass.PasswordChar = this.chkShowPass.Checked ? '\0' : '*';
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            using (var registerForm = new Register())
            {
                registerForm.FormClosed += (s, args) => this.Show();
                this.Hide();
                registerForm.ShowDialog();
            }
        }
    }
}
