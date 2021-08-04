using System.Data.Entity.ModelConfiguration;
using NewsPortal.Model.Models;

namespace NewsPortal.DataAccess.ModelConfiguration
{
    public class LikeConfiguration : EntityTypeConfiguration<Like>
    {
        public LikeConfiguration()
        {
            ToTable("likes");
            HasKey(property => property.LikeId);
            Property(property => property.LikeId).HasColumnName("like_id");

            Property(property => property.UserId).HasColumnName("user_id").IsRequired();
            Property(property => property.CommentId).HasColumnName("comment_id").IsRequired();
            HasRequired(property => property.User).WithMany(property => property.Likes).HasForeignKey(key => key.UserId).WillCascadeOnDelete(false);
            HasRequired(property => property.Comment).WithMany(property => property.Likes).HasForeignKey(key => key.CommentId);
        }
    }
}
