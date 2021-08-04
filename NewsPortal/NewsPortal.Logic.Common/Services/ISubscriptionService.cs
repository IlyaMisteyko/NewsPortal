using NewsPortal.Model.Models;
using System.Collections.Generic;

namespace NewsPortal.Logic.Common.Services
{
    public interface ISubscriptionService
    {
        bool IsFollowing(string followingId, string followerId);
        IEnumerable<ApplicationUser> GetFollowers(string userId);
        IEnumerable<ApplicationUser> GetFollowers(string userId, int page, int entityCount);
        IEnumerable<ApplicationUser> GetFollowings(string userId);
        IEnumerable<ApplicationUser> GetFollowings(string userId, int page, int entityCount);
        void CreateSubscription(Subscription subscription);
        void DeleteSubscription(string followerId, string followingId);
        void SaveSubscription();
    }
}
