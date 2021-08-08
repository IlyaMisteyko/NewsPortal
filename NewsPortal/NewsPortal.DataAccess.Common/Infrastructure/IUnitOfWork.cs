using NewsPortal.DataAccess.Common.Repositories;
using NewsPortal.DataAccess.Common.Identity;
using System;
using System.Threading.Tasks;
using NewsPortal.Model.Models;
using Microsoft.AspNet.Identity;

namespace NewsPortal.DataAccess.Common.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        UserManager<ApplicationUser> UserManager { get; }
        RoleManager<ApplicationRole> RoleManager { get; }
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
