using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDG.LoyaltyGames.Core.Entites
{
    public class AuditEntity : BaseEntity
    {
        public int EntityId { get; set; }
        public required string EntityName { get; set; }
    }
}
