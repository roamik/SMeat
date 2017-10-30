﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SMeat.DAL
{
    class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private DbContext context;
        private DbSet<T> dbSet;
        public GenericRepository(DbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public virtual IEnumerable<T> Data { get { return dbSet; } }

        public virtual List<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = dbSet;

            foreach(Expression<Func<T, object>> include in includes)
            {
                query = query.Include(include);
            }

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            return query.ToList();
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

        public virtual T GetById(int id)
        {
            return dbSet.Find(id);
        }

        public virtual bool Insert(T obj)
        {
            dbSet.Add(obj);
            return true;
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

        public virtual bool Update(T obj)
        {
            dbSet.Attach(obj);
            context.Entry(obj).State = EntityState.Modified;
            return true;
        }
    }
}
