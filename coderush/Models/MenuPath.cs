using HedgeLinks.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HedgeLinks.Models
{
    public class MenuPath:Detail
    {
        [DisplayName("Name")]
        [Required(ErrorMessage = "*This Field is Required")]
        public string Name { get; set; }
        [DisplayName("Description")]
        public string  Description{ get; set; }
      

        [DisplayName("Page Name")]
        [Required(ErrorMessage ="*This field is Required.")]
        public string  PageName{ get; set; }
    }
}