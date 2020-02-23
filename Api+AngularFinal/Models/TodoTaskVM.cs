using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_AngularFinal.Models
{
    public class TodoTaskVM
    {
        public string Id { get; set; }
        public string Task { get; set; }
        public string Date { get; set; }
        public string Place { get; set; }
        public string Description { get; set; }
    }
}
