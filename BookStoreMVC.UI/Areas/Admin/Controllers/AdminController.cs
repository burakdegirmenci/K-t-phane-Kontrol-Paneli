namespace BookStoreMVC.UI.Areas.Admin.Controllers
{
    public class AdminController : AdminBaseController
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _adminService.GetAllAsync();
            var adminVMs = result.Data.Adapt<List<AdminListVM>>();
            if(!result.IsSuccess)
            {
                NotifiyError(result.Messages);
                return View(adminVMs);
            }
            NotifiySuccess(result.Messages);
            return View(adminVMs);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AdminCreateVM model)
        {
            var result = await _adminService.CreateAsync(model.Adapt<AdminCreateDTO>());
            if(!result.IsSuccess)
            {
                NotifiyError(result.Messages);
                return View();
            }
            NotifiySuccess(result.Messages);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(Guid id)
        {
            var result = await _adminService.GetByIdAsync(id);
            var updatingAdminVM = (result.Data).Adapt<AdminUpdateVM>();
            if(!result.IsSuccess)
            {
                NotifiyError(result.Messages);
                return RedirectToAction("Index");
            }
            NotifiySuccess(result.Messages);
            return View(updatingAdminVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(AdminUpdateVM model)
        {
            var result = await _adminService.UpdateAsync(model.Adapt<AdminUpdateDTO>());
            if(!result.IsSuccess)
            {
                NotifiyError(result.Messages);
                return View();
            }
            NotifiySuccess(result.Messages);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete (Guid id)
        {
            var result = await _adminService.DeleteAsync(id);
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
