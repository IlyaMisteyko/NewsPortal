using System.ComponentModel.DataAnnotations;

namespace NewsPortal.Web.ViewModels
{
    public class CommentFormViewModel
    {
        public int CommentId { get; set; }

        [Required(ErrorMessage = "You forgot about a message!")]
        [Display(Name = "Message")]
        public string Message { get; set; }

        public int ArticleId { get; set; }
        
        public string UserId { get; set; }

        public int? ParentCommentId { get; set; }
    }
}