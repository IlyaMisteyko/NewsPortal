using NewsPortal.Model.Models;
using System.Data.Entity.ModelConfiguration;

namespace NewsPortal.DataAccess.ModelConfiguration
{
    public class ApplicationUserConfiguration : EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserConfiguration()
        {
            Property(property => property.Email).HasColumnName("email").IsRequired().HasMaxLength(100);
            Property(property => property.CreatedDate).HasColumnName("date_create").IsRequired();
            Property(property => property.AvatarUrl).HasColumnName("avatar_url").IsOptional();
            Property(property => property.Activated).HasColumnName("profile_state").IsRequired();
            Property(property => property.RoleId).HasColumnName("role_id");

            HasRequired(property => property.Profile).WithRequiredPrincipal(property => property.User);

            //HasMany(property => property.Articles).WithRequired(property => property.User).HasForeignKey(key => key.UserId);
            //HasMany(property => property.Comments).WithRequired(property => property.User).HasForeignKey(key => key.UserId);
            //HasMany(property => property.Likes).WithRequired(property => property.User).HasForeignKey(key => key.UserId);
            //HasMany(property => property.Rates).WithRequired(property => property.User).HasForeignKey(key => key.UserId);
        }
    }
}
