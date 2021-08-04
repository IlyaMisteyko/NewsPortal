using NewsPortal.Model.Models;
using System.Data.Entity.ModelConfiguration;

namespace NewsPortal.DataAccess.ModelConfiguration
{
    public class UserProfileConfiguration : EntityTypeConfiguration<UserProfile>
    {
        public UserProfileConfiguration()
        {
            ToTable("profiles");
            HasKey(property => property.UserProfileId);
            Property(property => property.UserProfileId).HasColumnName("user_profile_id");

            Property(property => property.FirstName).HasColumnName("firstname").HasMaxLength(100);
            Property(property => property.LastName).HasColumnName("lastname").HasMaxLength(100);
            Property(property => property.Gender).HasColumnName("gender");
            Property(property => property.DateOfBirth).HasColumnName("date_of_birth");
            Property(property => property.Adress).HasColumnName("adress").HasMaxLength(100);
            Property(property => property.Country).HasColumnName("country").HasMaxLength(100);
            Property(property => property.City).HasColumnName("city").HasMaxLength(100);
            Property(property => property.PhoneNumber).HasColumnName("phone_number").HasMaxLength(100);
            Property(property => property.Skype).HasColumnName("skype").HasMaxLength(100);
        }
    }
}
