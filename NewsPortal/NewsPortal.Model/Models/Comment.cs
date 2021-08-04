using System;
using System.Collections.Generic;

namespace NewsPortal.Model.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Message { get; set; }
        public DateTime PublishingDate { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public int ArticleId { get; set; }
        public virtual Article Article { get; set; }

        public int? ParentCommentId { get; set; }
        public virtual Comment ParentComment { get; set; }

        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Comment> ChildComments { get; set; }

        public Comment()
        {
            this.PublishingDate = DateTime.Now;
        }
    }
}
