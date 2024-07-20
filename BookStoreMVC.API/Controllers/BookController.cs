using BookStoreMVC.Application.DTOs.BookDTOs;
using BookStoreMVC.Application.DTOs.CategoryDTOs;
using BookStoreMVC.Application.Services.BookServices;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreMVC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _bookService.GetAllAsync();
            if(!result.IsSuccess)
            {
                return Ok(result);
            }
            return Ok(result);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(BookCreateDTO bookCreateDTO)
        {
            var result = await _bookService.CreateAsync(bookCreateDTO);
            return result.IsSuccess ? Ok(result) : Ok(result);
        }

        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult>Delete(Guid bookId)
        {
            var result = await _bookService.DeleteAsync(bookId);
            if(!result.IsSuccess)
            {
                return Ok(result);
            }
            return Ok(result);
        }
    }
}
