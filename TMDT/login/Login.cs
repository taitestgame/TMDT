using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TMDT
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            Application.Run(new Register());
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }
    }
}
