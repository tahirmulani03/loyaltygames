using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDG.LoyaltyGames.Core.Entites.Settings
{
    public class MongoDbSettings
    {
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }
        public string ScoreCollectionName { get; set; }
    }
}
