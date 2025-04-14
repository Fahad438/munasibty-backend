using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApi.Data;
using Zafaty.Server.Dtos;
using Zafaty.Server.Interfaces;
using Zafaty.Server.Model;
using Zafaty.Server.Repositories;
using Zafaty.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.RateLimiting;


namespace Zafaty.Server.Controllers
{
   

    [Route("api/[controller]")]
   
    [ApiController]
    //[Authorize]
    [EnableRateLimiting("UserRateLimit")]
    public class PostsController : ControllerBase
    {

        private readonly IPostService _postServ;
        private readonly AppDbContext _db;
        public PostsController(IPostService postServ,AppDbContext db)
        {
            _postServ = postServ;
            _db = db;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllpost()
        {
            var post = await _postServ.GetAllpostAsync();

            return Ok(post);
        }
        [HttpGet("post/{id}")]
        public async Task<IActionResult> GetAllpostByPostId(int id)
        {
            var post = await _postServ.GetAllpostByPostId(id);

            return Ok(post);
        }
        [HttpGet("user/{UserId}")]
        public async Task<IActionResult> GetAllpostById(string UserId)
        {
            var post = await _postServ.GetAllPostByIdAsync(UserId);

            return Ok(post);
        }

        [HttpPost]

        public async Task<IActionResult> AddPost(PostDto postDto)
        {

            //check if data null we req badReq
            if (postDto == null)
            {
                return BadRequest();
            }
            //here we take data from user and convert to object post 
            var post = new Post
            {
                Title = postDto.Title,
                Description = postDto.Description,
                ImgaeUrl = postDto.ImgaeUrl,
                price = postDto.price,
                available = postDto.available,
                Location = postDto.Location,
                UserId = postDto.UserId,
                CategoryId = postDto.CategoryId,
                 MediaUrls = postDto.MediaUrls
            };


            await _postServ.CreatePostAsync(post);
            return Ok("Post created successfully.");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id,  PostDto updatedPostDto)
        {
            //لسا
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //نسوي اوبجكت جديد ونحط القيم فيه من نوع بوست
            var updatedPost = new Post
            {
                Title = updatedPostDto.Title,
                Description = updatedPostDto.Description,
                ImgaeUrl = updatedPostDto.ImgaeUrl,
                price = updatedPostDto.price,
                Location = updatedPostDto.Location,
                available = updatedPostDto.available,
                UserId = updatedPostDto.UserId,
                CategoryId = updatedPostDto.CategoryId,
                MediaUrls = updatedPostDto.MediaUrls

            };

            try
            {
                await _postServ.UpdatePostAsync(id, updatedPost);
                return NoContent(); // إذا نجحت العملية، نرجع 204
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            await _postServ.DeletPostAsync(id);
            return Ok("Post Deleted successfully.");

        }


        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<GetPostDto>>> SearchUser(string? Title, int? CategoryId, int? Rating, string? sortByPrice,string? location)
        {
            var posts = await _postServ.SearchUser(Title, CategoryId, Rating, sortByPrice, location);

            if (posts == null || !posts.Any())
            {
                return NotFound(posts);
            }

            return Ok(posts);
        }

        [HttpGet("category")]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategories()
        {
            var categories = await _db.Category.ToListAsync();

            if (categories == null || !categories.Any())
            {
                return NotFound(categories);
            }

            return Ok(categories);
        }
        [HttpGet("category/{id}")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategorieById(int id)
        {
            var categories = await _db.Category.FindAsync(id);

            if (categories == null )
            {
                return NotFound(categories);
            }

            return Ok(categories);
        }
    }

    
}
