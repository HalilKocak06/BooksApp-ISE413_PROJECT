using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Identity;
using BLL.DAL;
using BLL.Services.Bases;

namespace BLL.Models
{
    public class BookModel
    {
        public Book Record { get; set; }

        [DisplayName("Book Name")]
        public string Name => Record.Name;

        [DisplayName("Number of Pages")]
        public short NumberOfPages => Record.NumberOfPages;

        [DisplayName("Publish Date")]
        public String PublishDate => Record.PublishDate?.ToString("MM/dd/yyyy") ?? "Not Published";

        [DisplayName("Price")]
        public String Price => $"{Record.Price:C}";

        [DisplayName("Top Seller")]
        public string IsTopSeller => Record.IsTopSeller ? "Yes" : "No";

        [DisplayName("Author ID")]
        public int AuthorId => Record.AuthorId;

        [DisplayName("Author Name")]
        public string Author => Record.Author?.Name ?? "Unknown";

        [DisplayName("Author Name and Surname")]
        public string AuthorNameAndSurname => $"{Record.Author?.Name} {Record.Author?.Surname}".Trim(); // Name+Surname

        public string Genre => string.Join("<br>", Record.BookGenre?.Select(bg => bg.Genre?.Name + " " )); // + bg.Genre?.Surname, Bende Surname yok hocaya sor.
        [DisplayName("Genres")]
        public List<int> GenreIds
        {
            get => Record.BookGenre?.Select(bg => bg.GenreId).ToList();
            set => Record.BookGenre = value.Select(v => new BookGenre() { GenreId = v }).ToList();  //Bu ikisi edit için işimize lazım.
        }
    }

}
