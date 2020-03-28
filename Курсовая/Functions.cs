using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork
{
    class Functions
    {
        //Генерация строки с информацией об одной публикации
        public static string GetInformation(Publication item)
        {
            string str = "";

            if (item.type != "") str += "type = \"" + item.type + "\"\n";
            if (item.tag != "") str += "tag = \"" + item.tag + "\"\n";
            if (item.title != "") str += "title = \"" + item.title + "\"\n";
            if (item.editor != "") str += "editor = \"" + item.editor + "\"\n";
            if (item.booktitle != "") str += "booktitle = \"" + item.booktitle + "\"\n";
            if (item.publisher != "") str += "publisher = \"" + item.publisher + "\"\n";
            if (item.journal != "") str += "journal = \"" + item.journal + "\"\n";
            if (item.volume != "") str += "volume = \"" + item.volume + "\"\n";
            if (item.pages != "")  str += "pages = \"" + item.pages + "\"\n";
            if (item.year != "") str += "year = \"" + item.year + "\"\n";
            if (item.note != "") str += "note = \"" + item.note + "\"\n";
            if (item.isbn != "") str += "isbn = \"" + item.isbn + "\"\n";
            if (item.doi != "") str += "doi = \"" + item.doi + "\"\n";

            string authors = "";
            for (int i = 0, authCnt = item.authors.Count; i < authCnt; i++)
            {
                if (i != authCnt - 1)
                    authors += item.authors[i] + " and ";
                else
                    authors += item.authors[i];
            }
            if (authors != "")
                str += "author = \"" + authors + "\"\n";

            string keywords = "";
            for (int i = 0, kwrdCnt = item.keywords.Count; i < kwrdCnt; i++)
            {
                if (i != kwrdCnt - 1)
                    keywords += item.keywords[i] + ", ";
                else
                    keywords += item.keywords[i];
            }
            if (keywords != "")
                str += "keywords = \"" + keywords + "\"";

            return str;
        }

        public static string CreateMetadata(Publication item)
        {
            string str = "";

            str += "@" + item.type + "{" + item.tag + ",\n";
            if (item.title != "") str += "title = \"" + item.title + "\",\n";
            if (item.editor != "") str += "editor = \"" + item.editor + "\",\n";
            if (item.booktitle != "") str += "booktitle = \"" + item.booktitle + "\",\n";
            if (item.publisher != "") str += "publisher = \"" + item.publisher + "\",\n";
            if (item.journal != "") str += "journal = \"" + item.journal + "\",\n";
            if (item.volume != "") str += "volume = \"" + item.volume + "\",\n";
            if (item.pages != "") str += "pages = \"" + item.pages + "\",\n";
            if (item.year != "") str += "year = \"" + item.year + "\",\n";
            if (item.note != "") str += "note = \"" + item.note + "\",\n";
            if (item.isbn != "") str += "isbn = \"" + item.isbn + "\",\n";
            if (item.doi != "") str += "doi = \"" + item.doi + "\",\n";

            string authors = "";
            for (int i = 0, authCnt = item.authors.Count; i < authCnt; i++)
            {
                if (i != authCnt - 1)
                    authors += item.authors[i] + " and ";
                else
                    authors += item.authors[i];
            }
            if (authors != "")
                str += "author = \"" + authors + "\",\n";

            string keywords = "";
            for (int i = 0, kwrdCnt = item.keywords.Count; i < kwrdCnt; i++)
            {
                if (i != kwrdCnt - 1)
                    keywords += item.keywords[i] + ", ";
                else
                    keywords += item.keywords[i];
            }
            if (keywords != "")
                str += "keywords = \"" + keywords + "\"\n";

            str += "}";

            return str;
        }
    }
}
