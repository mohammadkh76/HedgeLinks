using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HedgeLinks.Models.RESTViewModel
{
    public class ArticleVM
    {
        public string AuthorName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public string Keyword { get; set; }
        [ForeignKey("ArticleTopicId")]
        public ArticleTopic ArticleTopic { get; set; }
        public int ArticleTopicId { get; set; }
        public MenuPath Menupath { get; set; }
        public int? MenuPathId { get; set; }
        public string ExternalLink { get; set; }
        public bool isShow { get; set; }
    }
}
