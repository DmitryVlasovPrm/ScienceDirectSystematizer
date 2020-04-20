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

            object endOfDoc = "\\endofdoc";
            for (int i = 0, fbCnt = filterPublications.Count; i < fbCnt; i++)
            {
                string authors = "";
                string editors = "";
                string info = "";
                string pages = "";
                int start = 0, end = 0;

                var item = filterPublications[i];

                if (type == 1)
                {
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
                    //Авторов от 1 до 3 включительно
                    if (item.authors.Count <= 3)
                    {
                        for (int j = 0, authCnt = item.authors.Count; j < authCnt; j++)
                        {
                            if (j != authCnt - 1)
                                authors += item.authors[j] + ", ";
                            else
                                authors += item.authors[j];
                        }
                        //Для книги
                        if (item.isbn != "")
                        {
                            info = item.authors[0] + ". " + item.booktitle +
                                " : " + item.title + " / " + authors + ". – " + item.publisher + ", " +
                                item.year + ". – " + pages + " p.";
                        }
                        //Для других типов
                        else
                        {
                            info = item.authors[0] + ". " + item.title +
                                " / " + authors + " // " + item.journal + ". – " + item.year + ". – Vol. " + item.volume
                                + ". – P. " + item.pages + ".";
                        }
                    }
                    //Авторов больше 3
                    else
                    {
                        authors = item.authors[0] + " [et al.]";
                        //Для книг
                        if (item.isbn != "")
                        {
                            info = item.booktitle + " : " + item.title + " / " +
                                authors + ". – " + item.publisher + ", " + item.year + ". – " + pages + " p.";
                        }
                        else
                            info = item.title + " / " + authors + " // " + item.journal +
                                ". - " + item.year + ". – Vol. " + item.volume + ". – P. " + item.pages + ".";
                    }
                }
                else
                {
                    if (item.authors.Count == 1)
                        authors = item.authors[0] + ", ";
                    else
                    {
                        for (int j = 0; j < item.authors.Count; j++)
                        {
                            if (j != item.authors.Count - 2)
                                authors += item.authors[j] + ", ";
                            else
                            {
                                authors += item.authors[j] + " and " + item.authors[j + 1] + ", ";
                                break;
                            }
                        }
                    }

                    if (item.editor.Count == 1)
                        editors = item.editor[0] + ", Ed., ";
                    else
                    {
                        for (int j = 0; j < item.editor.Count; j++)
                        {
                            if (j != item.editor.Count - 2)
                                editors += item.editor[j] + ", ";
                            else
                            {
                                editors += item.editor[j] + " and " + item.editor[j + 1] + ", Eds., ";
                                break;
                            }
                        }
                    }

                    bool flag = false;
                    string edAuthors = authors;
                    if (new HashSet<string>(item.authors).SetEquals(item.editor))
                    {
                        if (item.authors.Count == 1) edAuthors += " Ed., ";
                        else edAuthors += " Eds., ";
                        flag = true;
                    }

                    start = edAuthors.Length + 7 + item.title.Length;
                    //Если книга
                    if (item.isbn != "")
                    {
                        if (!flag)
                            info = edAuthors + "\"" + item.title + ",\" in " + item.booktitle + ", " + editors +
                                item.publisher + ", " + item.year + ", pp. " + item.pages + ".";
                        else
                            info = edAuthors + "\"" + item.title + ",\" in " + item.booktitle + ", " +
                                item.publisher + ", " + item.year + ", pp. " + item.pages + ".";
                        end = start + item.booktitle.Length + 1;
                    }
                    //Другие варианты
                    else
                    {
                        info = authors + "\"" + item.title + ",\" in " + item.journal + ", " +
                                item.year + ", vol. " + item.volume + ", pp. " + item.pages + ", doi: " + item.doi + ".";
                        end = start + item.journal.Length + 1;
                    }
                }

                //Запись строки в файл
                Word.Paragraph paragraph;
                paragraph = wordDoc.Content.Paragraphs.Add();
                paragraph.Range.Text = info;
                if (type == 2)
                {
                    object oStart = paragraph.Range.Start + start;
                    object oEnd = paragraph.Range.Start + end;
                    Word.Range rBold = wordDoc.Range(ref oStart, ref oEnd);
                    rBold.Italic = 1;
                }
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
