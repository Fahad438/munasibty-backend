using Zafaty.Server.Dtos;
using Zafaty.Server.Model;

namespace Zafaty.Server.Interfaces
{
    public interface  IPostRepository
    {
        Task<IEnumerable<GetPostDto>> GetAllpostAsync();
        Task<IEnumerable<GetPostDto>> GetAllpostByPostId(int PostId);
        Task<IEnumerable<GetPostDto>> GetAllPostByIdAsync(string UserId);
        Task CreatePostAsync(Post post);
        Task<Post> GetPostByIdAsync(int id); // لجلب المنشور حسب الـ ID
        Task UpdatePostAsync(Post post);
        Task DeletPostAsync(int id);
        Task<IEnumerable<GetPostDto>> SearchUser(string? Title,int? CatgoryId,int? Reating,string? sortByPrice,string?location);



    }
}
