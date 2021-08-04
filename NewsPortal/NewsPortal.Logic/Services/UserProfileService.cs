using NewsPortal.DataAccess.Common.Infrastructure;
using NewsPortal.Logic.Common.Services;
using NewsPortal.Model.Models;
using System;

namespace NewsPortal.Logic.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserProfileService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }


        #region Methods of User profile Service

        public UserProfile GetProfile(string id)
        {
            return _unitOfWork.UserProfiles.GetById(id);
        }

        public UserProfile GetUser(string userId)
        {
            return _unitOfWork.UserProfiles.Get(profile => profile.UserProfileId == userId);
        }

        public void CreateUserProfile(string userId)
        {
            UserProfile newUserProfile = new UserProfile();
            newUserProfile.UserProfileId = userId;
            _unitOfWork.UserProfiles.Create(newUserProfile);
            SaveUserProfile();
        }
        public void UpdateUserProfile(UserProfile user)
        {
            _unitOfWork.UserProfiles.Update(user);
            SaveUserProfile();
        }
        public void SaveUserProfile()
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
