using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CourseWork
{
    class BibTeX
    {
        //Выбор файла для сохранения
        public static string FileOpen()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "bib files (*.bib)|*.bib|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.FileName = "Metadata";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                return saveFileDialog.FileName;
            else
                return String.Empty;
        }

        //Запись распределений в файл
        public static void CreatingList(string fileName, List<Publication> FilterPublications)
        {
            string output = "";
            for (int i = 0, pCnt = FilterPublications.Count; i < pCnt; i++)
            {
                output += Functions.CreateMetadata(FilterPublications[i]);
                if (i != pCnt - 1)
                    output += "\n";
            }

            try
            {
                StreamWriter file = new StreamWriter(fileName);
                file.WriteLine(output);
                file.Close();
                MessageBox.Show("Сохранение прошло успешно", "Science Direct Systematizer",
                    MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении.\nФайл не сохранен.\n" + ex.Message.ToString(),
                    "Science Direct Systematizer");
            }
        }
    }
}
