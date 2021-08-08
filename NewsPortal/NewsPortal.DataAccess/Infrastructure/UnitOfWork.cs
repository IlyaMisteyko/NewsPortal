using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using NewsPortal.DataAccess.Common.Infrastructure;
using NewsPortal.Model.Models;
using NewsPortal.DataAccess.Common.Repositories;
using NewsPortal.DataAccess.Context;
using NewsPortal.DataAccess.Common.Identity;
using Microsoft.AspNet.Identity;

namespace NewsPortal.DataAccess.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly NewsPortalEntities _dataContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IArticleRepository _articleRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly ILikeRepository _likeRepository;
        private readonly IRateRepository _rateRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IUserProfileRepository _userProfileRepository;

        public UnitOfWork(NewsPortalEntities newsPortalEntities, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager,
            IArticleRepository articleRepository, ICommentRepository commentRepository, 
            ILikeRepository likeRepository, IRateRepository rateRepository, ISubscriptionRepository subscriptionRepository,
            IUserProfileRepository userProfileRepository)
        {
            _dataContext = newsPortalEntities;
            _userManager = userManager;
            _roleManager = roleManager;

            _articleRepository = articleRepository ?? throw new ArgumentNullException(nameof(articleRepository));
            _commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
            _likeRepository = likeRepository ?? throw new ArgumentNullException(nameof(likeRepository));
            _rateRepository = rateRepository ?? throw new ArgumentNullException(nameof(rateRepository));
            _subscriptionRepository = subscriptionRepository ?? throw new ArgumentNullException(nameof(subscriptionRepository));
            _userProfileRepository = userProfileRepository ?? throw new ArgumentNullException(nameof(userProfileRepository));
        }

        public UserManager<ApplicationUser> UserManager => _userManager;
        public RoleManager<ApplicationRole> RoleManager => _roleManager;

        public IArticleRepository Articles => _articleRepository;

        public ICommentRepository Comments => _commentRepository;

        public ILikeRepository Likes => _likeRepository;

        public IRateRepository Rates => _rateRepository;

        public ISubscriptionRepository Subscriptions => _subscriptionRepository;

        public IUserProfileRepository UserProfiles => _userProfileRepository;

        public void Save()
        {
            _dataContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _dataContext.SaveChangesAsync();
        }

        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dataContext.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}