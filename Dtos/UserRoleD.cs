using System.ComponentModel.DataAnnotations;

namespace Zafaty.Server.Dtos
{
    public class UserRoleD
    {
        [Required]
        public int UserId { get; set; } // ID المستخدم

        [Required]
        public int RoleId { get; set; } // ID الدور
    }
}
