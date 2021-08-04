using NewsPortal.DataAccess.Common.Repositories;
using NewsPortal.DataAccess.Common.Identity;
using System;
using System.Threading.Tasks;

namespace NewsPortal.DataAccess.Common.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        ApplicationUserManager UserManager { get; }
        ApplicationRoleManager RoleManager { get; }
        IArticleRepository Articles { get; }
        ICommentRepository Comments { get; }
        ILikeRepository Likes { get; }
        IRateRepository Rates { get; }
        ISubscriptionRepository Subscriptions { get; }
        IUserProfileRepository UserProfiles { get; }

        void Save();
        Task SaveAsync();
    }
}
