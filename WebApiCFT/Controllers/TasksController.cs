using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiCFT.Models;

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
        public ActionResult<IEnumerable<Task>> Get([FromQuery]FilterModelTask filter)
        {
            if (!filter.IsValid(out var errorMessage))
            {
                return BadRequest(new{ message = errorMessage });
            }
            
            //get filtered data 
            var tasks = filter.Filter(_context.Tasks);
            
            return tasks.ToList();
        }

        // GET api/tasks/{id}
        [HttpGet("{id}")]
        public ActionResult<Task> Get(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task == null)
            {
                NotFound();
            }

            return task;
        }

        // POST api/tasks
        [HttpPost]
        public ActionResult<Task> Post([FromBody] Task task)
        {
            if (task.Id != 0)
            {
                return BadRequest(new { message = "You should not pass 'id' in post request"});
            }
            
            var proj = _context.Projects.Find(task.ProjectId);
            if (proj == null)
            {
                return BadRequest(new { message = "There is no project with such id"});
            }
            
            //set status and creation time
            task.CreationTime = DateTime.Now;
            task.LastModificationTime = DateTime.Now;
            task.Status = Status.New;
            
            _context.Tasks.Add(task);
            _context.SaveChanges();
            
            return CreatedAtAction("GET", new Task(){Id = task.Id}, task);
        }

        // PUT api/tasks/{id}
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Task task)
        {
            if (id != task.Id)
            {
                return BadRequest();
            }

            var entry = _context.Tasks.Find(id);
            if (entry != null)
            {
                //status checking
                if (task.Status == Status.Closed)
                {
                    return BadRequest(new { message = "You can't change the task with 'CLOSED' status"});
                }
                
                //user is not allowed to change the creation time
                if (task.CreationTime != entry.CreationTime)
                {
                    task.CreationTime = entry.CreationTime;
                }
                task.LastModificationTime = DateTime.Now;

                _context.Entry(task).State = EntityState.Modified;
                _context.SaveChanges();
                return NoContent();
            
            }
            else //didn't find any task with such id
            {
                return NotFound();
            }
        }

        // DELETE api/tasks/{id}
        [HttpDelete("{id}")]
        public ActionResult<Task> Delete(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(task);
            _context.SaveChanges();

            return task;
        }
    }
}