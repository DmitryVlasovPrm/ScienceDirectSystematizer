using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;
using System.Runtime.InteropServices.ComTypes;

namespace CourseWork
{
    class Word_List
    {
        //Выбор файла
        public static string fileOpen()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "doc files (*.docx)|*.docx|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.FileName = "List";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                return saveFileDialog.FileName;
            else
                return String.Empty;
        }

        //Сохранение в файл
        public static void Creating_Word_Lists(string fileName, List<Publication> FilterPublications)
        {
            Word.Application wordApp = new Word.Application();
            wordApp.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsNone;
            Word.Document wordDoc = new Word.Document();
            wordDoc = wordApp.Documents.Add();

            Word.Paragraph paragraph = null;
            Word.Range range = wordDoc.Range();

            paragraph = range.Paragraphs.Add();
            paragraph.Range.Text = FilterPublications[0].title;

            paragraph.Range.ListFormat.ApplyNumberDefault(Word.WdListGalleryType.wdNumberGallery);

            for (int i = 1, fbCnt = FilterPublications.Count; i < fbCnt; i++)
            {
                paragraph.Range.Font.Size = 14;
                paragraph.Range.InsertParagraphAfter();
                paragraph = range.Paragraphs.Add();
                paragraph.Range.Font.Size = 14;
                paragraph.Range.Text = FilterPublications[i].title;
            }

            //Сохранение Word
            DialogResult dialogResult = DialogResult.No;
            try
            {
                wordDoc.SaveAs2(fileName);
                dialogResult = MessageBox.Show("Сохранение прошло успешно\nОткрыть файл?", "Science Direct Systematizer",
                    MessageBoxButtons.YesNo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении.\nФайл не сохранен.\n" + ex.Message.ToString(),
                    "Science Direct Systematizer");
            }

            if (dialogResult == DialogResult.Yes)
            {
                wordApp.Visible = true;
                wordApp.Activate();
                wordApp.WindowState = Word.WdWindowState.wdWindowStateMaximize;
            }
            else
            {
                wordApp.Quit();
            }
        }
    }
}
