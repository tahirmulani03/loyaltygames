using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDG.LoyaltyGames.Core.Entites
{
    public class RuleEntity : BaseEntity
    {
        public int GameId { get; set; }
        public required string RuleName { get; set; }
        public required string Expression { get; set; }
        public required string RuleOprator { get; set; }
        public required string SuccessEvent { get; set; }
        public required string FailureEvent { get; set; }
        public bool IsRequired { get; set; }
        public bool IsEnabled { get; set; }

    }
}
