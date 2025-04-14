using System.ComponentModel.DataAnnotations;

namespace Zafaty.Server.Dtos
{
    public class UserD
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required] // تأكد من إضافة هذا السطر للتحقق من أن كلمة المرور مطلوبة
        public string Password { get; set; }
        [Required]
        [MaxLength(50)]
        [Phone]
        public string Phone { get; set; }
        [Required]
        public string Role { get; set; } // إضافة خاصية الرول

    }
}
