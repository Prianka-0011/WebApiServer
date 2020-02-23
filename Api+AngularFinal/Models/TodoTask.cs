using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_AngularFinal.Models
{
    public class TodoTask
    {
        public Guid Id { get; set; }
        public string Task { get; set; }
        public string Date { get; set; }
        public string Place { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public List<EventList> eventLists { get; set; }
    }
}
