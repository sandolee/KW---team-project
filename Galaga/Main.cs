using Galaga.Ui;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
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
            Login Login = new Login();
            Login.Owner = this;
            Login.StartPosition = FormStartPosition.CenterParent;
            Login.ShowDialog();

        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            SignUp SignUp = new SignUp();
            SignUp.Owner = this;
            SignUp.StartPosition = FormStartPosition.CenterParent;
            SignUp.ShowDialog();
        }

        private void btnLogin_KeyDown(object sender, KeyEventArgs e)
        {
            
            if(e.KeyCode == Keys.Enter)
            {
                CustomButton cbtn = (CustomButton)sender;
                if (cbtn.Text.Equals("Login"))
                    btnLogin_Click(sender, e);

                else if (cbtn.Text.Equals("SignUp"))
                    btnSignUp_Click(sender, e);
            }
        }
    }
}
