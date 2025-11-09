using Datamodels.Hrms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRMS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrganizationController : ControllerBase
    {
        private readonly IDbContextFactory<Hrms_dbContext> _contextFactory;

        public OrganizationController(IDbContextFactory<Hrms_dbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        [HttpGet("divisions")]
        public async Task<IActionResult> GetDivisions()
        {
            using var ctx = _contextFactory.CreateDbContext();
            var list = await ctx.Divisions.AsNoTracking().ToListAsync();
            return Ok(list);
        }

        [HttpPost("divisions")]
        public async Task<IActionResult> AddDivision([FromBody] Division division)
        {
            if (division == null || string.IsNullOrWhiteSpace(division.DivisionId))
                return BadRequest("Invalid division payload");

            using var ctx = _contextFactory.CreateDbContext();
            // prevent duplicate key
            var exists = await ctx.Divisions.AnyAsync(d => d.DivisionId == division.DivisionId);
            if (exists) return Conflict("Division with same id already exists");

            ctx.Divisions.Add(division);
            await ctx.SaveChangesAsync();
            return Ok(true);
        }

        [HttpPost("divisions/bulk")]
        public async Task<IActionResult> AddDivisionsBulk([FromBody] List<Division> divisions)
        {
            if (divisions == null || divisions.Count == 0) return BadRequest("No divisions provided");
            using var ctx = _contextFactory.CreateDbContext();
            foreach (var division in divisions)
            {
                if (string.IsNullOrWhiteSpace(division.DivisionId)) continue;
                var exists = await ctx.Divisions.AnyAsync(d => d.DivisionId == division.DivisionId);
                if (!exists) ctx.Divisions.Add(division);
            }
            await ctx.SaveChangesAsync();
            return Ok(true);
        }

        [HttpGet("divisions/{divisionId}/departments")]
        public async Task<IActionResult> GetDepartmentsByDivision(string divisionId)
        {
            using var ctx = _contextFactory.CreateDbContext();
            var list = await ctx.Departments.AsNoTracking().Where(d => d.DivisionId == divisionId).ToListAsync();
            return Ok(list);
        }

        [HttpPost("departments")]
        public async Task<IActionResult> AddDepartment([FromBody] Department department)
        {
            if (department == null || string.IsNullOrWhiteSpace(department.DeptId))
                return BadRequest("Invalid department payload");

            using var ctx = _contextFactory.CreateDbContext();
            var exists = await ctx.Departments.AnyAsync(d => d.DeptId == department.DeptId);
            if (exists) return Conflict("Department with same id already exists");

            ctx.Departments.Add(department);
            await ctx.SaveChangesAsync();
            return Ok(true);
        }

        [HttpPost("departments/bulk")]
        public async Task<IActionResult> AddDepartmentsBulk([FromBody] List<Department> departments)
        {
            if (departments == null || departments.Count == 0) return BadRequest("No departments provided");
            using var ctx = _contextFactory.CreateDbContext();
            foreach (var department in departments)
            {
                if (string.IsNullOrWhiteSpace(department.DeptId)) continue;
                var exists = await ctx.Departments.AnyAsync(d => d.DeptId == department.DeptId);
                if (!exists) ctx.Departments.Add(department);
            }
            await ctx.SaveChangesAsync();
            return Ok(true);
        }

        [HttpGet("departments/{deptId}/units")]
        public async Task<IActionResult> GetUnitsByDepartment(string deptId)
        {
            using var ctx = _contextFactory.CreateDbContext();
            var list = await ctx.WorkUnits.AsNoTracking().Where(u => u.DeptId == deptId).ToListAsync();
            return Ok(list);
        }

        [HttpPost("units")]
        public async Task<IActionResult> AddUnit([FromBody] WorkUnit unit)
        {
            if (unit == null || string.IsNullOrWhiteSpace(unit.UnitId))
                return BadRequest("Invalid unit payload");

            using var ctx = _contextFactory.CreateDbContext();
            var exists = await ctx.WorkUnits.AnyAsync(u => u.UnitId == unit.UnitId);
            if (exists) return Conflict("Unit with same id already exists");

            ctx.WorkUnits.Add(unit);
            await ctx.SaveChangesAsync();
            return Ok(true);
        }

        [HttpPost("units/bulk")]
        public async Task<IActionResult> AddUnitsBulk([FromBody] List<WorkUnit> units)
        {
            if (units == null || units.Count == 0) return BadRequest("No units provided");
            using var ctx = _contextFactory.CreateDbContext();
            foreach (var unit in units)
            {
                if (string.IsNullOrWhiteSpace(unit.UnitId)) continue;
                var exists = await ctx.WorkUnits.AnyAsync(u => u.UnitId == unit.UnitId);
                if (!exists) ctx.WorkUnits.Add(unit);
            }
            await ctx.SaveChangesAsync();
            return Ok(true);
        }

        [HttpDelete("divisions/{divisionId}")]
        public async Task<IActionResult> DeleteDivision(string divisionId)
        {
            using var ctx = _contextFactory.CreateDbContext();
            var division = await ctx.Divisions.FindAsync(divisionId);
            if (division == null) return NotFound();

            // remove dependent workunits and departments
            var deps = await ctx.Departments.Where(d => d.DivisionId == divisionId).ToListAsync();
            foreach (var d in deps)
            {
                var units = await ctx.WorkUnits.Where(u => u.DeptId == d.DeptId).ToListAsync();
                ctx.WorkUnits.RemoveRange(units);
            }
            ctx.Departments.RemoveRange(deps);

            ctx.Divisions.Remove(division);
            await ctx.SaveChangesAsync();
            return Ok(true);
        }

        [HttpDelete("departments/{deptId}")]
        public async Task<IActionResult> DeleteDepartment(string deptId)
        {
            using var ctx = _contextFactory.CreateDbContext();
            var dept = await ctx.Departments.FindAsync(deptId);
            if (dept == null) return NotFound();

            var units = await ctx.WorkUnits.Where(u => u.DeptId == deptId).ToListAsync();
            ctx.WorkUnits.RemoveRange(units);
            ctx.Departments.Remove(dept);
            await ctx.SaveChangesAsync();
            return Ok(true);
        }
    }
}
