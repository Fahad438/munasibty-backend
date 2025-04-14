using Zafaty.Server.Dtos;
using Zafaty.Server.Interfaces;
using Zafaty.Server.Model;

namespace Zafaty.Server.Services
{
    public class GuestService : IGuestsService { 
        private readonly IGuestsRepository _repository;

        public GuestService(IGuestsRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<GuestDto>> GetGuestAsync(int Userid, string? nameGuest, string? side)
        {
            return await _repository.GetAsync(Userid, nameGuest, side);
        }

        public async Task AddGuestAsync(Guest guest)
        {
            await _repository.AddAsync(guest);
        }

        public async Task UpdateGuestAsync(GuestUpdateDTO guesttUpdatDTO, int id)
        {
            var guest = await _repository.GetByIdAsync(guesttUpdatDTO.UserId, id);

            if (guest == null)
            {
                throw new ArgumentException("guest must be not null");
            }


            guest.NameGust = guesttUpdatDTO.NameGust;
            guest.Side = guesttUpdatDTO.Side;
            guest.Phone = guesttUpdatDTO.Phone;
            guest.NumberGuest = guesttUpdatDTO.NumberGuest;
            guest.IsInvited = guesttUpdatDTO.IsInvited;



            await _repository.UpdateAsync(guest);



        }

        public async Task DeleteGuestAsync(int id )
        {
           

            await _repository.DeleteAsync(id);

            
        }
    }
}
