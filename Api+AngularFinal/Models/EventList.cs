using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_AngularFinal.Models
{
    public class EventList
    {
        public Guid Id { get; set; }
        public string TaskId { get; set; }
        public int MyProperty { get; set; }
        public string UserId { get; set; }

    }
}
