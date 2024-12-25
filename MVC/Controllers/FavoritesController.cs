using BLL.Controllers.Bases;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    [Authorize] 
    public class FavoritesController : MvcController
    {
        const string SESSIONKEY = "Favorites";
        private readonly HttpServiceBase _httpService;

        public FavoritesController(HttpServiceBase httpService)
        {
            _httpService = httpService;
        }
        private int GetUserId() => Convert.ToInt32(User.Claims.SingleOrDefault(c => c.Type == "Id").Value);

        private List<FavoritesModel> GetSession()
        {
            var favorites = _httpService.GetSession<List<FavoritesModel>>(SESSIONKEY);
            return favorites?.Where(f => f.UserId == GetUserId()).ToList();
        }
        public IActionResult Get()
        {
            return View(GetSession());
        }
    }
}
