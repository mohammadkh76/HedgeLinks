using HedgeLinks.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace coderush.Models
{
    public class MenuPath:ModelSecurity
    {
        public int? Id { get; set; }
        [DisplayName("Title")]
        [Required(ErrorMessage = "*This Field is Required")]
        public string Title { get; set; }
        [DisplayName("Description")]
        public string  Description{ get; set; }
      

        [DisplayName("Page Name")]
        [Required(ErrorMessage ="*This field is Required.")]
        public string  PageName{ get; set; }
    }
}