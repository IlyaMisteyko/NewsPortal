using NewsPortal.DataAccess.Common.Infrastructure;
using NewsPortal.Logic.Common.Services;
using NewsPortal.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NewsPortal.Logic.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubscriptionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }


        #region  Methods of Subscription Service

        public bool IsFollowing(string followingId, string followerId)
        {
            return _unitOfWork.Subscriptions.Get(subscription => subscription.FollowingId == followingId && subscription.FollowerId == followerId) != null;
        }

        public IEnumerable<ApplicationUser> GetFollowers(string userId)
        {
            List<ApplicationUser> followers = new List<ApplicationUser>();

            var subscriptions = _unitOfWork.Subscriptions.GetMany(following => following.FollowingId == userId);

            foreach (var subscription in subscriptions)
                followers.Add(subscription.Follower);

            return followers;
        }

        public IEnumerable<ApplicationUser> GetFollowers(string userId, int page, int entityCount)
        {
            int skip = entityCount * page;
            List<ApplicationUser> followers = new List<ApplicationUser>();

            var subscriptions = _unitOfWork.Subscriptions.GetMany(following => following.FollowingId == userId);

            foreach (var subscription in subscriptions)
                followers.Add(subscription.Follower);

            followers = followers.Skip(skip).Take(entityCount).ToList();
            return followers;
        }

        public IEnumerable<ApplicationUser> GetFollowings(string userId)
        {
            List<ApplicationUser> followings = new List<ApplicationUser>();

            var subscriptions = _unitOfWork.Subscriptions.GetMany(follower => follower.FollowerId == userId);

            foreach (var subscription in subscriptions)
                followings.Add(subscription.Following);

            return followings;
        }

        public IEnumerable<ApplicationUser> GetFollowings(string userId, int page, int entityCount)
        {
            int skip = entityCount * page;
            List<ApplicationUser> followings = new List<ApplicationUser>();

            var subscriptions = _unitOfWork.Subscriptions.GetMany(follower => follower.FollowerId == userId);

            foreach (var subscription in subscriptions)
                followings.Add(subscription.Following);

            followings = followings.Skip(skip).Take(entityCount).ToList();
            return followings;
        }

        public void CreateSubscription(Subscription subscription)
        {
            _unitOfWork.Subscriptions.Create(subscription);
            SaveSubscription();
        }

        public void DeleteSubscription(string followerId, string followingId)
        {
            _unitOfWork.Subscriptions.Delete(subscription => subscription.FollowerId == followerId && subscription.FollowingId == followingId);
            SaveSubscription();
        }

        public void SaveSubscription()
        {
            _unitOfWork.Save();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        #endregion
    }
}
