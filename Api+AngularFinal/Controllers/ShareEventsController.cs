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

        [HttpGet]
        [Route("EventList")]
        public ActionResult<IEnumerable<EventList>> GetEventList()
        {
            string userId = User.Claims.First(c => c.Type == "UserId").Value;
            var eventList = _context.EventLists.Where(e => e.UserId == userId).ToList();

            return eventList;
        }
        [Authorize]
        [HttpPost]
        [Route("EventList")]
        public async Task<ActionResult<EventList>>PostEventList(List<string>Ids,string Id)
        {
            string userId = User.Claims.First(c => c.Type == "UserId").Value;
            var user = await _userManager.FindByIdAsync(userId);
            EventList eventList = new EventList();
            eventList.Task = eventListVM.Task;
            eventList.Date = eventListVM.Date;
            eventList.UserId = eventListVM.UserId;
            eventList.UserName = user.UserName;
            _context.EventLists.Add(eventList);
             await _context.SaveChangesAsync();//mistake async
            return CreatedAtAction("GetEventList", new { id = eventListVM.Id, eventListVM });

        }
    }
}