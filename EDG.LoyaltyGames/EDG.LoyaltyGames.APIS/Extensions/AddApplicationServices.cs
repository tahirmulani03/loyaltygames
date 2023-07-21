using EDG.LoyaltyGames.Core.Interfaces;
using EDG.LoyaltyGames.Infrastructure.Data;
using EDG.LoyaltyGames.Infrastructure.KeyVault;
using EDG.LoyaltyGames.Infrastructure.Repositories;
using EDG.LoyaltyGames.Services.Game;

namespace EDG.LoyaltyGames.APIS.Extensions
{
    public static class AddApplicationServices
    {
        public static IServiceCollection AddApplicationServicesExtension(this IServiceCollection services)
        {            
            services.AddScoped<IKeyVaultManager, KeyVaultManager>();
            services.AddScoped<IMongodbContext, MongodbContext>();
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IGameService, GameService>();

            return services;
        }
    }
}
