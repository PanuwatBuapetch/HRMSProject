using Datamodels.Hrms;
using Microsoft.EntityFrameworkCore;

namespace HRMS_API.Service
{
    public class ManagementService
    {
        private readonly IDbContextFactory<Hrms_dbContext> _contextFactory;
        public ManagementService(IDbContextFactory<Hrms_dbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        // (แก้ไข) เปลี่ยนจาก EmployeeTitle -> Management
        public async Task<List<Management>> GetAllManagementAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            // (แก้ไข) เปลี่ยนจาก .EmployeeTitles -> .Managements
            return await context.Managements
                .AsNoTracking()
                .OrderBy(m => m.ManagementId) // (แก้ไข)
                .ToListAsync();
        }

        public async Task<Management?> GetManagementByIdAsync(string id)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Managements.FindAsync(id);
        }

        // (แก้ไข)
        public async Task<Management> AddManagementAsync(Management management)
        {
            using var context = _contextFactory.CreateDbContext();
            // (แก้ไข)
            context.Managements.Add(management);
            await context.SaveChangesAsync();
            return management;
        }

        // (แก้ไข)
        public async Task<bool> UpdateManagementAsync(string id, Management management)
        {
            // (แก้ไข)
            if (id != management.ManagementId) return false;

            using var context = _contextFactory.CreateDbContext();
            // (แก้ไข)
            var existing = await context.Managements.FindAsync(id);
            if (existing == null) return false;

            // (แก้ไข) อัปเดต Property ของ Management
            existing.Isactive = management.Isactive;
            existing.EmployeeId = management.EmployeeId;
            existing.ManagementPositionId = management.ManagementPositionId;
            existing.TempAdminCode = management.TempAdminCode;
            existing.LocationId = management.LocationId;
            existing.DivisionId = management.DivisionId;
            existing.DeptId = management.DeptId;
            existing.TeamId = management.TeamId;
            existing.UnitId = management.UnitId;

            await context.SaveChangesAsync();
            return true;
        }

        // (แก้ไข)
        public async Task<bool> DeleteManagementAsync(string id)
        {
            using var context = _contextFactory.CreateDbContext();
            // (แก้ไข)
            var affectedRows = await context.Managements
                .Where(m => m.ManagementId == id) // (แก้ไข)
                .ExecuteDeleteAsync();
            return affectedRows > 0;
        }
    }
}