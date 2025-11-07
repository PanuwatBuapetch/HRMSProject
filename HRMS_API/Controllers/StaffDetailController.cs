using Datamodels.Hrms;
using HRMS_API.Service;
using Microsoft.AspNetCore.Mvc;

namespace HRMS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StaffDetailController : ControllerBase
    {
        private readonly StaffDetailService _staffDetailService;

        public StaffDetailController(StaffDetailService StaffDetailService)
        {
            _staffDetailService = StaffDetailService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var campuses = await _staffDetailService.GetEmployeesAsync();
            return Ok(campuses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var campus = await _staffDetailService.GetStaffByIdAsync(id);
            if (campus == null)
                return NotFound();

            return Ok(campus);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StaffDetail staffdetail)
        {
            await _staffDetailService.AddStaffAsync(staffdetail);
            return Ok(new { message = "staff created successfully" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] StaffDetail staffdetail)
        {
            if (id != staffdetail.StaffId)
                return BadRequest("ID mismatch");

            await _staffDetailService.UpdateStaffAsync(staffdetail);
            return Ok(new { message = "staff updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var success = await _staffDetailService.DeleteStaffAsync(id);
            if (!success)
                return NotFound();

            return Ok(new { message = "staff deleted successfully" });
        }
    }
}
