using System.ComponentModel.DataAnnotations;

namespace Zafaty.Server.Dtos
{
    public class RatingDto
    {

        [Required]
        public int Rating { get; set; }
        [Required]
        public int PostId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Comment { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
