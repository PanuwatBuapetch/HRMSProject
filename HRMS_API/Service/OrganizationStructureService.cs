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

            var divisions = await context.Divisions.AsNoTracking().Where(d => d.Isactive == "1").ToListAsync();
            var departments = await context.Departments.AsNoTracking().Where(d => d.Isactive == "1").ToListAsync();
            var workUnits = await context.WorkUnits.AsNoTracking().Where(w => w.Isactive == "1").ToListAsync();

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
        public async Task<List<Division>> GetAllDivisionsAsync()
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Divisions.AsNoTracking().Where(d => d.Isactive == "1").ToListAsync();
        }

        public async Task<Division?> GetDivisionByIdAsync(string id)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Divisions.AsNoTracking().FirstOrDefaultAsync(d => d.DivisionId == id && d.Isactive == "1");
        }

        public async Task<bool> AddDivisionAsync(Division division)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            var existing = await context.Divisions.FirstOrDefaultAsync(d => d.DivisionId == division.DivisionId);

            if (existing != null)
            {
                if (existing.Isactive == "1") return false;

                existing.Isactive = "1";
                existing.DivisionNameThai = division.DivisionNameThai;
                existing.DivisionNameEng = division.DivisionNameEng;
                context.Divisions.Update(existing);
            }
            else
            {
                context.Divisions.Add(division);
            }

            await context.SaveChangesAsync();
            return true;
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

                context.Divisions.Update(existing);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception) { return false; }
        }

        //public async Task<bool> DeleteDivisionAsync(string divisionId)
        //{
        //    using var context = await _contextFactory.CreateDbContextAsync();
        //    using var transaction = await context.Database.BeginTransactionAsync();
        //    try
        //    {
        //        var division = await context.Divisions.FindAsync(divisionId);
        //        if (division == null) return false;

        //        division.Isactive = "0";

        //        var deps = await context.Departments.Where(d => d.DivisionId == divisionId).ToListAsync();
        //        foreach (var d in deps) d.Isactive = "0";

        //        await context.SaveChangesAsync();
        //        await transaction.CommitAsync();
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        await transaction.RollbackAsync();
        //        return false;
        //    }
        //}
        public async Task<bool> DeleteDivisionAsync(string divisionId)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                // 1. หา สำนัก (Division) ที่ต้องการลบ
                var division = await context.Divisions.FindAsync(divisionId);
                if (division == null) return false;

                // 2. หา ฝ่าย (Departments) ทั้งหมดที่สังกัดสำนักนี้
                var departments = await context.Departments.Where(d => d.DivisionId == divisionId).ToListAsync();

                if (departments.Any())
                {
                    // ดึงเฉพาะ DeptId ออกมาเพื่อเอาไปค้นหากลุ่มงานต่อ
                    var deptIds = departments.Select(d => d.DeptId).ToList();

                    // 3. หา กลุ่มงาน (WorkUnits) ทั้งหมดที่สังกัดฝ่ายเหล่านั้น
                    var units = await context.WorkUnits.Where(u => deptIds.Contains(u.DeptId)).ToListAsync();

                    // 4. เริ่มลบจากชั้นล่างสุดก่อน: ลบกลุ่มงาน (WorkUnits)
                    if (units.Any())
                    {
                        context.WorkUnits.RemoveRange(units);
                    }

                    // 5. ลบชั้นกลาง: ลบฝ่าย (Departments)
                    context.Departments.RemoveRange(departments);
                }

                // 6. ลบชั้นบนสุด: ลบสำนัก (Division)
                context.Divisions.Remove(division);

                await context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                Console.WriteLine($"\n❌ Error Hard Delete Division: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"🔍 Inner Exception: {ex.InnerException.Message}\n");
                }
                return false;
            }
        }
        // #endregion

        // =========================================================
        // #region 3. Department (ฝ่าย)
        // =========================================================
        public async Task<List<Department>> GetAllDepartmentsAsync()
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            // ลบ .Where ออก เพื่อให้ดึงข้อมูลออกมาทั้งหมด ไม่ว่าจะถูก Soft Delete ไปแล้วหรือไม่
            return await context.Departments.AsNoTracking().ToListAsync();
        }

        public async Task<List<Department>> GetDepartmentsByDivisionIdAsync(string divisionId)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Departments.AsNoTracking().Where(d => d.DivisionId == divisionId && d.Isactive == "1").ToListAsync();
        }

        public async Task<Department?> GetDepartmentByIdAsync(string id)
        {
            using var context = await _contextFactory.CreateDbContextAsync();

            // ลบเงื่อนไข && d.Isactive == "1" ออก เพื่อให้ดึงข้อมูลที่ถูกลบไปแล้วได้ด้วย
            return await context.Departments
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.DeptId == id);
        }

        public async Task<bool> AddDepartmentAsync(Department department)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            var existing = await context.Departments.FirstOrDefaultAsync(d => d.DeptId == department.DeptId);

            if (existing != null)
            {
                if (existing.Isactive == "1") return false;

                existing.Isactive = "1";
                existing.DeptNameThai = department.DeptNameThai;
                existing.DeptNameEng = department.DeptNameEng;
                existing.DivisionId = department.DivisionId;
                context.Departments.Update(existing);
            }
            else
            {
                context.Departments.Add(department);
            }

            await context.SaveChangesAsync();
            return true;
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
                // existing.DivisionId = department.DivisionId; // Uncomment if you want to allow moving divisions

                context.Departments.Update(existing);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception) { return false; }
        }

        public async Task<bool> DeleteDepartmentAsync(string deptId)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                var dept = await context.Departments.FindAsync(deptId);
                if (dept == null) return false;

                // 1. ดึงกลุ่มงานลูกๆ ออกมา
                var units = await context.WorkUnits.Where(u => u.DeptId == deptId).ToListAsync();

                // 2. ใช้คำสั่ง RemoveRange เพื่อ "ลบจริง" (Hard Delete) กลุ่มงาน
                if (units.Any())
                {
                    context.WorkUnits.RemoveRange(units);
                }

                // 3. ใช้คำสั่ง Remove เพื่อ "ลบจริง" (Hard Delete) แผนก
                context.Departments.Remove(dept);

                await context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                Console.WriteLine($"Error Hard Delete: {ex.Message}");
                return false;
            }
        }
        // #endregion

        // =========================================================
        // #region 4. WorkUnit (กลุ่มงาน)
        // =========================================================
        public async Task<List<WorkUnit>> GetAllWorkUnitsAsync()
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.WorkUnits.AsNoTracking().Where(d => d.Isactive == "1").ToListAsync();
        }

        public async Task<List<WorkUnit>> GetWorkUnitsByDepartmentIdAsync(string deptId)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.WorkUnits.AsNoTracking().Where(u => u.DeptId == deptId && u.Isactive == "1").ToListAsync();
        }

        public async Task<WorkUnit?> GetWorkUnitByIdAsync(string id)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.WorkUnits.AsNoTracking().FirstOrDefaultAsync(d => d.UnitId == id && d.Isactive == "1");
        }

        public async Task<bool> AddWorkUnitAsync(WorkUnit unit)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                var existing = await context.WorkUnits.FirstOrDefaultAsync(u => u.UnitId == unit.UnitId);

                if (existing != null)
                {
                    if (existing.Isactive == "1") return false;

                    existing.Isactive = "1";
                    existing.UnitNameThai = unit.UnitNameThai;
                    existing.UnitNameEng = unit.UnitNameEng;
                    existing.DeptId = unit.DeptId;
                    context.WorkUnits.Update(existing);
                }
                else
                {
                    context.WorkUnits.Add(unit);
                }

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
        Task<OrganizationStructureData> GetFullOrganizationStructureAsync();

        Task<List<Division>> GetAllDivisionsAsync();
        Task<Division?> GetDivisionByIdAsync(string id);
        Task<bool> AddDivisionAsync(Division division);
        Task<bool> UpdateDivisionAsync(Division division);
        Task<bool> DeleteDivisionAsync(string divisionId);

        Task<List<Department>> GetAllDepartmentsAsync();
        Task<List<Department>> GetDepartmentsByDivisionIdAsync(string divisionId);
        Task<Department?> GetDepartmentByIdAsync(string id);
        Task<bool> AddDepartmentAsync(Department department);
        Task<bool> UpdateDepartmentAsync(Department department);
        Task<bool> DeleteDepartmentAsync(string deptId);

        Task<List<WorkUnit>> GetAllWorkUnitsAsync();
        Task<List<WorkUnit>> GetWorkUnitsByDepartmentIdAsync(string deptId);
        Task<WorkUnit?> GetWorkUnitByIdAsync(string id);
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