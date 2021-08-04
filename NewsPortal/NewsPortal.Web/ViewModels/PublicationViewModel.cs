using System;

namespace NewsPortal.Web.ViewModels
{
    public class PublicationViewModel
    {
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public DateTime PublishingDate { get; set; }
        public string AverageRating { get; set; }
        public string UserId { get; set; }
        public string ImageUrl { get; set; }
        public string Email { get; set; }
    }
}