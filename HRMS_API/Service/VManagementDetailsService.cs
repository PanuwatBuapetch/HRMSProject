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

        /// <summary>
        /// 1. ดึงข้อมูลผู้บริหารทั้งหมดจาก View
        /// (เรียงตามระดับ AdminLevel)
        /// </summary>
        public async Task<List<VManagementDetail>> GetAllManagementDetailsAsync()
        {
            using var context = _contextFactory.CreateDbContext();

            // Hrms_dbContext ของคุณจะมี DbSet ชื่อ 'VManagementDetails'
            // ที่ถูกสร้างอัตโนมัติมาจาก Entity 'VManagementDetail'
            return await context.VManagementDetails
                .AsNoTracking() // ใช้อ่านอย่างเดียว (Read-Only) จะเร็วขึ้น
                .OrderBy(v => v.AdminLevel) // เรียงตามระดับ
                .ThenBy(v => v.StaffId)
                .ToListAsync();
        }

        /// <summary>
        /// 2. ค้นหาข้อมูลผู้บริหารด้วย StaffId (รหัสพนักงาน)
        /// </summary>
        public async Task<VManagementDetail?> GetManagementDetailsByStaffIdAsync(string staffId)
        {
            using var context = _contextFactory.CreateDbContext();

            // View ไม่มี Primary Key เราจึงใช้ FindAsync() ไม่ได้
            // ต้องใช้ FirstOrDefaultAsync() แทน
            return await context.VManagementDetails
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.StaffId == staffId);
        }

        /// <summary>
        /// 3. ค้นหาข้อมูลผู้บริหารด้วย Key (รหัสตำแหน่งบริหาร)
        /// </summary>
        public async Task<VManagementDetail?> GetManagementDetailsByKeyAsync(string executiveKey)
        {
            using var context = _contextFactory.CreateDbContext();

            return await context.VManagementDetails
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.ExecutiveNameKey == executiveKey);
        }

        /// <summary>
        /// 4. (ตัวอย่าง) ค้นหาผู้บริหารตามชื่อ
        /// </summary>
        public async Task<List<VManagementDetail>> SearchManagementByNameAsync(string name)
        {
            using var context = _contextFactory.CreateDbContext();

            return await context.VManagementDetails
                .AsNoTracking()
                .Where(v => v.ExecutiveName != null && v.ExecutiveName.Contains(name))
                .OrderBy(v => v.AdminLevel)
                .ToListAsync();
        }
    }
}