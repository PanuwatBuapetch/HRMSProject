using Datamodels.Hrms;
using Microsoft.EntityFrameworkCore;

namespace HRMS_API.Service
{
    public class CampusService
    {
        private readonly IDbContextFactory<Hrms_dbContext> _contextFactory;

        public CampusService(IDbContextFactory<Hrms_dbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<Campus>> GetAllCampusAsync()
        {
            using var context = _contextFactory.CreateDbContext();

            return await context.Campuses
                .OrderBy(c => c.CampId)
                .ToListAsync();
        }

        public async Task<Campus?> GetCampusByIdAsync(string id)
        {
            using var context = _contextFactory.CreateDbContext();

            return await context.Campuses
                .FirstOrDefaultAsync(c => c.CampId == id);
        }

        public async Task<bool> AddCampusAsync(Campus campus)
        {
            using var context = _contextFactory.CreateDbContext();

            context.Campuses.Add(campus);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateCampusAsync(Campus campus)
        {
            using var context = _contextFactory.CreateDbContext();

            context.Campuses.Update(campus);
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
