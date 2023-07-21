using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDG.LoyaltyGames.Core.Entites
{
    public class GameCategory: BaseEntity
    {
        public required string CategoryName { get; set; }
    }
}
