using Datamodels.Hrms;
using HRMS_API.Service;
using Microsoft.AspNetCore.Mvc;

namespace HRMS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StaffController : ControllerBase
    {
        private readonly StaffService _staffService;

        public StaffController(StaffService StaffService)
        {
            _staffService = StaffService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var campuses = await _staffService.GetAllStaffAsync();
            return Ok(campuses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var campus = await _staffService.GetStaffByIdAsync(id);
            if (campus == null)
                return NotFound();

            return Ok(campus);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Staff staff)
        {
            await _staffService.AddStaffAsync(staff);
            return Ok(new { message = "staff created successfully" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] Staff staff)
        {
            if (id != staff.StaffId)
                return BadRequest("ID mismatch");

            await _staffService.UpdateStaffAsync(staff);
            return Ok(new { message = "staff updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var success = await _staffService.DeleteCampusAsync(id);
            if (!success)
                return NotFound();

            return Ok(new { message = "staff deleted successfully" });
        }
    }
}
