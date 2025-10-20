using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using TMDT.DAL;

namespace TMDT.Use
{
    public partial class AccountForm : Form
    {
        public AccountForm()
        {
            InitializeComponent();
            LoadCurrentInfo();
        }

        // Load current user's info into fields
        private void LoadCurrentInfo()
        {
            var user = Session.CurrentCustomer;
            if (user == null) return;
            this.txtHoTen.Text = user.FullName;
            this.txtEmail.Text = user.Email;
            this.txtPhone.Text = user.Phone;
        }

        // Save changes to name/phone (email kept as-is for demo)
        private void btnLuu_Click(object sender, EventArgs e)
        {
            var user = Session.CurrentCustomer;
            if (user == null) return;

            using (var db = new Model1())
            {
                var entity = db.Customers.FirstOrDefault(c => c.CustomerID == user.CustomerID);
                if (entity != null)
                {
                    entity.FullName = this.txtHoTen.Text?.Trim();
                    entity.Phone = this.txtPhone.Text?.Trim();
                    db.SaveChanges();

                    // Update session
                    user.FullName = entity.FullName;
                    user.Phone = entity.Phone;
                }
            }

            MessageBox.Show("Đã lưu thay đổi.");
        }

        // Close the account form
        
    }
}
