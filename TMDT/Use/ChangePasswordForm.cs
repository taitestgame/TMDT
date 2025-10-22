using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TMDT.DAL;

namespace TMDT.Use
{
    public partial class ChangePasswordForm : Form
    {
        public ChangePasswordForm()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var user = Session.CurrentCustomer;
            if (user == null)
            {
                MessageBox.Show("Vui lòng đăng nhập lại.");
                return;
            }

            string oldPass = txtOldPass.Text.Trim();
            string newPass = txtNewPass.Text.Trim();
            string confirm = txtConfirm.Text.Trim();

            if (string.IsNullOrEmpty(oldPass) || string.IsNullOrEmpty(newPass) || string.IsNullOrEmpty(confirm))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ các trường.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newPass != confirm)
            {
                MessageBox.Show("Mật khẩu mới và xác nhận không khớp.", "Lỗi xác nhận", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var db = new Model1())
            {
                var entity = db.Customers.FirstOrDefault(c => c.CustomerID == user.CustomerID);
                if (entity == null)
                {
                    MessageBox.Show("Không tìm thấy tài khoản người dùng.");
                    return;
                }

                if (entity.Password != oldPass)
                {
                    MessageBox.Show("Mật khẩu cũ không đúng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                entity.Password = newPass;
                db.SaveChanges();

                // Cập nhật lại session
                user.Password = newPass;

                MessageBox.Show("Đổi mật khẩu thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
