using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HedgeLinks.Models
{
    public class JobCategory:Detail
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
