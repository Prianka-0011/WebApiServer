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
            
            var task =  _context.TodoTask.Where(t => t.UserId == userId).ToList();
            var go = _context.eventsGoPeoples.ToList();
           List <TodoTaskFinal> todoTaskFinal1 = new List<TodoTaskFinal>();
            foreach (var item in task)
            {
                TodoTaskFinal Final = new TodoTaskFinal();

                  var gmn = go.Where(g => g.TaskId == item.Id && g.GMN == 1).ToList();
                    var gmn1 = go.Where(g => g.TaskId == item.Id && g.GMN == 2).ToList();
                    var gmn2 = go.Where(g => g.TaskId == item.Id && g.GMN == 3).ToList();
                     Final.Id = item.Id;
                     Final.Task = item.Task;
                     Final.UserId = item.UserId;
                     Final.Date = item.Date;
                     Final.Description = item.Description;
                     Final.Place = item.Place;
                     Final.Typego = gmn.Count();
                     Final.Typemay = gmn1.Count();
                     Final.TypeNinterest = gmn2.Count();
                    todoTaskFinal1.Add(Final);
 
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
        public async Task<IActionResult> PutTodoTask(Guid Id, TodoTaskFinal todoTaskFinal)
        {

            
            if (Id != todoTaskFinal.Id)
            {
                return BadRequest();
            }

                TodoTask todoTask = new TodoTask();
                todoTask.Task = todoTaskFinal.Task;
                todoTask.Date= todoTaskFinal.Date;
                todoTask.Description = todoTaskFinal.Description;
                todoTask.Place = todoTaskFinal.Place;
                _context.Entry(todoTask).State = EntityState.Modified;
            
           

            try
            {
                string userId = User.Claims.First(c => c.Type == "UserId").Value;
                if (todoTaskFinal.UserId==userId)
                {
                    await _context.SaveChangesAsync();
                }
                
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoTaskExists(Id))
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
