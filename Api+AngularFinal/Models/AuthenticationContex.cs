﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_AngularFinal.Models
{
    public class AuthenticationContex:IdentityDbContext
    {
        public AuthenticationContex(DbContextOptions options):base(options)
        {

        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Api_AngularFinal.Models.TodoTask> TodoTask { get; set; }
        public DbSet<Api_AngularFinal.Models.EventList> EventLists { get; set; }
        //public DbSet<Api_AngularFinal.Models.NotInterest> NotInterests { get; set; }
        //public DbSet<Api_AngularFinal.Models.GoingEvent> GoingEvents { get; set; }
        //public DbSet<Api_AngularFinal.Models.EventsGoPeople> MayBeGoingEs { get; set; }
        public DbSet<Api_AngularFinal.Models.EventsGoPeople> eventsGoPeoples { get; set; }
    }
}
