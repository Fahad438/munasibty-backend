namespace Zafaty.Server.Model
{
    public class Guest
    {

        public int Id { get; set; }
        public string NameGust { get; set; }

        public int NumberGuest { get; set; }
        public int Phone { get; set; }
        public string Side { get; set; }

        public bool IsInvited { set; get; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
