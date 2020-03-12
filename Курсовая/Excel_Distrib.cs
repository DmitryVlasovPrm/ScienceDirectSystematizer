using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace CourseWork
{
    class Excel_Distrib
    {
        //Выбор файла для сохранения
        public static string fileOpen()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "xls files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.FileName = "Distributions";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                return saveFileDialog.FileName;
            else
                return String.Empty;
        }

        //Запись распределений в файл
        public static void Creating_Excel_Distributions(string fileName, List<Year_count> Years_count, List<Keyword_count> Keywords_count,
            List<Author_year_count> Authors_years_count, List<int> author_count, Type_count Types_count)
        {
            //Создание распределений, если какие-то не создавались
            if (Years_count.Count == 0) MainForm.Create_years_distrib();
            if (Keywords_count.Count == 0) MainForm.Create_keywords_distrib();
            if (Authors_years_count.Count == 0) MainForm.Create_authors_years_distrib();
            if (MainForm.typeFlag != true) MainForm.Create_type_publications();

            Excel.Application excelApp = new Excel.Application();
            excelApp.DisplayAlerts = false;
            Excel.Workbook excelBook = excelApp.Workbooks.Add();

            //Распределение по кол-ву авторского коллектива и годам-------------------------------------------------------------
            Excel.Worksheet excelSheet1 = (Excel.Worksheet)excelBook.Worksheets.get_Item(1);
            excelSheet1.Name = "Авторский коллектив и года";
            Excel.Range range1 = excelSheet1.Range[excelSheet1.Cells[1, 1], excelSheet1.Cells[1, author_count.Count + 1]];
            range1.Font.Bold = true;
            excelSheet1.Cells[1, 1] = "Год " + @"\" + " Кол-во авторов";
            int row_count = 1;
            for (int i = 0, acCnt = author_count.Count;  i < acCnt; i++)
                excelSheet1.Cells[1, i + 2] = author_count[i];

            foreach (var item in Authors_years_count)
            {
                if (row_count == 1 || ((Excel.Range)excelSheet1.Cells[row_count, 1]).Value2.ToString() != item.year)
                {
                    excelSheet1.Cells[row_count + 1, 1] = item.year;
                    row_count++;
                }
                for (int j = 0, acCnt = author_count.Count; j < acCnt; j++)
                {
                    int cur_count = int.Parse(((Excel.Range)excelSheet1.Cells[1, j + 2]).Value2.ToString());
                    string cur_year = ((Excel.Range)excelSheet1.Cells[row_count - 1, 1]).Value2.ToString();
                    Author_year_count elem = Authors_years_count.Find(a => a.author_count == cur_count && a.year == cur_year);
                    if (elem != null)
                        excelSheet1.Cells[row_count, j + 2] = elem.publication_count;
                    else
                        excelSheet1.Cells[row_count, j + 2] = 0;
                }
            }
            range1 = excelSheet1.Range[excelSheet1.Cells[1, 1], excelSheet1.Cells[row_count, author_count.Count + 1]];
            range1.Font.Size = 14;
            range1.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            range1.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            range1.Columns.AutoFit();
            range1 = excelSheet1.Range[excelSheet1.Cells[1, 2], excelSheet1.Cells[row_count, author_count.Count + 1]];
            range1.Columns.ColumnWidth = 6;

            //Распределение по ключевым словам--------------------------------------------------------------------------------
            Excel.Worksheet excelSheet2 = (Excel.Worksheet)excelBook.Worksheets.Add();
            excelSheet2.Name = "Ключевые слова";
            excelSheet2.Cells[1, 1].Font.Bold = true; excelSheet2.Cells[1, 2].Font.Bold = true;
            excelSheet2.Cells[1, 1] = "Ключевое слово"; excelSheet2.Cells[1, 2] = "Количество публикаций";
            for (int i = 0, kwCnt = Keywords_count.Count; i < kwCnt; i++)
            {
                excelSheet2.Cells[i + 2, 1] = Keywords_count[i].keyword;
                excelSheet2.Cells[i + 2, 2] = Keywords_count[i].publication_count;
            }
            Excel.Range range2 = excelSheet2.Range[excelSheet2.Cells[1, 1], excelSheet2.Cells[Keywords_count.Count + 1, 2]];
            range2.Font.Size = 14;
            range2.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            range2.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            range2.Columns.AutoFit();

            //Распределение по годам--------------------------------------------------------------------------------------
            Excel.Worksheet excelSheet3 = (Excel.Worksheet)excelBook.Worksheets.Add();
            excelSheet3.Name = "Года издания";
            excelSheet3.Cells[1, 1].Font.Bold = true; excelSheet3.Cells[1, 2].Font.Bold = true;
            excelSheet3.Cells[1, 1] = "Год"; excelSheet3.Cells[1, 2] = "Количество публикаций";
            for (int i = 0, yCnt = Years_count.Count; i < yCnt; i++)
            {
                excelSheet3.Cells[i + 2, 1] = Years_count[i].year;
                excelSheet3.Cells[i + 2, 2] = Years_count[i].publication_count;
            }
            Excel.Range range3 = excelSheet3.Range[excelSheet3.Cells[1, 1], excelSheet3.Cells[Years_count.Count + 1, 2]];
            range3.Font.Size = 14;
            range3.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            range3.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            range3.Columns.AutoFit();

            //Сохранение книги excel
            DialogResult dialogResult = DialogResult.No;
            try
            {
                excelBook.SaveAs(fileName);
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
                excelApp.Visible = true;
                excelApp.WindowState = Excel.XlWindowState.xlMaximized;
            }
            else
            {
                excelApp.Quit();
            }
        }
    }
}
