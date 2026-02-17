using Datamodels.Hrms;
using Microsoft.EntityFrameworkCore;

namespace HRMS_API.Service
{
    public class VEmployeeDetailsService
    {
        private readonly IDbContextFactory<Hrms_dbContext> _contextFactory;

        public VEmployeeDetailsService(IDbContextFactory<Hrms_dbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        // ดึงข้อมูล View ทั้งหมด
        public async Task<List<VEmployeeDetail>> GetAllEmployeeDetailsAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.VEmployeeDetails
                .AsNoTracking()
                .OrderBy(v => v.EmployeeId)
                .ToListAsync();
        }

        // ดึงข้อมูล View ตาม ID
        public async Task<VEmployeeDetail?> GetEmployeeDetailsByIdAsync(string id)
        {
            using var context = _contextFactory.CreateDbContext();
            var result = await context.VEmployeeDetails
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.EmployeeId == id);

            // ถ้ามีข้อมูล และ PictureUrl ไม่เป็น null ให้ทำการ Trim ช่องว่างออก
            if (result != null && result.PictureUrl != null)
            {
                result.PictureUrl = result.PictureUrl.Trim();
            }

            return result;
        }

        // (ตัวอย่าง) ค้นหาจาก View
        public async Task<List<VEmployeeDetail>> SearchEmployeesByNameAsync(string name)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.VEmployeeDetails
               .AsNoTracking()
               .Where(v => (v.FirstNameThai + " " + v.LastNameThai).Contains(name))
               .OrderBy(v => v.FirstNameThai)
               .ToListAsync();
        }
    }
}