using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_AngularFinal.ViewModel
{
    public class ReturnShareEvent_VM
    {
        public Guid Id { get; set; }
        public string Task { get; set; }
        public string Date { get; set; }
        public string UserName { get; set; }
        public string Place { get; set; }
        public string Description { get; set; }
    }
}
