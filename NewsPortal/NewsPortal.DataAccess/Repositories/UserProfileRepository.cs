using NewsPortal.DataAccess.Common.Repositories;
using NewsPortal.DataAccess.Context;
using NewsPortal.DataAccess.Infrastructure;
using NewsPortal.Model.Models;

namespace NewsPortal.DataAccess.Repositories
{
    public class UserProfileRepository : GenericRepository<UserProfile>, IUserProfileRepository
    {
        public UserProfileRepository(NewsPortalEntities context) 
            : base(context)
        {

        }
    }
}
