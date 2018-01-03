﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SMeat.MODELS
{
    //its a interface to put multiply contexts as IContext to provide DI
    //all method signatures are from basic context
    public interface IContext : IDisposable
    {        
        DatabaseFacade Database { get; }

        ChangeTracker ChangeTracker { get; }

        IModel Model { get; }

        EntityEntry Add( object entity);

        EntityEntry<TEntity> Add<TEntity>( TEntity entity) where TEntity : class;

        Task<EntityEntry<TEntity>> AddAsync<TEntity>( TEntity entity, CancellationToken cancellationToken = default(CancellationToken)) where TEntity : class;

        void AddRange( IEnumerable<object> entities);

        void AddRange( params object[] entities);

        Task AddRangeAsync( params object[] entities);

        Task AddRangeAsync( IEnumerable<object> entities, CancellationToken cancellationToken = default(CancellationToken));

        EntityEntry<TEntity> Attach<TEntity>( TEntity entity) where TEntity : class;

        EntityEntry Attach( object entity);

        void AttachRange( IEnumerable<object> entities);

        void AttachRange( params object[] entities);

        EntityEntry<TEntity> Entry<TEntity>( TEntity entity) where TEntity : class;

        EntityEntry Entry( object entity);

        object Find( Type entityType,  params object[] keyValues);

        TEntity Find<TEntity>( params object[] keyValues) where TEntity : class;

        Task<object> FindAsync( Type entityType,  object[] keyValues, CancellationToken cancellationToken);

        Task<TEntity> FindAsync<TEntity>( object[] keyValues, CancellationToken cancellationToken) where TEntity : class;

        Task<TEntity> FindAsync<TEntity>( params object[] keyValues) where TEntity : class;

        Task<object> FindAsync( Type entityType,  params object[] keyValues);

        EntityEntry<TEntity> Remove<TEntity>( TEntity entity) where TEntity : class;

        EntityEntry Remove( object entity);
        void RemoveRange(IEnumerable<object> entities);
        void RemoveRange(params object[] entities);
        int SaveChanges();
        int SaveChanges(bool acceptAllChangesOnSuccess);
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken));
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}