using Datamodels.Hrms;
using Microsoft.EntityFrameworkCore;

namespace HRMS_API.Service
{
    public class StaffService
    {
        private readonly IDbContextFactory<Hrms_dbContext> _contextFactory;

        public StaffService(IDbContextFactory<Hrms_dbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<Staff>> GetAllStaffAsync()
        {
            using var context = _contextFactory.CreateDbContext();

            return await context.Staff
                .OrderBy(c => c.StaffId)
                .ToListAsync();
        }

        public async Task<Staff?> GetStaffByIdAsync(string id)
        {
            using var context = _contextFactory.CreateDbContext();

            return await context.Staff
                .FirstOrDefaultAsync(c => c.StaffId == id);
        }

        public async Task<bool> AddStaffAsync(Staff staff)
        {
            using var context = _contextFactory.CreateDbContext();

            context.Staff.Add(staff);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateStaffAsync(Staff staff)
        {
            using var context = _contextFactory.CreateDbContext();

            context.Staff.Update(staff);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCampusAsync(string id)
        {
            using var context = _contextFactory.CreateDbContext();

            var campus = await context.Campuses.FirstOrDefaultAsync(c => c.CampId == id);
            if (campus == null)
                return false;

            context.Campuses.Remove(campus);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
