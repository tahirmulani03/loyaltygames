using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDG.LoyaltyGames.Core.Entites
{
    public class GameScoreEntity
    {
        public int GameScoreId { get; set; }
        public Guid UserId { get; set; }
        public int GameScore { get; set; }
        public string? GameLevel { get; set; }
        public string? GameState { get; set; }
        public required string UserName { get; set; }
        public DateTime GameStartDateTime { get; set; }
        public DateTime GameEndDateTime { get; set;}
    }
}
