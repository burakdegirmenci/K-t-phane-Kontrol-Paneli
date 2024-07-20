namespace BookStoreMVC.UI.Areas.Admin.Controllers
{
	public class HomeController : AdminBaseController
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
