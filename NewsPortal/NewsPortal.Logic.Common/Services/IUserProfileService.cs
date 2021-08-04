using NewsPortal.Model.Models;

namespace NewsPortal.Logic.Common.Services
{
    public interface IUserProfileService
    {
        UserProfile GetProfile(string id);
        UserProfile GetUser(string userId);
        void CreateUserProfile(string userId);
        void UpdateUserProfile(UserProfile user);
        void SaveUserProfile();
    }
}
