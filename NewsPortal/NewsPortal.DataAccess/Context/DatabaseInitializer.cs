using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NewsPortal.DataAccess.Common.Identity;
using NewsPortal.DataAccess.Infrastructure;
using NewsPortal.DataAccess.Repositories;
using NewsPortal.Model.Models;

namespace NewsPortal.DataAccess.Context
{
    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<NewsPortalEntities>
    {
        protected override void Seed(NewsPortalEntities context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var ar = new ArticleRepository(context);
            var cr = new CommentRepository(context);
            var lr = new LikeRepository(context);
            var rr = new RateRepository(context);
            var sr = new SubscriptionRepository(context);
            var upr = new UserProfileRepository(context);
            var roleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(context));
            var uow = new UnitOfWork(context, userManager, roleManager, ar, cr, lr, rr, sr, upr);

            var adminRole = new ApplicationRole { Name = "admin" };
            var userRole = new ApplicationRole { Name = "user" };

            roleManager.Create(adminRole);
            roleManager.Create(userRole);

            var admin = new ApplicationUser { UserName = "admin@gmail.com", Email = "admin@gmail.com", Activated = true, RoleId = roleManager.FindByName("admin").Id };
            string password = "!QAZ2wsx!@#";

            //-------------------------------------
            string pass = "123456";
            for (int i = 1; i <= 5000; i++)
            {
                var p = new ApplicationUser { Id = $"{i}", UserName = $"p{i}@test.test", Email = $"p{i}@test.test", Activated = true, RoleId = roleManager.FindByName("user").Id };
                userManager.Create(p, pass);
            }

            for (int i = 1; i <= 100; i++)
            {
                var a = new Article { Category = $"{i}", Description = $"{i}", Title = $"{i}", UserId = "1" };
                uow.Articles.Create(a);
                for (int j = 1; j <= 5000; j++)
                {
                    uow.Rates.Create(new Rate { ArticleId = a.ArticleId, Mark = 5, UserId = $"{j}" });
                }
                uow.Save();
            }

            for (int i = 1; i <= 100; i++)
            {
                for (int j = 1; j <= 5000; j++)
                {
                    var com1 = new Comment { Message = $"{i}", ArticleId = i, UserId = $"{j}" };
                    var com2 = new Comment { Message = $"{i}", ArticleId = i, UserId = $"{j}" };
                    uow.Comments.Create(com1);
                    uow.Comments.Create(com2);
                    uow.Save();

                    uow.Likes.Create(new Like { CommentId = com1.CommentId, UserId = $"{j}" });
                    uow.Likes.Create(new Like { CommentId = com2.CommentId, UserId = $"{j}" });
                    uow.Save();
                }
            }

            //if (res.Result.Succeeded)
            //{
            //    var up = new UserProfile { UserProfileId = p.Id };
            //    userProfileRepo.Create(up);
            //    userManager.AddToRole(p.Id, userRole.Name);
            //}
        
            //-----------------------------------------------------

            var result = userManager.Create(admin, password);

            if (result.Succeeded)
            {
                var userProfile = new UserProfile{ UserProfileId = admin.Id };
                uow.UserProfiles.Create(userProfile);
                userManager.AddToRole(admin.Id, adminRole.Name);
            }

            base.Seed(context);
        }
    }
}
