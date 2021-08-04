using System.Data.Entity.ModelConfiguration;
using NewsPortal.Model.Models;

namespace NewsPortal.DataAccess.ModelConfiguration
{
    public class ArticleConfiguration : EntityTypeConfiguration<Article>
    {
        public ArticleConfiguration()
        {
            ToTable("articles");
            HasKey(property => property.ArticleId);
            Property(property => property.ArticleId).HasColumnName("article_id");

            Property(property => property.Title).HasColumnName("title").IsRequired().HasMaxLength(100);
            Property(property => property.Description).HasColumnName("description").IsRequired().HasMaxLength(1000);
            Property(property => property.Category).HasColumnName("category").IsRequired().HasMaxLength(50);
            Property(property => property.ImageUrl).HasColumnName("image_url").IsOptional();
            Property(property => property.PublishingDate).HasColumnName("date_published").IsRequired();

            Property(property => property.UserId).HasColumnName("user_id").IsRequired();
            HasRequired(property => property.User).WithMany(property => property.Articles).HasForeignKey(key => key.UserId);
            //HasMany(property => property.Rates).WithRequired(property => property.Article).HasForeignKey(key => key.ArticleId);
        }
    }
}
