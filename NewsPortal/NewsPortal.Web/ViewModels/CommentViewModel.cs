using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NewsPortal.Web.ViewModels
{
    public class CommentViewModel
    {
        public int CommentId { get; set; }

        [Required(ErrorMessage = "You forgot about a comment!")]
        public string Message { get; set; }

        public string CommentOwner { get; set; }

        public DateTime PublishingDate { get; set; }

        public int LikesCount { get; set; }

        public int ArticleId { get; set; }

        public string UserId { get; set; }

        public string AvatarUrl { get; set; }

        public int? ParentCommentId { get; set; }

        public ICollection<CommentViewModel> ChildComments { get; set; }
    }
}