using BLL.Controllers.Bases;
using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class UsersController : MvcController
    {
        private readonly IService<User, UserModel> _UserService;

        public UsersController(IService<User, UserModel> userService)
        {
            _UserService = userService;
        }


        public IActionResult Login()
        {
            return View();
        }
    }
}
