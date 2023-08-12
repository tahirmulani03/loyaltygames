using EDG.LoyaltyGames.Core.Entites.LeaderBoard;
using EDG.LoyaltyGames.Core.Interfaces.CacheService;
using EDG.LoyaltyGames.Core.Interfaces.Games;
using EDG.LoyaltyGames.Core.Interfaces.LeaderBoard;
using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;

namespace EDG.LoyaltyGames.Services.LeaderBoard
{
    public class LeaderBoardService : ILeaderBoardService
    {
        private readonly ILogger<LeaderBoardService> _logger;
        private readonly IGameRepository _gameRepository;
        private readonly ICacheService _cacheService;
        private readonly TelemetryClient _telemetryClient;

        public LeaderBoardService(ILogger<LeaderBoardService> logger, IGameRepository gameRepository, ICacheService cacheService, TelemetryClient telemetryClient)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
            _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
            _telemetryClient = telemetryClient ?? throw new ArgumentNullException(nameof(telemetryClient));
        }
        public async Task<IList<LeaderBoardEntity>> GetLeaderBoardById(ObjectId gameId, Guid userId)
        {
            var LeaderBoardProperties = new Dictionary<string, string>
            {
                { "GameId", gameId.ToString() },
                    { "UserId", userId.ToString() }
                };
            try
            {
                _logger.LogInformation("Get game leaderboard.");
                _telemetryClient.TrackEvent($"{nameof(LeaderBoardService)} : GetLeaderBoardById", LeaderBoardProperties);

                _telemetryClient.TrackDependency("Redis Cache", "Get Leaderboard from Redis Cache", gameId.ToString(), DateTimeOffset.Now, TimeSpan.FromSeconds(1), true);

                var cachedData = await _cacheService.GetAsync<IList<LeaderBoardEntity>>(gameId.ToString());
                if (cachedData != null)
                {
                    return cachedData;
                }
                _telemetryClient.TrackDependency("Redis Cache", "Get Leaderboard from Database", gameId.ToString(), DateTimeOffset.Now, TimeSpan.FromSeconds(1), true);

                var LeaderBoard = await LeaderBoardFromDb();
                await _cacheService.SetAsync(gameId.ToString(), LeaderBoard, TimeSpan.FromDays(1));
                return LeaderBoard;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                _telemetryClient.TrackException(ex, LeaderBoardProperties);
                throw;
            }
        }

        private async Task<IList<LeaderBoardEntity>> LeaderBoardFromDb()
        {
            try
            {
                _logger.LogInformation("Get Current user rank from database.");
                var games = await _gameRepository.GetAllAsync();
                var leaderBoardList = new List<LeaderBoardEntity>();
                leaderBoardList.Add(new LeaderBoardEntity { UserName = "Joy", Rank = 1, BrandId = 1, score = 1200, UserId = Guid.NewGuid() });
                leaderBoardList.Add(new LeaderBoardEntity { UserName = "Ram", Rank = 2, BrandId = 1, score = 1100, UserId = Guid.NewGuid() });
                leaderBoardList.Add(new LeaderBoardEntity { UserName = "Lila", Rank = 3, BrandId = 1, score = 1000, UserId = Guid.NewGuid() });
                leaderBoardList.Add(new LeaderBoardEntity { UserName = "Ali", Rank = 4, BrandId = 1, score = 800, UserId = Guid.NewGuid() });
                leaderBoardList.Add(new LeaderBoardEntity { UserName = "Swap", Rank = 5, BrandId = 1, score = 790, UserId = Guid.NewGuid() });
                leaderBoardList.Add(new LeaderBoardEntity { UserName = "Ajay", Rank = 6, BrandId = 1, score = 690, UserId = Guid.NewGuid() });
                leaderBoardList.Add(new LeaderBoardEntity { UserName = "Prem", Rank = 7, BrandId = 1, score = 590, UserId = Guid.NewGuid() });
                leaderBoardList.Add(new LeaderBoardEntity { UserName = "Om", Rank = 8, BrandId = 1, score = 490, UserId = Guid.NewGuid() });
                leaderBoardList.Add(new LeaderBoardEntity { UserName = "Ravi", Rank = 9, BrandId = 1, score = 390, UserId = Guid.NewGuid() });
                leaderBoardList.Add(new LeaderBoardEntity { UserName = "Joen", Rank = 10, BrandId = 1, score = 290, UserId = Guid.NewGuid() });
                return leaderBoardList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        private async Task<LeaderBoardEntity> GetCurrentUserRank(int userId) {

            try
            {
                _logger.LogInformation("Get Current user rank from database.");
                var games = await _gameRepository.GetAllAsync();
                return new LeaderBoardEntity { UserName = "Mick", Rank = 100, BrandId = 1, score = 10 };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message,ex);
                throw;
            }
        }
    }
}
