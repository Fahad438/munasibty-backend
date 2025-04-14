using System.ComponentModel.DataAnnotations;

namespace Zafaty.Server.Model
{
    public class Category
    {
       
            [Key]
            public int Id { get; set; }
            [Required]
            [MaxLength(100)]
            public string Name { get; set; }
            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
            //realtionsship with Post 1=>Many 
            public ICollection<Post> Posts { get; set; }
       
    }
}
