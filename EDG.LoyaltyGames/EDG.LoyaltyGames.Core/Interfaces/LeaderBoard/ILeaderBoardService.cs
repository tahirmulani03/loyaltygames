using EDG.LoyaltyGames.Core.Entites.LeaderBoard;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDG.LoyaltyGames.Core.Interfaces.LeaderBoard
{
    public interface ILeaderBoardService
    {
        Task<IList<LeaderBoardEntity>> GetLeaderBoardById(ObjectId gameId, Guid userId);
    }
}
