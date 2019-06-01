using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HedgeLinks.Models
{
    public class ArticleTopic:Detail
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsShow { get; set; }
    }
}
