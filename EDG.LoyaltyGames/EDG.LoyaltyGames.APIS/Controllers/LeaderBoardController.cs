using EDG.LoyaltyGames.Core.Entites;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EDG.LoyaltyGames.APIS.Controllers
{
    [Route("api/v1/games")]
    [ApiController]
    public class LeaderBoardController : ControllerBase
    {
       
        // GET leaderboard for selected game
        [HttpGet("{id}/leaderboard")]
        public async Task<ActionResult<IReadOnlyList<LeaderBoardEntity>>> Get(int id)
        {
            try
            {
                var leaderboard = new List<LeaderBoardEntity>();
                return Ok(leaderboard);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        
    }
}
