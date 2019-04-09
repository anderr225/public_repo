using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public ActionResult<IEnumerable<Project>> Get([FromQuery]FilterModelBase<Project> filter)
        {
            if (!filter.IsValid(out var errorMessage))
            {
                return BadRequest(new{ message = errorMessage });
            }
            
            //get data only from the specified page
            var pagedProject = filter.Filter(_context.Projects);
            
            return pagedProject.ToList();
        }

        // GET api/projects/{id}
        [HttpGet("{id}")]
        public ActionResult<Project> Get(int id)
        {
            var project = _context.Projects.Find(id);
            if (project == null)
            {
                return NotFound();
            }

            return project;
        }

        // POST api/projects
        [HttpPost]
        public ActionResult<Project> Post([FromBody] Project project)
        {
            if (project.Id != 0)
            {
                return BadRequest(new { message = "You should not pass 'id' in post request"});
            }
            
            //set the creation time
            project.CreationTime = DateTime.Now;
            project.LastModificationTime = DateTime.Now;
            
            _context.Projects.Add(project);
            _context.SaveChanges();
            
            return CreatedAtAction("GET", new Project(){Id = project.Id}, project);
        }

        // PUT api/projects/{id}
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Project project)
        {
            if (id != project.Id)
            {
                return BadRequest(new { message = "Id in the query and id in the body are different"});
            }

            var entry = _context.Projects.Find(id);
            if (entry != null)
            {
                //user is not allowed to change the creation time
                if (project.CreationTime != entry.CreationTime)
                {
                    project.CreationTime = entry.CreationTime;
                }
                project.LastModificationTime = DateTime.Now;
                
                _context.Entry(project).State = EntityState.Modified;
                _context.SaveChanges();
                return NoContent();
            }
            else//no project with the same id
            {
                return NotFound();
            }                
        }

        // DELETE api/projects/{id}
        [HttpDelete("{id}")]
        public ActionResult<Project> Delete(int id)
        {
            var project = _context.Projects.Find(id);
            if (project == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(project);
            _context.SaveChanges();

            return project;
        }
    }
}