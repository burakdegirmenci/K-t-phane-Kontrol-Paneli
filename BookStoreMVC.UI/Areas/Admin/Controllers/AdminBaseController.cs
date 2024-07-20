namespace BookStoreMVC.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")] //admin rolündekilerin açabilmesini sağlıyoruz - şimdilik yoruma alıyoruz
    public class AdminBaseController : BaseController
    {

    }
}
