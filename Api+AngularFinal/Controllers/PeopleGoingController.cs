using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api_AngularFinal.Models;
using Api_AngularFinal.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        //[Route("/going/eventId")]
        //[HttpGet("{id}")]
        //public ActionResult GetEventGoing(Guid eventId)
        //{
        //    var going = _context.GoingEvents.Where(g=>g.GoingTaskId== eventId).ToList();
        //    string userId = User.Claims.First(c => c.Type == "UserId").Value;
        //    if (going == null)
        //    {
        //        return NotFound();
        //    }
        //    var goingcount = going.Count();
        //        return Ok(goingcount);

        //}
        //[Route("/maybe/eventId")]
        //[HttpGet("{id}")]
        //public ActionResult GetEventGoingMaybe(Guid eventId)
        //{
        //    var maybe = _context.MayBeGoingEs.Where(g => g.MayBeTaskId == eventId).ToList();
        //    string userId = User.Claims.First(c => c.Type == "UserId").Value;
        //    if (maybe == null)
        //    {
        //        return NotFound();
        //    }
        //    var goingMaybecount = maybe.Count();
        //    return Ok(goingMaybecount);

        //}
        //[Route("/notinterest/eventId")]
        //[HttpGet("{id}")]
        //public ActionResult GetEventGoingNotIntrst(Guid eventId)
        //{
        //    var notinterest = _context.NotInterests.Where(g => g.NotTaskId == eventId).ToList();
        //    string userId = User.Claims.First(c => c.Type == "UserId").Value;
        //    if (notinterest == null)
        //    {
        //        return NotFound();
        //    }
        //    var goingnotinterstcount = notinterest.Count();
        //    return Ok(goingnotinterstcount);

        //}
        [HttpPost]
        [Route("PostMaybe")]

        public ActionResult PostGoingEvent(GMN goingevent)
        {
            string userId = User.Claims.First(c => c.Type == "UserId").Value;
            EventsGoPeople eventsGo = new EventsGoPeople();
            var gopeoplecon = _context.eventsGoPeoples.ToList();
            var count = 0;
            foreach (var item in gopeoplecon)
            {
                if (item.UserId.Equals(userId) )
                {
                    if(item.TaskId==goingevent.TaskId)
                    {
                        item.GMN = goingevent.Type;

                        _context.Entry(item).State = EntityState.Modified;
                        try
                        {
                            _context.SaveChanges();
                        }
                        catch(DbUpdateConcurrencyException)
                        {
                            return NotFound();
                        }
                        count = 1;
                    }
                    
                    //_context.Entry(goingevent).State = EntityState.Modified;
                    //try
                    //{

                    //        _context.SaveChangesAsync();


                    //}
                    //catch (DbUpdateConcurrencyException)
                    //{

                    //        return NotFound();

                    //}
                }
            }
            if(count==0)
            {
                eventsGo.UserId = userId;
                eventsGo.TaskId = goingevent.TaskId;
                eventsGo.GMN = goingevent.Type;

                _context.eventsGoPeoples.Add(eventsGo);
                _context.SaveChanges();
            }
           

            return Ok(goingevent);
        }

        //[HttpPost]
        //[Route("PostGoing")]

        //public ActionResult PostMaybeEvent(Guid goingTaskId)
        //{
        //    string userId = User.Claims.First(c => c.Type == "UserId").Value;
        //    var maybe = _context.MayBeGoingEs.Where(g => g.MayBeTaskId == goingTaskId).ToList();
        //    var notintereste = _context.NotInterests.Where(g => g.NotTaskId == goingTaskId).ToList();
        //    GoingEvent going = new GoingEvent();
        //    if (maybe!=null)
        //    {
        //        foreach (var item in maybe)
        //        {
        //            if (item.UserId.Equals(userId))
        //            {

        //                var goingmayEve = _context.MayBeGoingEs.Find(item.Id);
        //                _context.MayBeGoingEs.Remove(goingmayEve);
        //                _context.SaveChanges();

        //            }
        //        }
        //    }
        //    if (notintereste!=null)
        //    {
        //        foreach (var item in notintereste)
        //        {
        //            if (item.UserId.Equals(userId))
        //            {

        //                var nointerest1 = _context.NotInterests.Find(item.Id);
        //                _context.NotInterests.Remove(nointerest1);
        //                _context.SaveChanges();


        //            }
        //        }
        //    }
          
        //    going.UserId = userId;
        //    going.GoingTaskId = goingTaskId;
        //    _context.GoingEvents.Add(going);
        //    _context.SaveChanges();
        //    return Ok(goingTaskId);
        //}
        //[Route("PostNotInterest")]
        //[HttpPost]
        //public ActionResult PostNtInterestedEvent(Guid goingTaskId)
        //{
        //    string userId = User.Claims.First(c => c.Type == "UserId").Value;
        //    var going = _context.GoingEvents.Where(g => g.GoingTaskId == goingTaskId).ToList();
        //    var maybe = _context.MayBeGoingEs.Where(g => g.MayBeTaskId == goingTaskId).ToList();
        //    NotInterest notinterest = new NotInterest();
        //    if (going!=null)
        //    {
        //        foreach (var item in going)
        //        {
        //            if (item.UserId.Equals(userId))
        //            {

        //                var goingEve = _context.GoingEvents.Find(item.Id);
        //                _context.GoingEvents.Remove(goingEve);
        //                _context.SaveChanges();


        //            }
        //        }
        //    }
        //    if (maybe!=null)
        //    {
        //        foreach (var item in maybe)
        //        {
        //            if (item.UserId.Equals(userId))
        //            {

        //                var maybe1 = _context.MayBeGoingEs.Find(item.Id);
        //                _context.MayBeGoingEs.Remove(maybe1);
        //                _context.SaveChanges();

        //            }
        //        }
        //    }
            
        //    notinterest.UserId = userId;
        //    notinterest.NotTaskId = goingTaskId;
        //    _context.NotInterests.Add(notinterest);//here i make mistake
        //    _context.SaveChanges();
        //    return Ok(goingTaskId);
        //}

    }
}