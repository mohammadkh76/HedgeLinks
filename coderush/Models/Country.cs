using System.Collections.Generic;

namespace HedgeLinks.Models
{
    public class Country
    {
        public int Id{ get; set; }
        public string sortname { get; set; }
        public string name { get; set; }
        public string phoneCode { get; set; }
    }

    public class CountryRoot
    {
        public List<Country> countries { get; set; }
    }
}