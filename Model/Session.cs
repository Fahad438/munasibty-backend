using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zafaty.Server.Model
{
    public class Session
    {
        [Key]
        public int SessionID { get; set; }

      
        public int UserId { get; set; }

        public string SessionToken { get; set; }
        public User User { get; set; }

    }

}
