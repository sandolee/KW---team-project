using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Galaga.FileAccess;
using Galaga.Ui;

namespace Galaga
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (String.Equals(txtID.Text, String.Empty) || String.Equals(txtPW.Text, String.Empty))
            {
                MessageBox.Show("ID와 PW를 입력하세요");
                return;
            }
            bool check=false;
            var PlayerInfoList = FileAccess.FileAccess.ReadInfo();

            for (int row = 0; row < PlayerInfoList.Count; row++)
                if (string.Equals(PlayerInfoList[row].id, txtID.Text))
                {
                    check = true;
                    if(string.Equals(PlayerInfoList[row].password, txtPW.Text)){

                        this.Hide();
                        this.Owner.Hide();
                        UserPage userPage = new UserPage(txtID.Text);
                        userPage.Owner = this;
                        userPage.ShowDialog();
                        this.Close();
                    }
                    else
                        MessageBox.Show("Check PW");
                }
            if (!check)
                MessageBox.Show("Check ID");

        }
        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOk_Click(sender, e);
        }

    }
}
