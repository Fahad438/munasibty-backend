using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Zafaty.Server.Model;

namespace Zafaty.Server.Dtos
{
    public class PostDto
    {

        [Key]
    
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        [Required]
        [MaxLength(200)]
        public string Description { get; set; }
        [Required]
        public string ImgaeUrl { get; set; }
        [Required]
        [Column(TypeName = "decimal(10, 2)")]  // هنا يتم تحديد الـ scale

        public decimal price { get; set; }
        [Required] 
        public string Location { get; set; }
        [Required]
        public bool available { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public List<string> MediaUrls { get; set; } = new();



    }
}
