using StackExchange.Redis;

namespace SMeat.DAL.Abstract {
    public interface IRedisConnectionFactory {
        ConnectionMultiplexer Connection { get; }
        IDatabase Database { get; }
    }
}