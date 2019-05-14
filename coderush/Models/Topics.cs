using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HedgeLinks.Models
{
    public class Article:Detail
    {
        public int Id { get; set; }
        public string AuthorName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string keyword { get; set; }
        public MenuPath Menupath{ get; set; }
        public int MenuPathId{ get; set; }
        public string ExternalLink{ get; set; }
        public bool isShow { get; set; }

    }
}
