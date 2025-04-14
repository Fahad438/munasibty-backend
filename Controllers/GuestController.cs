using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Zafaty.Server.Dtos;
using Zafaty.Server.Interfaces;
using Zafaty.Server.Model;

namespace Zafaty.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    [EnableRateLimiting("UserRateLimit")]


    public class GuestController : ControllerBase
    {
        private readonly IGuestsService _guestsService;

        public GuestController(IGuestsService guestsService)
        {
            _guestsService = guestsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetGuest(int UserId, string? nameGuest, string? side)
        {
            if (UserId == 0)
            {
                NotFound();
            }
            var res = await _guestsService.GetGuestAsync(UserId, nameGuest, side);
            return Ok(res);

        }
        [HttpPost]
        public async Task<IActionResult> AddGuest(GuestCreateDTO guestDto)
        {
            if (guestDto == null)
            {
                NotFound();
            }
            var guest = new Guest
            {
                NameGust = guestDto.NameGust,
                Side = guestDto.Side,
                UserId = guestDto.UserId,
                NumberGuest= guestDto.NumberGuest,
                Phone= guestDto.Phone,
                IsInvited= guestDto.IsInvited
            };

            await _guestsService.AddGuestAsync(guest);

            return Ok("Add done");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGuest([FromBody] GuestUpdateDTO guestUpdateDTO, int id)
        {
            // التحقق من صحة المدخلات
            if (guestUpdateDTO == null)
            {
                return BadRequest(new { Message = "Guest data must not be null." });
            }


            try
            {
                // استدعاء الخدمة لتحديث الضيف
                await _guestsService.UpdateGuestAsync(guestUpdateDTO, id);
                return Ok(new { Message = "Guest updated successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                // إذا لم يتم العثور على الضيف
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                // التعامل مع الأخطاء العامة
                return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteGuest(int id)
        {
            if (id < 0)
            {
                BadRequest("Id Must Be greather then 0");
            }
            try
            {
                // استدعاء الخدمة لتحديث الضيف
                await _guestsService.DeleteGuestAsync(id);
                return Ok(new { Message = "Guest Deleted successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                // إذا لم يتم العثور على الضيف
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                // التعامل مع الأخطاء العامة
                return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }

        }
    }
}
