using Datamodels.Hrms; // (Namespace ของ Models)
using HRMS_API.Service; // (Namespace ของ Services)
using Microsoft.AspNetCore.Mvc;

namespace HRMS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // -> /api/Employee
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _employeeService;

        // 1. ฉีด (Inject) Service ที่เกี่ยวข้องเข้ามา
        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // 2. GET: api/Employee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return Ok(employees);
        }

        // 3. GET: api/Employee/1234567
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(string id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound(); // คืนค่า 404 ถ้าหาไม่เจอ
            }
            return Ok(employee);
        }

        // 4. POST: api/Employee
        [HttpPost]
        public async Task<ActionResult<Employee>> AddEmployee(Employee employee)
        {
            var newEmployee = await _employeeService.AddEmployeeAsync(employee);

            // คืนค่า 201 Created พร้อมบอก URL ของข้อมูลที่สร้างใหม่
            return CreatedAtAction(nameof(GetEmployee), new { id = newEmployee.EmployeeId }, newEmployee);
        }

        // 5. PUT: api/Employee/1234567
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(string id, Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return BadRequest("ID ใน URL ไม่ตรงกับ ID ใน Body");
            }

            var success = await _employeeService.UpdateEmployeeAsync(id, employee);
            if (!success)
            {
                return NotFound("ไม่พบ Employee ที่ต้องการอัปเดต");
            }

            return NoContent(); // คืนค่า 204 (สำเร็จ แต่ไม่มีข้อมูลส่งกลับ)
        }

        // 6. DELETE: api/Employee/1234567
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            var success = await _employeeService.DeleteEmployeeAsync(id);
            if (!success)
            {
                return NotFound("ไม่พบ Employee ที่ต้องการลบ");
            }

            return NoContent(); // คืนค่า 204
        }
    }
}