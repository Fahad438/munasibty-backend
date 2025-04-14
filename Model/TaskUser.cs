using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Zafaty.Server.Model
{
    public class TaskUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PersonDoIt { get; set; }
        public string DateTask { get; set; }
        public string CategoryTask { get; set; }
        public bool IsTaskDone { get; set; }
        //relationship
        public User User { get; set; }
        public int UserId { get; set; } 

    }
}
