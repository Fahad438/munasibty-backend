using Zafaty.Server.Dtos;
using Zafaty.Server.Model;

namespace Zafaty.Server.Interfaces
{
    public interface IPostService
    {
        Task<IEnumerable<GetPostDto>> GetAllpostAsync();
        Task<IEnumerable<GetPostDto>> GetAllpostByPostId(int PostId);

        Task<IEnumerable<GetPostDto>> GetAllPostByIdAsync(string UserId);
        Task CreatePostAsync(Post post);
        Task UpdatePostAsync(int id, Post updatedPost); // لتحديث المنشور حسب الـ ID
        Task DeletPostAsync(int id);
        Task<IEnumerable<GetPostDto>> SearchUser(string? Title, int? CatgoryId, int? Reating, string? sortByPrice,string? location);


    }
}
