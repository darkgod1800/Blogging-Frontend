using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using PostServices.Models.Domains;
using PostServices.Models.DTOs;
using PostServices.Services;

namespace PostServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BlogPostController : ControllerBase
    {
        public readonly IBlogPostService _service;
        public BlogPostController(IBlogPostService service) { _service = service; }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBlogPostRequestDTO request)
        {
            BlogPost blog = new BlogPost
            {
                Title = request.Title,
                ShortDescription = request.ShortDescription,
                Content = request.Content,
                FeaturedImageUrl = request.FeaturedImageUrl,
                UserHandle = request.UserHandle,
                PublishedDate = request.PublishedDate,
                Author = request.Author,
                IsVisible = request.IsVisible,

            };
            var result = await _service.Create(blog);

            if(result is BlogPost blogPost&& result is not null)
            {
                var response = new BlogPostDTO
                {
                    Title = result.Title,
                    ShortDescription = result.ShortDescription,
                    Content = result.Content,
                    FeaturedImageUrl = result.FeaturedImageUrl,
                    UserHandle = result.UserHandle,
                    PublishedDate = result.PublishedDate,
                    Author = result.Author,
                    IsVisible = result.IsVisible,
                    Id = result.Id
                };
                return Created();
            }
            return BadRequest();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllPosts()
        {
            var result = await _service.GetAllPost();

            if(result is List<BlogPost>)
            {
                return Ok(result);
            }
            return BadRequest();

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostById(Guid id)
        {
            var result = await _service.GetPostById(id);
            if(result is not null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(Guid id, [FromBody] CreateBlogPostRequestDTO request)
        {
            BlogPost blog = new BlogPost
            {
                Title = request.Title,
                ShortDescription = request.ShortDescription,
                Content = request.Content,
                FeaturedImageUrl = request.FeaturedImageUrl,
                UserHandle = request.UserHandle,
                PublishedDate = request.PublishedDate,
                Author = request.Author,
                IsVisible = request.IsVisible,
            };
            var result = await _service.UpdatePost(id, blog);

            if(result is BlogPost)
            {
                return Ok(result);
            }
            return BadRequest();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(Guid id)
        {
            var result = await _service.DeletePost(id);

            if(result > 0)
            {
                return Ok();
            }
            return BadRequest();
        }



    }
}
