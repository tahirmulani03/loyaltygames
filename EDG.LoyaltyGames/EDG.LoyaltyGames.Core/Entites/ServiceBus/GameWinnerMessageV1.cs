using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDG.LoyaltyGames.Core.Entites.ServiceBus
{
    public class GameWinnerMessageV1
    {
        public int Version { get; } = 1;
        public Guid LoyaltyId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string RewardApplicable { get;}
        public int GameScore { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set;}


    }
}
