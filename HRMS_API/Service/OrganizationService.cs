using Datamodels.Hrms;
using Microsoft.EntityFrameworkCore;

namespace HRMS_API.Service
{
    public class OrganizationStructureService : IOrganizationStructureService
    {
        private readonly IDbContextFactory<Hrms_dbContext> _contextFactory;

        public OrganizationStructureService(IDbContextFactory<Hrms_dbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        // =========================================================
        // #region 1. Structure (โหลดโครงสร้างทั้งหมด)
        // =========================================================
        public async Task<OrganizationStructureData> GetFullOrganizationStructureAsync()
        {
            using var context = await _contextFactory.CreateDbContextAsync();

            // ดึงเฉพาะข้อมูลที่สถานะเป็น Active (1)
            var divisions = await context.Divisions.AsNoTracking()
                                .Where(d => d.DivisionId != null && d.Isactive == "1")
                                .ToListAsync();

            var departments = await context.Departments.AsNoTracking()
                                .Where(d => d.DeptId != null && d.Isactive == "1")
                                .ToListAsync();

            var workUnits = await context.WorkUnits.AsNoTracking()
                                .Where(w => w.UnitId != null && w.Isactive == "1")
                                .ToListAsync();

            return new OrganizationStructureData
            {
                Divisions = divisions,
                Departments = departments,
                WorkUnits = workUnits
            };
        }
        // #endregion

        // =========================================================
        // #region 2. Division (สำนัก/กอง)
        // =========================================================
        public async Task<bool> AddDivisionAsync(Division division)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                // เช็คซ้ำ
                if (await context.Divisions.AnyAsync(d => d.DivisionId == division.DivisionId)) return false;

                context.Divisions.Add(division);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdateDivisionAsync(Division division)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                var existing = await context.Divisions.FirstOrDefaultAsync(d => d.DivisionId == division.DivisionId);
                if (existing == null) return false;

                existing.DivisionNameThai = division.DivisionNameThai;
                existing.DivisionNameEng = division.DivisionNameEng;
                // existing.Isactive = division.Isactive; // ถ้าต้องการแก้สถานะทางนี้

                context.Divisions.Update(existing);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception) { return false; }
        }

        public async Task<bool> DeleteDivisionAsync(string divisionId)
        {
            // Soft Delete: เปลี่ยน Isactive เป็น 0 แทนการลบจริง
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                var division = await context.Divisions.FindAsync(divisionId);
                if (division == null) return false;

                division.Isactive = "0";

                // (Optional) ปิด Departments ลูกๆ ด้วย
                var deps = await context.Departments.Where(d => d.DivisionId == divisionId).ToListAsync();
                foreach (var d in deps) d.Isactive = "0";

                context.Divisions.Update(division);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception) { return false; }
        }
        // #endregion

        // =========================================================
        // #region 3. Department (ฝ่าย)
        // =========================================================
        public async Task<bool> AddDepartmentAsync(Department department)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                if (await context.Departments.AnyAsync(d => d.DeptId == department.DeptId)) return false;

                context.Departments.Add(department);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception) { return false; }
        }

        public async Task<bool> UpdateDepartmentAsync(Department department)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                var existing = await context.Departments.FirstOrDefaultAsync(d => d.DeptId == department.DeptId);
                if (existing == null) return false;

                existing.DeptNameThai = department.DeptNameThai;
                existing.DeptNameEng = department.DeptNameEng;
                // existing.DivisionId = department.DivisionId;

                context.Departments.Update(existing);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception) { return false; }
        }

        public async Task<bool> DeleteDepartmentAsync(string deptId)
        {
            // Soft Delete
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                var dept = await context.Departments.FindAsync(deptId);
                if (dept == null) return false;

                dept.Isactive = "0";

                // (Optional) ปิด WorkUnits ลูกๆ ด้วย
                var units = await context.WorkUnits.Where(u => u.DeptId == deptId).ToListAsync();
                foreach (var u in units) u.Isactive = "0";

                context.Departments.Update(dept);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception) { return false; }
        }
        // #endregion

        // =========================================================
        // #region 4. WorkUnit (กลุ่มงาน)
        // =========================================================
        public async Task<bool> AddWorkUnitAsync(WorkUnit unit)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                if (await context.WorkUnits.AnyAsync(u => u.UnitId == unit.UnitId)) return false;

                context.WorkUnits.Add(unit);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception) { return false; }
        }

        public async Task<bool> UpdateWorkUnitAsync(WorkUnit unit)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                var existing = await context.WorkUnits.FirstOrDefaultAsync(u => u.UnitId == unit.UnitId);
                if (existing == null) return false;

                existing.UnitNameThai = unit.UnitNameThai;
                existing.UnitNameEng = unit.UnitNameEng;

                context.WorkUnits.Update(existing);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception) { return false; }
        }

        public async Task<bool> DeleteWorkUnitAsync(string unitId)
        {
            // Soft Delete
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                var unit = await context.WorkUnits.FindAsync(unitId);
                if (unit == null) return false;

                unit.Isactive = "0";

                context.WorkUnits.Update(unit);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception) { return false; }
        }
        // #endregion
    }

    // =========================================================
    // Interface & Models
    // =========================================================
    public interface IOrganizationStructureService
    {
        // Structure
        Task<OrganizationStructureData> GetFullOrganizationStructureAsync();

        // Division
        Task<bool> AddDivisionAsync(Division division);
        Task<bool> UpdateDivisionAsync(Division division);
        Task<bool> DeleteDivisionAsync(string divisionId);

        // Department
        Task<bool> AddDepartmentAsync(Department department);
        Task<bool> UpdateDepartmentAsync(Department department);
        Task<bool> DeleteDepartmentAsync(string deptId);

        // WorkUnit
        Task<bool> AddWorkUnitAsync(WorkUnit unit);
        Task<bool> UpdateWorkUnitAsync(WorkUnit unit);
        Task<bool> DeleteWorkUnitAsync(string unitId);
    }

    public class OrganizationStructureData
    {
        public List<Division> Divisions { get; set; } = new();
        public List<Department> Departments { get; set; } = new();
        public List<WorkUnit> WorkUnits { get; set; } = new();
    }
}