using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SMeat.DAL
{
    public interface IGenericRepository<T>
    {
        IQueryable<T> Data { get; }

        Task<List<T>> GetAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes);
        IQueryable<T> Query(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
        Task<T> GetByIdAsync(object id);
        Task<T> AddAsync(T obj);
        void Delete(T obj);
        T Update(T obj);
    }
}
