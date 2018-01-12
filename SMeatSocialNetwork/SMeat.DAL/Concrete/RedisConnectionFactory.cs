using System;
using Microsoft.Extensions.Options;
using SMeat.DAL.Abstract;
using SMeat.MODELS;
using SMeat.MODELS.Options;
using StackExchange.Redis;

namespace SMeat.DAL.Concrete {
    public class RedisConnectionFactory : IRedisConnectionFactory, IDisposable {
        private readonly Lazy<ConnectionMultiplexer> _connection;
        private readonly IOptions<RedisConfiguration> _redisOptions;
        private IDatabase _database;

        public RedisConnectionFactory ( IOptions<RedisConfiguration> redisOptions ) {
            _redisOptions = redisOptions;
            _connection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(_redisOptions.Value.ConnectionString));
        }

        public IDatabase Database => _database ?? (_database = _connection.Value.GetDatabase());
        public ConnectionMultiplexer Connection => _connection.Value;

        private bool _disposed;
        protected virtual void Dispose ( bool disposing ) {
            if ( _disposed ) return;
            if ( disposing ) {
                _connection.Value.Dispose();
            }
            _disposed = true;
        }

        public void Dispose () {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}