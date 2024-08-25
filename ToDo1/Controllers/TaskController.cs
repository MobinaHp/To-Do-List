using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDo.Data;
using ToDo.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo.Model.DTO;

namespace ToDo.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ToDoDbContext _context;

        public TaskController(ToDoDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetTaskDto>>> GetTasks()
        {
            var tasks = await _context.Tasks.ToListAsync();
            var taskDtos = tasks.Select(t => new GetTaskDto
            {
                Id = t.Id,
                Name = t.Name,
                DueDate = t.DueDate,
                Starred = t.Starred
            }).ToList();

            return Ok(taskDtos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Task>> GetTask(int id)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            var getTaskDto = new GetTaskDto
            {
                Id = task.Id,
                Name = task.Name,
                DueDate = task.DueDate,
                Starred = task.Starred
            };

            return Ok(getTaskDto);
        }

        [HttpPost]
        public async Task<ActionResult<Task>> CreateTask([FromBody] CreateTaskDto taskDto)
        {
            var task = new Task
            {
                Name = taskDto.Name,
                DueDate = taskDto.DueDate,
                ListId = taskDto.ListId,
                Starred = taskDto.Starred
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return Ok(task);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Task>> UpdateTask(int id, [FromBody] UpdateTaskDto taskDto)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            task.Name = taskDto.Name;
            task.DueDate = taskDto.DueDate;
            task.Starred = taskDto.Starred;
            task.Checked = taskDto.Checked;

            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();

            return Ok(task);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Task>> DeleteTask(int id)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return Ok(task);
        }

        [HttpGet("FilternotCompleted")]
        public async Task<ActionResult<IEnumerable<GetTaskDto>>> GetNotCompletedTasks()
        {
            var tasks = await _context.Tasks
                .Where(t => !t.Checked)
                .ToListAsync();

            var taskDtos = tasks.Select(t => new GetTaskDto
            {
                Id = t.Id,
                Name = t.Name,
                DueDate = t.DueDate,
                Starred = t.Starred
            }).ToList();

            return Ok(taskDtos);
        }

        [HttpGet("Filterstarred")]
        public async Task<ActionResult<IEnumerable<GetTaskDto>>> GetStarredTasks()
        {
            var tasks = await _context.Tasks
                .Where(t => t.Starred)
                .ToListAsync();

            var taskDtos = tasks.Select(t => new GetTaskDto
            {
                Id = t.Id,
                Name = t.Name,
                DueDate = t.DueDate,
                Starred = t.Starred
            }).ToList();

            return Ok(taskDtos);
        }
    }
}
