﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork
{
    //Каждая статья
    public class Publication
    {
        public int id;
        public string type;
        public string tag;
        public string title;
        public string journal;
        public string volume;
        public string pages;
        public string year;
        public string note;
        public string isbn;
        public string doi;
        public List<string> authors = new List<string>();
        public List<string> keywords = new List<string>();
        public Publication(int id, string type, string tag, string title, string journal, string volume,
            string pages, string year, string note, string isbn, string doi, List<string> authors, List<string> keywords)
        {
            this.id = id;
            this.type = type;
            this.tag = tag;
            this.title = title;
            this.journal = journal;
            this.volume = volume;
            this.pages = pages;
            this.year = year;
            this.note = note;
            this.isbn = isbn;
            this.doi = doi;
            this.authors.AddRange(authors.ToArray());
            this.keywords.AddRange(keywords.ToArray());
        }
    }

    //Для распределения по годам
    public class YearCount
    {
        public string year;
        public int publication_count;
        public YearCount(string year)
        {
            this.year = year;
            publication_count = 1;
        }
    }

    //Для распределения по ключевым словам
    public class KeywordCount
    {
        public string keyword;
        public int publication_count;
        public KeywordCount(string keyword)
        {
            this.keyword = keyword;
            publication_count = 1;
        }
    }

    //Для распределения по количественному составу авторского коллектива и годам
    public class AuthorYearCount
    {
        public int author_count;
        public string year;
        public int publication_count;
        public AuthorYearCount(int author_count, string year)
        {
            this.author_count = author_count;
            this.year = year;
            publication_count = 1;
        }
    }

    //Для распределения по типам
    public class TypeCount
    {
        public int journal_count;
        public int conference_count;
        public int book_count;
        //public int other_count;

        public TypeCount()
        {
            this.journal_count = 0;
            this.conference_count = 0;
            this.book_count = 0;
            //this.other_count = 0;
        }

        public void Clear()
        {
            this.journal_count = 0;
            this.conference_count = 0;
            this.book_count = 0;
            //this.other_count = 0;
        }
    }

    //Для распределения по кол-ву журналов (конференциий) и годам
    public class YearCountType
    {
        public string name;
        public string year;
        public int count;
        public YearCountType(string name, string year)
        {
            this.name = name;
            this.year = year;
            this.count = 1;
        }
    }
}
