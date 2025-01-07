using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DAL
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        
        public short NumberOfPages { get; set; }

        public DateTime? PublishDate { get; set; }  //nullable olabilir ...

        [Required]
        public decimal Price { get; set; }

        public bool IsTopSeller { get; set; }

        public int AuthorId { get; set; }

        public Author Author { get; set; } //navigational category.

        public List<BookGenre> BookGenre { get; set; }

    }
}
