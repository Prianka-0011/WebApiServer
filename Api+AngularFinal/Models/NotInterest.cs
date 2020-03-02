using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_AngularFinal.Models
{
    public class NotInterest
    {
        public Guid Id { get; set; }
        public Guid NotTaskId { get; set; }
        public string UserId { get; set; }
    }
}
