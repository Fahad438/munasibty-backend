using Microsoft.EntityFrameworkCore;
using TestApi.Data;
using Zafaty.Server.Dtos;
using Zafaty.Server.Interfaces;
using Zafaty.Server.Model;

namespace Zafaty.Server.Repositories
{
    public class CommentRepository : ICommentRepository
    {

        private readonly AppDbContext _db;
        public CommentRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task CreateCommentAsync(Comments comment)
        {
            if (comment == null)
            {
                throw new ArgumentNullException("Comment is null");
            }

           //check if if <0
            if (comment.UserId <= 0)
                throw new ArgumentException("Invalid UserId.");
            if (comment.PostId <= 0)
                throw new ArgumentException("Invalid PostId.");

            _db.Comments.Add(comment);

            await _db.SaveChangesAsync();

        }

        public async Task<IEnumerable<GetCommentDto>> GetCommentAsync(int PostId)
        {
            if (PostId < 0)
            {
                throw new ArgumentException("PostId must be greater than or equal to 0.");

            }
            return await _db.Comments
                 .Where(c => c.PostId == PostId)
                 .Select(c => new GetCommentDto
                 {
                     Id = c.Id,
                     Text = c.Text,
                     UserId = c.UserId,
                     PostId = c.PostId,
                     UserName= c.User.Name

                 })

                .ToListAsync();

        }
        public async Task DeleteCommentAsync(int postId, int id)
        {
            if (postId < 0)
            {
                throw new ArgumentException("PostId must be a positive number.");
            }
            if (id < 0)
            {
                throw new ArgumentException("Id must be a positive number.");
            }

          
            var comment = await _db.Comments
                                   .FirstOrDefaultAsync(c => c.PostId == postId && c.Id == id);

            if (comment == null)
            {
                throw new KeyNotFoundException("Comment not found.");
            }

            // حذف التعليق
            _db.Comments.Remove(comment);
            await _db.SaveChangesAsync();
        }

        public async Task CreateRating(Rating rating)
        {
            if(rating == null)
            {
                throw new ArgumentNullException("Rating is null");
             }

            _db.Rating.Add(rating);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> isRatingExist(int userId, int postId)
        {
         return await _db.Rating.AnyAsync(r => r.UserId == userId && r.PostId == postId);
        }
    }

}

