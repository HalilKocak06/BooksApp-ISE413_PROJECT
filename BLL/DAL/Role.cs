using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DAL
{
    public class Role
    {

        public int Id { get; set; }


        [Required, StringLength(5)] // Admin, User

        public string Name { get; set; }

        public List<User> Users { get; set; } = new List<User>(); // Role sahip BİRDEN ÇOK kullanıcılar
    }
}
