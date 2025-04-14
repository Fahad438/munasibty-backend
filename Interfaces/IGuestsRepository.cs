using TestApi.Data;
using Zafaty.Server.Dtos;
using Zafaty.Server.Model;

namespace Zafaty.Server.Interfaces
{
    public interface IGuestsRepository
    {
        Task<IEnumerable<GuestDto>> GetAsync(int Userid, string? nameGuest, string? side);
        Task AddAsync(Guest guest);
        Task UpdateAsync(Guest guest);
        Task<Guest> GetByIdAsync(int id, int userId);
        Task DeleteAsync(int id);



    }
}
