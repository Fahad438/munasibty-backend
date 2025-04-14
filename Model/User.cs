using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zafaty.Server.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; } 
    
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        [MinLength(10)]
        public string Phone { get; set; }

        public bool IsActive { get; set; } = false;  // الافتراضي يكون غير مفعل

        public string VerificationToken { get; set; }
        public DateTime? VerificationTokenExpiry { get; set; }

        public string? ResetToken { get; set; }
        public DateTime? ResetTokenExpiry { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // علاقة One-to-Many مع Role
       
        public int RoleID { get; set; }
        public Role Role { get; set; } // الربط مع جدول Roles

        // علاقة One-to-Many مع Posts
        public ICollection<Post> Posts { get; set; }

        public ICollection<Session> Sessions { get; set; }
        
        public ICollection<Comments> Comments { get; set; }

        public ICollection<Rating> Rating { get; set; }
        //here it just realtionship with Gust User Have Many Gust 
        public ICollection<Guest> Guests { get; set; }
        public ICollection<TaskUser> Tasks { get; set; }
        public ICollection<Budget> Budgets{ get; set; } = new List<Budget>();



    }

}
