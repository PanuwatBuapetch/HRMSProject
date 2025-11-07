using Datamodels.Hrms;
using HRMS_API.Service;
using Microsoft.AspNetCore.Mvc;

namespace HRMS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamController : ControllerBase
    {
        private readonly TeamService _teamService;

        public TeamController(TeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetAllTeams()
        {
            var items = await _teamService.GetAllTeamsAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> GetTeam(string id)
        {
            var item = await _teamService.GetTeamByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<Team>> AddTeam(Team team)
        {
            var newItem = await _teamService.AddTeamAsync(team);
            return CreatedAtAction(nameof(GetTeam), new { id = newItem.TeamId }, newItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeam(string id, Team team)
        {
            if (id != team.TeamId) return BadRequest();
            var success = await _teamService.UpdateTeamAsync(id, team);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(string id)
        {
            var success = await _teamService.DeleteTeamAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}