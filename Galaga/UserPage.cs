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
using Galaga.Ui;

namespace Galaga
{
    public partial class UserPage : Form
    {
        private String userName;
        public UserPage(String userName)
        {
            InitializeComponent();
            this.userName = userName;
        }

        private void Ranking_Click(object sender, EventArgs e)
        {
            Ranking Ranking = new Ranking();
            Ranking.Owner = this;
            Ranking.StartPosition = FormStartPosition.Manual;
            Ranking.Location = new Point(this.Location.X, this.Location.Y);
            Ranking.Show();
        }

        private void UserPage_Load(object sender, EventArgs e)
        {
            var userInfo = FileAccess.FileAccess.ReadInfo();
            var userIndex = userInfo.FindIndex(it => string.Equals(it.id, userName));

            lblName.Text = userInfo[userIndex].id;
            lblStage.Text = userInfo[userIndex].stage.ToString();
            lblScore.Text = userInfo[userIndex].score.ToString();

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 Game = new Form1();
            Game.Owner = this;
            Game.StartPosition = FormStartPosition.CenterParent;
            Game.ShowDialog();
            this.Show();
        }

        private void UserPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Owner.Owner.Close();
        }
    }
}
