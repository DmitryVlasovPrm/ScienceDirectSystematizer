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
    class WordList
    {
        //Выбор файла
        public static string FileOpen(int type)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "doc files (*.docx)|*.docx|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;
            if (type == 1) saveFileDialog.FileName = "Список (ГОСТ)";
            else saveFileDialog.FileName = "Список (IEEE)";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                return saveFileDialog.FileName;
            else
                return String.Empty;
        }

        //Сохранение в файл
        public static void CreatingWordList(string fileName, List<Publication> filterPublications, int type)
        {
            Word.Application wordApp = new Word.Application();
            wordApp.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsNone;
            Word.Document wordDoc = new Word.Document();
            wordDoc = wordApp.Documents.Add();

            /*
            Word.Paragraph paragraph = null;
            Word.Range range = wordDoc.Range();

            paragraph = range.Paragraphs.Add();
            paragraph.Range.Text = FilterPublications[0].title;

            paragraph.Range.ListFormat.ApplyNumberDefault(Word.WdListGalleryType.wdNumberGallery);
            */

            object endOfDoc = "\\endofdoc";
            for (int i = 0, fbCnt = filterPublications.Count; i < fbCnt; i++)
            {
                string authors = "";
                string info = "";
                string pages = "";

                var item = filterPublications[i];

                //Попытка вычисления кол-ва страниц
                try
                {
                    string[] endStart = item.pages.Split(new string[] { " - " }, StringSplitOptions.None);
                    pages = (int.Parse(endStart[1]) - int.Parse(endStart[0])).ToString();
                }
                catch
                {
                    pages = item.pages;
                }

                //Формирование строки
                if (item.authors.Count <= 3)
                {
                    for (int j = 0, authCnt = item.authors.Count; j < authCnt; j++)
                    {
                        if (j != authCnt - 1)
                            authors += item.authors[j] + ", ";
                        else
                            authors += item.authors[j] + ". – ";
                    }
                    if (item.isbn != "")
                    {
                        info = item.authors[0] + ". " + item.booktitle +
                            " : " + item.title + " / " + authors + item.publisher + ", " +
                            item.year + ". - " + pages + " p.";
                    }
                    else
                        info = item.title;
                }
                else
                {
                    authors = item.authors[0] + " [et al.]. – ";
                    if (item.isbn != "")
                    {
                        info = item.booktitle + " : " + item.title + " / " +
                            authors + item.publisher + ", " + item.year + ". – " + pages + " p.";
                    }
                    else
                        info = item.title;
                }

                //Запись строки в файл
                Word.Paragraph paragraph;
                paragraph = wordDoc.Content.Paragraphs.Add();
                paragraph.Range.Text = info;
                paragraph.Range.Font.Size = 14;
                paragraph.Range.Font.Name = "Times New Roman";

                if (i == 0)
                    paragraph.Range.ListFormat.ApplyNumberDefault(Word.WdListGalleryType.wdNumberGallery);
                if (i != fbCnt - 1)
                    paragraph.Range.InsertParagraphAfter();
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
