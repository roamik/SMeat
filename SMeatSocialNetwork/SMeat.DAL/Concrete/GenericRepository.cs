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
        private ApplicationContext context;
        private DbSet<T> dbSet;
        public GenericRepository(ApplicationContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public virtual IQueryable<T> Data { get { return dbSet; } }

        public virtual Task<List<T>> GetAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = dbSet;
            foreach (Expression<Func<T, object>> include in includes) {
                query = query.Include(include);
            }
            if (filter != null) {
                query = query.Where(filter);
            }
            if (orderBy != null){
                query = orderBy(query);
            }
            return query.ToListAsync();
        }

        public virtual IQueryable<T> Query(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            return query;
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
