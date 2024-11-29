using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DAL;

namespace BLL.Models
{
    public class AuthorModel
    {
        public Author Record { get; set; }

        public string Name => Record.Name;

        public string Surname => Record.Surname;

        [DisplayName("Full Name")]
        public string FullName => $"{Record.Name} {Record.Surname}".Trim();

        [DisplayName("Number of Books")]
        public int NumberOfBooks => Record.Books?.Count ?? 0; // Yazarın kitaplar listesinin uzunluğunu alır ve yazarın kaç kitabı olduğunu gösterir
        public ICollection<string> BookTitles => Record.Books?.Select(b => b.Name).ToList() ?? new List<string>(); //Her kitabı liste halinde döndürür ve isimlerini listeye alır.
    }
}
