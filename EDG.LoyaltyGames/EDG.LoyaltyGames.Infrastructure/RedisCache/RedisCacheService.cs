using EDG.LoyaltyGames.Core.Interfaces.CacheService;
using EDG.LoyaltyGames.Core.Interfaces.LeaderBoard;
using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace EDG.LoyaltyGames.Infrastructure.RedisCache
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDatabase _redisDatabase;
        private readonly ILogger<RedisCacheService> _logger;
        private readonly TelemetryClient _telemetryClient;

        public RedisCacheService(IConnectionMultiplexer redisConnection, ILogger<RedisCacheService> logger, TelemetryClient telemetryClient)
        {
            _redisDatabase = redisConnection.GetDatabase();            
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _telemetryClient = telemetryClient ?? throw new ArgumentNullException(nameof(telemetryClient));
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var RedisCacheKey = new Dictionary<string, string>{
                { "key", key }
                };
            try
            {
                _logger.LogInformation("RedisCacheService: get Cache key Value");

                _telemetryClient.TrackEvent($"{nameof(RedisCacheService)} : GetAsync", RedisCacheKey);
                _telemetryClient.TrackDependency("Redis Cache", $"Get {key} key value from Redis Cache", key, DateTimeOffset.Now, TimeSpan.FromSeconds(1), true);

                var value = await _redisDatabase.StringGetAsync(key);
                if (value.HasValue)
                {
                    return JsonConvert.DeserializeObject<T>(value);
                }

                return default;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _telemetryClient.TrackException(ex, RedisCacheKey);
                return default;
            }     
            
        }

        public async Task RemoveAsync(string key)
        {
            await _redisDatabase.KeyDeleteAsync(key);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan expiration)
        {
            var RedisCacheKey = new Dictionary<string, string>{
                { "key", key },
                { "Value Type", typeof(T).Name.ToString()}
                };
            try
            {
                _telemetryClient.TrackEvent($"{nameof(RedisCacheService)} : SetAsync", RedisCacheKey);
                _telemetryClient.TrackDependency("Redis Cache", $"Set {key} key value in Redis Cache", key, DateTimeOffset.Now, TimeSpan.FromSeconds(1), true);

                var serializedValue = JsonConvert.SerializeObject(value);
                await _redisDatabase.StringSetAsync(key, serializedValue, expiration, When.Always, CommandFlags.None);
            }
            catch (Exception ex)
            {
                _telemetryClient.TrackException(ex, RedisCacheKey);
                throw;
            }
            
        }
    }
}
