using Datamodels.Hrms;
using HRMS_API.Service;
using Microsoft.AspNetCore.Mvc;

namespace HRMS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly DepartmentService _departmentService;

        public DepartmentController(DepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetAllDepartments()
        {
            var items = await _departmentService.GetAllDepartmentsAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartment(string id)
        {
            var item = await _departmentService.GetDepartmentByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<Department>> AddDepartment(Department department)
        {
            var newItem = await _departmentService.AddDepartmentAsync(department);
            return CreatedAtAction(nameof(GetDepartment), new { id = newItem.DeptId }, newItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(string id, Department department)
        {
            if (id != department.DeptId) return BadRequest();
            var success = await _departmentService.UpdateDepartmentAsync(id, department);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(string id)
        {
            var success = await _departmentService.DeleteDepartmentAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}