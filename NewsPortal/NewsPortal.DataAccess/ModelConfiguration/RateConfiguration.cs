using System.Data.Entity.ModelConfiguration;
using NewsPortal.Model.Models;

namespace NewsPortal.DataAccess.ModelConfiguration
{
    public class RateConfiguration : EntityTypeConfiguration<Rate>
    {
        public RateConfiguration()
        {
            ToTable("rates");
            HasKey(property => property.RateId);
            Property(property => property.RateId).HasColumnName("rate_id");

            Property(property => property.Mark).HasColumnName("mark").IsRequired();

            Property(property => property.UserId).HasColumnName("user_id").IsRequired();
            Property(property => property.ArticleId).HasColumnName("article_id").IsRequired();
            HasRequired(property => property.User).WithMany(property => property.Rates).HasForeignKey(key => key.UserId).WillCascadeOnDelete(false);
            HasRequired(property => property.Article).WithMany(property => property.Rates).HasForeignKey(key => key.ArticleId);
        }
    }
}
