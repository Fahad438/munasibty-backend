using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Zafaty.Server.Model;


namespace TestApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<Comments> Comments { get; set; }

        public DbSet<Category> Category { get; set; }
        public DbSet<Rating> Rating { get; set; }

        public DbSet<Role> Roles { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<TaskUser> Tasks { get; set; }
        public DbSet<Budget> Budget { get; set; }
        public DbSet<Expense> Expense { get; set; }
        


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Comments>()
            .HasOne(c => c.Post)
            .WithMany(p => p.Comments)
            .HasForeignKey(c => c.PostId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Rating>()
                .HasOne(r => r.Post)
                .WithMany(p => p.Rating)
                .HasForeignKey(r => r.PostId)
                .OnDelete(DeleteBehavior.Cascade);



            //newLine
            //modelBuilder.Entity<User>()
            //    .HasIndex(u => u.Email)
            //    .IsUnique();


            base.OnModelCreating(modelBuilder);

        

        }


    }
}
