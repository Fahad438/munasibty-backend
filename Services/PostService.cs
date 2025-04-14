using Zafaty.Server.Dtos;
using Zafaty.Server.Interfaces;
using Zafaty.Server.Model;
using Zafaty.Server.Repositories;

namespace Zafaty.Server.Services
{
    public class PostService: IPostService
    {

        private readonly IPostRepository _postRepo;

        public PostService(IPostRepository postRepo) { 
        _postRepo = postRepo;
        }
        public async Task<IEnumerable<GetPostDto>> GetAllpostAsync()
        {

            return await _postRepo.GetAllpostAsync();
        }
        public async Task<IEnumerable<GetPostDto>> GetAllpostByPostId(int PostId)
        {
            if (PostId < 0)
            {
                throw new ArgumentException(nameof(PostId));

            }
            return await _postRepo.GetAllpostByPostId(PostId);
        }

        public async Task<IEnumerable<GetPostDto>> GetAllPostByIdAsync(string UserId)
        {

            int UserIdInt = int.Parse(UserId);
            return await _postRepo.GetAllPostByIdAsync(UserId);

        }

        public async Task CreatePostAsync(Post post)
            
        {
            if (post == null)
            {
                throw new ArgumentNullException(nameof(post), "The post cannot be null.");
            }
            await _postRepo.CreatePostAsync(post);
        }

        public async Task UpdatePostAsync(int id, Post updatedPost)
        {
            // تحقق من صحة البيانات
            if (updatedPost == null)
            {
                throw new ArgumentNullException(nameof(updatedPost), "The post cannot be null.");
            }

            // جلب المنشور الحالي
            var existingPost = await _postRepo.GetPostByIdAsync(id);
            if (existingPost == null)
            {
                throw new KeyNotFoundException($"Post with ID {id} does not exist.");
            }

           // تحديث الحقول فقط باستخدام updatedPostDto
            existingPost.Title = updatedPost.Title ?? existingPost.Title;
            existingPost.Description = updatedPost.Description ?? existingPost.Description;
            existingPost.ImgaeUrl = updatedPost.ImgaeUrl ?? existingPost.ImgaeUrl;
            existingPost.price = updatedPost.price != 0 ? updatedPost.price : existingPost.price;
            existingPost.available = updatedPost.available;
            existingPost.Location = updatedPost.Location ?? existingPost.Location;
            existingPost.UserId = updatedPost.UserId != 0 ? updatedPost.UserId : existingPost.UserId;
            existingPost.CategoryId = updatedPost.CategoryId != 0 ? updatedPost.CategoryId : existingPost.CategoryId;
            existingPost.MediaUrls = updatedPost.MediaUrls ?? existingPost.MediaUrls;

            // حفظ التغييرات
            await _postRepo.UpdatePostAsync(existingPost);
        }
        public async Task DeletPostAsync(int id)
        {
            // البحث عن الكائن المطلوب حذفه
           await _postRepo.DeletPostAsync(id);
        }

        public async Task<IEnumerable<GetPostDto>> SearchUser(string? Title, int? CatgoryId, int? Reating, string? sortByPrice, string? location)
        {
            return await _postRepo.SearchUser(Title, CatgoryId, Reating, sortByPrice, location);
        }

    }
}
