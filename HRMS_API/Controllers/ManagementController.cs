using Datamodels.Hrms;
using HRMS_API.Service;
using Microsoft.AspNetCore.Mvc;

namespace HRMS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ManagementController : ControllerBase
    {
        private readonly ManagementService _managementService;

        public ManagementController(ManagementService managementService)
        {
            _managementService = managementService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Management>>> GetAllManagement()
        {
            var items = await _managementService.GetAllManagementAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Management>> GetManagement(string id)
        {
            var item = await _managementService.GetManagementByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<Management>> AddManagement(Management management)
        {
            var newItem = await _managementService.AddManagementAsync(management);
            return CreatedAtAction(nameof(GetManagement), new { id = newItem.ManagementId }, newItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateManagement(string id, Management management)
        {
            if (id != management.ManagementId) return BadRequest();
            var success = await _managementService.UpdateManagementAsync(id, management);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteManagement(string id)
        {
            var success = await _managementService.DeleteManagementAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}