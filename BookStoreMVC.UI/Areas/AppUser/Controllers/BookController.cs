using BookStoreMVC.UI.Areas.AppUser.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreMVC.UI.Areas.AppUser.Controllers
{
    public class BookController : AppUserBaseController
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _bookService.GetAllBooksForUserAsync();
            var bookListVMs = result.Data.Adapt<List<AppUserBookListVM>>();
            return View(bookListVMs);
        }
    }
}
