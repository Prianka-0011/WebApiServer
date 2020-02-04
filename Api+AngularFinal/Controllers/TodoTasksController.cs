using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api_AngularFinal.Models;

namespace Api_AngularFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoTasksController : ControllerBase
    {
        private readonly AuthenticationContext _context;

        public TodoTasksController(AuthenticationContext context)
        {
            _context = context;
        }

        // GET: api/TodoTasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoTask>>> GetTodoTask()
        {
            return await _context.TodoTask.ToListAsync();
        }

        // GET: api/TodoTasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoTask>> GetTodoTask(int id)
        {
            var todoTask = await _context.TodoTask.FindAsync(id);

            if (todoTask == null)
            {
                return NotFound();
            }

            return todoTask;
        }

        // PUT: api/TodoTasks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoTask(int id, TodoTask todoTask)
        {
            if (id != todoTask.Id)
            {
                return BadRequest();
            }

            _context.Entry(todoTask).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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
        public async Task<ActionResult<TodoTask>> PostTodoTask(TodoTask todoTask)
        {
            _context.TodoTask.Add(todoTask);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTodoTask", new { id = todoTask.Id }, todoTask);
        }

        // DELETE: api/TodoTasks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoTask>> DeleteTodoTask(int id)
        {
            var todoTask = await _context.TodoTask.FindAsync(id);
            if (todoTask == null)
            {
                return NotFound();
            }

            _context.TodoTask.Remove(todoTask);
            await _context.SaveChangesAsync();

            return todoTask;
        }

        private bool TodoTaskExists(int id)
        {
            return _context.TodoTask.Any(e => e.Id == id);
        }
    }
}
