using BookStoreMVC.Application.DTOs.AuthorDTOs;
using BookStoreMVC.Application.DTOs.PublisherDTOs;
using BookStoreMVC.Application.Services.AuthorServices;
using BookStoreMVC.Application.Services.PublisherServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreMVC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherServices _publisherServices;

        public PublisherController(IPublisherServices publisherServices)
        {
            _publisherServices = publisherServices;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _publisherServices.GetAllAsync();
            return Ok(result);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(PublisherCreateDTO model)
        {
            var result = await _publisherServices.CreateAsync(model);
            return Ok(result);
        }
        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _publisherServices.DeleteAsync(id);
            return Ok(result);
        }
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Update(PublisherUpdateDTO model)
        {
            var result = await _publisherServices.UpdateAsync(model);
            return Ok(model);
        }
    }
}
