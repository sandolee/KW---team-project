using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Galaga
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //this.Hide();
            Login Login = new Login();
            Login.Owner = this;
            Login.ShowDialog();

        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            sign_up SignUp = new sign_up();
            SignUp.Owner = this;
            SignUp.ShowDialog();
        }
    }
}
