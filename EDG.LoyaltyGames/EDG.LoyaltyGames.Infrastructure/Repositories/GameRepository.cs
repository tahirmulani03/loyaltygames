using EDG.LoyaltyGames.Core.Entites;
using EDG.LoyaltyGames.Core.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EDG.LoyaltyGames.Infrastructure.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly IMongodbContext _mongodbContext;
        private readonly string _gameCollection;
        public GameRepository(IOptions<MongoDbSettings> mongoDbSettings, IMongodbContext mongodbContext)
        {
            _mongodbContext = mongodbContext ?? throw new ArgumentNullException(nameof(mongodbContext));
            _gameCollection = mongoDbSettings.Value.CollectionName;
        }

        public async Task CreateAsync(GameRequest gameEntity)
        {
            try
            {
                var dbInstance = await _mongodbContext.GetDbInstance();
                var gameList = dbInstance.GetCollection<GameRequest>(_gameCollection);
                gameList.InsertOne(gameEntity);                
            }
            catch (Exception ex)
            {
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
                var dbInstance = await _mongodbContext.GetDbInstance();
                var gameList = dbInstance.GetCollection<GameEntity>(_gameCollection);                
                var gameEntityList = await gameList.Find(new BsonDocument()).ToListAsync();
                return gameEntityList;
            }
            catch (Exception ex)
            {

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
    }
}
