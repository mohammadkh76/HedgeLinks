using System.Collections.Generic;
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
    public class CityVM
    {
        public string Id { get; set; }
        public string name { get; set; }
        [ForeignKey("state_id")]
        public State  state{ get; set; }

        public string state_id { get; set; }
        
    }
    public class CityRoot
    {
        public List<CityVM> Cities{ get; set; }
        
    }
}