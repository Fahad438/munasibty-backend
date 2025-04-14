using System.ComponentModel.DataAnnotations;
using Zafaty.Server.Model;

namespace Zafaty.Server.Dtos
{
    public class AddCommentDto
    {

        [Required]
        [StringLength(50)]
        public string Text { get; set; }

        // استخدام ForeignKey لتوضيح العلاقة مع User
        [Required]
        public int UserId { get; set; }

        // استخدام ForeignKey لتوضيح العلاقة مع Post
        [Required]
        public int PostId { get; set; }
    }
}
