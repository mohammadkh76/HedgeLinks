﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HedgeLinks.Models.RESTViewModel
{
    public class CommercialTipsEditVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Keyword { get; set; }
        public string FilePath { get; set; }
    }
}
