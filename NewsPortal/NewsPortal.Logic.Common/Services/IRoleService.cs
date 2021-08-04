using NewsPortal.Model.Models;
using System.Collections.Generic;

namespace NewsPortal.Logic.Common.Services
{
    public interface IRoleService
    {
        string GetUserRole(string roleId);
        IEnumerable<ApplicationRole> GetAllRoles();
    }
}
