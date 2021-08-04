using System;
using System.Collections.Generic;

namespace NewsPortal.Model.Models
{
    public class Article
    {
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string ImageUrl { get; set; }
        public DateTime PublishingDate { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Rate> Rates { get; set; }

        public Article()
        {
            this.PublishingDate = DateTime.Now;
        }
    }
}
