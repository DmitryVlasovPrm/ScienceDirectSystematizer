using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Word = Microsoft.Office.Interop.Word;

namespace CourseWork
{
    public partial class MainForm : Form
    {
        #region Инициализация главного окна и глобальные переменные
        public MainForm()
        {
            InitializeComponent();
            type = 0;
            is_list = false;
            Open_file = false;
            SaveWordToolStripMenuItem.Enabled = false; SaveExcelToolStripMenuItem.Enabled = false;
            SaveBibTeXToolStripMenuItem.Enabled = false;
            DistributionToolStripMenuItem.Enabled = false; ViewToolStripMenuItem.Enabled = false;
            SearchToolStripMenuItem.Enabled = false;
            DiagrammToolStripMenuItem.Enabled = false;
            dataGridView1.RowTemplate.Height = 30;
        }

        private bool _list;
        public bool is_list
        {
            get
            {
                return _list;
            }
            set
            {
                _list = value;
                if (_list)
                {
                    SaveWordToolStripMenuItem.Enabled = true;
                    SaveBibTeXToolStripMenuItem.Enabled = true;
                }
                else
                {
                    SaveWordToolStripMenuItem.Enabled = false;
                    SaveBibTeXToolStripMenuItem.Enabled = false;
                }
            }
        }

        private bool Open_file;
        private int type;
        #endregion

        #region Списки
        //Список всех публикаций
        static List<Publication> Publications = new List<Publication>();
        //Списки для распределений
        public static List<Year_count> Years_count = new List<Year_count>();
        public static List<Keyword_count> Keywords_count = new List<Keyword_count>();
        static List<Author_year_count> Authors_years_count = new List<Author_year_count>(); static List<int> author_count = new List<int>();
        
        public static Type_count typesCount = new Type_count(); public static bool typeFlag = false;
        public static List<YearCount> journals = new List<YearCount>(); public static List<YearCount> conferences = new List<YearCount>();
        static List<string> journalsYears = new List<string>(); static List<string> conferencesYears = new List<string>();
        //Список для поиска
        static List<Publication> FilterPublications = new List<Publication>();
        #endregion

        #region Открытие файла и чтение из него
        //Выбор файла
        private void OpenFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = DialogResult.Yes;

            //Если файлы ранее открывались
            if (Open_file)
            {
                const string message = "Вы учерены, что хотите открыть новый файл?";
                const string caption = "Sciense Direct Systematizer";
                result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.YesNoCancel,
                                             MessageBoxIcon.Question);
            }

