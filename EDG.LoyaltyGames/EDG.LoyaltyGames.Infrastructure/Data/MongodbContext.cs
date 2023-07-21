using EDG.LoyaltyGames.Core.Entites;
using EDG.LoyaltyGames.Core.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EDG.LoyaltyGames.Infrastructure.Data
{
    public class MongodbContext: IMongodbContext
    {
        private readonly IKeyVaultManager _keyVaultManager;
        private readonly string? _databaseName;
        private readonly string _dbSecretKey;
        public MongodbContext(IOptions<MongoDbSettings> mongoDbSettings, IOptions<KeyVaultSettings> keyVaultSettings, IKeyVaultManager keyVaultManager)
        {
            _keyVaultManager = keyVaultManager ?? throw new ArgumentNullException(nameof(keyVaultManager));
            _databaseName = mongoDbSettings.Value.DatabaseName;
            _dbSecretKey = keyVaultSettings.Value.DbSecretKey;
        }

        public async Task<IMongoDatabase> GetDbInstance() {
            try
            {
                var mongoConnectionString = await _keyVaultManager.GetSecret(_dbSecretKey);
                if (mongoConnectionString == null) { 
                throw new ArgumentNullException(nameof(mongoConnectionString));
                }

                MongoClient client = new MongoClient(mongoConnectionString);
                IMongoDatabase mongoDatabase = client.GetDatabase(_databaseName);

                return mongoDatabase;
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
