using HedgeLinks.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HedgeLinks.Models
{
    public class Detail
    {
        public int Id { get; set; }

        [ForeignKey("CreatedUserId")]
        public ApplicationUser CreatedUser{ get; set; }
        public string CreatedUserId { get; set; }
        [ForeignKey("EditUserId")]
        public ApplicationUser EditedUser{ get; set; }
        public string EditUserId { get; set; }

        public string CreateDate { get; set; }
        public string EditDate { get; set; }
    }
}
