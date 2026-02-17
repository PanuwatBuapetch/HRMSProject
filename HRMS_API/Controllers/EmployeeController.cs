using Datamodels.Hrms;
using HRMS_API.Service;
using Microsoft.AspNetCore.Mvc;

namespace HRMS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(string id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            return employee != null ? Ok(employee) : NotFound("ไม่พบข้อมูลพนักงาน");
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> AddEmployee(Employee employee)
        {
            try
            {
                var newEmployee = await _employeeService.AddEmployeeAsync(employee);
                return CreatedAtAction(nameof(GetEmployee), new { id = newEmployee.EmployeeId }, newEmployee);
            }
            catch (Exception ex)
            {
                // ส่ง Error Message กลับไปให้ Frontend แสดงผล (เช่น รหัสซ้ำ)
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(string id, Employee employee)
        {
            try
            {
                var success = await _employeeService.UpdateEmployeeAsync(id, employee);
                return success ? NoContent() : NotFound("ไม่พบพนักงาน หรือ ID ไม่ถูกต้อง");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // Endpoint พิเศษ: สำหรับ Unassign (Organization Chart)
        [HttpPatch("unassign/{id}")]
        public async Task<IActionResult> UnassignEmployee(string id)
        {
            var success = await _employeeService.UnassignEmployeeAsync(id);
            return success ? Ok(true) : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            var success = await _employeeService.DeleteEmployeeAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}