using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_AngularFinal.ViewModel
{
    public class TodoTaskFinal
    {
        public Guid Id { get; set; }
        public string Task { get; set; }
        public string Date { get; set; }
        public string Place { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public int Typego { get; set; }
        public int Typemay { get; set; }
        public int TypeNinterest { get; set; }
    }
}
