using NewsPortal.DataAccess.Common.Infrastructure;
using NewsPortal.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace NewsPortal.DataAccess.Infrastructure
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private NewsPortalEntities _dataContext;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(NewsPortalEntities context)
        {
            _dataContext = context;
            _dbSet = context.Set<T>();
        }

        public void Create(T entity)
        {
            _dbSet.Add(entity);
        }
        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _dataContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> collection = _dbSet.Where<T>(where).AsEnumerable();
            foreach (T item in collection)
                _dbSet.Remove(item);
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public T GetById(string id)
        {
            return _dbSet.Find(id);
        }

        public T Get(Expression<Func<T, bool>> where)
        {
            return _dbSet.Where<T>(where).FirstOrDefault<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return _dbSet.Where(where).ToList();
        }
    }
}
