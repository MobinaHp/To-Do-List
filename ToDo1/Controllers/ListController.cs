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
    [Route("api/lists")]
    [ApiController]
    public class ListController : ControllerBase
    {
        private readonly ToDoDbContext _context;

        public ListController(ToDoDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetListDto>>> GetLists()
        {
            var lists = await _context.Lists
                .Include(l => l.Tasks)
                .Select(l => new GetListDto
                {
                    Id = l.Id,
                    Name = l.Name,
                    Tasks = l.Tasks.Select(t => new GetTaskDto
                    {
                        Id = t.Id,
                        Name = t.Name,
                        DueDate = t.DueDate,
                        Starred = t.Starred
                    }).ToList()
                })
                .ToListAsync();

            return Ok(lists);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetListDto>> GetList(int id)
        {
            var list = await _context.Lists
                .Include(l => l.Tasks)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (list == null)
            {
                return NotFound();
            }

            var getListDto = new GetListDto
            {
                Id = list.Id,
                Name = list.Name,
                Tasks = list.Tasks.Select(t => new GetTaskDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    DueDate = t.DueDate,
                    Starred = t.Starred
                }).ToList()
            };

            return Ok(getListDto);
        }

        [HttpGet("FilterListId")]
        public async Task<ActionResult<IEnumerable<GetListDto>>> FilterListId([FromQuery] int listId)
        {
            var filteredLists = await _context.Lists
                .Include(l => l.Tasks)
                .Where(l => l.Id == listId)
                .Select(l => new GetListDto
                {
                    Id = l.Id,
                    Name = l.Name,
                    Tasks = l.Tasks.Select(t => new GetTaskDto
                    {
                        Id = t.Id,
                        Name = t.Name,
                        DueDate = t.DueDate,
                        Starred = t.Starred
                    }).ToList()
                })
                .ToListAsync();

            return Ok(filteredLists);
        }

        [HttpPost]
        public async Task<ActionResult<List>> CreateList([FromBody] CreateListDto listDto)
        {
            var list = new List 
            {
                Name = listDto.Name
            };
            _context.Lists.Add(list);
            await _context.SaveChangesAsync();

            return Ok(list);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<List>> UpdateList(int id, [FromBody] UpdateListDto listDto)
        {
            var list = await _context.Lists.FirstOrDefaultAsync(t => t.Id == id);
            if(list == null)
            {
                return NotFound();
            }
            list.Name = listDto.Name;

            _context.Lists.Update(list);
            await _context.SaveChangesAsync();

            return Ok(list);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<List>> DeleteList(int id)
        {
            var list = await _context.Lists.FindAsync(id);
            if (list == null)
            {
                return NotFound();
            }

            _context.Lists.Remove(list);
            await _context.SaveChangesAsync();

            return Ok(list);
        }

        private bool ListExists(int id)
        {
            return _context.Lists.Any(e => e.Id == id);
        }
    }
}
