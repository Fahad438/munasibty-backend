using Zafaty.Server.Dtos;
using Zafaty.Server.Interfaces;
using Zafaty.Server.Model;
using Zafaty.Server.Repositories;

namespace Zafaty.Server.Services
{
    public class TaskService:ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public async Task<IEnumerable<TaskeDTO>> GetAllTaskAsync(int UserId, string? nameTask, string? TaskType)
        {
            try
            {
                // استدعاء الدالة غير المتزامنة والانتظار للحصول على النتيجة
                var taskUsers = await _taskRepository.GetAllAsync(UserId,nameTask,TaskType);

                // التحقق من وجود بيانات
                if (taskUsers == null || !taskUsers.Any())
                {
                    // إذا لم توجد مهام، إرجاع قائمة فارغة
                    return new List<TaskeDTO>();
                }

                // تحويل البيانات من TaskUser إلى TaskDTO
                var taskDTOs = taskUsers.Select(t => new TaskeDTO
                {
                    Id = t.Id,
                    Name = t.Name,
                    CategoryTask = t.CategoryTask,
                    DateTask = t.DateTask,
                    IsTaskDone = t.IsTaskDone,
                    PersonDoIt = t.PersonDoIt,
                    UserId=t.UserId
                });

                return taskDTOs;
            }
            catch (Exception ex)
            {
                // رفع استثناء مخصص أو تسجيل الأخطاء إذا كانت هناك مشكلة في استرجاع البيانات
                throw new Exception("An error occurred while fetching tasks.", ex);
            }

        }
        public async Task<TaskeDTO> GetByIdAsync(int id)
        {
            try
            {
                // استدعاء الدالة غير المتزامنة والانتظار للحصول على النتيجة
                var taskUsers = await _taskRepository.GetByIdAsync(id);

                // التحقق من وجود بيانات
                if (taskUsers == null)
                {
                    // إذا لم توجد مهام، إرجاع قائمة فارغة
                    return null;
                }

                // تحويل البيانات من TaskUser إلى TaskDTO
                var taskDTOs = new TaskeDTO
                {
                    Id = taskUsers.Id,
                    Name = taskUsers.Name,
                    CategoryTask = taskUsers.CategoryTask,
                    DateTask = taskUsers.DateTask,
                    IsTaskDone = taskUsers.IsTaskDone,
                    PersonDoIt = taskUsers.PersonDoIt,
                    UserId = taskUsers.UserId
                };

                return taskDTOs;
            }
            catch (Exception ex)
            {
                // رفع استثناء مخصص أو تسجيل الأخطاء إذا كانت هناك مشكلة في استرجاع البيانات
                throw new Exception("An error occurred while fetching tasks.", ex);
            }

        }
        public async Task AddAsync(CreatTaskeDTO taskDto)
        {
            var task = new TaskUser
            {
                
                UserId = taskDto.UserId,
                Name = taskDto.Name,
                CategoryTask = taskDto.CategoryTask,
                DateTask = taskDto.DateTask,
                IsTaskDone = taskDto.IsTaskDone,
                PersonDoIt = taskDto.PersonDoIt,


            };

            
            await _taskRepository.AddAsync(task);
        }

        public async Task UpdateAsync(int id, CreatTaskeDTO taskDto)
        {
            try
            {
                var existTask = await _taskRepository.GetByIdAsync(id);

                if (existTask == null)
                {

                }
                existTask.Name = taskDto.Name;
                existTask.PersonDoIt = taskDto.PersonDoIt;
                existTask.DateTask = taskDto.DateTask;
                existTask.CategoryTask = taskDto.CategoryTask;
                existTask.IsTaskDone = taskDto.IsTaskDone;

                await _taskRepository.UpdateAsync(existTask);
            }
            catch (Exception ex)
            {
                // رفع استثناء مخصص أو تسجيل الأخطاء إذا كانت هناك مشكلة في استرجاع البيانات
                throw new Exception("An error occurred while fetching tasks.", ex);
            }

        }

        public async Task DeleteAsync(int id)
        {

            try
            {
                var existTask = await _taskRepository.GetByIdAsync(id);

                if (existTask == null)
                {

                }
                
                await _taskRepository.DeleteAsync(existTask);
            }
            catch (Exception ex)
            {
                // رفع استثناء مخصص أو تسجيل الأخطاء إذا كانت هناك مشكلة في استرجاع البيانات
                throw new Exception("An error occurred while fetching tasks.", ex);
            }

        }

        public async Task<IEnumerable<string>> GetAllCtgoryTask(int UserId)
        {
            try
            {
                // استدعاء الدالة غير المتزامنة والانتظار للحصول على النتيجة
                var taskUsers = await _taskRepository.GetAllCtgoryTask(UserId);

                // التحقق من وجود بيانات
                if (taskUsers == null || !taskUsers.Any())
                {
                    // إذا لم توجد مهام، إرجاع قائمة فارغة
                    return new List<string>();
                }

                return taskUsers;
            }
            catch (Exception ex)
            {
                // رفع استثناء مخصص أو تسجيل الأخطاء إذا كانت هناك مشكلة في استرجاع البيانات
                throw new Exception("An error occurred while fetching tasks.", ex);
            }
        }
    }

    }


