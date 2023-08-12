using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDG.LoyaltyGames.Core.Entites.ServiceBus
{
    public class ServiceBusSetting
    {
        public int MessageDeliveryCount { get; set; }
        public int MessageTTLDays { get; set; }
        public int MessageLockDurationMinutes { get; set; }
        public string WinnerQueueName { get; set; }
        public string RewardTopicName { get; set; }
        public string RewardSubscription { get; set; }
        public int MaxConcurrentCalls { get; set; }


    }
}
