using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HedgeLinks.Models
{
    public class City
    {
        public int Id { get; set; }
        public string name { get; set; }
        [ForeignKey("state_id")]
        public State  state{ get; set; }

        public int state_id { get; set; }
        
    }
}