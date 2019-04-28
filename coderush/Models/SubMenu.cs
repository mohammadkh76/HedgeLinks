using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalHospital.Models
{
    public class SubMenu
    {
        public int Id { get; set; }
        [DisplayName(" نام ")]
        [Required(ErrorMessage ="وارد کردن نام الزامی می باشد.")]
        public string Name { get; set; }
        [DisplayName(" نام عربی")]
        public string Name_ar { get; set; }
        [DisplayName(" نام انگلیسی")]
        public string Name_eng { get; set; }
        [DisplayName("آدرس به")]

        public string Path { get; set; }
        [NotMapped]
        public int  SelectedPage { get; set; }
        [DisplayName("آدرس")]
        [ForeignKey("MenuPathId")]
        public MenuPath MenuPath { get; set; }
        public int? MenuPathId { get; set; }
        [ForeignKey("MenubarId")]
        public Menubar Menubar { get; set; }
        public int MenubarId { get; set; }
    }
}