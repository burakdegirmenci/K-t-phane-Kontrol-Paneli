using Microsoft.AspNetCore.Mvc;

namespace BookStoreMVC.UI.Areas.AppUser.Controllers
{
    [Area("AppUser")]
    [Authorize(Roles = "AppUser")] //appUser rolündekilerin açabilmesini sağlıyoruz - şimdilik yoruma alıyoruz
    public class AppUserBaseController : BaseController
    {

    }
}
