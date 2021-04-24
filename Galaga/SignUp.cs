using Galaga.FileAccess;
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
    public partial class sign_up : Form
    {
        public sign_up()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            FileAccess.FileAccess fileAccess = new FileAccess.FileAccess();
            List<string[]> userInfo = fileAccess.readInfo();

            if(txtID.Text is null || txtPW.Text is null)
                MessageBox.Show("반드시 ID와 PW 값을 모두 입력해야합니다.");

            bool check = false;
            for (int row = 0; row < userInfo.Count; row++)
                if (string.Equals(txtID.Text, userInfo[row][0]))
                {
                    check = true;
                    break;
                }

            if (!check)
            {

                if (Equals(txtPWCheck.Text, txtPW.Text))
                {
                    fileAccess.WriteInfo(txtID.Text, txtPW.Text);
                    this.Close();
                }
                else
                    MessageBox.Show("비밀번호가 일치하지 않습니다.");
            }
            else
                MessageBox.Show("중복된 ID입니다");
        }
    }
}
