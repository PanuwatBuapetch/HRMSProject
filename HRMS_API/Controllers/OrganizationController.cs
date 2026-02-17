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

        // =================================================================================
        // #region 1. Structure (โหลดข้อมูลโครงสร้างทั้งหมด)
        // =================================================================================
        #region Structure
        [HttpGet("structure")]
        public async Task<IActionResult> GetStructure()
        {
            // เรียกใช้ Service เพื่อดึงข้อมูลทั้งหมด (ที่ IsActive = 1)
            var data = await _orgService.GetFullOrganizationStructureAsync();
            return Ok(data);
        }
        #endregion

        // =================================================================================
        // #region 2. Divisions (สำนัก/กอง)
        // =================================================================================
        #region Divisions

        [HttpGet("divisions")]
        public async Task<IActionResult> GetDivisions()
        {
            using var ctx = await _contextFactory.CreateDbContextAsync();
            // กรองเอาเฉพาะที่ยังใช้งานอยู่ (Soft Delete)
            var list = await ctx.Divisions.AsNoTracking()
                                .Where(d => d.Isactive == "1")
                                .ToListAsync();
            return Ok(list);
        }

        [HttpPost("divisions")]
        public async Task<IActionResult> AddDivision([FromBody] Division division)
        {
            if (division == null) return BadRequest("Invalid payload");
            var result = await _orgService.AddDivisionAsync(division);
            return result ? Ok(true) : Conflict("ID already exists");
        }

        [HttpPut("division")]
        public async Task<IActionResult> UpdateDivision([FromBody] Division division)
        {
            if (division == null) return BadRequest();
            var result = await _orgService.UpdateDivisionAsync(division);
            return result ? Ok(new { message = "Update successful" }) : StatusCode(500, "Error updating");
        }

        [HttpDelete("divisions/{divisionId}")]
        public async Task<IActionResult> DeleteDivision(string divisionId)
        {
            // ใช้ Service เพื่อทำ Soft Delete
            var result = await _orgService.DeleteDivisionAsync(divisionId);
            return result ? Ok(true) : NotFound();
        }

        // (Optional) Bulk Insert
        [HttpPost("divisions/bulk")]
        public async Task<IActionResult> AddDivisionsBulk([FromBody] List<Division> divisions)
        {
            if (divisions == null || divisions.Count == 0) return BadRequest("No divisions provided");
            using var ctx = await _contextFactory.CreateDbContextAsync();
            foreach (var division in divisions)
            {
                if (string.IsNullOrWhiteSpace(division.DivisionId)) continue;
                if (!await ctx.Divisions.AnyAsync(d => d.DivisionId == division.DivisionId))
                    ctx.Divisions.Add(division);
            }
            await ctx.SaveChangesAsync();
            return Ok(true);
        }
        #endregion

        // =================================================================================
        // #region 3. Departments (ฝ่าย/คณะ)
        // =================================================================================
        #region Departments

        [HttpGet("divisions/{divisionId}/departments")]
        public async Task<IActionResult> GetDepartmentsByDivision(string divisionId)
        {
            using var ctx = await _contextFactory.CreateDbContextAsync();
            var list = await ctx.Departments.AsNoTracking()
                                .Where(d => d.DivisionId == divisionId && d.Isactive == "1")
                                .ToListAsync();
            return Ok(list);
        }

        [HttpPost("departments")]
        public async Task<IActionResult> AddDepartment([FromBody] Department department)
        {
            if (department == null) return BadRequest("Invalid payload");
            var result = await _orgService.AddDepartmentAsync(department);
            return result ? Ok(true) : Conflict("ID already exists");
        }

        [HttpPut("department")]
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
            return result ? Ok(true) : NotFound();
        }

        // (Optional) Bulk Insert
        [HttpPost("departments/bulk")]
        public async Task<IActionResult> AddDepartmentsBulk([FromBody] List<Department> departments)
        {
            if (departments == null || departments.Count == 0) return BadRequest("No data");
            using var ctx = await _contextFactory.CreateDbContextAsync();
            foreach (var d in departments)
            {
                if (!await ctx.Departments.AnyAsync(x => x.DeptId == d.DeptId))
                    ctx.Departments.Add(d);
            }
            await ctx.SaveChangesAsync();
            return Ok(true);
        }
        #endregion

        // =================================================================================
        // #region 4. WorkUnits (กลุ่มงาน/หน่วยงานย่อย)
        // =================================================================================
        #region WorkUnits

        [HttpGet("departments/{deptId}/units")]
        public async Task<IActionResult> GetUnitsByDepartment(string deptId)
        {
            using var ctx = await _contextFactory.CreateDbContextAsync();
            var list = await ctx.WorkUnits.AsNoTracking()
                                .Where(u => u.DeptId == deptId && u.Isactive == "1")
                                .ToListAsync();
            return Ok(list);
        }

        [HttpPost("units")]
        public async Task<IActionResult> AddUnit([FromBody] WorkUnit unit)
        {
            if (unit == null) return BadRequest("Invalid payload");
            var result = await _orgService.AddWorkUnitAsync(unit);
            return result ? Ok(true) : Conflict("ID already exists");
        }

        [HttpPut("unit")]
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

        // (Optional) Bulk Insert
        [HttpPost("units/bulk")]
        public async Task<IActionResult> AddUnitsBulk([FromBody] List<WorkUnit> units)
        {
            if (units == null || units.Count == 0) return BadRequest("No data");
            using var ctx = await _contextFactory.CreateDbContextAsync();
            foreach (var u in units)
            {
                if (!await ctx.WorkUnits.AnyAsync(x => x.UnitId == u.UnitId))
                    ctx.WorkUnits.Add(u);
            }
            await ctx.SaveChangesAsync();
            return Ok(true);
        }
        #endregion
    }
}