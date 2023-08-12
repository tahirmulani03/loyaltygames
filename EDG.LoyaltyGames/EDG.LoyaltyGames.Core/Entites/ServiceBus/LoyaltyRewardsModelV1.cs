using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDG.LoyaltyGames.Core.Entites.ServiceBus
{
    public class LoyaltyRewardsModelV1
    {
        public int Version { get; set; }
        public Guid RewardId { get; set; }
        public string RewardName { get; set; }
        public string Description { get; set; }
        public string RewardType { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public bool IsActive { get; set; }
    }
}
