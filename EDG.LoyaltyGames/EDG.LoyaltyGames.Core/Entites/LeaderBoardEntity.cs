namespace EDG.LoyaltyGames.Core.Entites
{
    public class LeaderBoardEntity
    {
        public Guid UserId { get; set; }
        public required string UserName { get; set; }
        public int Rank { get; set; }
        public int BrandId { get; set; }
    }
}
