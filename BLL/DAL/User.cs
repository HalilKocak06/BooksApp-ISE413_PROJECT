﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DAL
{
    public class User
    {
        public int Id { get; set; }


        [Required]
        [StringLength(150)]
        public string UserName { get; set; }

        public string Password { get; set; }

        public bool IsActive { get; set; }

        public int RoleId { get; set; }

        public Role Role { get; set; }


    }
}
