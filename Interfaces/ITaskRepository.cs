using System.Collections.Generic;
using Zafaty.Server.Model;

namespace Zafaty.Server.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskUser>> GetAllAsync(int UserId,string ?nameTask,string ?TaskType);
        Task<IEnumerable<string>> GetAllCtgoryTask(int UserId);

        Task<TaskUser> GetByIdAsync(int Id);
        Task AddAsync(TaskUser task);
        Task UpdateAsync(TaskUser task);
        Task DeleteAsync(TaskUser task);
    }
}
