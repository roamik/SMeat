using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SMeat.DAL
{
    public interface IGenericRepository<T>
    {
        IEnumerable<T> Data { get; }

        List<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes);
        IQueryable<T> Query(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
        T GetById(int id);
        bool Insert(T obj);
        void Delete(T obj);
        bool Update(T obj);
    }
}
