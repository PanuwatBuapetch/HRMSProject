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

        public async Task<List<Management>> GetAllManagementAsync()
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Managements.AsNoTracking().ToListAsync();
        }

        // ดึงข้อมูลจาก View (สำคัญมากสำหรับหน้าจอสรุป)
        public async Task<List<VManagementDetail>> GetAllManagementDetailsAsync()
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.VManagementDetails.AsNoTracking().ToListAsync();
        }

        // ดึงรายชื่อตำแหน่งบริหารมาทำ Dropdown
        public async Task<List<ManagementPosition>> GetAllManagementPositionsAsync()
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.ManagementPositions.AsNoTracking().ToListAsync();
        }

        public async Task<Management?> GetManagementByIdAsync(string id)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Managements.FindAsync(id);
        }

        public async Task<Management> AddManagementAsync(Management management)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            if (string.IsNullOrEmpty(management.ManagementId)) management.ManagementId = Guid.NewGuid().ToString();

            context.Managements.Add(management);
            await context.SaveChangesAsync();
            return management;
        }

        public async Task<bool> UpdateManagementAsync(string id, Management management)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            var existing = await context.Managements.FindAsync(id);
            if (existing == null) return false;

            // ใช้ SetValues เพื่ออัปเดตทุกฟิลด์ที่ส่งมาจาก UI
            context.Entry(existing).CurrentValues.SetValues(management);

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteManagementAsync(string id)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            var affectedRows = await context.Managements
                .Where(m => m.ManagementId == id)
                .ExecuteDeleteAsync();
            return affectedRows > 0;
        }
    }
}