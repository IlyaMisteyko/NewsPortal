using NewsPortal.Model.Models;
using NewsPortal.DataAccess.Common.Repositories;
using NewsPortal.DataAccess.Context;
using NewsPortal.DataAccess.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System;

namespace NewsPortal.DataAccess.Repositories
{
    public class RateRepository : GenericRepository<Rate>, IRateRepository
    {
        public RateRepository(NewsPortalEntities context)
            : base(context)
        {

        }

        public IDictionary<int, int> GetRatesCount(Expression<Func<Rate, bool>> where)
        {
            var dbSet = GetDbSet();

            return dbSet.AsQueryable()
                .Where(where)
                .GroupBy(rate => rate.Mark)
                .Select(rate => new { rate.Key, count = rate.Count() })
                .ToDictionary(key => key.Key, value => value.count);
        }

        public double GetAverageRating(Expression<Func<Rate, bool>> where)
        {
            var dbSet = GetDbSet();

            return dbSet.AsQueryable()
                .Where(where)
                .Select(rate => rate.Mark)
                .DefaultIfEmpty()
                .Average();
        }
    }
}
