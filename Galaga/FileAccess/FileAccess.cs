using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static System.IO.Directory;
using System.Windows.Forms;

namespace Galaga.FileAccess
{

    public class Account
    {
        public string id;
        public string password;
        public int stage;
        public int score;
        public Account(string id, int stage, int score, string password)
        {
            this.id = id; this.password = password; this.stage = stage; this.score = score;
        }
    }
    static class FileAccess
    {
        static String directoryPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "PlayerInfo"));
        static String path = Path.GetFullPath(Path.Combine(directoryPath, "PlayerInfo.csv"));
        
        public static List<Account> ReadInfo()
        {
            CreateInfo();

            StreamReader sr = new StreamReader(path);
            List<Account> accounts = new List<Account>();
            var csvList = new List<string[]>();
            try
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    csvList.Add(line.Split(','));
                }
                for (int row = 0; row < csvList.Count; row++)
                {
                    accounts.Add(new Account(csvList[row][0], int.Parse(csvList[row][1]), int.Parse(csvList[row][2]), csvList[row][3]));
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "File Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sr.Close();
            }
            return accounts;
        }

        public static void WriteInfo(String ID, String PW)
        {
            CreateInfo();

            try
            {
                //ID, Stage, Score,PW 
                File.AppendAllText(path, ID + ",0,0," + PW+"\n");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "File Write Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void UpdateInfo(string ID, int stage, int score)
        {
            CreateInfo();
            var csvList = new List<string[]>();
            try
            {
                string[] line;
                line = File.ReadAllLines(path);
                int index=Array.FindIndex(line, row=>row.Contains(ID));
                string PW = line[index].Split(',')[3];
                line[index] = ID + "," + stage.ToString() + "," + score.ToString() + "," + PW;
                File.WriteAllLines(path, line);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "File Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static void CreateInfo()
        {

            if (!Exists(directoryPath))
            {
                CreateDirectory(directoryPath);
                FileStream stream = File.Create(path);
            }
        }
    }
}
