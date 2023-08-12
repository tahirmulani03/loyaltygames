using EDG.LoyaltyGames.Core.Entites.Games;
using EDG.LoyaltyGames.Core.Interfaces.Games;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using MongoDB.Driver;
using System.Runtime.CompilerServices;

namespace EDG.LoyaltyGames.APIS.Controllers
{
    [Route("api/v1/games")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly ILogger<GameController> _logger;
        private readonly IGameService _gameService;
        private readonly TelemetryClient _telemetryClient;
        public GameController( IGameService gameService, ILogger<GameController> logger, TelemetryClient telemetryClient)
        {
            _gameService = gameService ?? throw new ArgumentNullException(nameof(gameService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _telemetryClient = telemetryClient ?? throw new ArgumentNullException(nameof(telemetryClient));
        }
        // Get all games details
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetGames() {
            try
            {
                _logger.LogInformation("Get all games details.");

                Request.Headers.TryGetValue("Request-Id", out StringValues headerValue);
                // Begin tracking a request
                var requestTelemetry = new RequestTelemetry
                {
                    Name = "api/v1/games",
                    Timestamp = DateTimeOffset.UtcNow
                };                

                // Start the timer to measure request duration
                var timer = System.Diagnostics.Stopwatch.StartNew();

                var games = await _gameService.GetGamesAsync();

                // Stop the timer and set the duration
                timer.Stop();
                requestTelemetry.Duration = timer.Elapsed;
                requestTelemetry.ResponseCode = "200"; // HTTP response code
                requestTelemetry.Success = true;
                //requestTelemetry.Context.Properties["Request-Id"] = headerValue;
                _telemetryClient.Context.Properties["Request-Id"] = headerValue;
                // Track the request
                _telemetryClient.TrackRequest(requestTelemetry);

                return Ok(games);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                _telemetryClient.TrackException(ex);
                return BadRequest(ex.Message);
            }

        }

        // Get selected game details
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetGame(int id)
        {
            try
            {
                var games = await _gameService.GetGamesAsync();
                return Ok(games);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        
        // Onboard/add game in app
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddAsync([FromForm] List<IFormFile> gameMetaData, [FromForm] GameRequest gameRequest) {
            try
            {
                _telemetryClient.TrackRequest("Add Game", DateTimeOffset.Now, TimeSpan.FromMilliseconds(123), "200", true);
                await _gameService.CreateAsync(gameRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                _telemetryClient.TrackException(ex);
                return BadRequest(ex.Message);
            }
        }

        // Update/Edit selected game details
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] GameRequest gameRequest)
        {
            try
            {                
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE selected game
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);                
            }
        }

        // Update/Edit selected game score
        [HttpPut("{id}/score")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateScoreAsync(int id, [FromBody] GameScoreRequest gameRequest)
        {
            try
            {                   
                await _gameService.UpdateScoreAsync(gameRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
