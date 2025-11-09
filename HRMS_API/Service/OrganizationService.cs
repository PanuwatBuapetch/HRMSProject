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

        public async Task<OrganizationStructureData> GetFullOrganizationStructureAsync()
        {
            using var context = _contextFactory.CreateDbContext();

            // ดึงข้อมูลทั้งหมดที่จำเป็น
            var divisionsTask = context.Divisions.AsNoTracking().Where(d => d.DivisionId != null).ToListAsync();
            var departmentsTask = context.Departments.AsNoTracking().Where(d => d.DeptId != null).ToListAsync();
            var workUnitsTask = context.WorkUnits.AsNoTracking().Where(w => w.UnitId != null).ToListAsync();

            await Task.WhenAll(divisionsTask, departmentsTask, workUnitsTask);

            // ส่งข้อมูลกลับไป
            return new OrganizationStructureData
            {
                Divisions = divisionsTask.Result,
                Departments = departmentsTask.Result,
                WorkUnits = workUnitsTask.Result
            };
        }
    }

    public interface IOrganizationStructureService
    {
        // เมท็อดนี้ดึงข้อมูลโครงสร้างหลักทั้งหมดมาพร้อมกัน
        Task<OrganizationStructureData> GetFullOrganizationStructureAsync();
    }

    // Model ที่ใช้ส่งข้อมูลโครงสร้างทั้งหมด
    public class OrganizationStructureData
    {
        public List<Division> Divisions { get; set; } = new(); // Top Level (Campus/Division)
        public List<Department> Departments { get; set; } = new(); // Second Level (Faculty/Department)
        public List<WorkUnit> WorkUnits { get; set; } = new();
    }
}