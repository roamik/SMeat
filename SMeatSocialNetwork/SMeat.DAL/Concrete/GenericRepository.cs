using Microsoft.EntityFrameworkCore;
using SMeat.MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SMeat.DAL
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private IApplicationContext context;
        private DbSet<T> dbSet;
        public GenericRepository(IApplicationContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public virtual IQueryable<T> Data { get { return dbSet; } }

        public virtual IQueryable<T> GetAsync(List<Expression<Func<T, bool>>> filters = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = dbSet;
            if (includes != null)
            {
                foreach (Expression<Func<T, object>> include in includes)
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
            return dbSet.FindAsync(id);
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            return (await dbSet.AddAsync(entity)).Entity;
        }

        public virtual void Delete(T obj)
        {
            T entityToDelete = dbSet.Find(obj);
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual T Update(T entity)
        {
            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            context.Entry(entity).State = EntityState.Modified;
            return entity;
        }
    }
}
