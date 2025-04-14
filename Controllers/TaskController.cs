using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Zafaty.Server.Dtos;
using Zafaty.Server.Interfaces;

namespace Zafaty.Server.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    //[Authorize]
    [EnableRateLimiting("UserRateLimit")]


    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }
        [HttpGet("UserId/{UserId}")]
        public async Task<IActionResult> GetAllTask(int UserId, string? nameTask, string? TaskType) {

            if (UserId == null) {
                BadRequest();
            }

            var taskes= await _taskService.GetAllTaskAsync(UserId,nameTask,TaskType);
            return Ok(taskes);
        
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetTaskById(int Id)
        {

            if (Id == null)
            {
                BadRequest();
            }

            var task = await _taskService.GetByIdAsync(Id);
            return Ok(task);

        }

        [HttpPost]
        public async Task<IActionResult> AddTask(CreatTaskeDTO creatTaskeDTO)
        {
            if (creatTaskeDTO == null) {
                BadRequest("Invalid Task data. ");
            }

             await _taskService.AddAsync(creatTaskeDTO);
            return Ok("Task Add ");
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id,CreatTaskeDTO creatTaskeDTO)
        {
            if(creatTaskeDTO == null)
            {
               return BadRequest();
            }
            await _taskService.UpdateAsync(id, creatTaskeDTO);
            return Ok("Update done");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DelteTask(int id)
        {
           
            await _taskService.DeleteAsync(id);
            return Ok("Delete done");
        }

        [HttpGet("tasktype")]
        public async Task<IActionResult> GetAllCtgoryTask(int UserId)
        {
            if (UserId == null)
            {
                BadRequest();
            }

            var taskes = await _taskService.GetAllCtgoryTask(UserId);
            return Ok(taskes);

        }
    }
}
