using System.ComponentModel.DataAnnotations;

namespace Zafaty.Server.Dtos
{
    public class GuestCreateDTO
    {
        [Required]
        [MaxLength(50)]
        public string NameGust { get; set; }
        [Required]
        [MaxLength(10)]
        public string Side { get; set; }
        [Required]
        public int UserId { get; set; }
        public int NumberGuest { get; set; }
        public int Phone { get; set; }
        public bool IsInvited { get; set; }

    }
}
