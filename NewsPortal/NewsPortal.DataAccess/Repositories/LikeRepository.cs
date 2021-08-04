using NewsPortal.Model.Models;
using NewsPortal.DataAccess.Common.Repositories;
using NewsPortal.DataAccess.Context;
using NewsPortal.DataAccess.Infrastructure;

namespace NewsPortal.DataAccess.Repositories
{
    public class LikeRepository : GenericRepository<Like>, ILikeRepository
    {
        public LikeRepository(NewsPortalEntities context)
            : base(context)
        {

        }
    }
}
