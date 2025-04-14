using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zafaty.Server.Model
{
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // تحديد التزايد التلقائي
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ImgaeUrl { get; set; }

        [Column(TypeName = "decimal(10, 2)")]  // هنا يتم تحديد الـ scale
        public decimal price { get; set; }

        public bool available { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

      public string Location { get; set; }
        public int UserId {  get; set; }

        public User User { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        // إضافة علاقة One-to-Many مع Comments
        public ICollection<Comments> Comments { get; set; }

        public ICollection<Rating> Rating { get; set; }
        public List<string> MediaUrls { get; set; } = new();

    }
}
