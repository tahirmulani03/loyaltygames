using EDG.LoyaltyGames.Core.Entites.Rules;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EDG.LoyaltyGames.APIS.Controllers
{
    [Route("api/v1/games")]
    [ApiController]
    public class RulesController : ControllerBase
    {
        // GET all rules of all games or selected game for admin
        [HttpGet("{gameId}/rules")]
        public async Task <ActionResult<IReadOnlyList<RuleEntity>>> Get(int gameId)
        {
            try
            {
                var rules = new List<RuleEntity>();
                return Ok(rules);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        //Get selected rule for given game
        [HttpGet("{gameId}/rules/{ruleId}")]
        public async Task<ActionResult<RuleEntity>> Get(int gameId, int ruleId)
        {
            try
            {
                var rules = new List<RuleEntity>();
                return Ok(rules);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // Add rules for selected game
        [HttpPost("{gameId}/rules")]
        public async Task<ActionResult> Post(int gameId, [FromBody] RuleRequest ruleRequest)
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

        // Update rule of selected game
        [HttpPut("{gameId}/rules/{ruleId}")]
        public async Task<ActionResult> Put(int gameid, int ruleid, [FromBody] RuleRequest ruleRequest)
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

        // DELETE all rules of selected game
        [HttpDelete("{gameId}/rules")]
        public async Task<ActionResult> Delete(int gameId)
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

        // DELETE individual rule of selected game
        [HttpDelete("{gameId}/rules/{ruleId}")]
        public async Task<ActionResult> Delete(int gameId, int ruleId)
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
    }
}
