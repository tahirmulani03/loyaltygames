using EDG.LoyaltyGames.Core.Entites.Settings;
using EDG.LoyaltyGames.Core.Interfaces;
using EDG.LoyaltyGames.Infrastructure.Repositories;
using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EDG.LoyaltyGames.Infrastructure.Data
{
    public class MongodbContext: IMongodbContext
    {
        private readonly string? _databaseName;
        private readonly IMongoClient _mongoClient;
        private readonly ILogger<MongodbContext> _logger;
        private readonly TelemetryClient _telemetryClient;
        public MongodbContext(IOptions<MongoDbSettings> mongoDbSettings,ILogger<MongodbContext> logger, IMongoClient mongoClient, TelemetryClient telemetryClient)
        {            
            _databaseName = mongoDbSettings.Value.DatabaseName;            
            _mongoClient = mongoClient ?? throw new ArgumentNullException(nameof(mongoClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _telemetryClient = telemetryClient ?? throw new ArgumentNullException(nameof(telemetryClient));
        }

        public IMongoDatabase GetDatabase()
        {
            try
            {
                _logger.LogInformation("Create database.");
                _telemetryClient.TrackTrace($"{nameof(MongodbContext)} : Create database.");
                IMongoDatabase mongoDatabase = _mongoClient.GetDatabase(_databaseName);

                return mongoDatabase;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                _telemetryClient.TrackException(ex);
                throw;
            }
        }

    }
}
