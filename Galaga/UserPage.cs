using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Galaga.FileAccess;

namespace Galaga
{
    public partial class UserPage : Form
    {
        private String userName;
        public UserPage(String userName)
        {
            this.userName = userName;
            InitializeComponent();
        }


        private void Ranking_Click(object sender, EventArgs e)
        {
            Ranking Ranking = new Ranking();
            Ranking.Owner = this;
            Ranking.ShowDialog();

        }

        private void UserPage_Load(object sender, EventArgs e)
        {
            FileAccess.FileAccess fileAccess = new FileAccess.FileAccess();
            var userInfo = fileAccess.readInfo();
            int userIndex=0;


            for (int row = 0; row < userInfo.Count; row++)
                if (string.Equals(userInfo[row][0], userName))
                    userIndex = row;

            lblName.Text = userInfo[userIndex][0];
            lblStage.Text = userInfo[userIndex][1];
            lblScore.Text = userInfo[userIndex][2];

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 Game = new Form1();
            Game.Owner = this;
            Game.ShowDialog();
            this.Show();
        }

        private void UserPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Owner.Owner.Close();
        }
    }
}
