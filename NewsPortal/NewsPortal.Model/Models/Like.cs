namespace NewsPortal.Model.Models
{
    public class Like
    {
        public int LikeId { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public int CommentId { get; set; }
        public virtual Comment Comment { get; set; }
    }
}
