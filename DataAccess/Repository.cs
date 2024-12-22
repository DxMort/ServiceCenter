using System;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;

namespace ServiceCentre.DataAccess
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsQueryable();
        }
        public T SingleOrDefault(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.SingleOrDefault(predicate);
        }
        public IQueryable<T> GetPaged<TKey>(int pageIndex, int pageSize, Expression<Func<T, TKey>> orderBy)
        {
            return _dbSet.OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }
        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }
        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }
        public void Update(T entity)
        {
            var local = _context.Set<T>()
                .Local
                .FirstOrDefault(entry => entry.Equals(entity));

            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}