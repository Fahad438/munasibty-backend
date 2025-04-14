using Zafaty.Server.Model;

namespace Zafaty.Server.Dtos
{
    public class GuestDto
    {
        public int Id { get; set; }
        public string NameGust { get; set; }
        public string Side { get; set; }
        public int UserId { get; set; }
        public int NumberGuest { get; set; }
        public int Phone { get; set; }
        public bool IsInvited { get; set; }


    }
}
