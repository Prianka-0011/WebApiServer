using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api_AngularFinal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api_AngularFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleGoingController : ControllerBase
    {
        private readonly AuthenticationContex _context;

        private UserManager<ApplicationUser> _userManager;
        public PeopleGoingController(AuthenticationContex context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("PostGoing")]

        public ActionResult PostGoingEvent(Guid goingTaskId)
        {
            string userId = User.Claims.First(c => c.Type == "UserId").Value;
            var maybe = _context.MayBeGoingEs.Where(g => g.MayBeTaskId == goingTaskId).ToList();
            var notintereste = _context.NotInterests.Where(g => g.NotTaskId == goingTaskId).ToList();
            foreach (var item in maybe)
            {
                if (item.UserId.Equals(userId))
                {
                    GoingEvent goEvent = new GoingEvent();
                    var maybeEvent = _context.MayBeGoingEs.Find(item.Id);
                    _context.MayBeGoingEs.Remove(maybeEvent);
                    _context.SaveChanges();
                    goEvent.UserId = userId;
                    goEvent.GoingTaskId = goingTaskId;
                    _context.GoingEvents.Add(goEvent);
                    _context.SaveChanges();

                }
            }
            foreach (var item in notintereste)
            {
                if (item.UserId.Equals(userId))
                {
                    GoingEvent goEvent = new GoingEvent();
                    var nointerest1 = _context.NotInterests.Find(item.Id);
                    _context.NotInterests.Remove(nointerest1);
                    _context.SaveChanges();
                    goEvent.UserId = userId;
                    goEvent.GoingTaskId = goingTaskId;
                    _context.GoingEvents.Add(goEvent);
                    _context.SaveChanges();

                }
            }
            return Ok(goingTaskId);
        }

        [HttpPost]
        [Route("PostGoing")]

        public ActionResult PostMaybeEvent(Guid goingTaskId)
        {
            string userId = User.Claims.First(c => c.Type == "UserId").Value;
            var going = _context.GoingEvents.Where(g => g.GoingTaskId == goingTaskId).ToList();
            var notintereste = _context.NotInterests.Where(g => g.NotTaskId == goingTaskId).ToList();
            foreach (var item in going)
            {
                if (item.UserId.Equals(userId))
                {
                    MayBeGoingE maybe = new MayBeGoingE();
                    var goingEve = _context.GoingEvents.Find(item.Id);
                    _context.GoingEvents.Remove(goingEve);
                    _context.SaveChanges();
                    maybe.UserId = userId;
                    maybe.MayBeTaskId = goingTaskId;
                    _context.MayBeGoingEs.Add(maybe);//here i make mistake
                    _context.SaveChanges();

                }
            }
            foreach (var item in notintereste)
            {
                if (item.UserId.Equals(userId))
                {
                    GoingEvent goEvent = new GoingEvent();
                    var nointerest1 = _context.NotInterests.Find(item.Id);
                    _context.NotInterests.Remove(nointerest1);
                    _context.SaveChanges();
                    goEvent.UserId = userId;
                    goEvent.GoingTaskId = goingTaskId;
                    _context.GoingEvents.Add(goEvent);
                    _context.SaveChanges();

                }
            }
            return Ok(goingTaskId);
        }



    }
}