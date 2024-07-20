using BookStoreMVC.Application.DTOs.AuthorDTOs;
using BookStoreMVC.Application.Services.AuthorServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreMVC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAll()
        {
            var result =await _authorService.GetAllAsync();
            return Ok(result);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(AuthorCreateDTO model)
        {
            var result =await _authorService.CreateAsync(model);
            return Ok(result);
        }
        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _authorService.DeleteAsync(id);
            return Ok(result);
        }
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Update(AuthorUpdateDTO model)
        {
            var result = await _authorService.UpdateAsync(model);
            return Ok(model);
        }
    }
}
