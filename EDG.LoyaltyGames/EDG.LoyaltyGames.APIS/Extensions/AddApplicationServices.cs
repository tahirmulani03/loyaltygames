using EDG.LoyaltyGames.Core.Interfaces.CacheService;
using EDG.LoyaltyGames.Core.Interfaces.Games;
using EDG.LoyaltyGames.Core.Interfaces.LeaderBoard;
using EDG.LoyaltyGames.Infrastructure.Data;
using EDG.LoyaltyGames.Infrastructure.KeyVault;
using EDG.LoyaltyGames.Infrastructure.RedisCache;
using EDG.LoyaltyGames.Infrastructure.Repositories;
using EDG.LoyaltyGames.Services.Game;
using EDG.LoyaltyGames.Services.LeaderBoard;

namespace EDG.LoyaltyGames.APIS.Extensions
{
    public static class AddApplicationServices
    {
        public static IServiceCollection AddApplicationServicesExtension(this IServiceCollection services)
        {  
            services.AddSingleton<IGameService, GameService>();
            services.AddSingleton<ILeaderBoardService, LeaderBoardService>();
            return services;
        }
    }
}
