using MailKit.Security;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TestApi.Data;
using Zafaty.Server.Dtos;
using Zafaty.Server.Model;

namespace Zafaty.Server.Controllers
{

    [Route("api/[controller]")]

    [ApiController]
    [EnableRateLimiting("UserRateLimit")]

    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IConfiguration _config;

        public AuthController(AppDbContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserD userDto)
        {
            if (userDto == null)
                return BadRequest("User data is required.");

            var existingUser = await _db.Users.FirstOrDefaultAsync(u => u.Email == userDto.Email);
            if (existingUser != null)
                return BadRequest("Email is already taken.");

            var role = await _db.Roles.FirstOrDefaultAsync(r => r.RoleName == userDto.Role);
            if (role == null)
                return BadRequest("Role does not exist.");

            var user = new User
            {
                Email = userDto.Email,
                Name = userDto.Name,
                Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
                Phone = userDto.Phone,
                RoleID = role.Id,
                IsActive = false, // حساب غير مفعل بشكل افتراضي
                VerificationToken = Guid.NewGuid().ToString(),
                VerificationTokenExpiry = DateTime.UtcNow.AddHours(1) // الرمز صالح لمدة ساعة
            };

            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();

            // إرسال البريد الإلكتروني للتحقق
            var verificationLink = $"{_config["App:BaseUrl"]}/verify-email/{user.VerificationToken}";
            await SendVerificationEmail(user.Email, verificationLink);

            return Ok("Registration successful. Please check your email for verification link.");
        }

       

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == userDto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(userDto.Password, user.Password)) // تحقق من كلمة المرور
            {
                return Unauthorized("Invalid email or password."); // إرجاع خطأ إذا كانت البيانات غير صحيحة
            }

            // إضافة تحقق من حالة تفعيل الحساب
            if (!user.IsActive)
            {
                return Unauthorized("Your account is not activated. Please verify your email.");
            }

            var userRole = await _db.Roles.FirstOrDefaultAsync(r => r.Id == user.RoleID);

            var tokenString = GenerateJwtToken(user, userRole?.RoleName ?? "User");

            return Ok(new
            {
                message = "Login successful",
                token = tokenString,
                userId = user.Id,
                userRole = userRole?.RoleName
            });
        }
        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail([FromBody] string token)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.VerificationToken == token);
            if (user == null || user.VerificationTokenExpiry < DateTime.UtcNow)
            {
                return BadRequest("Invalid or expired verification token.");
            }

            user.IsActive = true; // تفعيل الحساب
            user.VerificationToken = "null"; // حذف الرمز بعد التفعيل

            await _db.SaveChangesAsync();

            return Ok("Your account has been activated.");
            //return Redirect("/login"); // أو يمكنك تعديلها لتوجه إلى URL آخر حسب حاجتك

        }
       [HttpPost("forget-password")]
