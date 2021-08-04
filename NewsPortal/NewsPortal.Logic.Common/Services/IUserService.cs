using NewsPortal.Logic.Common.Infrastructure;
using NewsPortal.Model.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NewsPortal.Logic.Common.Services
{
    public interface IUserService
    {
        Task<ClaimsIdentity> Authenticate(string email, string password);
        Task<ApplicationUser> GetUserById(string userId);
        Task<ApplicationUser> GetUser(string userId, string email);
        Task<ApplicationUser> GetUserByEmail(string email);
        IEnumerable<ApplicationUser> GetUsers();
        void SaveAvatar(string userId, string path);
        Task<OperationDetails> ChangePassword(string userId, string oldPassword, string newPassword);
        IEnumerable<ApplicationUser> SearchUsers(string search);
        void ChangeRole(string userId, string oldRole, string newRole);
        Task<OperationDetails> CreateUser(ApplicationUser user, string password);
        void UpdateUser(ApplicationUser user);
        void DeleteUser(string userId);
        void SaveUser();
    }
}
