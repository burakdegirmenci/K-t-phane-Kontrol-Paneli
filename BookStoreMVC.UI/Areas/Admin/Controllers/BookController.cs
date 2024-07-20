using Microsoft.AspNetCore.Mvc;

namespace BookStoreMVC.UI.Areas.Admin.Controllers
{
    public class BookController : AdminBaseController
    {
        private readonly IBookService bookService;
        private readonly IAuthorService authorService;
        private readonly ICategoryService categoryService;
        private readonly IPublisherServices publisherService;
        public BookController(IBookService bookService, IAuthorService authorService, ICategoryService categoryService, IPublisherServices publisherService)
        {
            this.bookService = bookService;
            this.authorService = authorService;
            this.categoryService = categoryService;
            this.publisherService = publisherService;
        }

        public async Task<IActionResult> Index()
        {
            var bookDTOs = (await bookService.GetAllAsync()).Data;
            return View(bookDTOs.Adapt<List<AdminBookListVM>>());
        }
        public async Task<IActionResult> Create()
        {
            var bookCreateVM = new AdminBookCreateVM();
            bookCreateVM.Authors = await GetAuthors();
            bookCreateVM.Categories = await GetCategories();
            bookCreateVM.Publishers = await GetPublisher();
            return View(bookCreateVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AdminBookCreateVM model)
        {
            await bookService.CreateAsync(model.Adapt<BookCreateDTO>());
            return RedirectToAction("Index");
        }
        private async Task<SelectList> GetAuthors(Guid? authorId = null)
        {
            var authors = (await authorService.GetAllAsync()).Data;
            return new SelectList(authors.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name,
                Selected = x.Id == (authorId != null ? authorId.Value : authorId)
            }).OrderBy(x => x.Text), "Value", "Text");
        }
        private async Task<SelectList> GetCategories(Guid? categoryId = null)
        {
            var categories = (await categoryService.GetAllAsync()).Data;
            return new SelectList(categories.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name,
                Selected = x.Id == (categoryId != null ? categoryId.Value : categoryId)
            }).OrderBy(x => x.Text), "Value", "Text");
        }
        private async Task<SelectList> GetPublisher(Guid? publisherId = null)
        {
            var publishers = (await publisherService.GetAllAsync()).Data;
            return new SelectList(publishers.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name,
                Selected = x.Id == (publisherId != null ? publisherId.Value : publisherId)
            }).OrderBy(x => x.Text), "Value", "Text");
        }

        public async Task<IActionResult> Update(Guid id)
        {
            var updatingBook = await bookService.GetByIdAsync(id);
            var result = updatingBook.Data.Adapt<AdminBookUpdateVM>();
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Update(AdminBookUpdateVM model)
        {
            var result = await bookService.UpdateAsync(model.Adapt<BookUpdateDTO>());
            if (!result.IsSuccess)
            {
                NotifiyError(result.Messages);
                return RedirectToAction("Index");
            }
            NotifiySuccess(result.Messages);
            return View("Index");
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            var deletingBook = await bookService.DeleteAsync(id);
            if (!deletingBook.IsSuccess)
            {
                NotifiyError(deletingBook.Messages);
                return RedirectToAction("Index");
            }
            NotifiySuccess(deletingBook.Messages);
            return RedirectToAction("Index");

        }
        [HttpPost]
        public async Task<JsonResult> UpdateAvailability([FromBody] UpdateAvailabilityModel model)
        {
            try
            {
                await bookService.UpdateAvailability(model.BookId, model.IsBookAvailable);
                return Json(new { success = true });
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }

    }
}
