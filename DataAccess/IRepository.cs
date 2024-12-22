using System;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Input;

namespace ServiceCentre.DataAccess
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T SingleOrDefault(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetPaged<TKey>(int pageIndex, int pageSize, Expression<Func<T, TKey>> orderBy);
        T GetById(int id);
        void Add(T entity);
        void Remove(T entity);
        void Update(T entity);
    }
}