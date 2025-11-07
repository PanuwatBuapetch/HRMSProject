using Datamodels.Hrms;
using HRMS_API.Service;
using Microsoft.AspNetCore.Mvc;

namespace HRMS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeTitleController : ControllerBase
    {
        private readonly EmployeeTitleService _employeeTitleService;

        public EmployeeTitleController(EmployeeTitleService employeeTitleService)
        {
            _employeeTitleService = employeeTitleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeTitle>>> GetAllEmployeeTitles()
        {
            var items = await _employeeTitleService.GetAllEmployeeTitlesAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeTitle>> GetEmployeeTitle(string id)
        {
            var item = await _employeeTitleService.GetEmployeeTitleByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeTitle>> AddEmployeeTitle(EmployeeTitle employeeTitle)
        {
            var newItem = await _employeeTitleService.AddEmployeeTitleAsync(employeeTitle);
            return CreatedAtAction(nameof(GetEmployeeTitle), new { id = newItem.TitleId }, newItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployeeTitle(string id, EmployeeTitle employeeTitle)
        {
            if (id != employeeTitle.TitleId) return BadRequest();
            var success = await _employeeTitleService.UpdateEmployeeTitleAsync(id, employeeTitle);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeTitle(string id)
        {
            var success = await _employeeTitleService.DeleteEmployeeTitleAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}