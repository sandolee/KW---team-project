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
            FileAccess.FileAccess fileAccess = new FileAccess.FileAccess();
            bool check=false;
            List<string[]> PlayerInfoList = fileAccess.readInfo();

            //playerInfo는 ID, Stage, Score, PW 순서로 저장되어 있음
            for (int row = 0; row < PlayerInfoList.Count; row++)
                if (string.Equals(PlayerInfoList[row][0], txtID.Text))
                    check = true;
            if (check)
            {
                check = false;

                for (int row = 0; row < PlayerInfoList.Count; row++)
                    if (string.Equals(PlayerInfoList[row][3], txtPW.Text))
                        check = true;
            }
            if (check)
            {
                this.Hide();
                this.Owner.Hide();
                UserPage UserPage = new UserPage(txtID.Text);
                UserPage.Owner = this;
                UserPage.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Check ID or PW");
            }
            
            
        }
    }
}
