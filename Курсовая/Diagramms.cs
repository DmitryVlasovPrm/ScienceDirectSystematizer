using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork
{
    public partial class Diagramms : Form
    {
        #region Инициализация окна для диаграммы и глобальные переменные
        private int curType;
        public Diagramms(int curType)
        {
            this.curType = curType;
            InitializeComponent();
            CreateBarChart(curType);
        }
        #endregion

        #region Столбчатая диаграмма
        private void ColumnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkBox1.Visible = true;
            checkBox1.Checked = false;
            CreateBarChart(curType);
        }
        private void CreateBarChart(int curType)
        {
            Diagramm.Titles.Clear();
            Diagramm.Series.Clear();
            Diagramm.ChartAreas.Clear();
            ColumnToolStripMenuItem.Enabled = false;
            CircleToolStripMenuItem.Enabled = true;

            Diagramm.Series.Add("first");
            Diagramm.ChartAreas.Add("first");
            Diagramm.Series["first"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            Diagramm.Series["first"].IsVisibleInLegend = false;

            Diagramm.Series["first"].Font = Diagramm.ChartAreas["first"].AxisY.TitleFont = Diagramm.ChartAreas["first"].AxisX.TitleFont =
            Diagramm.ChartAreas["first"].AxisX.LabelStyle.Font = Diagramm.ChartAreas["first"].AxisY.LabelStyle.Font = new Font("Calibri", 12f);
            Diagramm.ChartAreas["first"].AxisX.Interval = 1;

            Diagramm.Series["first"].IsValueShownAsLabel = true;

            //Выбор распределения
            switch (curType)
            {
                case 1:
                    Diagramm.Titles.Add("Распределение публикаций по годам").Font = new Font("Calibri", 16f);
                    Diagramm.ChartAreas["first"].AxisY.Title = "Кол-во публикаций";

                    for (int i = 0, yCnt = MainForm.yearsCount.Count; i < yCnt; i++)
                        Diagramm.Series["first"].Points.AddXY(MainForm.yearsCount[i].year,
                            MainForm.yearsCount[i].publication_count);

                    if (Diagramm.Series["first"].Points.Count > 20)
                    {
                        Diagramm.ChartAreas["first"].AxisX.ScaleView.Position = 0;
                        Diagramm.ChartAreas["first"].AxisX.ScaleView.Size = 10;
                    }
                    break;

                case 2:
                    Diagramm.Titles.Add("Распределение публикаций по ключевым словам\n(" +
                        "показаны первые 100 значений)").Font = new Font("Calibri", 16f);
                    Diagramm.ChartAreas["first"].AxisY.Title = "Кол-во публикаций";

                    for (int i = 0, kwCnt = MainForm.keywordsCount.Count; i < kwCnt && i < 100; i++)
                        Diagramm.Series["first"].Points.AddXY(MainForm.keywordsCount[i].keyword,
                            MainForm.keywordsCount[i].publication_count);

                    if (Diagramm.Series["first"].Points.Count > 20)
                    {
                        Diagramm.ChartAreas["first"].AxisX.ScaleView.Position = 0;
                        Diagramm.ChartAreas["first"].AxisX.ScaleView.Size = 10;
                    }

                    break;

                case 4:
                    Diagramm.Titles.Add("Распределение публикаций по типу публикации").Font = new Font("Calibri", 16f);
                    Diagramm.ChartAreas["first"].AxisY.Title = "Кол-во публикаций";

                    Diagramm.Series["first"].Points.AddXY("Книги", MainForm.typesCount.bookPubl.Count);
                    Diagramm.Series["first"].Points.AddXY("Журналы", MainForm.typesCount.journalPubl.Count);
                    Diagramm.Series["first"].Points.AddXY("Конференции", MainForm.typesCount.conferencePubl.Count);

                    break;
            }
        }
        #endregion

        #region Круговая диаграмма
        private void CircleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkBox1.Visible = false;
            CreatePieChart(curType);
        }

        private void CreatePieChart(int curType)
        {
            Diagramm.Titles.Clear();
            Diagramm.Series.Clear();
            Diagramm.ChartAreas.Clear();
            ColumnToolStripMenuItem.Enabled = true;
            CircleToolStripMenuItem.Enabled = false;

            Diagramm.Series.Add("first");
            Diagramm.ChartAreas.Add("first");
            Diagramm.Series["first"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            Diagramm.Series["first"].IsVisibleInLegend = false;

            Diagramm.Series["first"].Font = new Font("Calibri", 12f);

            //Выбор распределения
            switch (curType)
            {
                case 1:
                    Diagramm.Titles.Add("Распределение публикаций по годам").Font = new Font("Calibri", 16f);

                    for (int i = 0, yCnt = MainForm.yearsCount.Count; i < yCnt; i++)
                        Diagramm.Series["first"].Points.AddXY(MainForm.yearsCount[i].year,
                            MainForm.yearsCount[i].publication_count);

                    
                    double total1 = Diagramm.Series["first"].Points.Sum(item => item.YValues[0]);
                    foreach (var point in Diagramm.Series["first"].Points)
                        point.Label = point.AxisLabel + " " + (point.YValues[0] / total1 * 100).ToString("0.0") + "% (" + point.YValues[0] + ")";

                    Diagramm.Series["first"]["PieLabelStyle"] = "Outside";
                    Diagramm.Series["first"]["PieLineColor"] = "Black";
                    Diagramm.ChartAreas["first"].Area3DStyle.Enable3D = true;
                    Diagramm.ChartAreas["first"].Area3DStyle.Inclination = 10;

                    break;
                
                case 2:
                    Diagramm.Titles.Add("Распределение публикаций по ключевым словам\n(" +
                        "показаны первые 30 значений)").Font = new Font("Calibri", 16f);

                    for (int i = 0, kwCnt = MainForm.keywordsCount.Count; i < kwCnt && i < 30; i++)
                        Diagramm.Series[0].Points.AddXY(MainForm.keywordsCount[i].keyword,
                            MainForm.keywordsCount[i].publication_count);

                    double total2 = Diagramm.Series["first"].Points.Sum(item => item.YValues[0]);
                    foreach (var point in Diagramm.Series["first"].Points)
                        point.Label = point.AxisLabel + " " + (point.YValues[0] / total2 * 100).ToString("0.0") + "% (" + point.YValues[0] + ")";

                    Diagramm.Series["first"]["PieLabelStyle"] = "Outside";
                    Diagramm.Series["first"]["PieLineColor"] = "Black";
                    Diagramm.ChartAreas["first"].Area3DStyle.Enable3D = true;
                    Diagramm.ChartAreas["first"].Area3DStyle.Inclination = 10;

                    break;
                    
                case 4:
                    Diagramm.Titles.Add("Распределение публикаций по типу публикации").Font = new Font("Calibri", 16f);

                    Diagramm.Series["first"].Points.AddXY("Книги", MainForm.typesCount.bookPubl.Count);
                    Diagramm.Series["first"].Points.AddXY("Журналы", MainForm.typesCount.journalPubl.Count);
                    Diagramm.Series["first"].Points.AddXY("Конференции", MainForm.typesCount.conferencePubl.Count);

                    double total4 = Diagramm.Series["first"].Points.Sum(item => item.YValues[0]);
                    foreach (var point in Diagramm.Series["first"].Points)
                        point.Label = point.AxisLabel + " " + (point.YValues[0] / total4 * 100).ToString("0.0") + "% (" + point.YValues[0] + ")";

                    break;
            }
        }
        #endregion

        #region Закрытие формы, сохранение в png, поворот надписей
        //Закрытие формы
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Сохранение png
        public string fileOpen()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "png files (*.png)|*.png|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.FileName = "Diagramm";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                return saveFileDialog.FileName;
            else
                return String.Empty;
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fileName = fileOpen();
            if (fileName != String.Empty)
                try
                {
                    Diagramm.SaveImage(fileName, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Png);
                    MessageBox.Show("Сохранение прошло успешно", "Science Direct Systematizer");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при сохранении.\nФайл не сохранен.\n" + ex.Message.ToString(),
                        "Science Direct Systematizer");
                }
            else
                return;
        }

        //Поворот надписей
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
                Diagramm.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
            else
                Diagramm.ChartAreas[0].AxisX.LabelStyle.Angle = 0;
        }
        #endregion
    }
}
