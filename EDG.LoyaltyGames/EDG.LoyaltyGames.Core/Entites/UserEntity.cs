using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDG.LoyaltyGames.Core.Entites
{
    public class UserEntity
    {
        public Guid UserId { get; set; }
        public required string UserName { get; set; }
        public Guid LoyaltyId { get; set; }
        public int? BrandId { get; set; }
    }
}
