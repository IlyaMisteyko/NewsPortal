namespace NewsPortal.Model.Models
{
    public class Rate
    {
        public int RateId { get; set; }
        public int Mark { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public int ArticleId { get; set; }
        public virtual Article Article { get; set; }
    }
}
