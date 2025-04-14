using System.ComponentModel.DataAnnotations.Schema;
using Zafaty.Server.Model;

namespace Zafaty.Server.Dtos
{
    public class GetPostDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImgaeUrl { get; set; }
        [Column(TypeName = "decimal(10, 2)")]  // هنا يتم تحديد الـ scale

        public decimal Price { get; set; }
        public bool available { get; set; }
        public string Location { get; set; }

        public string Phone { get; set; }
        public int UserId { get; set; }
        public string Category { get; set; }
        public int CategoryId  { get; set; } // ارتباط مع
        public double RitAvg { get; set; }                            // 

        //public IEnumerable<Comments> Comments { get; set; }
        public IEnumerable<RatingDto> Rating { get; set; }
        public List<string> MediaUrls { get; set; } = new();

    }
}