            if (result == DialogResult.Yes)
            {
                List<string> files = new List<string>(); //Список файлов, которые не удалось считать
                OpenFileDialog openfile = new OpenFileDialog();
                openfile.Multiselect = true;

                if (openfile.ShowDialog() == DialogResult.OK)
                {
                    bool first_time = true; //Чтение первого файла (без ошибок)
                    foreach (string file in openfile.FileNames)
                    {
                        if (ReadInformation(file, first_time))
                        {
                            Open_file = true;
                            first_time = false;
                        }
                        else
                            files.Add(Path.GetFileNameWithoutExtension(file));
                    }

                    //Составление списка непрочитанных файлов
                    string s = "";
                    if (files.Count > 1)
                        s = files.Aggregate((a, b) => a + ", " + b);
                    if (files.Count == 1)
                        s = files[0];

                    if (files.Count != 0)
                    {
                        MessageBox.Show("Непрочитанные файлы: " + s, "Science Direct Systematizer");
                    }
                    if (files.Count != openfile.FileNames.Length)
                    {
                        Activation();
                        MessageBox.Show("Файл(ы) прочитан(ы)", "Science Direct Systematizer");
                    }
                }
            }
        }

        //Чтение информации (739 мс)
        private bool ReadInformation(string path, bool first_time)
        {
            //Список публикаций только для текущего файла
            List<Publication> This_publications = new List<Publication>();

            try
            {
                //id в зависимости от количества файлов
                if (first_time) Clear();     
                int id = Publications.Count;

                using (StreamReader sr = new StreamReader(path, Encoding.Default))
                {
                    string line, buffer = "";
                    string tag = "", type = "", title = "", journal = "", volume = "", year = "", note = "", 
                        pages = "", isbn = "", doi = "";
                    List<string> authors = new List<string>(); List<string> keywords = new List<string>();
                    while ((line = sr.ReadLine()) != null)
                    {
                        buffer += line;
                        if (buffer == "}")
                        {
                            Publication item = new Publication(id, type, tag, title, journal, volume,
                                pages, year, note, isbn, doi, authors, keywords);

                            if (!CheckDuplicate(item))
                            {
                                This_publications.Add(item);
                                id++;
                            }

                            keywords.Clear(); authors.Clear();
                            buffer = tag = type = title = journal = volume = year = note = pages = isbn = doi = "";
                        }
                        else
                        {
                            if (line[line.Length - 1] != '"' && line[line.Length - 2] != '"' && line[0] != '@')
                                continue;

                            if (buffer[0] == '@')
                            {
                                type = buffer.Substring(1, buffer.IndexOf('{') - 1);
                                tag = buffer.Substring(buffer.IndexOf('{') + 1, buffer.LastIndexOf(',') - buffer.IndexOf('{') - 1);
                            }

                            if (buffer.Contains("title = ")) title = ParseLine(buffer);
                            if (buffer.Contains("journal = ")) journal = ParseLine(buffer);
                            if (buffer.Contains("volume = ")) volume = ParseLine(buffer);
                            if (buffer.Contains("pages = ")) pages = ParseLine(buffer);
                            if (buffer.Contains("year = ")) year = ParseLine(buffer);
                            if (buffer.Contains("note = ")) note = ParseLine(buffer);
                            if (buffer.Contains("isbn = ")) isbn = ParseLine(buffer);
                            if (buffer.Contains("doi = ")) doi = ParseLine(buffer);
                            if (buffer.Contains("author = "))
                            {
                                string[] result = ParseLine(buffer).Split(new string[] { " and " }, StringSplitOptions.RemoveEmptyEntries);
                                for (int i = 0; i < result.Length; i++)
                                {
                                    string str = result[i].TrimStart().TrimEnd();
                                    if (str != "") authors.Add(str);
                                }
                            }
                            if (buffer.Contains("keywords = "))
                            {     
                                string[] result = ParseLine(buffer).Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                                for (int i = 0; i < result.Length; i++)
                                {
                                    string str = result[i].TrimStart().TrimEnd();
                                    if (str != "") keywords.Add(str);
                                }
                            }
                            buffer = "";
                        }
                    }
                    sr.Close();
                }

                Publications.AddRange(This_publications);
            }
            //Файл не удалось считать (ошибка)
            catch
            {
                return false;
            }

            return true;
        }

        private string ParseLine(string buffer)
        {
            return buffer.Substring(buffer.IndexOf('"') + 1,
                                  buffer.LastIndexOf('"') - buffer.IndexOf('"') - 1);
        }
        #endregion

        #region Вспомогательные функции
        //Проверка на дубликаты и мусор
        public bool CheckDuplicate(Publication item)
        {
            //Если нет авторов
            if (item.authors.Count == 0)
                return true;

            //Если кол-во страниц <= 3 или такого нет
            if (item.pages == "")
                return true;
            int start = 0; int end = 0;
            if (item.pages.Contains("-"))
            {
                string[] counts = item.pages.Split(new char[] { '-' });
                try
                {
                    start = int.Parse(counts[0]);
                    end = int.Parse(counts[1]);
                    if (end - start <= 3)
                        return true;
                }
                catch
                {
                    //MessageBox.Show(counts[0].ToString() + "\n" + counts[1].ToString());
                }
            }
            
            //Если есть дубликат по названию и авторам
            var authors = new HashSet<string>(item.authors);
            var element = Publications.Find(a => a.title == item.title);
            if (element != null)
                if (authors.SetEquals(element.authors))
                    return true;

            return false;
        }

        //Очищаем списки
        public void Clear()
        {
            Clear_table();
            Publications.Clear();
            Years_count.Clear();
            Keywords_count.Clear();
            Authors_years_count.Clear(); author_count.Clear();
            typesCount = new Type_count(); typeFlag = false;
            FilterPublications.Clear();
        }

        //Активируем кнопки
        private void Activation()
        {
            SaveExcelToolStripMenuItem.Enabled = true;
            DistributionToolStripMenuItem.Enabled = true; ViewToolStripMenuItem.Enabled = true;
            SearchToolStripMenuItem.Enabled = true;
        }

        //При закрытии формы
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            const string message = "Вы учерены, что хотите выйти из программы?";
            const string caption = "Sciense Direct Systematizer";
            DialogResult result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNoCancel,
                                         MessageBoxIcon.Question);

            if (result == DialogResult.No || result == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        //Очистка таблицы
        private void Clear_table()
        {
            int count = dataGridView1.Columns.Count;
            for (int i = 0; i < count; i++)
            {
                dataGridView1.Columns.RemoveAt(0);
            }
            type = 0;
            DiagrammToolStripMenuItem.Enabled = false;
            is_list = false;
        }
        private void ClearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clear_table();
        }
        #endregion

        #region Создание распределений
        //Распределение по годам
        public static void Create_years_distrib()
        {
            foreach (var item in Publications)
            {
                if (item.year == "") continue;
                var element = Years_count.Find(a => a.year == item.year);
                if (element != null)
                {
                    element.publication_count++;
                }
                else
                {
                    Year_count it = new Year_count(item.year);
                    Years_count.Add(it);
                }
            }
            Years_count.Sort((a, b) => b.year.CompareTo(a.year));
        }

        //Распределение по ключевым словам
        public static void Create_keywords_distrib()
        {
            foreach (var item in Publications)
            {
                foreach (var word in item.keywords)
                {
                    var element = Keywords_count.Find(a => a.keyword.ToLower() == word.ToLower());
                    if (element != null)
                    {
                        element.publication_count++;
                    }
                    else
                    {
                        Keyword_count it = new Keyword_count(word);
                        Keywords_count.Add(it);
                    }
                }
            }
            Keywords_count.Sort((a, b) => b.publication_count.CompareTo(a.publication_count));
        }

        private void YearsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clear_table();
            if (Years_count.Count > 0)
                ShowTable1And2(1);
            else
            {
                Create_years_distrib();
                ShowTable1And2(1);
            }
        }

        private void KeywordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clear_table();
            if (Keywords_count.Count > 0)
                ShowTable1And2(2);
            else
            {
                Create_keywords_distrib();
                ShowTable1And2(2);
            }
        }

        //По количественному составу авторского коллектива и годам
        public static void Create_authors_years_distrib()
        {
            foreach (var item in Publications)
            {
                if (item.year == "" || item.authors.Count == 0) continue;
                var element = Authors_years_count.Find(a => a.year == item.year && a.author_count == item.authors.Count);
                if (element != null)
                {
                    element.publication_count++;
                }
                else
                {
                    Author_year_count it = new Author_year_count(item.authors.Count, item.year);
                    Authors_years_count.Add(it);
                    if (!author_count.Contains(item.authors.Count)) author_count.Add(item.authors.Count);
                }
            }
            author_count.Sort((a, b) => b.CompareTo(a));
            Authors_years_count.Sort((a, b) => b.year.CompareTo(a.year));
        }
        private void AuthorsYearsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clear_table();
            if (Authors_years_count.Count > 0)
                ShowTable3();
            else
            {
                Create_authors_years_distrib();
                ShowTable3();
            }
        }

        //Распределние по типу публикаций
        public static void Create_type_publications()
        {
            foreach (var item in Publications)
            {
                if (item.isbn != "")
                {
                    typesCount.book_count++;
                    continue;
                }

                if (item.note.ToLower().Contains("conference") || item.note.ToLower().Contains("symposium") ||
                        item.note.ToLower().Contains("issue") || item.note.ToLower().Contains("celebrating") ||
                            item.journal.ToLower().Contains("procedia"))
                {
                    typesCount.conference_count++;

                    var elem = conferences.Find(a => a.name == item.note);
                    YearCount newElem = new YearCount(item.note, item.year);
                    if (elem != null)
                        elem.count++;
                    else
                        conferences.Add(newElem);
                    if (!conferencesYears.Contains(item.year))
                        conferencesYears.Add(item.year);
                    
                    continue;
                }

                if (item.journal != "" && item.volume != "")
                {
                    typesCount.journal_count++;

                    var elem = journals.Find(a => a.name == item.journal);
                    YearCount newElem = new YearCount(item.journal, item.year);
                    if (elem != null)
                        elem.count++;
                    else
                        journals.Add(newElem);
                    if (!journalsYears.Contains(item.year))
                       journalsYears.Add(item.year);

                    continue;
                }
            }
            conferencesYears.Sort((a, b) => b.CompareTo(a));
            journalsYears.Sort((a, b) => b.CompareTo(a));
            journals.Sort((a, b) => b.count.CompareTo(a.count));
            conferences.Sort((a, b) => b.count.CompareTo(a.count));
            typeFlag = true;
        }

        private void TypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clear_table();
            if (typeFlag)
                ShowTable4();
            else
            {
                Create_type_publications();
                ShowTable4();
            }
        }

        //Распределение по количеству журналов и годам 
        private void JournalYearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clear_table();
            if (typeFlag)
                ShowTable5();
            else
            {
                Create_type_publications();
                ShowTable5();
            }
        }

        //Распределение по количеству конференций и годам
        private void ConferenceYearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clear_table();
            if (typeFlag)
                ShowTable6();
            else
            {
                Create_type_publications();
                ShowTable6();
            }
        }
        #endregion

        #region Вывод распределений
        //Вывод таблицы для распределения по годам или по ключевым словам
        private void ShowTable1And2(int variant)
        {
            string header;
            if (variant == 1)
                header = "Год";
            else
                header = "Ключевое слово (" + Keywords_count.Count.ToString() + ")";

            for (int i = 0; i < 2; i++)
            {
                var column = new DataGridViewColumn();
                column.HeaderCell.Style.Font = new System.Drawing.Font(dataGridView1.DefaultCellStyle.Font, FontStyle.Bold);
                if (i == 0) column.HeaderText = header;
                else column.HeaderText = "Количество публикаций";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                column.ReadOnly = true;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                column.CellTemplate = new DataGridViewTextBoxCell();
                dataGridView1.Columns.Add(column);
            }
            dataGridView1.AllowUserToAddRows = false;

            if (variant == 1)
                foreach (var item in Years_count)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1[0, dataGridView1.Rows.Count - 1].Value = item.year;
                    dataGridView1[1, dataGridView1.Rows.Count - 1].Value = item.publication_count;
                }
            else
                foreach (var item in Keywords_count)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1[0, dataGridView1.Rows.Count - 1].Value = item.keyword.Substring(0, 1).ToUpper() +
                                                                                            item.keyword.Remove(0, 1);
                    dataGridView1[1, dataGridView1.Rows.Count - 1].Value = item.publication_count;
                }

            type = variant;
            DiagrammToolStripMenuItem.Enabled = true;
        }

        //Вывод таблицы для распределения по количественному составу авторского коллектива и годам
        private void ShowTable3()
        {
            var column0 = new DataGridViewColumn();
            column0.HeaderCell.Style.Font = new System.Drawing.Font(dataGridView1.DefaultCellStyle.Font, FontStyle.Bold);
            column0.HeaderText = "Год " + @"\" + " Кол-во авторов";
            column0.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            column0.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            column0.ReadOnly = true;
            column0.Width = 230;
            column0.CellTemplate = new DataGridViewTextBoxCell();
            dataGridView1.Columns.Add(column0);

            for (int i = 0, aCnt = author_count.Count; i < aCnt; i++)
            {
                var current_column = new DataGridViewColumn();
                current_column.HeaderCell.Style.Font = new System.Drawing.Font(dataGridView1.DefaultCellStyle.Font, FontStyle.Bold);
                current_column.HeaderText = author_count[i].ToString();
                current_column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                current_column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                current_column.ReadOnly = true;
                current_column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                current_column.CellTemplate = new DataGridViewTextBoxCell();
                dataGridView1.Columns.Add(current_column);
            }
            dataGridView1.AllowUserToAddRows = false;

            foreach (var item in Authors_years_count)
            {
                if (dataGridView1.Rows.Count == 0 ||
                    dataGridView1[0, dataGridView1.Rows.Count - 1].Value.ToString() != item.year)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1[0, dataGridView1.Rows.Count - 1].Value = item.year;
                }
                dataGridView1[author_count.FindIndex(x => x == item.author_count) + 1, dataGridView1.Rows.Count - 1].Value
                    = item.publication_count;
            }

            type = 3;
            DiagrammToolStripMenuItem.Enabled = false;
        }

        private void ShowTable4()
        {
            for (int i = 0; i < 2; i++)
            {
                var column = new DataGridViewColumn();
                column.HeaderCell.Style.Font = new System.Drawing.Font(dataGridView1.DefaultCellStyle.Font, FontStyle.Bold);
                if (i == 0) column.HeaderText = "Тип публикаций";
                else column.HeaderText = "Количество публикаций";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                column.ReadOnly = true;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                column.CellTemplate = new DataGridViewTextBoxCell();
                dataGridView1.Columns.Add(column);
            }
            dataGridView1.AllowUserToAddRows = false;

            for (int i = 0; i < 3; i++)
            {
                dataGridView1.Rows.Add();

                string name = ""; int count = 0;
                switch (i)
                {
                    case 0:
                        name = "Конференции";
                        count = typesCount.conference_count;
                        break;
                    case 1:
                        name = "Журналы";
                        count = typesCount.journal_count;
                        break;
                    case 2:
                        name = "Книги";
                        count = typesCount.book_count;
                        break;
                }

                dataGridView1[0, i].Value = name;
                dataGridView1[1, i].Value = count;
            }

            type = 4;
            DiagrammToolStripMenuItem.Enabled = true;
        }

        private void ShowTable5()
        {

        }

        private void ShowTable6()
        {
            var column0 = new DataGridViewColumn();
            column0.HeaderCell.Style.Font = new System.Drawing.Font(dataGridView1.DefaultCellStyle.Font, FontStyle.Bold);
            column0.HeaderText = "Конференция " + @"\" + " Год";
            column0.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            column0.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            column0.ReadOnly = true;
            column0.Width = 350;
            column0.CellTemplate = new DataGridViewTextBoxCell();
            dataGridView1.Columns.Add(column0);

            for (int i = 0, cnt = conferencesYears.Count; i < cnt; i++)
            {
                var current_column = new DataGridViewColumn();
                current_column.HeaderCell.Style.Font = new System.Drawing.Font(dataGridView1.DefaultCellStyle.Font, FontStyle.Bold);
                current_column.HeaderText = conferencesYears[i].ToString();
                current_column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                current_column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                current_column.ReadOnly = true;
                current_column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                current_column.CellTemplate = new DataGridViewTextBoxCell();
                dataGridView1.Columns.Add(current_column);
            }
            dataGridView1.AllowUserToAddRows = false;

            foreach (var item in conferences)
            {
                if (dataGridView1.Rows.Count == 0 ||
                    dataGridView1[0, dataGridView1.Rows.Count - 1].Value.ToString() != item.name)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1[0, dataGridView1.Rows.Count - 1].Value = item.name;
                }
                
                dataGridView1[conferencesYears.FindIndex(x => x == item.year) + 1, dataGridView1.Rows.Count - 1].Value
                    = item.count;
            }
            
            type = 6;
            DiagrammToolStripMenuItem.Enabled = false;
        }
        #endregion

        #region Вывод всех публикаций и поиск + вывод всей информации о выбранной публикации
        private void ShowAllPublicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clear_table();
            var column0 = new DataGridViewColumn();
            column0.Visible = false;
            column0.CellTemplate = new DataGridViewTextBoxCell();
            var column1 = new DataGridViewColumn();
            column1.HeaderCell.Style.Font = new System.Drawing.Font(dataGridView1.DefaultCellStyle.Font, FontStyle.Bold);
            column1.HeaderText = "Публикации (" + Publications.Count.ToString() + ")";
            column1.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            column1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            column1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            column1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            column1.ReadOnly = true;
            column1.CellTemplate = new DataGridViewTextBoxCell();

            dataGridView1.Columns.Add(column0);
            dataGridView1.Columns.Add(column1);
            dataGridView1.AllowUserToAddRows = false;

            foreach (var item in Publications)
            {
                dataGridView1.Rows.Add();
                dataGridView1[0, dataGridView1.Rows.Count - 1].Value = item.id;
                dataGridView1[1, dataGridView1.Rows.Count - 1].Value = item.title;
            }

            FilterPublications = Publications;
            is_list = true;
        }

        //Показ информации о публикации
        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.Columns[1].HeaderText.ToString() == "Публикации (" + Publications.Count.ToString() + ")")
            {
                int id = int.Parse(dataGridView1[0, e.RowIndex].Value.ToString());
                var element = Publications.Find(a => a.id == id);

                string str = Functions.GetInformation(element);
                MessageBox.Show(str, "Information");
            }
        }
        #endregion

        #region Сохранение информации
        //Сохранение распределений в Excel
        private async void SaveExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fileName = Excel_Distrib.fileOpen();
            if (fileName != String.Empty)
            {
                Cur_status.Text = "Cохранение...";
                await Task.Run(() => Excel_Distrib.Creating_Excel_Distributions(fileName, Years_count, Keywords_count,
                    Authors_years_count, author_count, typesCount));
                Cur_status.Text = "";
            }
            else
                return;
        }

        //Сохранение списка в Word
        private async void SaveWordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fileName = Word_List.fileOpen();
            if (fileName != String.Empty)
            {
                Cur_status.Text = "Cохранение...";
                await Task.Run(() => Word_List.Creating_Word_Lists(fileName, FilterPublications));
                Cur_status.Text = "";
            }
            else return;
        }

        //Сохранение списка в BibTeX
        private void SaveBibTeXToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Создание диаграммы
        //Создание диаграммы
        private void DiagrammToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Diagramms diagr = new Diagramms(type);
            diagr.Show();
        }
        #endregion
    }
}
