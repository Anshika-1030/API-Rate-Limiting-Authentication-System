using StackExchange.Redis;
using Microsoft.Extensions.Configuration;

namespace SecureRateLimitAPI.Helpers
{
    public class RedisHelper
    {
        private static ConnectionMultiplexer _redis;

        public RedisHelper(IConfiguration configuration)
        {
            if (_redis == null)
            {
                var connection = configuration.GetSection("Redis:Connection").Value;
                _redis = ConnectionMultiplexer.Connect(connection);
            }
        }

        public IDatabase GetDatabase()
        {
            return _redis.GetDatabase();
        }
    }
}
