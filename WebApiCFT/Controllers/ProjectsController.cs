using System;
using System.Collections.Generic;
using System.Linq;
using ThreadingTask = System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiCFT.Models;
using Task = WebApiCFT.Models.Task;

namespace WebApiCFT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly CftContext _context;

        public ProjectsController(CftContext context) => _context = context;
        
        // GET api/projects
        [HttpGet]
        public async ThreadingTask.Task<ActionResult<IEnumerable<Project>>> Get([FromQuery]FilterModelBase<Project> filter)
        {
            if (!filter.IsValid(out var errorMessage))
            {
                return BadRequest(new{ message = errorMessage });
            }
            
            //get data only from the specified page
            var pagedProject = filter.Filter(_context.Projects);
            if (await pagedProject.AnyAsync())
            {
                return await pagedProject.ToListAsync();                
            }
            else
            {
                return NotFound();
            }
        }

        // GET api/projects/{id}
        [HttpGet("{id}")]
        public async ThreadingTask.Task<ActionResult<Project>> Get(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            return project;
        }

        // POST api/projects
        [HttpPost]
        public async ThreadingTask.Task<ActionResult<Project>> Post([FromBody] Project project)
        {
            if (project.Id != 0)
            {
                return BadRequest(new { message = "You should not pass 'id' in post request"});
            }
            
            //checking of uniqueness
            if (await _context.Projects.AnyAsync(p => p.Name == project.Name))
            {
                return BadRequest(new { message = "Name should be unique. Project with the same name already exists"});
            }

            //set the creation time
            project.CreationTime = DateTime.Now;
            project.LastModificationTime = DateTime.Now;
            
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction("GET", new Project(){Id = project.Id}, project);
        }

        // PUT api/projects/{id}
        [HttpPut("{id}")]
        public async ThreadingTask.Task<IActionResult> Put(int id, [FromBody] Project project)
        {
            if (id != project.Id)
            {
                return BadRequest(new { message = "Id in the query and id in the body are different"});
            }

            var entry = await _context.Projects.FindAsync(id);
            if (entry != null)
            {
                //if user changed the name, we should check uniqueness of new name
                if (entry.Name != project.Name && await _context.Projects.AnyAsync(p => p.Name == project.Name))
                {
                    return BadRequest(new { message = "This name is already taken"});
                }
                
                //updating
                entry.LastModificationTime = DateTime.Now;
                entry.Name = project.Name;
                entry.Description = project.Description;

                _context.Entry(entry).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            else//no project with the same id
            {
                return NotFound();
            }                
        }

        // DELETE api/projects/{id}
        [HttpDelete("{id}")]
        public async ThreadingTask.Task<IActionResult> Delete(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}