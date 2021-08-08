using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NewsPortal.DataAccess.Common.Infrastructure
{
    public interface IGenericRepository<T> where T : class
    {
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> where);
        T GetById(int id);
        T GetById(string id);
        T Get(Expression<Func<T, bool>> where);
        IList<T> GetAll();
        IList<T> GetMany(Expression<Func<T, bool>> where);
    }
}
