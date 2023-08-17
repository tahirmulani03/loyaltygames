using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDG.LoyaltyGames.Core.Entites.Settings;
using EDG.LoyaltyGames.Core.Interfaces;
using EDG.LoyaltyGames.Core.Interfaces.Repository;
using EDG.LoyaltyGames.Core.Interfaces.ServiceBus;
using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EDG.LoyaltyGames.Infrastructure.Repositories
{
    public class MongoRepository<T> : IRepository<T> where T : class
    {
        private readonly ILogger<GameRepository> _logger;
        private readonly IMongodbContext _mongodbContext;
        private readonly TelemetryClient _telemetryClient;
        private readonly MongoDbSettings _mongoDbSettings;
        public MongoRepository(IOptions<MongoDbSettings> mongoDbSettings, IMongodbContext mongodbContext, ILogger<GameRepository> logger,
            TelemetryClient telemetryClient)
        {
            _mongoDbSettings = mongoDbSettings.Value;
        }
        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task InsertAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
