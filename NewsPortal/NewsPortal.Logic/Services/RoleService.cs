using Microsoft.AspNet.Identity;
using NewsPortal.DataAccess.Common.Infrastructure;
using NewsPortal.Logic.Common.Services;
using NewsPortal.Model.Models;
using System;
using System.Collections.Generic;

namespace NewsPortal.Logic.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }


        #region  Methods of Role Service

        public string GetUserRole(string roleId)
        {
            return _unitOfWork.RoleManager.FindById(roleId).Name;
        }

        public IEnumerable<ApplicationRole> GetAllRoles()
        {
            return _unitOfWork.RoleManager.Roles;
        }

        #endregion
    }
}
