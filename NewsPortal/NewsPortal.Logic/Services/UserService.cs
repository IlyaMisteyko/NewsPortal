using NewsPortal.DataAccess.Common.Infrastructure;
using NewsPortal.Model.Models;
using NewsPortal.Logic.Common.Services;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using NewsPortal.Logic.Common.Infrastructure;
using System;

namespace NewsPortal.Logic.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }


        #region Methods of User Service

        public async Task<ClaimsIdentity> Authenticate(string email, string password)
        {
            ClaimsIdentity claim = null;
            var user = await _unitOfWork.UserManager.FindAsync(email, password);

            if (user != null)
            {
                claim = await _unitOfWork.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            }

            return claim;
        }

        public async Task<ApplicationUser> GetUserById(string userId)
        {
            return await _unitOfWork.UserManager.FindByIdAsync(userId);
        }

        public async Task<ApplicationUser> GetUser(string userId, string email)
        {
            return await _unitOfWork.UserManager.FindAsync(userId, email);
        }

        public async Task<ApplicationUser> GetUserByEmail(string email)
        {
            return await _unitOfWork.UserManager.FindByEmailAsync(email);
        }

        public IList<ApplicationUser> GetUsers()
        {
            return _unitOfWork.UserManager.Users.OrderBy(user => user.Email).ToList();
        }

        public void SaveAvatar(string userId, string path)
        {
            var user = _unitOfWork.UserManager.FindById(userId);
            user.AvatarUrl = path;
            UpdateUser(user);
        }

        public async Task<OperationDetails> ChangePassword(string userId, string oldPassword, string newPassword)
        {
            var result = await _unitOfWork.UserManager.ChangePasswordAsync(userId, oldPassword, newPassword);

            if(result.Errors.Count() > 0)
                return new OperationDetails(false, result.Errors.FirstOrDefault(), "");

            return new OperationDetails(true, "Password was changed successfully!", "");
        }

        public IList<ApplicationUser> SearchUsers(string search)
        {
            return _unitOfWork.UserManager.Users
                .Where(user => user.Profile.FirstName.Contains(search) || user.Profile.LastName.Contains(search) || user.Email.Contains(search))
                .OrderBy(user => user.Profile.FirstName)
                .ThenBy(user => user.Profile.LastName).ToList();
        }

        public void ChangeRole(string userId, string oldRole, string newRole)
        {
            _unitOfWork.UserManager.RemoveFromRole(userId, oldRole);
            _unitOfWork.UserManager.AddToRole(userId, newRole);
        }

        public async Task<OperationDetails> CreateUser(ApplicationUser user, string password)
        {
            if (_unitOfWork.UserManager.Users.FirstOrDefault(u => u.Email == user.Email) == null)
            {
                user.RoleId = _unitOfWork.RoleManager.FindByName("user").Id;

                var result = await _unitOfWork.UserManager.CreateAsync(user, password);

                if (result.Errors.Count() > 1)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");

                await _unitOfWork.UserManager.AddToRoleAsync(user.Id, "user");
                await _unitOfWork.SaveAsync();
                return new OperationDetails(true, "Register was finished successfully!", "");
            }
            else
                return new OperationDetails(false, "User with this e-mail is existed!", "Email");
        }

        public void UpdateUser(ApplicationUser user)
        {
            _unitOfWork.UserManager.Update(user);
            SaveUser();
        }

        public void DeleteUser(string userId)
        {
            var user = _unitOfWork.UserManager.FindById(userId);
            _unitOfWork.UserManager.Delete(user);
            SaveUser();
        }

        public void SaveUser()
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
