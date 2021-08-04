using System;

namespace NewsPortal.Web.ViewModels
{
    public class SearchCommentViewModel
    {
        public int CommentId { get; set; }
        public string Message { get; set; }
        public string CommentOwner { get; set; }
        public DateTime PublishingDate { get; set; }
        public string AvatarUrl { get; set; }
        public int ArticleId { get; set; }
        public string UserId { get; set; }
    }
}