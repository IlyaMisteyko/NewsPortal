using NewsPortal.Model.Models;
using System.Data.Entity.ModelConfiguration;

namespace NewsPortal.DataAccess.ModelConfiguration
{
    public class SubscriptionConfiguration : EntityTypeConfiguration<Subscription>
    {
        public SubscriptionConfiguration()
        {
            ToTable("subscriptions");
            HasKey(key => key.SubscriptionId);
            Property(property => property.SubscriptionId).HasColumnName("subscription_id");

            Property(property => property.CreatedDate).HasColumnName("date_create").IsRequired();

            Property(property => property.FollowerId).HasColumnName("follower_id");
            Property(property => property.FollowingId).HasColumnName("following_id").IsRequired();
            HasRequired(property => property.Follower).WithMany(property => property.Followers).HasForeignKey(key => key.FollowerId).WillCascadeOnDelete(false);
            HasRequired(property => property.Following).WithMany(property => property.Followings).HasForeignKey(key => key.FollowingId);
        }
    }
}
