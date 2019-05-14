using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HedgeLinks.Models
{
    public class Job:Detail
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Compensation { get; set; }
        public string DatePlaced { get; set; }
        public string Position { get; set; }
        [ForeignKey("JobCategoryId")]
        public JobCategory JobCategry { get; set; }
        public int JobCategoryId { get; set; }
    }
}
