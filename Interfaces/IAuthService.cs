using Zafaty.Server.Model;

namespace Zafaty.Server.Interfaces
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAsync(RegisterModel model);
        Task<AuthModel> GetTokenAsync(TokenRequestModel model);
        //Task<string> AddRoleAsync(AddRoleModel model);
        Task<bool> DeleteUser(string id);
        Task<string> ChangePasswordAsync(string userId, string newPassword);

        Task<bool> DeleteRole(string userId, string role);
    }
}
