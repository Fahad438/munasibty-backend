using Microsoft.EntityFrameworkCore;
using TestApi.Data;
using Zafaty.Server.Dtos;
using Zafaty.Server.Interfaces;
using Zafaty.Server.Model;

namespace Zafaty.Server.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly AppDbContext _db;
        public PostRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<GetPostDto>> GetAllpostAsync()
        {

            return await _db.Post
                  .Include(p => p.User) // تحميل الـ User المرتبط بكل Post

                .Include(p => p.Category) // تحميل الـ Category المرتبط بكل Post
                 .Include(p => p.Comments)
                  .Include(p => p.Rating)

                .Select(p => new GetPostDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description,
                    ImgaeUrl = p.ImgaeUrl,
                    Price = p.price, // تأكد من تضمين باقي الخصائص كما ينبغي
                    available = p.available, // تأكد من تضمين باقي الخصائص كما ينبغي
                    UserId = p.UserId,
                    Location = p.Location,
                    Category = p.Category.Name,
                    CategoryId = p.Category.Id,
                    MediaUrls = p.MediaUrls,
                    Phone = p.User.Phone,


                    //Comments = p.Comments,
                    Rating = p.Rating.Select(r => new RatingDto
                    {
                        Rating = r.star, // فقط نستخدم الحقل الستار من التقييم
                        UserId = r.UserId,
                        PostId = r.PostId,
                        Comment = r.Comment,
                    }).ToList(),
                    RitAvg = p.Rating.Any() ? p.Rating.Average(r => r.star) : 0,
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<GetPostDto>> GetAllpostByPostId(int PostId)
        {
            if (PostId < 0)
            {
                throw new ArgumentException(nameof(PostId));

            }
            return await _db.Post
                .Include(p => p.User) // تحميل الـ User المرتبط بكل Post

                .Include(p => p.Category) // تحميل الـ Category المرتبط بكل Post
                 .Include(p => p.Comments)
                  .Include(p => p.Rating)
                .Where(p => p.Id == PostId)
                .Select(p => new GetPostDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description,
                    ImgaeUrl = p.ImgaeUrl,
                    Price = p.price, // تأكد من تضمين باقي الخصائص كما ينبغي
                    available = p.available, // تأكد من تضمين باقي الخصائص كما ينبغي
                    UserId = p.UserId,
                    Location = p.Location,
                    Category = p.Category.Name,
                    CategoryId = p.Category.Id,
                    MediaUrls = p.MediaUrls,
                    Phone = p.User.Phone,

                    //Comments = p.Comments,
                    Rating = p.Rating.Select(r => new RatingDto
                    {
                        Rating = r.star, // فقط نستخدم الحقل الستار من التقييم
                        UserId = r.UserId,
                        PostId = r.PostId,
                        Comment = r.Comment,
                    }).ToList(),
                    RitAvg = p.Rating.Any() ? p.Rating.Average(r => r.star) : 0,


                })
                .ToListAsync();
        }


        public async Task<IEnumerable<GetPostDto>> GetAllPostByIdAsync(string UserId)
        {
            int UserIdInt = int.Parse(UserId);

            return await _db.Post
                .Include(p => p.User) // تحميل الـ User المرتبط بكل Post

                .Include(p => p.Category) // تحميل الـ Category المرتبط بكل Post
                 .Include(p => p.Comments)
                  .Include(p => p.Rating)
                .Where(p => p.UserId == UserIdInt)
                .Select(p => new GetPostDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description,
                    ImgaeUrl = p.ImgaeUrl,
                    Price = p.price, // تأكد من تضمين باقي الخصائص كما ينبغي
                    available = p.available, // تأكد من تضمين باقي الخصائص كما ينبغي
                    UserId = p.UserId,
                    Location = p.Location,
                    Category = p.Category.Name,
                    Phone = p.User.Phone,

                    CategoryId = p.Category.Id,
                    MediaUrls = p.MediaUrls,

                    //Comments = p.Comments,
                    Rating = p.Rating.Select(r => new RatingDto
                    {
                        Rating = r.star, // فقط نستخدم الحقل الستار من التقييم
                        UserId = r.UserId,
                        PostId = r.PostId,
                        Comment = r.Comment,
                    }).ToList(),
                    RitAvg = p.Rating.Any() ? p.Rating.Average(r => r.star) : 0,


                })
                .ToListAsync();
        }


        public async Task CreatePostAsync(Post post)
        {
            //here i need to take post then put it in db 

            if (post == null)
            {
                throw new ArgumentNullException(nameof(post), "The post cannot be null.");
            }
            var categoryExists = await _db.Category.FindAsync(post.CategoryId);

            if (categoryExists == null)
            {
                throw new ArgumentException("CategoryId غير موجود.");
            }

            var User = await _db.Users.FindAsync(post.UserId);

            if (User == null)
            {
                throw new ArgumentException("UserId غير موجود.");
            }

            _db.Post.Add(post); // يفترض أن لديك DbSet<Post> باسم Posts في DbContext

            // حفظ التغييرات في قاعدة البيانات
            await _db.SaveChangesAsync();
        }



        public async Task<Post> GetPostByIdAsync(int id)
        {
            return await _db.Post.FindAsync(id);
        }

        public async Task UpdatePostAsync(Post post)
        {
            _db.Post.Update(post); // يقوم بتتبع الكائن وتحديثه
            await _db.SaveChangesAsync();
        }

        public async Task DeletPostAsync(int id)
        {
            // البحث عن الكائن المطلوب حذفه
            var post = await _db.Post.FindAsync(id);
            if (id == null)
            {
                throw new ArgumentNullException("The Id cannot be null.");
            }
            _db.Post.Remove(post);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<GetPostDto>> SearchUser(string? Title, int? CatgoryId, int? Rating, string? sortByPrice, string? location)
        {

            var query =  _db.Post.AsQueryable();


            if (!string.IsNullOrEmpty(Title))
            {
                query= query.Where(x => x.Title.Contains(Title));
            }

            if (CatgoryId.HasValue)
            {
                query=query.Where(p=>p.CategoryId== CatgoryId);

            }
            if (Rating.HasValue)
            {
                query = query.Include(p => p.Rating)
                                .Where(p => p.Rating.Any(r => r.star == Rating.Value));
            }

            if (location != null) {
                query = query.Where(p => p.Location.Contains(location));
            }



            if (!string.IsNullOrEmpty(sortByPrice))
            {
                switch (sortByPrice.ToLower())
                {
                    case "asc":
                        query = query.OrderBy(p => p.price);
                        break;
                    case "desc":
                        query = query.OrderByDescending(p => p.price);
                        break;
                    default:
                         throw new ArgumentException("Invalid sort order. Use 'asc' or 'desc'.");
                }
            }
            var result = await query.Select(p => new GetPostDto
            {
                Id = p.Id,
                UserId = p.UserId,
                Title = p.Title,
                Category = p.Category.Name,
                CategoryId = p.CategoryId,
                Price = p.price,
                available = p.available,
                Location = p.Location,
                Description = p.Description,
                ImgaeUrl = p.ImgaeUrl,
                MediaUrls = p.MediaUrls,
                Phone = p.User.Phone,


                //Comments = p.Comments,
                Rating = p.Rating.Select(r => new RatingDto
                {
                    Rating = r.star, // فقط نستخدم الحقل الستار من التقييم
                    UserId = r.UserId,
                    PostId = r.PostId,
                    Comment = r.Comment,


                }).ToList(), //
                    RitAvg = p.Rating.Any() ? p.Rating.Average(r => r.star) : 0,

            }).ToListAsync();

           return result;

        }
    }
}
