using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;
using WebApplication1.DTOs;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly TaskService _taskService;

        public TasksController(TaskService taskService)
        {
            _taskService = taskService;
        }

        // GET api/tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskResponseDto>>> GetAllTasks()
        {
            var tasks = await _taskService.GetAllTasks();
            var taskResponses = tasks.Select(t => new TaskResponseDto(t.Id, t.Title, t.Completed)).ToList();
            return Ok(taskResponses);
        }

        // GET api/tasks/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TaskResponseDto>> GetTaskById(int id)
        {
            var task = await _taskService.GetTaskById(id);
            if (task is null)
            {
                return NotFound(new { error = "Task not found." });
            }

            var taskResponse = new TaskResponseDto(task.Id, task.Title, task.Completed);
            return Ok(taskResponse);
        }

        // POST api/tasks
        [HttpPost]
        public async Task<ActionResult<TaskResponseDto>> CreateTask([FromBody] CreateTaskDto newTask)
        {
            var createdTask = await _taskService.CreateTask(newTask.Title);
            var taskResponse = new TaskResponseDto(createdTask.Id, createdTask.Title, createdTask.Completed);
            return Ok(taskResponse);
        }

        // PUT api/tasks/{id}
        [HttpPut("{id:int}")]
        public async Task<ActionResult<TaskResponseDto>> UpdateTask(int id, [FromBody] UpdateTaskDto updatedTask)
        {
            var task = await _taskService.UpdateTask(id, updatedTask.Title, updatedTask.Completed);
            if (task is null)
            {
                return NotFound(new { error = "Task not found." });
            }

            var taskResponse = new TaskResponseDto(task.Id, task.Title, task.Completed);
            return Ok(taskResponse);
        }

        // DELETE api/tasks/{id}
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteTask(int id)
        {
            var deleted = await _taskService.DeleteTask(id);
            if (!deleted)
            {
                return NotFound(new { error = "Task not found." });
            }

            return NoContent();
        }
    }
}