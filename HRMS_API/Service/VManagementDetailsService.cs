using Datamodels.Hrms;
using Microsoft.EntityFrameworkCore;

namespace HRMS_API.Service
{
    public class VManagementDetailsService
    {
        private readonly IDbContextFactory<Hrms_dbContext> _contextFactory;

        public VManagementDetailsService(IDbContextFactory<Hrms_dbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<VManagementDetail>> GetAllManagementDetailsAsync()
        {
            using var context = _contextFactory.CreateDbContext();

            return await context.VManagementDetails
                .AsNoTracking()
                // แก้ไข: ตัด AdminLevel ออก เพราะใน Model ที่คุณส่งมาไม่มีฟิลด์นี้
                .OrderBy(v => v.StaffId)
                .ToListAsync();
        }

        public async Task<VManagementDetail?> GetManagementDetailsByStaffIdAsync(string staffId)
        {
            using var context = _contextFactory.CreateDbContext();

            return await context.VManagementDetails
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.StaffId == staffId);
        }

        public async Task<VManagementDetail?> GetManagementDetailsByKeyAsync(string executiveKey)
        {
            using var context = _contextFactory.CreateDbContext();

            // แก้ไข: ใช้ Key ตามที่ปรากฏใน Model ของคุณ
            return await context.VManagementDetails
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.Key == executiveKey);
        }

        public async Task<List<VManagementDetail>> SearchManagementByNameAsync(string name)
        {
            using var context = _contextFactory.CreateDbContext();

            return await context.VManagementDetails
                .AsNoTracking()
                // แก้ไข: ใช้ StaffNameThai ให้ตรงกับ Model
                .Where(v => v.StaffNameThai != null && v.StaffNameThai.Contains(name))
                .OrderBy(v => v.StaffId)
                .ToListAsync();
        }
    }
}