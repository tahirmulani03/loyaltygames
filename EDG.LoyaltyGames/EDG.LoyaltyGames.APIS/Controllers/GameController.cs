using EDG.LoyaltyGames.Core.Entites;
using EDG.LoyaltyGames.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace EDG.LoyaltyGames.APIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        public GameController(IGameService gameService)
        {
            _gameService = gameService ?? throw new ArgumentNullException(nameof(gameService));
        }
        [HttpGet("v1/games")]
        public async Task<IActionResult> GetGames() {
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

        [HttpPost("v1/games")]
        public async Task<IActionResult> AddAsync(GameRequest gameRequest) {
            try
            {
                await _gameService.CreateAsync(gameRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
