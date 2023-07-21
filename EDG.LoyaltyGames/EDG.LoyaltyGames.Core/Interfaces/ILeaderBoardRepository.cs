using EDG.LoyaltyGames.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDG.LoyaltyGames.Core.Interfaces
{
    public interface ILeaderBoardRepository
    {
        Task<IReadOnlyList<LeaderBoardEntity>> GetLeaderBoardsAsync(int gameId);
        Task<bool> UpdateLeaderBoardAsync(LeaderBoardEntity leaderBoardEntity);
        Task<bool> DeleteLeaderBoardAsync(int gameId);
    }
}
