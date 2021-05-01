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
    public partial class Ranking : Form
    {
        private String[] rankingInfo ={"Rank","Name","Stage","Score" };
        public Ranking()
        {
            InitializeComponent();
        }

        private void Ranking_Load(object sender, EventArgs e)
        {
            var userInfo = FileAccess.FileAccess.ReadInfo();
            lvwRanking.View = View.Details;

            //colum 추가 
            int colWidth = (int) lvwRanking.ClientSize.Width / 4;
            for (int i = 0; i < rankingInfo.Length; i++)
                lvwRanking.Columns.Add(rankingInfo[i], colWidth, HorizontalAlignment.Center);

            //정렬=
            var sortedList = userInfo.OrderByDescending(x1 => x1.score).ToList();

            //item 추가 
            //playerInfo는 ID, Stage, Score, PW 순서로 저장되어 있음
            for (int row = 0; row <sortedList.Count; row++)
            {
                ListViewItem newItem = new ListViewItem((row+1).ToString());
                newItem.SubItems.Add(sortedList[row].id);
                newItem.SubItems.Add(sortedList[row].stage.ToString());
                newItem.SubItems.Add(sortedList[row].score.ToString());
                newItem.SubItems.Add(sortedList[row].password);

                lvwRanking.Items.Add(newItem);
            }
            

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
