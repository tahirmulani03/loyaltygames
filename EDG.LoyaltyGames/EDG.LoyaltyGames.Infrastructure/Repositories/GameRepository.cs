using EDG.LoyaltyGames.Core.Entites.Games;
using EDG.LoyaltyGames.Core.Entites.ServiceBus;
using EDG.LoyaltyGames.Core.Entites.Settings;
using EDG.LoyaltyGames.Core.Interfaces;
using EDG.LoyaltyGames.Core.Interfaces.Games;
using EDG.LoyaltyGames.Core.Interfaces.ServiceBus;
using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EDG.LoyaltyGames.Infrastructure.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly ILogger<GameRepository> _logger;
        private readonly IMongodbContext _mongodbContext;
        private readonly TelemetryClient _telemetryClient;
        private readonly MongoDbSettings _mongoDbSettings;

        //Tobe removed once done with Demo
        private readonly IGameServiceBusClient _gameServiceBusClient;
        private readonly IReceiveServiceBusClient _receiveServiceBusClient; 
        public GameRepository(IOptions<MongoDbSettings> mongoDbSettings, IMongodbContext mongodbContext, ILogger<GameRepository> logger,
            IGameServiceBusClient gameServiceBusClient, IReceiveServiceBusClient receiveServiceBusClient, TelemetryClient telemetryClient)
        {
            _mongodbContext = mongodbContext ?? throw new ArgumentNullException(nameof(mongodbContext));
            _mongoDbSettings = mongoDbSettings.Value;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _telemetryClient = telemetryClient ?? throw new ArgumentNullException(nameof(telemetryClient));

            //Tobe removed once done with Demo
            _gameServiceBusClient = gameServiceBusClient ?? throw new ArgumentNullException(nameof(gameServiceBusClient));
            _receiveServiceBusClient = receiveServiceBusClient ?? throw new ArgumentNullException(nameof(receiveServiceBusClient));
            
        }

        public async Task CreateAsync(GameRequest gameEntity)
        {
            var gameProperties = new Dictionary<string, string>
                {
                    { "GameName", gameEntity.GameName },
                    { "BrandName", gameEntity.BrandName }
                };
            try
            {
                _logger.LogInformation("Start adding games in GameReposiory");
                _telemetryClient.TrackEvent($"{nameof(GameRepository)} : CreateAsync", gameProperties);
                _telemetryClient.TrackDependency("Mongo Database", "Mongo Database Open", gameEntity.GameName, DateTimeOffset.Now, TimeSpan.FromSeconds(1), true);

                var dbInstance = _mongodbContext.GetDatabase();
                var gameList = dbInstance.GetCollection<GameRequest>(_mongoDbSettings.CollectionName);
                await gameList.InsertOneAsync(gameEntity);                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _telemetryClient.TrackException(ex, gameProperties);
                throw;
            }
        }

        public Task<bool> DeleteAsync(GameEntity gameEntity)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<GameEntity>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation($"{nameof(GetAllAsync)}");

                var dbInstance = _mongodbContext.GetDatabase();
                var gameList = dbInstance.GetCollection<GameEntity>(_mongoDbSettings.CollectionName);                
                var gameEntityList = await gameList.Find(new BsonDocument()).ToListAsync();
                return gameEntityList;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message);
                throw;
            }
            
        }

        public Task<GameEntity> GetByIdAsync(int gameId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(GameEntity gameEntity)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateScoreAsync(GameScoreRequest gameScoreRequest)
        {
            try
            {
                _logger.LogInformation("Game score insert started.");

                var dbInstance = _mongodbContext.GetDatabase();
                
                var gameList = dbInstance.GetCollection<GameScoreRequest>(_mongoDbSettings.ScoreCollectionName);
                await gameList.InsertOneAsync(gameScoreRequest);


                //Tobe removed once done with Demo
                var gameWinnerModel = new GameWinnerMessageV1 { BrandId = 1, BrandName = "BWS", GameScore = 2000, LoyaltyId = Guid.NewGuid(), UserId = Guid.NewGuid(), UserName = "Tahir" };

                await _gameServiceBusClient.SendAsync<GameWinnerMessageV1>(gameWinnerModel, "game-winner");

                //Tobe removed once done with Demo
                var sbResponse = await _receiveServiceBusClient.ReceiveAsync<GameWinnerMessageV1>("game-winner");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
