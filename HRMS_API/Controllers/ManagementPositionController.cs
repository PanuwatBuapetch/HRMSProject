using Datamodels.Hrms;
using HRMS_API.Service;
using Microsoft.AspNetCore.Mvc;

namespace HRMS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ManagementPositionController : ControllerBase
    {
        private readonly ManagementPositionService _managementPositionService;

        public ManagementPositionController(ManagementPositionService managementPositionService)
        {
            _managementPositionService = managementPositionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ManagementPosition>>> GetAllManagementPositions()
        {
            var items = await _managementPositionService.GetAllManagementPositionsAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ManagementPosition>> GetManagementPosition(string id)
        {
            var item = await _managementPositionService.GetManagementPositionByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<ManagementPosition>> AddManagementPosition(ManagementPosition managementPosition)
        {
            try
            {
                var newItem = await _managementPositionService.AddManagementPositionAsync(managementPosition);
                return CreatedAtAction(nameof(GetManagementPosition), new { id = newItem.ManagementPositionId }, newItem);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message); // ส่ง 409 Conflict กลับไปถ้าซ้ำ
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateManagementPosition(string id, ManagementPosition managementPosition)
        {
            if (id != managementPosition.ManagementPositionId) return BadRequest();
            var success = await _managementPositionService.UpdateManagementPositionAsync(id, managementPosition);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteManagementPosition(string id)
        {
            var success = await _managementPositionService.DeleteManagementPositionAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}