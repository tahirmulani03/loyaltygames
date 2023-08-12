using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDG.LoyaltyGames.Core.Entites.Games
{
    public class GameCategory : BaseEntity
    {
        public required string CategoryName { get; set; }
    }
}
