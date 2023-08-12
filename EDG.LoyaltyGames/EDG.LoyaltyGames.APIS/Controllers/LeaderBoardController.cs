using EDG.LoyaltyGames.Core.Entites.LeaderBoard;
using EDG.LoyaltyGames.Core.Interfaces.LeaderBoard;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EDG.LoyaltyGames.APIS.Controllers
{
    [Route("api/v1/games")]
    [ApiController]
    public class LeaderBoardController : ControllerBase
    {
        private readonly ILeaderBoardService _leaderBoardService;
        private readonly ILogger<LeaderBoardController> _logger;
        private readonly TelemetryClient _telemetryClient;

        public LeaderBoardController(ILogger<LeaderBoardController> logger, ILeaderBoardService leaderBoardService, TelemetryClient telemetryClient)
        {

            _logger = logger?? throw new ArgumentNullException(nameof(logger));
            _leaderBoardService = leaderBoardService ?? throw new ArgumentNullException($"{nameof(leaderBoardService)}");
            _telemetryClient = telemetryClient ?? throw new ArgumentNullException(nameof(telemetryClient));

        }

        // GET leaderboard for selected game
        [HttpGet("{gameId}/leaderboard")]
        public async Task<ActionResult<IReadOnlyList<LeaderBoardEntity>>> Get(string gameId)
        {
            try
            {       
                var userId = new Guid();
                _logger.LogInformation("Get Leaderboard for game.");
                _telemetryClient.TrackRequest("Get LeaderBoard", DateTimeOffset.Now, TimeSpan.FromMilliseconds(123), "200", true);
                var leaderboard = await _leaderBoardService.GetLeaderBoardById(new ObjectId(gameId), userId);
                return Ok(leaderboard);
            }
            catch (Exception ex)
            {
                _telemetryClient.TrackException(ex);
                return BadRequest(ex.Message);
            }
            
        }
        
    }
}
