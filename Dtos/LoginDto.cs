using System.ComponentModel.DataAnnotations;

namespace Zafaty.Server.Dtos
{
    public class LoginDto
    {
        [Required] // تأكد من أن البريد الإلكتروني مطلوب
        [EmailAddress] // تحقق من أن البريد الإلكتروني في التنسيق الصحيح
        public string Email { get; set; }

        [Required] // تأكد من أن كلمة المرور مطلوبة
        [StringLength(100, MinimumLength = 6)] // تحقق من أن كلمة المرور ليست قصيرة جدًا
        public string Password { get; set; }
    }
}