using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Npgsql;
using PostServices.Models.Domains;
using PostServices.Models.DTOs;
using PostServices.Services;

namespace PostServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly IPostCategoryService _service;
        public CategoriesController(IPostCategoryService service) 
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequestDTO request)
        {
            var category = new Category
            {
                Name = request.Name,
                UrlHandle = request.UrlHandle,
            };
            var c = await _service.AddCategory(category);

            if(c is Category && c is not null)
            {
                var response = new CategoryDTO
                {
                    Name = c.Name,
                    UrlHandle = c.UrlHandle,
                    Id = c.Id
                };

                return Created();
            }
            return BadRequest();

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveCategory(Guid id)
        {
            var result = await _service.DeleteCategory(id);
            if (result > 0) 
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var result = await _service.GetAllCategory();
            if(result is List<Category>)
            {
                return Ok(result);
            } 
            return BadRequest();
        }

    }
}
