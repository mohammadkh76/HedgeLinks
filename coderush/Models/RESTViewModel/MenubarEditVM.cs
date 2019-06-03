using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HedgeLinks.Models.RESTViewModel
{
    public class MenubarEditVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public int? SelectedPage { get; set; }
    }
}
