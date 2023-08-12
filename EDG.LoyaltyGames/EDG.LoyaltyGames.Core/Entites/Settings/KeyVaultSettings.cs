using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDG.LoyaltyGames.Core.Entites.Settings
{
    public class KeyVaultSettings
    {
        public string DbSecretKey { get; set; }
        public string? VaultUri { get; set; }
        public string RedisKey { get; set; }
        public string ServiceBusKey { get; set; }
        public string SignalRKey { get; set; }
    }
}
