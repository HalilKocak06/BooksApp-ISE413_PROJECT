using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class FavoritesModel
    {
        public int BookId { get; set; }

        public int UserId { get; set; }

        [DisplayName("Book Name")]
        public string BookName { get; set; }


    }
}
