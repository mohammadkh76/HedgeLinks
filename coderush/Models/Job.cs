using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HedgeLinks.Models
{
    public class Job:Detail
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string RequiredExp { get; set; }
        public string FilePath { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Compensation { get; set; }
        public string DatePlaced { get; set; }
        public string RequiredRole { get; set; }
        public string Keyword { get; set; }
        public string CompanyName { get; set; }
        public string ExternalLink { get; set; }
        public bool isEasyApply { get; set; }
        public bool isTrend { get; set; }
        [ForeignKey("JobIndustryId")]
        public JobIndustry JobIndustry { get; set; }
        public int JobIndustryId { get; set; }
        [ForeignKey("JobTypeId")]
        public JobType JobType { get; set; }
        public int JobTypeId { get; set; }
        [ForeignKey("MenuPathId")]
        public MenuPath MenuPath { get; set; }
        public int? MenuPathId { get; set; }
    }
}
