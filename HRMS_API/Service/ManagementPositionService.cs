using Datamodels.Hrms;
using Microsoft.EntityFrameworkCore;

namespace HRMS_API.Service
{
    public class ManagementPositionService
    {
        private readonly IDbContextFactory<Hrms_dbContext> _contextFactory;
        public ManagementPositionService(IDbContextFactory<Hrms_dbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
        
        public async Task<List<ManagementPosition>> GetAllManagementPositionsAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.ManagementPositions
                .AsNoTracking()
                .OrderBy(mp => mp.ManagementPositionId)
                .ToListAsync();
        }
        public async Task<ManagementPosition?> GetManagementPositionByIdAsync(string id)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.ManagementPositions.FindAsync(id);
        }
        public async Task<ManagementPosition?> AddManagementPositionAsync(ManagementPosition managementPosition)
        {
            using var context = await _contextFactory.CreateDbContextAsync();

            // 1. ตรวจสอบว่ารหัสตำแหน่งบริหารนี้มีอยู่แล้วหรือไม่ (ป้องกัน PK Violation)
            var isExist = await context.ManagementPositions
                                       .AnyAsync(x => x.ManagementPositionId == managementPosition.ManagementPositionId);

            if (isExist)
            {
                // อาจจะส่ง Exception เฉพาะทางออกไป หรือ Return null เพื่อให้ Controller จัดการต่อ
                throw new InvalidOperationException("รหัสตำแหน่งบริหารนี้มีอยู่ในระบบแล้ว");
            }

            try
            {
                context.ManagementPositions.Add(managementPosition);
                await context.SaveChangesAsync();
                return managementPosition;
            }
            catch (Exception ex)
            {
                // Log Error ที่เกิดขึ้น
                return null;
            }
        }

        public async Task<bool> UpdateManagementPositionAsync(string id, ManagementPosition managementPosition)
        {
            // 1. ตรวจสอบความถูกต้องของ ID
            if (string.IsNullOrEmpty(id) || id != managementPosition.ManagementPositionId)
                return false;

            using var context = await _contextFactory.CreateDbContextAsync(); // ใช้ Async เพื่อประสิทธิภาพที่ดีกว่า

            // 2. ค้นหาข้อมูลเดิม
            var existing = await context.ManagementPositions.FindAsync(id);
            if (existing == null) return false;

            // 3. ใช้ CurrentValues.SetValues (เร็วกว่าและไม่ต้องไล่เขียนทีละบรรทัด)
            // วิธีนี้จะอัปเดตเฉพาะฟิลด์ที่มีชื่อตรงกันจาก Object ที่ส่งมา
            context.Entry(existing).CurrentValues.SetValues(managementPosition);

            try
            {
                await context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                // กรณีมีการแก้ไขข้อมูลพร้อมกันใน Database
                return false;
            }
        }
        public async Task<bool> DeleteManagementPositionAsync(string id)
        {
            using var context = _contextFactory.CreateDbContext();
            var affectedRows = await context.ManagementPositions
                .Where(mp => mp.ManagementPositionId == id)
                .ExecuteDeleteAsync();
            return affectedRows > 0;
        }
    }
}