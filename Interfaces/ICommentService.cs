using Zafaty.Server.Dtos;
using Zafaty.Server.Model;

namespace Zafaty.Server.Interfaces
{
    public interface ICommentService
    {
        Task<IEnumerable<GetCommentDto>> GetCommentAsync(int PostId);
        Task CreateCommentAsync(Comments commentDto);

        Task DeleteCommentAsync(int PostId, int id);
        Task CreateRating(Rating rating);
        Task<bool> isRatingExistService(int userId, int postId);
    }
}
