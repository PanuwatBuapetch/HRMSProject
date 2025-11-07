using Datamodels.Hrms;
using HRMS_API.Service;
using Microsoft.AspNetCore.Mvc;

namespace HRMS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkUnitController : ControllerBase
    {
        private readonly WorkUnitService _workUnitService;

        public WorkUnitController(WorkUnitService workUnitService)
        {
            _workUnitService = workUnitService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkUnit>>> GetAllWorkUnits()
        {
            var items = await _workUnitService.GetAllWorkUnitsAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WorkUnit>> GetWorkUnit(string id)
        {
            var item = await _workUnitService.GetWorkUnitByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<WorkUnit>> AddWorkUnit(WorkUnit workUnit)
        {
            var newItem = await _workUnitService.AddWorkUnitAsync(workUnit);
            return CreatedAtAction(nameof(GetWorkUnit), new { id = newItem.UnitId }, newItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWorkUnit(string id, WorkUnit workUnit)
        {
            if (id != workUnit.UnitId) return BadRequest();
            var success = await _workUnitService.UpdateWorkUnitAsync(id, workUnit);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkUnit(string id)
        {
            var success = await _workUnitService.DeleteWorkUnitAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}