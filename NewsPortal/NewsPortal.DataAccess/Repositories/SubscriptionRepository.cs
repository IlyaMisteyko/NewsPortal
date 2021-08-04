using NewsPortal.Model.Models;
using NewsPortal.DataAccess.Common.Repositories;
using NewsPortal.DataAccess.Context;
using NewsPortal.DataAccess.Infrastructure;

namespace NewsPortal.DataAccess.Repositories
{
    public class SubscriptionRepository : GenericRepository<Subscription>, ISubscriptionRepository
    {
        public SubscriptionRepository(NewsPortalEntities context)
            : base(context)
        {

        }
    }
}
