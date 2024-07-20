namespace BookStoreMVC.UI.Areas.Admin.Controllers
{
    public class AuthorController : AdminBaseController
    {
        private readonly IAuthorService authorService;

        public AuthorController(IAuthorService authorService)
        {
            this.authorService = authorService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await authorService.GetAllAsync();
            if (!result.IsSuccess)
            {
                NotifiyError(result.Messages);
                //await Console.Out.WriteLineAsync(result.Messages);
                return View();
            }
            var authorListVMs = result.Data.Adapt<List<AdminAuthorListVM>>();
            NotifiySuccess(result.Messages);
            //await Console.Out.WriteAsync(result.Messages);
            return View(authorListVMs);
            
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AdminAuthorCreateVM model)
        {
            var result = await authorService.CreateAsync(model.Adapt<AuthorCreateDTO>());
            if (!result.IsSuccess)
            {
                return View(model);
            }
            NotifiySuccess(result.Messages);
            //await Console.Out.WriteLineAsync(result.Messages);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await authorService.DeleteAsync(id);
            if (!result.IsSuccess)
            {
                await Console.Out.WriteLineAsync(result.Messages);
                return RedirectToAction("Index");
            }
            NotifiySuccess(result.Messages);
            //await Console.Out.WriteLineAsync(result.Messages);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(Guid id)
        {
            var result = await authorService.GetByIdAsync(id);
            var authorUpdateVm = result.Data.Adapt<AdminAuthorUpdateVM>();
            return View(authorUpdateVm);

        }

        [HttpPost]
        public async Task<IActionResult> Update(AdminAuthorUpdateVM model)
        {
            var result = await authorService.UpdateAsync(model.Adapt<AuthorUpdateDTO>());
            if (!result.IsSuccess)
            {
                NotifiyError(result.Messages);
                //await Console.Out.WriteAsync(result.Messages);
                return RedirectToAction("Index");
            }
            NotifiySuccess(result.Messages);

            return RedirectToAction("Index");
        }




    }
}
