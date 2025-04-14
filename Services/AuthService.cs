using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Zafaty.Server.Helpers;
using Zafaty.Server.Interfaces;
using Zafaty.Server.Model;

namespace Zafaty.Server.Services
{
    public class AuthService : IAuthService
    {
        //private readonly UserManager<ApplicationUser> _userManager;

        //private readonly JWT _jwt;

        //private readonly RoleManager<IdentityRole> _roleManager;


        //public AuthService(UserManager<ApplicationUser> userManger, IOptions<JWT> jwt, RoleManager<IdentityRole> roleManager)
        //{
        //    _userManager = userManger;
        //    _jwt = jwt.Value;
        //    _roleManager = roleManager;
        //}




        //public Task<string> ChangePasswordAsync(string userId, string newPassword)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<bool> DeleteRole(string userId, string role)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<bool> DeleteUser(string id)
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task<AuthModel> GetTokenAsync(TokenRequestModel model)
        //{
        //    var authModel = new AuthModel();

        //    var user = await _userManager.FindByEmailAsync(model.Email);

        //    if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
        //    {
        //        authModel.Message = "Email or Password is incorrect!";
        //        return authModel;
        //    }

        //    var jwtSecurityToken = await CreateJwtToken(user);
        //    var rolesList = await _userManager.GetRolesAsync(user);

        //    authModel.IsAuthenticated = true;
        //    authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        //    authModel.Email = user.Email;
        //    authModel.Username = user.UserName;
        //    authModel.ExpiresOn = jwtSecurityToken.ValidTo;
        //    authModel.Roles = rolesList.ToList();

        //    return authModel;
        //}

        //public async Task<AuthModel> RegisterAsync(RegisterModel model)
        //{
        //    // التحقق من وجود المستخدم بناءً على الإيميل
        //    if (await _userManager.FindByEmailAsync(model.Email) != null)
        //    {
        //        return new AuthModel { Message = "Email already exists" };
        //    }

        //    // التحقق من وجود المستخدم بناءً على اسم المستخدم
        //    if (await _userManager.FindByNameAsync(model.Username) != null)
        //    {
        //        return new AuthModel { Message = "Username already exists" };
        //    }

        //    // إنشاء مستخدم جديد بناءً على البيانات القادمة
        //    var user = new ApplicationUser
        //    {
        //        FirstName = model.FirstName,
        //        LastName = model.LastName,
        //        Email = model.Email,
        //        UserName = model.Username,
        //        PhoneNumber=model.Phone

        //    };

        //    // محاولة إنشاء المستخدم في النظام
        //    var result = await _userManager.CreateAsync(user, model.Password);

        //    if (!result.Succeeded)
        //    {
        //        var errors = string.Empty;
        //        foreach (var error in result.Errors)
        //        {
        //            errors += error.Description + Environment.NewLine;
        //        }
        //        return new AuthModel { Message = errors };
        //    }

        //    // تعيين الرول بناءً على المدخلات
        //    var role = string.IsNullOrWhiteSpace(model.Role) ? "User" : model.Role;

        //    // التحقق من صحة الرول (يجب أن يكون موجودًا مسبقًا)
        //    if (!await _roleManager.RoleExistsAsync(role))
        //    {
        //        return new AuthModel { Message = $"Role '{role}' does not exist." };
        //    }

        //    await _userManager.AddToRoleAsync(user, role);

        //    // إنشاء التوكن
        //    var jwtSecurityToken = await CreateJwtToken(user);

        //    // إرجاع النتيجة
        //    return new AuthModel
        //    {
        //        Message = "User created successfully",
        //        ExpiresOn = jwtSecurityToken.ValidTo,
        //        IsAuthenticated = true,
        //        Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
        //        Roles = new List<string> { role },
        //        Username = user.UserName,
        //        Email = user.Email
        //    };
        //}


        //private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        //{
        //    var userClaims = await _userManager.GetClaimsAsync(user);
        //    var roles = await _userManager.GetRolesAsync(user);
        //    var roleClaims = new List<Claim>();

        //    foreach (var role in roles)
        //        roleClaims.Add(new Claim("roles", role));

        //    var claims = new[]
        //    {
        //        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
        //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        //        new Claim("uid", user.Id)
        //    }
        //    .Union(userClaims)
        //    .Union(roleClaims);

        //    var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
        //    var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        //    var jwtSecurityToken = new JwtSecurityToken(
        //        issuer: _jwt.Issuer,
        //        audience: _jwt.Audience,
        //        claims: claims,
        //        expires: DateTime.Now.AddDays(_jwt.DurationInDays),
        //        signingCredentials: signingCredentials);

        //    return jwtSecurityToken;
        //}
        public Task<string> ChangePasswordAsync(string userId, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteRole(string userId, string role)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUser(string id)
        {
            throw new NotImplementedException();
        }

        public Task<AuthModel> GetTokenAsync(TokenRequestModel model)
        {
            throw new NotImplementedException();
        }

        public Task<AuthModel> RegisterAsync(RegisterModel model)
        {
            throw new NotImplementedException();
        }
    }
}
