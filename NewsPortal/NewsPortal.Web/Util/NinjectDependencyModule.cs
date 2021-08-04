using NewsPortal.Logic.Common.Services;
using NewsPortal.Logic.Services;
using Ninject.Modules;

namespace NewsPortal.Web.Util
{
    public class NinjectDependencyModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IArticleService>().To<ArticleService>();
            Bind<ICommentService>().To<CommentService>();
            Bind<ILikeService>().To<LikeService>();
            Bind<IRateService>().To<RateService>();
            Bind<IRoleService>().To<RoleService>();
            Bind<ISubscriptionService>().To<SubscriptionService>();
            Bind<IUserProfileService>().To<UserProfileService>();
            Bind<IUserService>().To<UserService>();
        }
    }
}