using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Zafaty.Server.Dtos;
using Zafaty.Server.Interfaces;
using Zafaty.Server.Model;

namespace Zafaty.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    [EnableRateLimiting("UserRateLimit")]

    public class CommentController : ControllerBase
    {

        private readonly ICommentService _CommentServicse;

        public CommentController(ICommentService commentServicse)
        {
            _CommentServicse = commentServicse;
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(AddCommentDto addCommentDto)
        {
            if (addCommentDto == null)
            {

                return BadRequest();
            }
            var comment = new Comments
            {
                Text = addCommentDto.Text,
                UserId = addCommentDto.UserId,
                PostId = addCommentDto.PostId,
            };

            await _CommentServicse.CreateCommentAsync(comment);

            return Ok("Add Done");

        }

        [HttpGet("{PostId}")]
        public async Task<IActionResult> GetComments(int PostId)
        {
            if (PostId < 0)
            {
                return BadRequest();

            }

            var comment = await _CommentServicse.GetCommentAsync(PostId);
            return Ok(comment);

        }

        [HttpDelete("{PostId}/{Id}")]
        public async Task<IActionResult> DeletComment(int PostId, int Id)
        {
            await _CommentServicse.DeleteCommentAsync(PostId, Id);
            return Ok("comment is deleted");
        }

        [HttpPost("Rating")]

        public async Task<IActionResult> CreateRating(RatingDto ratingDto)
        {
            if (ratingDto == null)
            {
                return BadRequest();
            }
            var ratng = new Rating
            {
                UserId = ratingDto.UserId,
                PostId= ratingDto.PostId,
                star = ratingDto.Rating,
                Comment=ratingDto.Comment                                                                                 

            };

            await _CommentServicse.CreateRating(ratng);
            return Ok("add done");
        }

        [HttpGet("hasRated")]
        public async Task<ActionResult> HasUserRated([FromQuery] int userId, [FromQuery] int postId)
        {
            bool hasRated = await _CommentServicse.isRatingExistService(userId, postId);
            return Ok(new
            {
                message = hasRated ? "المستخدم قام بالتقييم مسبقًا." : "يمكن للمستخدم التقييم.",
                value = hasRated
            });
        }
    }
}