public async Task<IActionResult> ForgetPassword([FromBody]string email)
{
    var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);

    // إذا لم يتم العثور على المستخدم
    if (user == null)
    {
        return BadRequest("Email not found.");
    }

    // إذا كانت صلاحية التوكن قد انتهت أو لم يكن هناك توكن، قم بإنشاء توكن جديد
    if (string.IsNullOrEmpty(user.ResetToken) || user.ResetTokenExpiry < DateTime.UtcNow)
    {
        user.ResetToken = Guid.NewGuid().ToString();  // إنشاء توكن جديد
        user.ResetTokenExpiry = DateTime.UtcNow.AddMinutes(10);  // تحديد صلاحية التوكن الجديد
    }

    // حفظ التغييرات في قاعدة البيانات
    try
    {
        await _db.SaveChangesAsync();
    }
    catch (Exception ex)
    {
        return StatusCode(500, "Error saving changes.");
    }

    // إنشاء رابط التحقق باستخدام التوكن الجديد
    var verificationLink = $"{_config["App:BaseUrl"]}/reast-password/{user.ResetToken}";

    // إرسال الرابط إلى البريد الإلكتروني للمستخدم
    await SendPasswordResetEmail(user.Email, verificationLink);

    return Ok("A new reset link has been sent to your email.");
}



        [HttpPost("verify-reset-token")]
        public async Task<IActionResult> VerifyResetToken([FromBody] string token)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.ResetToken == token);
            if (user == null || user.ResetTokenExpiry < DateTime.UtcNow)
            {
                return BadRequest("Invalid or expired token.");
            }

            return Ok(new { message = "Token is valid" });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto model)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.ResetToken == model.Token);
            if (user == null || user.ResetTokenExpiry < DateTime.UtcNow)
            {
                return BadRequest("Invalid or expired token.");
            }

            // تحديث كلمة المرور
            user.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
            user.ResetToken = "";  // إلغاء التوكن بعد إعادة تعيين كلمة المرور
            user.ResetTokenExpiry = null;  // إلغاء صلاحية التوكن
            await _db.SaveChangesAsync();

            return Ok(new { success = true, message = "Password reset successfully" });
        }


        [HttpPatch("user/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] JsonPatchDocument<User> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest("Invalid patch document.");
            }

            var user = await _db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            // تطبيق التعديلات على المستخدم
            patchDoc.ApplyTo(user, ModelState);

            // تحقق من الأخطاء في الـ ModelState
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // تحديث قاعدة البيانات
            _db.Users.Update(user);
            await _db.SaveChangesAsync();

            return NoContent(); // تعني أنه تم التحديث بنجاح
        }
        [HttpPatch("password/{id}")]
        public async Task<IActionResult> UpdatePassword(int id, [FromBody] JsonPatchDocument<UpdatePasswordDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest("Invalid patch document.");
            }

            var user = await _db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            // استلام البيانات المحدثة عبر الـ JsonPatch
            var dto = new UpdatePasswordDto();
            patchDoc.ApplyTo(dto, ModelState);

            // التحقق من الباسورد القديم
            if (!BCrypt.Net.BCrypt.Verify(dto.OldPassword, user.Password))
            {
                return BadRequest("Invalid old password.");
            }

            // تشفير الباسورد الجديد
            user.Password = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);

            // تحقق من الأخطاء في الـ ModelState
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // تحديث قاعدة البيانات
            _db.Users.Update(user);
            await _db.SaveChangesAsync();

            return NoContent(); // تعني أنه تم التحديث بنجاح
        }
        [HttpPatch("email/{id}")]

        public async Task<IActionResult> UpdateEamil(int id, [FromBody] JsonPatchDocument<UpdateEamilDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest("Invalid patch document.");
            }
            var user = await _db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            // استلام البيانات المحدثة عبر الـ JsonPatch
            var dto = new UpdateEamilDto();
            patchDoc.ApplyTo(dto, ModelState);
            //// التحقق من صحة كلمة المرور
            //if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            //{
            //    return BadRequest("Invalid password.");
            //}
            // التحقق من عدم وجود مستخدم آخر بنفس البريد الإلكتروني
            var existingUser = await _db.Users.FirstOrDefaultAsync(u => u.Email == dto.NewEmail);
            if (existingUser != null)
            {
                return BadRequest("Email is already taken.");
            }


            // تحديث البريد الإلكتروني
            user.Email = dto.NewEmail;
            // تحقق من الأخطاء في الـ ModelState
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // تحديث قاعدة البيانات
            _db.Users.Update(user);
            await _db.SaveChangesAsync();
            return NoContent(); // تعني أنه تم التحديث بنج
        }

        private async Task SendVerificationEmail(string email, string verificationLink)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("monasabh", "imonasabh@gmail.com"));
            message.To.Add(new MailboxAddress("", email));
            message.Subject = "تفعيل عنوان بريدك الإلكتروني";

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = $"<p>مرحبًا،</p><p>شكراً لتسجيلك في موقعنا! من فضلك، اضغط على الرابط أدناه لتفعيل حسابك:</p><p><a href='{verificationLink}' style='background-color: #6D28D9; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px;'>تفعيل البريد الإلكتروني</a></p><p>إذا لم تكن قد قمت بالتسجيل، يمكنك تجاهل هذه الرسالة.</p>"
            };

            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;  // تجاهل التحقق من الشهادة
                    await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync("imonasabh@gmail.com", "xrwuwtxdxgarspkm");
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error sending email: {ex.Message}");
                }
            }
        }
        private async Task SendPasswordResetEmail(string email, string resetLink)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Monasabh Support", "imonasabh@gmail.com"));
            message.To.Add(new MailboxAddress("", email));
            message.Subject = "إعادة تعيين كلمة المرور";

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = $"<p>مرحبًا،</p><p>لقد طلبت إعادة تعيين كلمة المرور الخاصة بك على موقعنا. من فضلك، اضغط على الرابط أدناه لإعادة تعيين كلمة المرور الخاصة بك:</p><p><a href='{resetLink}' style='background-color: #6D28D9; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px;'>إعادة تعيين كلمة المرور</a></p><p>إذا لم تطلب إعادة تعيين كلمة المرور، يمكنك تجاهل هذه الرسالة.</p>"
            };

            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;  // تجاهل التحقق من الشهادة
                    await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync("imonasabh@gmail.com", "xrwuwtxdxgarspkm");
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error sending email: {ex.Message}");
                }
            }
        }


        private string GenerateJwtToken(User user, string role)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("id", user.Id.ToString()),
                new Claim("email", user.Email),
                  new Claim("name", user.Name),
                new Claim("role", role)
            };

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }

        
    }
}
