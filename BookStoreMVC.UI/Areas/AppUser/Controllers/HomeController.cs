using Microsoft.AspNetCore.Mvc;

namespace BookStoreMVC.UI.Areas.AppUser.Controllers
{
    public class HomeController : AppUserBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
