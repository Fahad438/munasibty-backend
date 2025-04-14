using System.ComponentModel.DataAnnotations;
using Zafaty.Server.Model;

namespace Zafaty.Server.Dtos
{
    public class CreatTaskeDTO
    {
        [Required,MaxLength(100)]
        public string Name { get; set; }
        [Required, MaxLength(50)]
        public string PersonDoIt { get; set; }
        [Required, MaxLength(50)]
        public string DateTask { get; set; }
        [Required, MaxLength(50)]
        public string CategoryTask { get; set; }
        [Required]
        public bool IsTaskDone { get; set; }
        [Required]
        public int UserId { get; set; }

    }
}
