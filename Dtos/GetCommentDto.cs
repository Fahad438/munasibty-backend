using System.ComponentModel.DataAnnotations;
using Zafaty.Server.Model;

namespace Zafaty.Server.Dtos
{
    public class GetCommentDto
    {
        [Key]
        public int Id { get; set; }

     
        public string Text { get; set; }

        // استخدام ForeignKey لتوضيح العلاقة مع User
      
        public int UserId { get; set; }
        public string UserName { get; set; }


        // استخدام ForeignKey لتوضيح العلاقة مع Post

        public int PostId { get; set; }
    }
}
