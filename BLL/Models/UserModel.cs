using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Identity;
using BLL.DAL;

namespace BLL.Models
{
    public class UserModel
    {
        public User Record { get; set; }

        public string UserName => Record.UserName;

        public string Password => Record.Password;

        public string IsActive => Record.IsActive ? "Active" : "Not Active";

        public string Role => Record.Role?.Name;
    }
}
