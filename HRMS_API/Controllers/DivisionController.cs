using Datamodels.Hrms;
using HRMS_API.Service;
using Microsoft.AspNetCore.Mvc;

namespace HRMS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DivisionController : ControllerBase
    {
        private readonly DivisionService _divisionService;

        public DivisionController(DivisionService divisionService)
        {
            _divisionService = divisionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Division>>> GetAllDivisions()
        {
            var items = await _divisionService.GetAllDivisionsAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Division>> GetDivision(string id)
        {
            var item = await _divisionService.GetDivisionByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<Division>> AddDivision(Division division)
        {
            var newItem = await _divisionService.AddDivisionAsync(division);
            return CreatedAtAction(nameof(GetDivision), new { id = newItem.DivisionId }, newItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDivision(string id, Division division)
        {
            if (id != division.DivisionId) return BadRequest();
            var success = await _divisionService.UpdateDivisionAsync(id, division);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDivision(string id)
        {
            var success = await _divisionService.DeleteDivisionAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}