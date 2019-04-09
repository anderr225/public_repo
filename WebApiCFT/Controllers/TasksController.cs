using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiCFT.Models;
using ThreadingTask = System.Threading.Tasks;

namespace WebApiCFT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
       private readonly CftContext _context;

        public TasksController(CftContext context) => _context = context;
        
        // GET api/tasks
        [HttpGet]
        public async ThreadingTask.Task<ActionResult<IEnumerable<Task>>> Get([FromQuery]FilterModelTask filter)
        {
            if (!filter.IsValid(out var errorMessage))
            {
                return BadRequest(new{ message = errorMessage });
            }
            
            //get filtered data 
            var tasks = filter.Filter(_context.Tasks);
            if (await tasks.AnyAsync())
            {
                return await tasks.ToListAsync();                
            }
            else
            {
                return NotFound();
            }
        }

        // GET api/tasks/{id}
        [HttpGet("{id}")]
        public async ThreadingTask.Task<ActionResult<Task>> Get(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                NotFound();
            }

            return task;
        }

        // POST api/tasks
        [HttpPost]
        public async ThreadingTask.Task<ActionResult<Task>> Post([FromBody] Task task)
        {
            if (task.Id != 0)
            {
                return BadRequest(new { message = "You should not pass 'id' in post request"});
            }
            
            var proj = await _context.Projects.FindAsync(task.ProjectId);
            if (proj == null)
            {
                return BadRequest(new { message = "There is no project with such id"});
            }
            
            if (await _context.Tasks.AnyAsync(t => t.ProjectId == task.ProjectId && t.Name == task.Name))
            {
                return BadRequest(new { message = "This project already has task with the same name"});
            }
            
            //set status and creation time
            task.CreationTime = DateTime.Now;
            task.LastModificationTime = DateTime.Now;
            task.Status = Status.New;
            
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction("GET", new Task{Id = task.Id}, task);
        }

        // PUT api/tasks/{id}
        [HttpPut("{id}")]
        public async ThreadingTask.Task<ActionResult> Put(int id, [FromBody] Task task)
        {
            if (id != task.Id)
            {
                return BadRequest();
            }

            var entry = await _context.Tasks.FindAsync(id);
            if (entry != null)
            {
                //status checking
                if (entry.Status == Status.Closed)
                {
                    return BadRequest(new { message = "You can't change the task with 'CLOSED' status"});
                }

                if (! await _context.Projects.AnyAsync(p => p.Id == task.ProjectId))
                {
                    return BadRequest(new { message = "No project with such id"});
                }
                
                //if unique pair has changed
                if (task.ProjectId != entry.ProjectId || task.Name != entry.Name)
                {
                    if (await _context.Tasks.AnyAsync(p => p.Name == task.Name && p.ProjectId == task.ProjectId))
                    {
                        return BadRequest(new { message = "This project already has task with the same name"});
                    }
                }

                //update
                entry.Status = task.Status;
                entry.Name = task.Name;
                entry.Description = task.Description;
                entry.ProjectId = task.ProjectId;
                entry.Priority = task.Priority;
                entry.LastModificationTime = DateTime.Now;

                _context.Entry(entry).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                
                return NoContent();
            }
            else //didn't find any task with such id
            {
                return NotFound();
            }
        }

        // DELETE api/tasks/{id}
        [HttpDelete("{id}")]
        public async ThreadingTask.Task<ActionResult<Task>> Delete(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return task;
        }
    }
}