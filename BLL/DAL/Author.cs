using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DAL
{
    public class Author
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [Required]
        [StringLength(150)]
        public string Surname { get; set; }

        public ICollection<Book> Books { get; set; } //Burada denmek istenen şu : Author'un birden çok kitabı olabilir .

    }
}
