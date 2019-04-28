﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace coderush.Models
{
    public class Menubar
    {
        public int Id { get; set; }
        [DisplayName("Name")]
        [Required(ErrorMessage ="وارد کردن نام الزامی است.")]
        public string Name { get; set; }

        public string  Path{ get; set; }
        [NotMapped]
        public int  SelectedPage { get; set; }
        [ForeignKey("MenuPathId")]
        public virtual MenuPath  MenuPath{ get; set; }
        public int? MenuPathId { get; set; }
        public virtual List<SubMenu> SubMenus{ get; set; }
    }
}