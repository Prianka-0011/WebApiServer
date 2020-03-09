﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_AngularFinal.Models
{
    public class EventsGoPeople
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public int GMN { get; set; }
        public Guid TaskId { get; set; }
        public TodoTask TodoTask { get; set; }
    }
}
