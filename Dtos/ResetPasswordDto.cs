using NuGet.Common;

namespace Zafaty.Server.Dtos
{
    public class ResetPasswordDto
    {
        public string Token { get; set; }      // التوكن الذي تم إرساله عبر البريد الإلكتروني

        public string Password { get; set; }   // كلمة المرور الجديدة    }
    }
}
