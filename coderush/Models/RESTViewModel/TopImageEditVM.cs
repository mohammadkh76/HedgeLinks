using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HedgeLinks.Models.RESTViewModel
{
    public class TopImageEditVM
    {
        public int Id { get; set; }
        public string ImageTitle { get; set; }
        public string ImageSubtitle { get; set; }
        public string Keyword { get; set; }
        public string FilePath { get; set; }
    }
}
