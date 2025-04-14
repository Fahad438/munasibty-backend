using Zafaty.Server.Dtos;
using Zafaty.Server.Model;

namespace Zafaty.Server.Interfaces
{
    public interface IGuestsService
    {
        Task<IEnumerable<GuestDto>> GetGuestAsync(int Userid,string ? nameGuest,string ? side);
        Task AddGuestAsync(Guest guest);
        Task UpdateGuestAsync(GuestUpdateDTO guest,int id);
        Task DeleteGuestAsync(int id);
    }
}
