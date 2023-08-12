using EDG.LoyaltyGames.Core.Interfaces;
using EDG.LoyaltyGames.Core.Interfaces.CacheService;
using EDG.LoyaltyGames.Core.Interfaces.Games;
using EDG.LoyaltyGames.Core.Interfaces.ServiceBus;
using EDG.LoyaltyGames.Infrastructure.Data;
using EDG.LoyaltyGames.Infrastructure.KeyVault;
using EDG.LoyaltyGames.Infrastructure.RedisCache;
using EDG.LoyaltyGames.Infrastructure.Repositories;
using EDG.LoyaltyGames.Infrastructure.ServiceBus;
using Microsoft.Extensions.DependencyInjection;

namespace EDG.LoyaltyGames.Infrastructure.Extension
{
    public static class AddServices
    {
        public static IServiceCollection AddServicesExtension(this IServiceCollection services)
        {
            services.AddSingleton<IKeyVaultManager, KeyVaultManager>();
            services.AddSingleton<IMongodbContext, MongodbContext>();
            services.AddSingleton<IGameRepository, GameRepository>();
            services.AddSingleton<ICacheService, RedisCacheService>();
            services.AddSingleton<IGameServiceBusClient, GameServiceBusClient>();
            services.AddSingleton<IReceiveServiceBusClient, ReceiveServiceBusClient>();
            services.AddHostedService<LoyaltyRewardHandler>();

            return services;
        }
    }
}
