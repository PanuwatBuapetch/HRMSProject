using Datamodels.Hrms;
using HRMS_API.Service;
using Microsoft.AspNetCore.Mvc;

namespace HRMS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MissionController : ControllerBase
    {
        private readonly MissionService _missionService;

        public MissionController(MissionService missionService)
        {
            _missionService = missionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mission>>> GetAllMissions()
        {
            var items = await _missionService.GetAllMissionsAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Mission>> GetMission(string id)
        {
            var item = await _missionService.GetMissionByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<Mission>> AddMission(Mission mission)
        {
            var newItem = await _missionService.AddMissionAsync(mission);
            return CreatedAtAction(nameof(GetMission), new { id = newItem.MissionId }, newItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMission(string id, Mission mission)
        {
            if (id != mission.MissionId) return BadRequest();
            var success = await _missionService.UpdateMissionAsync(id, mission);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMission(string id)
        {
            var success = await _missionService.DeleteMissionAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}