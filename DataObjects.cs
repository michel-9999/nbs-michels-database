using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace csharp_michels_database
{
    public class Database
    {
        public int SchemaVersion { get; set; } = 1;
        public ContentCategory? GetCategoryById(Guid id)
        {
            return Categories.FirstOrDefault(c => c.Id == id);
        }

        public ContentSubject? GetSubjectById(Guid id)
        {
            return Subjects.FirstOrDefault(c => c.Id == id);
        }

        public string GetCategoryName(Guid? id)
        {
            if (id == null)
                return "";

            return Categories.FirstOrDefault(c => c.Id == id.Value)?.CategoryName ?? "";
        }

        public string GetSubjectName(Guid? id)
        {
            if (id == null)
                return "";

            return Subjects.FirstOrDefault(s => s.Id == id.Value)?.SubjectName ?? "";
        }

        public List<CollectionEntry> Entries { get; set; } = [];
        public List<ContentCategory> Categories { get; set; } = [];
        public List<ContentSubject> Subjects { get; set; } = [];
    }

    public class CollectionEntry
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = "";
        public List<EntryContent> Contents { get; set; } = [];
        public string Cover { get; set; } = "";
        public string TOC { get; set; } = "";
        public string PDF { get; set; } = "";
    }

    public class EntryContent
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = "";
        public int PageStart { get; set; }
        public int PageEnd { get; set; }
        public Guid? MainCategoryId { get; set; }
        public List<Guid> SubjectIds { get; set; } = [];
        public string Comment { get; set; } = "";
    }

    public class ContentCategory
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string CategoryName { get; set; } = "";
    }

    public class ContentSubject
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string SubjectName { get; set; } = "";
    }

}
