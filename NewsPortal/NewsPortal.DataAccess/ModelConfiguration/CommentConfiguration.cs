using System.Data.Entity.ModelConfiguration;
using NewsPortal.Model.Models;

namespace NewsPortal.DataAccess.ModelConfiguration
{
    public class CommentConfiguration : EntityTypeConfiguration<Comment>
    {
        public CommentConfiguration()
        {
            ToTable("comments");
            HasKey(property => property.CommentId);
            Property(property => property.CommentId).HasColumnName("comment_id");

            Property(property => property.Message).HasColumnName("message").IsRequired().HasMaxLength(255);
            Property(property => property.PublishingDate).HasColumnName("date_of_publishing").IsRequired();
            Property(property => property.ParentCommentId).HasColumnName("parent_comment_id").IsOptional();

            Property(property => property.UserId).HasColumnName("user_id").IsRequired();
            Property(property => property.ArticleId).HasColumnName("article_id").IsRequired();
            HasRequired(property => property.User).WithMany(property => property.Comments).HasForeignKey(key => key.UserId).WillCascadeOnDelete(false);
            HasRequired(property => property.Article).WithMany(property => property.Comments).HasForeignKey(key => key.ArticleId);
            HasOptional(propery => propery.ParentComment).WithMany(property => property.ChildComments).HasForeignKey(key => key.ParentCommentId).WillCascadeOnDelete(false);
            //HasMany(property => property.Likes).WithRequired(property => property.Comment).HasForeignKey(key => key.CommentId);
        }
    }
}
