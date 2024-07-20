namespace BookStoreMVC.UI.Areas.Admin.Controllers
{

    public class CategoryController : AdminBaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _categoryService.GetAllAsync();
            if (!result.IsSuccess)
            {
                NotifiyError(result.Messages);
                return View();
            }
            var categoryListVMs = result.Data.Adapt<List<CategoryListVM>>();
            NotifiySuccess(result.Messages);
            return View(categoryListVMs);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create (AdminCategoryCreateVM model)
        {
           var result= await _categoryService.CreateAsync(model.Adapt<CategoryCreateDTO>());
            if(!result.IsSuccess)
            {
                NotifiyError(result.Messages);
                return View(model);
            }
            NotifiySuccess(result.Messages);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _categoryService.DeleteAsync(id);
            if (!result.IsSuccess)
            {
                NotifiyError(result.Messages);
                // await Console.Out.WriteLineAsync(result.Messages);
                return RedirectToAction("Index");
            }
            NotifiySuccess(result.Messages);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(Guid id)
        {
            var result = await _categoryService.GetByIdAsync(id);
            var categoryUpdateVm = result.Data.Adapt<AdminCategoryUpdateVM>();
            return View(categoryUpdateVm);

        }

        [HttpPost]
        public async Task<IActionResult> Update(AdminCategoryUpdateVM model)
        {
            var result = await _categoryService.UpdateAsync(model.Adapt<CategoryUpdateDTO>());
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
