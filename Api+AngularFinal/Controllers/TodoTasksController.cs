using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api_AngularFinal.Models;
using Microsoft.AspNetCore.Authorization;

namespace Api_AngularFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoTasksController : ControllerBase
    {
        private readonly AuthenticationContex _context;

        public TodoTasksController(AuthenticationContex context)
        {
            _context = context;
        }

        // GET: api/TodoTasks
        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<TodoTask>> GetTodoTask()
        {
            string userId = User.Claims.First(c => c.Type == "UserId").Value;
            var task =  _context.TodoTask.Where(t => t.UserId == userId);
            return Ok(task);           
        }

        // GET: api/TodoTasks/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<TodoTask>> GetTodoTask(Guid id)
        {
            var todoTask = await _context.TodoTask.FindAsync(id);
            string userId = User.Claims.First(c => c.Type == "UserId").Value;
            if (todoTask == null)
            {
                return NotFound();
            }
            if(todoTask.UserId==userId)
            {
                return todoTask;
            }
            else
            {
                return NotFound();
            }
            
        }

        // PUT: api/TodoTasks/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutTodoTask(Guid id, TodoTask todoTask)
        {
            string userId = User.Claims.First(c => c.Type == "UserId").Value;
            if (id != todoTask.Id)
            {
                return BadRequest();
            }

            _context.Entry(todoTask).State = EntityState.Modified;

            try
            {
                if(todoTask.UserId==userId)
                {
                    await _context.SaveChangesAsync();
                }
                
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoTaskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TodoTasks
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<TodoTask>> PostTodoTask(TodoTaskVM todoTask)
        {
            string userId = User.Claims.First(c => c.Type == "UserId").Value;
            TodoTask task = new TodoTask();
            task.Task = todoTask.Task;
            task.Date = todoTask.Date;
            task.UserId = userId;
            _context.TodoTask.Add(task);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTodoTask", new { id = todoTask.Id }, todoTask);
        }
 
        // DELETE: api/TodoTasks/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<TodoTask>> DeleteTodoTask(Guid id)
        {
            string userId = User.Claims.First(c => c.Type == "UserId").Value;
            var todoTask = await _context.TodoTask.FindAsync(id);
            if (todoTask == null||userId==null)
            {
                return NotFound();
            }
            if(todoTask.UserId==userId)
            {
                _context.TodoTask.Remove(todoTask);
                await _context.SaveChangesAsync();
                return todoTask;
            }
            else
            {
                return NotFound();
            }
           

            
        }

        private bool TodoTaskExists(Guid id)
        {
            return _context.TodoTask.Any(e => e.Id == id);
        }
    }
}
