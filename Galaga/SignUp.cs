using Galaga.FileAccess;
using Galaga.Ui;
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
    public partial class SignUp : Form
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            var userInfo = FileAccess.FileAccess.ReadInfo();

            if (String.Equals(txtID.Text, String.Empty) || String.Equals(txtPW.Text, String.Empty))
            {
                MessageBox.Show("반드시 ID와 PW 값을 모두 입력해야합니다.");
                return;
            }

            bool check = false;
            for (int row = 0; row < userInfo.Count; row++)
            {
                if (string.Equals(txtID.Text, userInfo[row].id))
                {
                    check = true;
                    break;
                }
            }

            if (!check)
            {

                if (Equals(txtPWCheck.Text, txtPW.Text))
                {
                    FileAccess.FileAccess.WriteInfo(txtID.Text, txtPW.Text);
                    Close();
                }
                else
                    MessageBox.Show("비밀번호가 일치하지 않습니다.");
            }
            else
                MessageBox.Show("중복된 ID입니다");
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOk_Click(sender, e);
        }
    }
}
