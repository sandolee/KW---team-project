using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Galaga.FileAccess
{
    class FileAccess
    {
        String path = System.IO.Directory.GetCurrentDirectory()+"./../../PlayerInfo/PlayerInfo.csv";
        public List<string[]> readInfo()
        {
            StreamReader sr = new StreamReader(path);
            var csvList = new List<string[]>();
            try
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                    csvList.Add(line.Split(','));

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "File Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sr.Close();
            }
            return csvList;
        }

        public void WriteInfo(String ID, String PW)
        {
            try
            {
                File.AppendAllText(path, ID + ",0,0," + PW+"\n");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "File Write Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
