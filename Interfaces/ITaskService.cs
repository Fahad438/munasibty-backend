using System.Collections.Generic;
using Zafaty.Server.Dtos;

namespace Zafaty.Server.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskeDTO>> GetAllTaskAsync(int UserId, string? nameTask, string? TaskType);
        Task<IEnumerable<string>> GetAllCtgoryTask(int UserId);

        Task<TaskeDTO> GetByIdAsync(int id);
        Task AddAsync(CreatTaskeDTO taskDto);
        Task UpdateAsync(int id, CreatTaskeDTO taskDto);
        Task DeleteAsync(int id);




    }
}
