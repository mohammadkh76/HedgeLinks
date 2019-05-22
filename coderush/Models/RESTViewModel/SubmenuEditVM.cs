using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HedgeLinks.Models.RESTViewModel
{
    public class SubmenuEditVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string SelectedPage { get; set; }
        public string MenubarId { get; set; }
    }
}
