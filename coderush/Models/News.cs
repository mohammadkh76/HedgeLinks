using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HedgeLinks.Models
{
    public class News : Detail
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Date { get; set; }
        public string ShortSummery { get; set; }
        public string NewsBody{ get; set; }

    }
}
