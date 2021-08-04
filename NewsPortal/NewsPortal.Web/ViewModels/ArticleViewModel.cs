using System;
using System.Collections.Generic;

namespace NewsPortal.Web.ViewModels
{
    public class ArticleViewModel
    {
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public DateTime PublishingDate { get; set; }
        public string AverageRating { get; set; }
        public bool Rated { get; set; }
        public Dictionary<int, string> Rating { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<CommentViewModel> Comments { get; set; }
    }
}