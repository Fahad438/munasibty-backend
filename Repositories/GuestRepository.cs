using Microsoft.EntityFrameworkCore;
using TestApi.Data;
using Zafaty.Server.Dtos;
using Zafaty.Server.Interfaces;
using Zafaty.Server.Model;

namespace Zafaty.Server.Repositories
{
    public class GuestRepository : IGuestsRepository
    {

        private readonly AppDbContext _db;

        public GuestRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<GuestDto>> GetAsync(int id,string? nameGuest,string? side) {
            var query = _db.Guests.AsQueryable();

            if ( id > 0)
            {
                //we take the id from the user
                query = query.Where(g => g.UserId == id);
            }
            else
            {
                throw new ArgumentException("id must be > 0");
            }

            if (!string.IsNullOrWhiteSpace(nameGuest))
            {
                //we take the name from the user
                query = query.Where(g => g.NameGust.Contains(nameGuest));
            }

            if (!string.IsNullOrWhiteSpace(side))
            {
                //we take the side from the user
                query = query.Where(g => g.Side == side);
            }

            return await query.Select(g => new GuestDto
            {
                Id = g.Id,
                NameGust = g.NameGust,
                Side = g.Side,
                UserId = g.UserId,
                Phone = g.Phone,
                NumberGuest = g.NumberGuest,
                IsInvited = g.IsInvited,
            }).ToListAsync();


        }

        public async Task AddAsync(Guest guest)
        {
            if (guest == null)
            {
                throw new ArgumentException("guest must be not null");

            }
            _db.Guests.Add(guest);
            await _db.SaveChangesAsync();

        }

        public async Task<Guest> GetByIdAsync( int UserId , int id)
        {
            

            return await _db.Guests.FirstOrDefaultAsync(g => g.Id == id && g.UserId == UserId); ;
        }

        public async Task UpdateAsync(Guest guest)
        {

            if (guest == null) {
                throw new ArgumentException("guest must be not null");
                    }
            _db.Guests.Update(guest);
            await _db.SaveChangesAsync();


        }


        public async Task DeleteAsync(int id)
        {
            var guest = await _db.Guests.FirstOrDefaultAsync(g => g.Id == id);

            _db.Remove(guest);
            await _db.SaveChangesAsync();

        }

    }


}


