using Datamodels.Hrms;
using HRMS_API.Service;
using Microsoft.AspNetCore.Mvc;

namespace HRMS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobPositionController : ControllerBase
    {
        private readonly JobPositionService _jobPositionService;

        public JobPositionController(JobPositionService jobPositionService)
        {
            _jobPositionService = jobPositionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobPosition>>> GetAllJobPositions()
        {
            var items = await _jobPositionService.GetAllJobPositionsAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<JobPosition>> GetJobPosition(string id)
        {
            var item = await _jobPositionService.GetJobPositionByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<JobPosition>> AddJobPosition(JobPosition jobPosition)
        {
            var newItem = await _jobPositionService.AddJobPositionAsync(jobPosition);
            return CreatedAtAction(nameof(GetJobPosition), new { id = newItem.PositionId }, newItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJobPosition(string id, JobPosition jobPosition)
        {
            if (id != jobPosition.PositionId) return BadRequest();
            var success = await _jobPositionService.UpdateJobPositionAsync(id, jobPosition);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobPosition(string id)
        {
            var success = await _jobPositionService.DeleteJobPositionAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}