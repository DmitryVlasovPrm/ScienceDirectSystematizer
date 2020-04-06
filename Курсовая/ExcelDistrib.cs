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
    class ExcelDistrib
    {
        //Выбор файла для сохранения
        public static string FileOpen()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "xls files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.FileName = "Распределения";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                return saveFileDialog.FileName;
            else
                return String.Empty;
        }

        //Запись распределений в файл
        public static void CreatingExcelDistributions(string fileName, List<YearCount> yearsCount, List<KeywordCount> keywordsCount,
            List<AuthorYearCount> authorsYearsCount, List<int> authorCount, TypeCount typesCount, List<YearCountType> conferences,
            List<string> conferencesYears, List<YearCountType> journals, List<string> journalsYears)
        {
            //Создание распределений, если какие-то не создавались
            if (yearsCount.Count == 0) MainForm.Create_years_distrib();
            if (keywordsCount.Count == 0) MainForm.Create_keywords_distrib();
            if (authorsYearsCount.Count == 0) MainForm.Create_authors_years_distrib();
            if (MainForm.typeFlag != true) MainForm.Create_type_publications();

            Excel.Application excelApp = new Excel.Application();
            excelApp.DisplayAlerts = false;
            Excel.Workbook excelBook = excelApp.Workbooks.Add();

            //Распределение по кол-ву авторского коллектива и годам-------------------------------------------------------------
            Excel.Worksheet excelSheet1 = (Excel.Worksheet)excelBook.Worksheets.get_Item(1);
            excelSheet1.Name = "Авторский коллектив и года";
            Excel.Range range1 = excelSheet1.Range[excelSheet1.Cells[1, 1], excelSheet1.Cells[1, authorCount.Count + 1]];
            range1.Font.Bold = true;
            excelSheet1.Cells[1, 1] = "Год " + @"\" + " Кол-во авторов";
            int row_count1 = 1;
            for (int i = 0, acCnt = authorCount.Count;  i < acCnt; i++)
                excelSheet1.Cells[1, i + 2] = authorCount[i];

            foreach (var item in authorsYearsCount)
            {
                if (row_count1 == 1 || ((Excel.Range)excelSheet1.Cells[row_count1, 1]).Value2.ToString() != item.year)
                {
                    excelSheet1.Cells[row_count1 + 1, 1] = item.year;
                    row_count1++;
                }

                excelSheet1.Cells[row_count1, authorCount.FindIndex(x => x == item.author_count) + 2].Value
                    = item.publication_count;
            }
            range1 = excelSheet1.Range[excelSheet1.Cells[1, 1], excelSheet1.Cells[row_count1, authorCount.Count + 1]];
            range1.Font.Size = 14;
            range1.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            range1.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            range1.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            range1.Columns.AutoFit();
            range1 = excelSheet1.Range[excelSheet1.Cells[1, 2], excelSheet1.Cells[row_count1, authorCount.Count + 1]];
            range1.Columns.ColumnWidth = 6;

            //Распределение по кол-ву конференций и годам-------------------------------------------------------------
            Excel.Worksheet excelSheet2 = (Excel.Worksheet)excelBook.Worksheets.Add();
            excelSheet2.Name = "Конференции и года";
            Excel.Range range2 = excelSheet2.Range[excelSheet2.Cells[1, 1], excelSheet2.Cells[1, conferencesYears.Count + 1]];
            range2.Font.Bold = true;
            excelSheet2.Cells[1, 1] = "Конференция " + @"\" + " Год";
            int row_count2 = 1;
            for (int i = 0, cyCnt = conferencesYears.Count; i < cyCnt; i++)
                excelSheet2.Cells[1, i + 2] = conferencesYears[i];

            List<string> names = new List<string>();
            foreach (var item in conferences)
            {
                if (!names.Contains(item.name))
                {
                    excelSheet2.Cells[row_count2 + 1, 1] = item.name;
                    row_count2++;
                    names.Add(item.name);
                }

                excelSheet2.Cells[names.FindIndex(x => x == item.name) + 2,
                    conferencesYears.FindIndex(x => x == item.year) + 2].Value = item.count;
            }

            range2 = excelSheet2.Range[excelSheet2.Cells[1, 1], excelSheet2.Cells[row_count2, 1]];
            range2.Columns.ColumnWidth = 70;
            range2.WrapText = true;
            range2 = excelSheet2.Range[excelSheet2.Cells[1, 1], excelSheet2.Cells[row_count2, conferencesYears.Count + 1]];
            range2.Font.Size = 14;
            range2.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            range2.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            range2.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            range2 = excelSheet2.Range[excelSheet2.Cells[1, 2], excelSheet2.Cells[row_count2, conferencesYears.Count + 1]];
            range2.Columns.ColumnWidth = 10;

            //Распределение по кол-ву журналов и годам-------------------------------------------------------------
            Excel.Worksheet excelSheet3 = (Excel.Worksheet)excelBook.Worksheets.Add();
            excelSheet3.Name = "Журналы и года";
            Excel.Range range3 = excelSheet3.Range[excelSheet3.Cells[1, 1], excelSheet3.Cells[1, journalsYears.Count + 1]];
            range3.Font.Bold = true;
            excelSheet3.Cells[1, 1] = "Журнал " + @"\" + " Год";
            int row_count3 = 1;
            for (int i = 0, jCnt = journalsYears.Count; i < jCnt; i++)
                excelSheet3.Cells[1, i + 2] = journalsYears[i];

            List<string> jourNames = new List<string>();
            foreach (var item in journals)
            {
                if (!jourNames.Contains(item.name))
                {
                    excelSheet3.Cells[row_count3 + 1, 1] = item.name;
                    row_count3++;
                    jourNames.Add(item.name);
                }

                excelSheet3.Cells[jourNames.FindIndex(x => x == item.name) + 2,
                    journalsYears.FindIndex(x => x == item.year) + 2].Value = item.count;
            }

            range3 = excelSheet3.Range[excelSheet3.Cells[1, 1], excelSheet3.Cells[row_count3, 1]];
            range3.Columns.ColumnWidth = 70;
            range3.WrapText = true;
            range3 = excelSheet3.Range[excelSheet3.Cells[1, 1], excelSheet3.Cells[row_count3, journalsYears.Count + 1]];
            range3.Font.Size = 14;
            range3.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            range3.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            range3.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            range3.Columns.AutoFit();
            range3 = excelSheet3.Range[excelSheet3.Cells[1, 2], excelSheet3.Cells[row_count3, journalsYears.Count + 1]];
            range3.Columns.ColumnWidth = 10;

            //Распределение по ключевым словам--------------------------------------------------------------------------------
            Excel.Worksheet excelSheet4 = (Excel.Worksheet)excelBook.Worksheets.Add();
            excelSheet4.Name = "Ключевые слова";
            excelSheet4.Cells[1, 1].Font.Bold = true; excelSheet4.Cells[1, 2].Font.Bold = true;
            excelSheet4.Cells[1, 1] = "Ключевое слово"; excelSheet4.Cells[1, 2] = "Количество публикаций";
            for (int i = 0, kwCnt = keywordsCount.Count; i < kwCnt; i++)
            {
                excelSheet4.Cells[i + 2, 1] = keywordsCount[i].keyword;
                excelSheet4.Cells[i + 2, 2] = keywordsCount[i].publication_count;
            }
            Excel.Range range4 = excelSheet4.Range[excelSheet4.Cells[1, 1], excelSheet4.Cells[keywordsCount.Count + 1, 2]];
            range4.Font.Size = 14;
            range4.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            range4.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            range4.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            range4.Columns.AutoFit();

            //Распределение по годам--------------------------------------------------------------------------------------
            Excel.Worksheet excelSheet5 = (Excel.Worksheet)excelBook.Worksheets.Add();
            excelSheet5.Name = "Года издания";
            excelSheet5.Cells[1, 1].Font.Bold = true; excelSheet5.Cells[1, 2].Font.Bold = true;
            excelSheet5.Cells[1, 1] = "Год"; excelSheet5.Cells[1, 2] = "Количество публикаций";
            for (int i = 0, yCnt = yearsCount.Count; i < yCnt; i++)
            {
                excelSheet5.Cells[i + 2, 1] = yearsCount[i].year;
                excelSheet5.Cells[i + 2, 2] = yearsCount[i].publication_count;
            }
            Excel.Range range5 = excelSheet5.Range[excelSheet5.Cells[1, 1], excelSheet5.Cells[yearsCount.Count + 1, 2]];
            range5.Font.Size = 14;
            range5.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            range5.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            range5.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            range5.Columns.AutoFit();

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
