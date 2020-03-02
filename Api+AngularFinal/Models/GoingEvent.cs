using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_AngularFinal.Models
{
    public class GoingEvent
    {
        public Guid Id { get; set; }
        public Guid GoingTaskId { get; set; }
        public string UserId { get; set; }
    }
}
