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
        public string IsTopSellerDisplay => Record.IsTopSeller ? "Yes" : "No";

        [DisplayName("Author ID")]
        public int AuthorId => Record.AuthorId;

        [DisplayName("Author Name")]
        public string AuthorName => Record.Author?.Name ?? "Unknown";

    }

}
