using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace coderush.Models
{
    public class SubMenu
    {
        public int Id { get; set; }
        [DisplayName(" Name ")]
        [Required(ErrorMessage ="*This Field is Required")]
        public string Name { get; set; }
        [DisplayName("Link To")]

        public string Path { get; set; }
        [NotMapped]
        public int  SelectedPage { get; set; }
        [DisplayName("Page Name")]
        [ForeignKey("MenuPathId")]
        public MenuPath MenuPath { get; set; }
        public int? MenuPathId { get; set; }
        [ForeignKey("MenubarId")]
        public Menubar Menubar { get; set; }
        public int MenubarId { get; set; }
    }
}