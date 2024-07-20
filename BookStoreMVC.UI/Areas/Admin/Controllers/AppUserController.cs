namespace BookStoreMVC.UI.Areas.Admin.Controllers
{
    public class AppUserController : AdminBaseController
    {
        private readonly IAppUserService _appUserService;

        public AppUserController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _appUserService.GetAllAsync();
            var appUserVMs = result.Data.Adapt<List<AppUserListVM>>();
            if(!result.IsSuccess)
            {
                NotifiyError(result.Messages);
                return View(appUserVMs);
            }
            NotifiySuccess(result.Messages);
            return View(appUserVMs);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AppUserCreateVm model)
        {
            var result = await _appUserService.CreateAsync(model.Adapt<AppUserCreateDTO>());
            if (!result.IsSuccess)
            {
                NotifiyError(result.Messages);
                return View(model);
            }
            NotifiySuccess(result.Messages);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(Guid id)
        {
            var updatingAppUserVM = (await _appUserService.GetByIdAsync(id)).Data.Adapt<AppUserUpdateVM>();
            return View(updatingAppUserVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update (AppUserUpdateVM model)
        {
            var updatingAppUserDTO = model.Adapt<AppUserUpdateDTO>();
            var result = await _appUserService.UpdateAsync(updatingAppUserDTO);
            if (!result.IsSuccess)
            {
                NotifiyError(result.Messages);
                return View(model);
            }
            NotifiySuccess (result.Messages);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _appUserService.DeleteAsync(id);
            if (!result.IsSuccess)
            {
                NotifiyError(result.Messages);
                return RedirectToAction("Index");
            }
            NotifiySuccess(result.Messages);
            return RedirectToAction("Index");
        }
    }
}
