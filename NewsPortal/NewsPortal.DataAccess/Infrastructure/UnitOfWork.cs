using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using NewsPortal.DataAccess.Common.Infrastructure;
using NewsPortal.Model.Models;
using NewsPortal.DataAccess.Common.Repositories;
using NewsPortal.DataAccess.Context;
using NewsPortal.DataAccess.Common.Identity;
using NewsPortal.DataAccess.Repositories;

namespace NewsPortal.DataAccess.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private NewsPortalEntities _dataContext;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        private IArticleRepository _articleRepository;
        private ICommentRepository _commentRepository;
        private ILikeRepository _likeRepository;
        private IRateRepository _rateRepository;
        private ISubscriptionRepository _subscriptionRepository;
        private IUserProfileRepository _userProfileRepository;

        public UnitOfWork()
        {
            _dataContext = new NewsPortalEntities();
            _userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_dataContext));
            _roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(_dataContext));
        }

        public ApplicationUserManager UserManager => _userManager;
        public ApplicationRoleManager RoleManager => _roleManager;

        public IArticleRepository Articles
        {
            get
            {
                if (_articleRepository == null)
                    _articleRepository = new ArticleRepository(_dataContext);
                return _articleRepository;
            }
        }

        public ICommentRepository Comments
        {
            get
            {
                if (_commentRepository == null)
                    _commentRepository = new CommentRepository(_dataContext);
                return _commentRepository;
            }
        }

        public ILikeRepository Likes
        {
            get
            {
                if (_likeRepository == null)
                    _likeRepository = new LikeRepository(_dataContext);
                return _likeRepository;
            }
        }

        public IRateRepository Rates
        {
            get
            {
                if (_rateRepository == null)
                    _rateRepository = new RateRepository(_dataContext);
                return _rateRepository;
            }
        }

        public ISubscriptionRepository Subscriptions
        {
            get
            {
                if (_subscriptionRepository == null)
                    _subscriptionRepository = new SubscriptionRepository(_dataContext);
                return _subscriptionRepository;
            }
        }

        public IUserProfileRepository UserProfiles
        {
            get
            {
                if (_userProfileRepository == null)
                    _userProfileRepository = new UserProfileRepository(_dataContext);
                return _userProfileRepository;
            }
        }

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