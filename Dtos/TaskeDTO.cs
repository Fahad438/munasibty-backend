using System.ComponentModel.DataAnnotations;

namespace Zafaty.Server.Dtos
{
    public class TaskeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PersonDoIt { get; set; }
        public string DateTask { get; set; }
        public string CategoryTask { get; set; }
        public bool IsTaskDone { get; set; }
        public int UserId { get; set; }
    }
}
