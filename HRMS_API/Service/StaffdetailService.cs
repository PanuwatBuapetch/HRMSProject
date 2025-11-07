using Datamodels.Hrms;
using Microsoft.EntityFrameworkCore;

namespace HRMS_API.Service
{
    public class StaffDetailService
    {
        private readonly IDbContextFactory<Hrms_dbContext> _contextFactory;

        public StaffDetailService(IDbContextFactory<Hrms_dbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<StaffDetail>> GetEmployeesAsync()
        {
            using var context = _contextFactory.CreateDbContext();

            return await context.StaffDetails
                .OrderBy(s => s.StaffId)
                .ToListAsync();

        }
        public async Task<StaffDetail?> GetStaffByIdAsync(string id)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.StaffDetails
                .FirstOrDefaultAsync(s => s.StaffId == id);
        }
        public async Task AddStaffAsync(StaffDetail staffDetail)
        {
            using var context = _contextFactory.CreateDbContext();
            context.StaffDetails.Add(staffDetail);
            await context.SaveChangesAsync();
        }
        public async Task<bool> UpdateStaffAsync(StaffDetail staffDetail)
        {
            using var context = _contextFactory.CreateDbContext();
            context.StaffDetails.Update(staffDetail);
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteStaffAsync(string id)
        {
            using var context = _contextFactory.CreateDbContext();
            var staffDetail = await context.StaffDetails.FirstOrDefaultAsync(s => s.StaffId == id);
            if (staffDetail == null)
                return false;
            context.StaffDetails.Remove(staffDetail);
            await context.SaveChangesAsync();
            return true;
        }



    }
}
