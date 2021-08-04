using Microsoft.AspNet.Identity.EntityFramework;
using NewsPortal.Model.Models;
using NewsPortal.DataAccess.ModelConfiguration;
using System.Data.Entity;

namespace NewsPortal.DataAccess.Context
{
    public class NewsPortalEntities : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }

        public NewsPortalEntities()
            : base("NewsPortalEntities")
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new ApplicationUserConfiguration());
            modelBuilder.Configurations.Add(new ArticleConfiguration());
            modelBuilder.Configurations.Add(new CommentConfiguration());
            modelBuilder.Configurations.Add(new LikeConfiguration());
            modelBuilder.Configurations.Add(new RateConfiguration());
            modelBuilder.Configurations.Add(new SubscriptionConfiguration());
            modelBuilder.Configurations.Add(new UserProfileConfiguration());

            Database.SetInitializer<NewsPortalEntities>(new DatabaseInitializer());
        }
    }
}
