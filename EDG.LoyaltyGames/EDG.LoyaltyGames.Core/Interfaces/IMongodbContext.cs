using MongoDB.Driver;

namespace EDG.LoyaltyGames.Core.Interfaces
{
    public interface IMongodbContext
    {
        Task<IMongoDatabase> GetDbInstance();
    }
}
