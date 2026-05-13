    using Datamodels.Hrms;
    using HRMS_API.Service;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    namespace HRMS_API.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class OrganizationController : ControllerBase
        {
            private readonly IDbContextFactory<Hrms_dbContext> _contextFactory;
            private readonly IOrganizationStructureService _orgService;

            public OrganizationController(
                IDbContextFactory<Hrms_dbContext> contextFactory,
                IOrganizationStructureService orgService)
            {
                _contextFactory = contextFactory;
                _orgService = orgService;
            }

            #region Structure
            [HttpGet("structure")]
            public async Task<IActionResult> GetStructure()
            {
                var data = await _orgService.GetFullOrganizationStructureAsync();
                return Ok(data);
            }
            #endregion

            // =================================================================================
            #region Divisions
            // =================================================================================

            [HttpGet("divisions")]
            public async Task<IActionResult> GetDivisions()
            {
                var list = await _orgService.GetAllDivisionsAsync();
                return Ok(list);
            }

            [HttpGet("divisions/{id}")]
            public async Task<IActionResult> GetDivisionById(string id)
            {
                var item = await _orgService.GetDivisionByIdAsync(id);
                return item != null ? Ok(item) : NotFound();
            }

            [HttpPost("divisions")]
            public async Task<IActionResult> AddDivision([FromBody] Division division)
            {
                if (division == null) return BadRequest("Invalid payload");
                var result = await _orgService.AddDivisionAsync(division);
                return result ? Ok(true) : Conflict("ID already exists");
            }

            [HttpPut("divisions")]
            public async Task<IActionResult> UpdateDivision([FromBody] Division division)
            {
                if (division == null) return BadRequest();
                var result = await _orgService.UpdateDivisionAsync(division);
                return result ? Ok(new { message = "Update successful" }) : StatusCode(500, "Error updating");
            }

            [HttpDelete("divisions/{divisionId}")]
            public async Task<IActionResult> DeleteDivision(string divisionId)
            {
                var result = await _orgService.DeleteDivisionAsync(divisionId);
                return result ? Ok(true) : NotFound();
            }
            #endregion

            // =================================================================================
            #region Departments
            // =================================================================================

            [HttpGet("departments")]
            public async Task<IActionResult> GetDepartments()
            {
                var list = await _orgService.GetAllDepartmentsAsync();
                return Ok(list);
            }

            [HttpGet("departments/{id}")]
            public async Task<IActionResult> GetDepartmentById(string id)
            {
                var item = await _orgService.GetDepartmentByIdAsync(id);
                return item != null ? Ok(item) : NotFound();
            }

            [HttpGet("divisions/{divisionId}/departments")]
            public async Task<IActionResult> GetDepartmentsByDivision(string divisionId)
            {
                var list = await _orgService.GetDepartmentsByDivisionIdAsync(divisionId);
                return Ok(list);
            }

            [HttpPost("departments")]
            public async Task<IActionResult> AddDepartment([FromBody] Department department)
            {
                if (department == null) return BadRequest("Invalid payload");
                var result = await _orgService.AddDepartmentAsync(department);
                return result ? Ok(true) : Conflict("ID already exists");
            }

            [HttpPut("departments")]
            public async Task<IActionResult> UpdateDepartment([FromBody] Department dept)
            {
                if (dept == null) return BadRequest();
                var result = await _orgService.UpdateDepartmentAsync(dept);
                return result ? Ok(new { message = "Update successful" }) : StatusCode(500, "Error updating");
            }

            [HttpDelete("departments/{deptId}")]
            public async Task<IActionResult> DeleteDepartment(string deptId)
            {
                var result = await _orgService.DeleteDepartmentAsync(deptId);
                return result ? Ok(true) : StatusCode(500, "ไม่สามารถลบข้อมูลได้ หรือข้อมูลถูกอ้างอิงอยู่");
            }
            #endregion

            // =================================================================================
            #region WorkUnits
            // =================================================================================

            [HttpGet("units")]
            public async Task<IActionResult> GetWorkUnits()
            {
                var list = await _orgService.GetAllWorkUnitsAsync();
                return Ok(list);
            }

            [HttpGet("units/{id}")]
            public async Task<IActionResult> GetWorkUnitById(string id)
            {
                var item = await _orgService.GetWorkUnitByIdAsync(id);
                return item != null ? Ok(item) : NotFound();
            }

            [HttpGet("departments/{deptId}/units")]
            public async Task<IActionResult> GetUnitsByDepartment(string deptId)
            {
                var list = await _orgService.GetWorkUnitsByDepartmentIdAsync(deptId);
                return Ok(list);
            }

            [HttpPost("units")]
            public async Task<IActionResult> AddUnit([FromBody] WorkUnit unit)
            {
                if (unit == null) return BadRequest("Invalid payload");
                var result = await _orgService.AddWorkUnitAsync(unit);
                return result ? Ok(true) : Conflict("ID already exists");
            }

            [HttpPut("units")]
            public async Task<IActionResult> UpdateWorkUnit([FromBody] WorkUnit unit)
            {
                var result = await _orgService.UpdateWorkUnitAsync(unit);
                return result ? Ok(true) : StatusCode(500, "Error updating");
            }

            [HttpDelete("units/{unitId}")]
            public async Task<IActionResult> DeleteWorkUnit(string unitId)
            {
                var result = await _orgService.DeleteWorkUnitAsync(unitId);
                return result ? Ok(true) : NotFound();
            }
            #endregion

            // =================================================================================
            #region Employee Assignments (จัดการบุคลากร)
            // =================================================================================

            [HttpPut("employees/unassign/{employeeId}")]
            public async Task<IActionResult> UnassignEmployee(string employeeId)
            {
                using var ctx = await _contextFactory.CreateDbContextAsync();
                var emp = await ctx.Employees.FirstOrDefaultAsync(e => e.EmployeeId == employeeId);

                if (emp == null) return NotFound($"ไม่พบพนักงาน: {employeeId}");

                emp.DeptId = null;
                emp.UnitId = null;

                try
                {
                    await ctx.SaveChangesAsync();
                    return Ok(new { success = true });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }

            [HttpPut("employees/assign/{employeeId}/{deptId}")]
            public async Task<IActionResult> AssignEmployee(string employeeId, string deptId)
            {
                using var ctx = await _contextFactory.CreateDbContextAsync();
                var emp = await ctx.Employees.FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
                if (emp == null) return NotFound();

                emp.DeptId = deptId;
                await ctx.SaveChangesAsync();

                return Ok(new { success = true });
            }
            #endregion
        }
    }