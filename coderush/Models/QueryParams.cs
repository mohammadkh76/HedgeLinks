using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HedgeLinks.Models
{
    public class QueryParams
    {
        [BindRequired]
        public int A { get; set; }
        public int B { get; set; }
    }
}
