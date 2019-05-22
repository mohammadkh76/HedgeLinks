using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HedgeLinks.Models
{
    public class Menubar:Detail
    {
        [DisplayName("Name")]
        [Required(ErrorMessage = "*This Field is Required")]
        public string Name { get; set; }

        public string Path { get; set; }
        [NotMapped]
        public int SelectedPage { get; set; }
        [ForeignKey("MenuPathId")]
        public virtual MenuPath MenuPath { get; set; }
        public int? MenuPathId { get; set; }
        public virtual List<SubMenu> SubMenus { get; set; }
    }
}