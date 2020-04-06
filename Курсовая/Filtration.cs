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
    public partial class Filtration : Form
    {
        private List<Publication> answer = new List<Publication>();

        public Filtration()
        {
            InitializeComponent();
        }

        private void sortCrit_SelectedValueChanged(object sender, EventArgs e)
        {
            sortValue.Text = sortValue.Items[0].ToString();
        }

        //Фильтрация
        private void search_Click(object sender, EventArgs e)
        {
            MainForm.filterPublications = MainForm.publications.GetRange(0, MainForm.publications.Count);

            string stYear = startYear.Text;
            string enYear = endYear.Text;
            string journal = findJournal.Text;
            string title = findTitle.Text;
            string type = typeValue.Text;
            string authors = findAuthors.Text;
            string keywords = findKeywords.Text;
            string sCrit = sortCrit.Text;
            int direction = sortValue.SelectedIndex;

            if (type != "")
            {
                if (!MainForm.typeFlag) MainForm.Create_type_publications();
                if (type == "Книга") MainForm.filterPublications = MainForm.typesCount.bookPubl.GetRange(0, MainForm.typesCount.bookPubl.Count);
                if (type == "Журнал") MainForm.filterPublications = MainForm.typesCount.journalPubl.GetRange(0, MainForm.typesCount.journalPubl.Count);
                if (type == "Конференция") MainForm.filterPublications = MainForm.typesCount.conferencePubl.GetRange(0, MainForm.typesCount.conferencePubl.Count);
            }
            if (stYear != "" || enYear != "")
            {
                if (stYear == "") stYear = "0";
                if (enYear == "") enYear = DateTime.Now.Year.ToString();

                while (true)
                {
                    var element = MainForm.filterPublications.Find(a => int.Parse(a.year) >= int.Parse(stYear) &&
                                                                                   int.Parse(a.year) <= int.Parse(enYear));
                    if (!AddElement(element)) break;
                    else MainForm.filterPublications.Remove(element);
                }
            }
            if (journal != "")
            {
                while (true)
                {
                    var element = MainForm.filterPublications.Find(a => a.journal.ToLower().Contains(journal.ToLower()));
                    if (!AddElement(element)) break;
                    else MainForm.filterPublications.Remove(element);
                }
            }
            if (title != "")
            {
                while (true)
                {
                    var element = MainForm.filterPublications.Find(a => a.title.ToLower().Contains(title.ToLower()));
                    if (!AddElement(element)) break;
                    else MainForm.filterPublications.Remove(element);
                }
            }
            if (authors != "")
            {
                string[] strResult = authors.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                while (true)
                {
                    var element = MainForm.filterPublications.Find(a => CheckAuthors(a.authors, strResult));
                    if (!AddElement(element)) break;
                    else MainForm.filterPublications.Remove(element);
                }
            }
            if (keywords != "")
            {
                string[] strResult = keywords.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                while (true)
                {
                    var element = MainForm.filterPublications.Find(a => CheckKeywords(a.keywords, strResult));
                    if (!AddElement(element)) break;
                    else MainForm.filterPublications.Remove(element);
                }
            }

            //Часть отвечающая за сортировку
            if (sCrit != "")
            {
                if (direction == 0)
                {
                    if (sCrit == "Год") MainForm.filterPublications.Sort((a, b) => a.year.CompareTo(b.year));
                    if (sCrit == "Журнал") MainForm.filterPublications.Sort((a, b) => a.journal.CompareTo(b.journal));
                    if (sCrit == "Заголовок") MainForm.filterPublications.Sort((a, b) => a.title.CompareTo(b.title));
                    if (sCrit == "Количество авторов") MainForm.filterPublications.Sort((a, b) => a.authors.Count.CompareTo(b.authors.Count));
                    if (sCrit == "Количество ключевых слов") MainForm.filterPublications.Sort((a, b) => a.keywords.Count.CompareTo(b.keywords.Count));
                }
                else
                {
                    if (sCrit == "Год") MainForm.filterPublications.Sort((a, b) => b.year.CompareTo(a.year));
                    if (sCrit == "Журнал") MainForm.filterPublications.Sort((a, b) => b.journal.CompareTo(a.journal));
                    if (sCrit == "Заголовок") MainForm.filterPublications.Sort((a, b) => b.title.CompareTo(a.title));
                    if (sCrit == "Количество авторов") MainForm.filterPublications.Sort((a, b) => b.authors.Count.CompareTo(a.authors.Count));
                    if (sCrit == "Количество ключевых слов") MainForm.filterPublications.Sort((a, b) => b.keywords.Count.CompareTo(a.keywords.Count));
                }
            }

            this.DialogResult = DialogResult.OK;
        }

        //Добавление элемента
        private bool AddElement(Publication element)
        {
            if (element != null)
            {
                answer.Add(element);
                return true;
            }
            else
            {
                MainForm.filterPublications = answer.GetRange(0, answer.Count);
                return false;
            }
        }

        //Проверка на авторов
        private bool CheckAuthors(List<string> pubAuthors, string[] findAuthors)
        {
            pubAuthors = pubAuthors.ConvertAll(a => a.ToLower());
            for (int i = 0; i < findAuthors.Length; i++)
            {
                string curAuthor = findAuthors[i].TrimStart().TrimEnd().ToLower();
                if (!pubAuthors.Exists(a =>
                    a.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList().ConvertAll(b => b.ToLower()).Contains(curAuthor)))
                        return false;
            }
            return true;
        }

        //Проверка на ключевые слова
        private bool CheckKeywords(List<string> pubKeywords, string[] findKeywords)
        {
            pubKeywords = pubKeywords.ConvertAll(a => a.ToLower());
            for (int i = 0; i < findKeywords.Length; i++)
            {
                string curKeyword = findKeywords[i].TrimStart().TrimEnd().ToLower();
                if (!pubKeywords.Exists(a =>
                    a.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList().ConvertAll(b => b.ToLower()).Contains(curKeyword)))
                    return false;
            }
            return true;
        }

        //Отмена
        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
