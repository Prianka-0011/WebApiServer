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
        [Route("/going/eventId")]
        [HttpGet("{id}")]
        public ActionResult GetEventGoing(Guid eventId)
        {
            var going = _context.GoingEvents.Where(g=>g.GoingTaskId== eventId).ToList();
            string userId = User.Claims.First(c => c.Type == "UserId").Value;
            if (going == null)
            {
                return NotFound();
            }
            var goingcount = going.Count();
                return Ok(goingcount);

        }
        [Route("/maybe/eventId")]
        [HttpGet("{id}")]
        public ActionResult GetEventGoingMaybe(Guid eventId)
        {
            var maybe = _context.MayBeGoingEs.Where(g => g.MayBeTaskId == eventId).ToList();
            string userId = User.Claims.First(c => c.Type == "UserId").Value;
            if (maybe == null)
            {
                return NotFound();
            }
            var goingMaybecount = maybe.Count();
            return Ok(goingMaybecount);

        }
        [Route("/notinterest/eventId")]
        [HttpGet("{id}")]
        public ActionResult GetEventGoingNotIntrst(Guid eventId)
        {
            var notinterest = _context.NotInterests.Where(g => g.NotTaskId == eventId).ToList();
            string userId = User.Claims.First(c => c.Type == "UserId").Value;
            if (notinterest == null)
            {
                return NotFound();
            }
            var goingnotinterstcount = notinterest.Count();
            return Ok(goingnotinterstcount);

        }
        [HttpPost]
        [Route("PostMaybe")]

        public ActionResult PostGoingEvent(Guid goingTaskId)
        {
            string userId = User.Claims.First(c => c.Type == "UserId").Value;
            var going = _context.GoingEvents.Where(g => g.GoingTaskId == goingTaskId).ToList();
            var notintereste = _context.NotInterests.Where(g => g.NotTaskId == goingTaskId).ToList();

            MayBeGoingE maygoEvent = new MayBeGoingE();
            if (going!=null)
            {
                foreach (var item in going)
                {
                    if (item.UserId.Equals(userId))
                    {
                        var goingEvent = _context.GoingEvents.Find(item.Id);
                        _context.GoingEvents.Remove(goingEvent);
                        _context.SaveChanges();



                    }

                }
            }
            if (notintereste!=null)
            {
                foreach (var item1 in notintereste)
                {
                    if (item1.UserId.Equals(userId))
                    {

                        var nointerest1 = _context.NotInterests.Find(item1.Id);
                        _context.NotInterests.Remove(nointerest1);
                        _context.SaveChanges();

                    }
                }
            }
           
            maygoEvent.UserId = userId;
            maygoEvent.MayBeTaskId = goingTaskId;
            _context.MayBeGoingEs.Add(maygoEvent);
            _context.SaveChanges();

            return Ok(goingTaskId);
        }

        [HttpPost]
        [Route("PostGoing")]

        public ActionResult PostMaybeEvent(Guid goingTaskId)
        {
            string userId = User.Claims.First(c => c.Type == "UserId").Value;
            var maybe = _context.MayBeGoingEs.Where(g => g.MayBeTaskId == goingTaskId).ToList();
            var notintereste = _context.NotInterests.Where(g => g.NotTaskId == goingTaskId).ToList();
            GoingEvent going = new GoingEvent();
            if (maybe!=null)
            {
                foreach (var item in maybe)
                {
                    if (item.UserId.Equals(userId))
                    {

                        var goingmayEve = _context.MayBeGoingEs.Find(item.Id);
                        _context.MayBeGoingEs.Remove(goingmayEve);
                        _context.SaveChanges();

                    }
                }
            }
            if (notintereste!=null)
            {
                foreach (var item in notintereste)
                {
                    if (item.UserId.Equals(userId))
                    {

                        var nointerest1 = _context.NotInterests.Find(item.Id);
                        _context.NotInterests.Remove(nointerest1);
                        _context.SaveChanges();


                    }
                }
            }
          
            going.UserId = userId;
            going.GoingTaskId = goingTaskId;
            _context.GoingEvents.Add(going);
            _context.SaveChanges();
            return Ok(goingTaskId);
        }
        [Route("PostNotInterest")]
        [HttpPost]
        public ActionResult PostNtInterestedEvent(Guid goingTaskId)
        {
            string userId = User.Claims.First(c => c.Type == "UserId").Value;
            var going = _context.GoingEvents.Where(g => g.GoingTaskId == goingTaskId).ToList();
            var maybe = _context.MayBeGoingEs.Where(g => g.MayBeTaskId == goingTaskId).ToList();
            NotInterest notinterest = new NotInterest();
            if (going!=null)
            {
                foreach (var item in going)
                {
                    if (item.UserId.Equals(userId))
                    {

                        var goingEve = _context.GoingEvents.Find(item.Id);
                        _context.GoingEvents.Remove(goingEve);
                        _context.SaveChanges();


                    }
                }
            }
            if (maybe!=null)
            {
                foreach (var item in maybe)
                {
                    if (item.UserId.Equals(userId))
                    {

                        var maybe1 = _context.MayBeGoingEs.Find(item.Id);
                        _context.MayBeGoingEs.Remove(maybe1);
                        _context.SaveChanges();

                    }
                }
            }
            
            notinterest.UserId = userId;
            notinterest.NotTaskId = goingTaskId;
            _context.NotInterests.Add(notinterest);//here i make mistake
            _context.SaveChanges();
            return Ok(goingTaskId);
        }

    }
}