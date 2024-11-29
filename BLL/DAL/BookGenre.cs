using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DAL
{
    public class BookGenre
    {
        public int Id { get; set; }


        [Required]
        [StringLength(150)]
        public int BookId { get; set; }

        public int GenreId { get; set; }

        public Book Book { get; set; }

        public Genre Genre { get; set; }


    }
}
