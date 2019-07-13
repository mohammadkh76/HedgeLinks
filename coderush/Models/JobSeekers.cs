using System.ComponentModel.DataAnnotations.Schema;

namespace HedgeLinks.Models
{
    public class JobSeekers
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        [ForeignKey("JobSeekerDetailId")] 
        public JobSeekerDetail JobSeekerDetail { get; set; }
        public int? JobSeekerDetailId { get; set; }
        [ForeignKey("CountryId")]
        public Country Country { get; set; }
        public int CountryId { get; set; }
        [ForeignKey("StateId")]
        public State State { get; set; }
        public int? StateId { get; set; }
        public string city { get; set; }
        public string DesiredJobTitle { get; set; }
        public string ResumeFile { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }
    }
}