using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HedgeLinks.Models
{
    public class State
    {
        public int  Id { get; set; }
        public string name { get; set; }
        [ForeignKey("country_id")] 
        public Country Country { get; set; }
        public int country_id { get; set; }
    }
}