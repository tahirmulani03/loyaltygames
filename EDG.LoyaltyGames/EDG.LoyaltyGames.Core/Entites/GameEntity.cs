using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDG.LoyaltyGames.Core.Entites
{
    public class GameEntity : BaseEntity
    {
        public required string GameName { get; set; }
        public required string GameUrl { get; set; }
        public string Thumbnail { get; set; }
        public required bool IsSocial { get; set; }
        public required bool IsDeleted { get; set; }
        public string GameMetaData { get; set; }
        public int PlayersCount { get; set; }
        public string? GameUnits { get; set; }
        public int GameCategory { get; set; }
        public int BrandId { get; set; }
        public string? BrandName { get; set;}

    }
}
