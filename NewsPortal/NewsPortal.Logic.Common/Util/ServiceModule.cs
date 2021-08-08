using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NewsPortal.DataAccess.Common.Identity;
using NewsPortal.DataAccess.Common.Infrastructure;
using NewsPortal.DataAccess.Common.Repositories;
using NewsPortal.DataAccess.Context;
using NewsPortal.DataAccess.Infrastructure;
using NewsPortal.DataAccess.Repositories;
using NewsPortal.Model.Models;
using Ninject.Modules;
using System.Data.Entity;

namespace NewsPortal.Logic.Common.Util
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>();
            Bind<IArticleRepository>().To<ArticleRepository>();
            Bind<ICommentRepository>().To<CommentRepository>();
            Bind<ILikeRepository>().To<LikeRepository>();
            Bind<IRateRepository>().To<RateRepository>();
            Bind<ISubscriptionRepository>().To<SubscriptionRepository>();
            Bind<IUserProfileRepository>().To<UserProfileRepository>();
            Bind<NewsPortalEntities>().ToSelf().InSingletonScope();
            Bind<DbContext>().To<NewsPortalEntities>();
            Bind<IUserStore<ApplicationUser>>().To<UserStore<ApplicationUser>>();
            Bind<RoleStore<ApplicationRole>>().To<RoleStore<ApplicationRole>>();
            Bind<UserManager<ApplicationUser>>().To<ApplicationUserManager>();
            Bind<RoleManager<ApplicationRole>>().To<ApplicationRoleManager>();
        }
    }
}