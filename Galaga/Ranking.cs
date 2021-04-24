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
    public partial class Ranking : Form
    {
        private String[] rankingInfo ={"Rank","Name","Stage","Score" };
        public Ranking()
        {
            InitializeComponent();
        }


        private void Ranking_Load(object sender, EventArgs e)
        {
            FileAccess.FileAccess fileAccess = new FileAccess.FileAccess();
            List<String[]> userInfo = fileAccess.readInfo();
            lvwRanking.View = View.Details;

            //colum 추가 
            int colWidth = (int) lvwRanking.ClientSize.Width / 4;
            for (int i = 0; i < rankingInfo.Length; i++)
                lvwRanking.Columns.Add(rankingInfo[i], colWidth, HorizontalAlignment.Center);

            //정렬=
            var sortedList = userInfo.OrderByDescending(x1 => int.Parse(x1[2])).ToList();

            //item 추가 
            //playerInfo는 ID, Stage, Score, PW 순서로 저장되어 있음
            for (int row = 0; row <sortedList.Count; row++)
            {
                ListViewItem newItem = new ListViewItem((row+1).ToString());
                for (int i = 0; i < sortedList[row].Length; i++)
                    newItem.SubItems.Add(sortedList[row][i]);

                lvwRanking.Items.Add(newItem);
            }
            

        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
