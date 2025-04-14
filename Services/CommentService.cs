using Zafaty.Server.Dtos;
using Zafaty.Server.Interfaces;
using Zafaty.Server.Model;

namespace Zafaty.Server.Services
{
    public class CommentService:ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }
        public async Task CreateCommentAsync(Comments comment)
        {
            if (comment == null)
            {
                throw new ArgumentNullException("Comment is null");
            }
           await _commentRepository.CreateCommentAsync(comment);

        }

        public async Task<IEnumerable<GetCommentDto>> GetCommentAsync(int PostId)
        {
            if (PostId < 0)
            {
                throw new ArgumentException("value must be not null");

            }

            return await _commentRepository.GetCommentAsync(PostId);
        }

        public async Task DeleteCommentAsync(int PostId, int id)
        {
            await _commentRepository.DeleteCommentAsync(PostId, id);
        }

        public async Task CreateRating(Rating rating)
        {
            if (rating == null) {
                throw new ArgumentNullException("Rating is null");

            }

            await _commentRepository.CreateRating(rating);
        }

        public Task<bool> isRatingExistService(int userId, int postId)
        {
            return _commentRepository.isRatingExist(userId, postId);
        }
    }
}
