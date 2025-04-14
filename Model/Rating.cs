using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zafaty.Server.Model
{
    public class Rating
    {
        [Key]
       public int id {  get; set; }
        [Required]
        public string Comment { get; set; }
        [Required][MaxLength(5)]
        public int star { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

   
        public int UserId { get; set; }
        public User User { get; set; }  // التنقل إلى الكائن المرتبط بـ User

        // استخدام ForeignKey لتوضيح العلاقة مع Post
      
        public int PostId { get; set; }
        public Post Post { get; set; }  // التنقل إلى الكائن المرتبط بـ Post

    }
}
