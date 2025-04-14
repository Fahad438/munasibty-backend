using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zafaty.Server.Model
{
    public class Comments
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        // استخدام ForeignKey لتوضيح العلاقة مع User
    
        public int UserId { get; set; }
        public User User { get; set; }  // التنقل إلى الكائن المرتبط بـ User

        // استخدام ForeignKey لتوضيح العلاقة مع Post
       
        public int PostId { get; set; }
        public Post Post { get; set; }  // التنقل إلى الكائن المرتبط بـ Post
    }
}
