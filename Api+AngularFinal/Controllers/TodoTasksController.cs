using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api_AngularFinal.Models;
using Microsoft.AspNetCore.Authorization;
using Api_AngularFinal.ViewModel;

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
        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<TodoTaskFinal>> GetTodoTask()
        {
            string userId = User.Claims.First(c => c.Type == "UserId").Value;
            TodoTaskFinal todoTaskFinal = new TodoTaskFinal();
            var task =  _context.TodoTask.Where(t => t.UserId == userId);
            var go = _context.eventsGoPeoples.ToList();
           List <TodoTaskFinal> todoTaskFinal1 = new List<TodoTaskFinal>();
            foreach (var item in task)
            {
                
                    var gmn = go.Where(g => g.TaskId == item.Id && g.GMN == 1).ToList();
                    var gmn1 = go.Where(g => g.TaskId == item.Id && g.GMN == 2).ToList();
                    var gmn2 = go.Where(g => g.TaskId == item.Id && g.GMN == 3).ToList();
                    todoTaskFinal.Id = item.Id;
                    todoTaskFinal.Task = item.Task;
                    todoTaskFinal.Date = item.Date;
                    todoTaskFinal.Description = item.Description;
                    todoTaskFinal.Place = item.Place;
                    todoTaskFinal.Typego = gmn.Count();
                    todoTaskFinal.Typemay = gmn1.Count();
                    todoTaskFinal.TypeNinterest = gmn2.Count();
                    todoTaskFinal1.Add(todoTaskFinal);
                
                
            }
            
            return Ok(todoTaskFinal1);           
        }
        // GET: api/TodoTasks/5
        [Authorize]
        [HttpGet("{id}")]
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
        [Authorize]
        [HttpPut("{id}")]
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
            task.Place = todoTask.Place;
            task.Description = todoTask.Description;
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
