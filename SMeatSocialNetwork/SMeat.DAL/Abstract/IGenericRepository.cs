using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SMeat.DAL.Abstract
{
    public interface IGenericRepository<T> where T : class {
        IQueryable<T> Data { get; }

        IQueryable<T> GetAsync(List<Expression<Func<T, bool>>> filter = null, /*Func<T,object> orderBy = null,*/ params Expression<Func<T, object>>[] includes);
        Task<List<T>> GetPagedAsync(List<Expression<Func<T, bool>>> filters = null, /*Func<T,object> orderBy = null,*/ int count = 10, int page = 0, params Expression<Func<T, object>>[] includes);
        Task<List<T>> GetPagedAsync(Expression<Func<T, bool>> filter = null, /*Func<T,object> orderBy = null,*/ int count = 10, int page = 0, params Expression<Func<T, object>>[] includes);
        Task<T> FirstOrDefaultAsync(List<Expression<Func<T, bool>>> filters = null, /*Func<T,object> orderBy = null,*/ params Expression<Func<T, object>>[] includes);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter = null, /*Func<T,object> orderBy = null,*/ params Expression<Func<T, object>>[] includes);
        Task<int> CountAsync(List<Expression<Func<T, bool>>> filters = null);
        Task<int> CountAsync(Expression<Func<T, bool>> filter = null);
        Task<T> GetByIdAsync(object id);
        Task<T> GetByIdAsync ( string id, params Expression<Func<T, object>>[] includes );
        Task<T> AddAsync(T obj);
        void Delete(T obj);
        T Update(T obj);
    }
}
