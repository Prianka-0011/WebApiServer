using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api_AngularFinal.Models;
using Api_AngularFinal.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api_AngularFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShareEventsController : ControllerBase
    {
        private readonly AuthenticationContex _context;

        private UserManager<ApplicationUser> _userManager;

        public ShareEventsController(AuthenticationContex context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [Authorize]
        [HttpGet]
        [Route("GetSharedEvents")]
        public ActionResult<IEnumerable<EventList>> GetEventList()
        {
            List<ReturnShareEvent_VM> todoTaskList=new List<ReturnShareEvent_VM>();
           
            var task = _context.TodoTask.ToList();
            string userId = User.Claims.First(c => c.Type == "UserId").Value;
            var eventList = _context.EventLists.Where(e => e.UserId == userId).ToList();
            foreach (var item in eventList)
            {
                foreach (var item1 in task)
                {
                    if(item.TaskId.Equals(item1.Id))
                    {
                        ReturnShareEvent_VM share1 = new ReturnShareEvent_VM();
                        var user = _context.ApplicationUsers.Find(item1.UserId);
                       
                        share1.UserName = user.UserName;
                        share1.Id = item1.Id;
                        share1.Task = item1.Task;
                        share1.Date = item1.Date;
                        share1.Place = item1.Place;
                        share1.Description = item1.Description;
                        share1.go = true;
                        share1.may = true;
                        share1.not = true;
                        todoTaskList.Add(share1);
                    }
                }
            }
            
            return Ok(todoTaskList);
        }
        [Authorize]
        [HttpPut]
        [Route("EventList/{taskId}")]
        public async Task<ActionResult<EventList>>PostEventList(Guid taskId, List<string>userIds)
        {
            string userId = User.Claims.First(c => c.Type == "UserId").Value;
            var user = await _userManager.FindByIdAsync(userId);
           
            foreach (var item in userIds)
            {
                EventList eventList = new EventList();
                eventList.TaskId = taskId;
                eventList.UserId = item;
                _context.EventLists.Add(eventList);
                await _context.SaveChangesAsync();
            }
            //eventList.Task = eventListVM.Task;
            //eventList.Date = eventListVM.Date;
            //eventList.UserId = eventListVM.UserId;
            //eventList.UserName = user.UserName;
            //_context.EventLists.Add(eventList);
            // await _context.SaveChangesAsync();//mistake async
            return CreatedAtAction("GetEventList", new { id = taskId, userIds });

        }
    }
}