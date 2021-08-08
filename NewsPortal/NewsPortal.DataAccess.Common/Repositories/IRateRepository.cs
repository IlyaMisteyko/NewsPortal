using NewsPortal.DataAccess.Common.Infrastructure;
using NewsPortal.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NewsPortal.DataAccess.Common.Repositories
{
    public interface IRateRepository : IGenericRepository<Rate>
    {
        IDictionary<int, int> GetRatesCount(Expression<Func<Rate, bool>> where);
        double GetAverageRating(Expression<Func<Rate, bool>> where);
    }
}
