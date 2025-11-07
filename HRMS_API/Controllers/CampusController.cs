using Datamodels.Hrms;
using HRMS_API.Service;
using Microsoft.AspNetCore.Mvc;

namespace HRMS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CampusController : ControllerBase
    {
        private readonly CampusService _campusService;

        public CampusController(CampusService campusService)
        {
            _campusService = campusService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var campuses = await _campusService.GetAllCampusAsync();
            return Ok(campuses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var campus = await _campusService.GetCampusByIdAsync(id);
            if (campus == null)
                return NotFound();

            return Ok(campus);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Campus campus)
        {
            await _campusService.AddCampusAsync(campus);
            return Ok(new { message = "Campus created successfully" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] Campus campus)
        {
            if (id != campus.CampId)
                return BadRequest("ID mismatch");

            await _campusService.UpdateCampusAsync(campus);
            return Ok(new { message = "Campus updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var success = await _campusService.DeleteCampusAsync(id);
            if (!success)
                return NotFound();

            return Ok(new { message = "Campus deleted successfully" });
        }
    }
}
