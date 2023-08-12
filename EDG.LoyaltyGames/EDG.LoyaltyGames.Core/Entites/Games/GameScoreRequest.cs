using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDG.LoyaltyGames.Core.Entites.Games
{
    public class GameScoreRequest
    {
        public int GameId { get; set; }
        public Guid UserId { get; set; }
        public int GameScore { get; set; }
        public int GameLevel { get; set; }
        public string? UserName { get; set; }
        public string? GameState { get; set; }
        public DateTime GameStartDateTime { get; set; }
        public DateTime GameEndDateTime { get; set; }
    }
}
