using NewsPortal.Model.Models;
using System.Collections.Generic;

namespace NewsPortal.Logic.Common.Services
{
    public interface ISubscriptionService
    {
        bool IsFollowing(string followingId, string followerId);
        IList<ApplicationUser> GetFollowers(string userId);
        IList<ApplicationUser> GetFollowers(string userId, int page, int entityCount);
        IList<ApplicationUser> GetFollowings(string userId);
        IList<ApplicationUser> GetFollowings(string userId, int page, int entityCount);
        void CreateSubscription(Subscription subscription);
        void DeleteSubscription(string followerId, string followingId);
        void SaveSubscription();
    }
}
