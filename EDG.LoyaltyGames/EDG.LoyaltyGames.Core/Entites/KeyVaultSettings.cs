using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDG.LoyaltyGames.Core.Entites
{
    public class KeyVaultSettings
    {
        public string DbSecretKey { get; set; }
        public string? VaultUri { get; set;}
    }
}
