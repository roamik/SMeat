using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SMeat.DAL.Abstract;
using SMeat.MODELS;

namespace SMeat.DAL.Concrete
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly IApplicationContext _context;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(IApplicationContext context)
        {
           _context = context;
           _dbSet = context.Set<T>();
        }

        public virtual IQueryable<T> Data => _dbSet;

        public virtual IQueryable<T> GetAsync(List<Expression<Func<T, bool>>> filters = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            if (filters != null)
            {
                foreach (var filter in filters)
                {
                    query = query.Where(filter);
                }
            }

            return query;
        }

        public virtual async Task<List<T>> GetPagedAsync(List<Expression<Func<T, bool>>> filters = null, /*Func<T, object> orderBy = null,*/ int count = 10, int page = 0, params Expression<Func<T, object>>[] includes)
        {
            return await GetAsync(filters, includes)
                .Skip(page * count)
                .Take(count)
                .ToListAsync();
        }

        public virtual Task<List<T>> GetPagedAsync(Expression<Func<T, bool>> filter = null, /*Func<T, object> orderBy = null,*/ int count = 10, int page = 0, params Expression<Func<T, object>>[] includes)
        {
            return GetPagedAsync(filters: filter != null ? new List<Expression<Func<T, bool>>> { filter } : null, /*orderBy: orderBy,*/ count: count, page: page, includes: includes);
        }

        public virtual async Task<T> FirstOrDefaultAsync(List<Expression<Func<T, bool>>> filters = null, /*Func<T,object> orderBy = null,*/ params Expression<Func<T, object>>[] includes)
        {
            return await GetAsync(filters, includes)
                .FirstOrDefaultAsync();
        }

        public virtual Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter = null, /*Func<T,object> orderBy = null,*/ params Expression<Func<T, object>>[] includes)
        {
            return FirstOrDefaultAsync(filters: filter != null ? new List<Expression<Func<T, bool>>> { filter } : null, /*orderBy: orderBy,*/ includes: includes);
        }

        public virtual async Task<int> CountAsync(List<Expression<Func<T, bool>>> filters = null)
        {
            return await GetAsync(filters)
                .CountAsync();
        }

        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> filter = null)
        {
            return await GetAsync(filters: filter != null ? new List<Expression<Func<T, bool>>> { filter } : null)
                .CountAsync();
        }

        public virtual Task<T> GetByIdAsync(object id)
        {
            return _dbSet.FindAsync(id);
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            return (await _dbSet.AddAsync(entity)).Entity;
        }

        public virtual void Delete(T obj)
        {
            var entityToDelete = _dbSet.Find(obj);
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
        }

        public virtual T Update(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
               _dbSet.Attach(entity);
            }
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }
    }
}
