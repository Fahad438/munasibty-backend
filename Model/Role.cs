using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Zafaty.Server.Model
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        //العلاقه معا جدول اليوزر
        public ICollection<User> Users { get; set; } // تغيير القائمة من Roles إلى Users
        [Required]
        [StringLength(255)]
        public string RoleName { get; set; }

    }

}
