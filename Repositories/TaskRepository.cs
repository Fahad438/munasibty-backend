using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using TestApi.Data;
using Zafaty.Server.Dtos;
using Zafaty.Server.Interfaces;
using Zafaty.Server.Model;

namespace Zafaty.Server.Repositories
{
    public class TaskRepository:ITaskRepository
    {
        private readonly AppDbContext _db;
        public TaskRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<TaskUser>> GetAllAsync(int UserId,string? NameTask,string? TaskType)
        {
            var tasks = _db.Tasks.AsQueryable(); // تأكد أن لديك DbSet<TaskUser> في _context

            // تطبيق الفلاتر فقط عند وجود قيم مدخلة
            if (UserId > 0)
                tasks = tasks.Where(t => t.UserId == UserId);

            if (!string.IsNullOrEmpty(NameTask))
                tasks = tasks.Where(t => t.Name.Contains(NameTask));

            if (!string.IsNullOrEmpty(TaskType))
                tasks = tasks.Where(t => t.CategoryTask == TaskType);

            return await tasks.Select(t => new TaskUser
            {
                Id = t.Id,
                Name = t.Name,
                PersonDoIt = t.PersonDoIt,
                DateTask = t.DateTask,
                CategoryTask = t.CategoryTask,
                IsTaskDone = t.IsTaskDone,
                UserId = t.UserId

            }).ToListAsync();
        }
        public async Task<TaskUser> GetByIdAsync(int Id)
        {
            return await _db.Tasks.FindAsync(Id);
               
        }

        public async Task AddAsync(TaskUser task)
        {
            _db.Tasks.Add(task);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(TaskUser task)
        {
            _db.Tasks.Update(task);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(TaskUser task)
        {
            _db.Tasks.Remove(task);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<string>> GetAllCtgoryTask(int UserId)
        {
            return await _db.Tasks
                .Where(t => t.UserId == UserId)
                .Select(t => t.CategoryTask)
                .Distinct()
                .ToListAsync(); 
        }
    }
}
