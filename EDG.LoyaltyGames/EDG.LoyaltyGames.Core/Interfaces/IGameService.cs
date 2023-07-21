using EDG.LoyaltyGames.Core.Entites;
using MongoDB.Bson;

namespace EDG.LoyaltyGames.Core.Interfaces
{
    public interface IGameService
    {
        Task<IReadOnlyList<GameEntity>> GetGamesAsync();
        Task<GameEntity> GetGameAsync(ObjectId gameId);
        Task CreateAsync(GameRequest gameRequest);
        Task<bool> UpdateAsync(GameEntity gameEntity);
        Task<bool> DeleteAsync(ObjectId gameId);
        Task<GameEntity> UpdateScoreAsync(GameEntity gameEntity);

    }
}
