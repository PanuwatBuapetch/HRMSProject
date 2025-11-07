using Datamodels.Hrms;
using Microsoft.EntityFrameworkCore;

namespace HRMS_API.Service
{
    public class DivisionService
    {
        private readonly IDbContextFactory<Hrms_dbContext> _contextFactory;

        public DivisionService(IDbContextFactory<Hrms_dbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<Division>> GetAllDivisionsAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Divisions
                .AsNoTracking()
                .OrderBy(l => l.DivisionId)
                .ToListAsync();
        }

        public async Task<Division?> GetDivisionByIdAsync(string id)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Divisions.FindAsync(id);
        }

        public async Task<Division> AddDivisionAsync(Division division)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Divisions.Add(division);
            await context.SaveChangesAsync();
            return division;
        }

        public async Task<bool> UpdateDivisionAsync(string id, Division location)
        {
            if (id != location.LocationId) return false;

            using var context = _contextFactory.CreateDbContext();

            var existing = await context.Divisions.FindAsync(id);
            if (existing == null) return false;

            existing.DivisionNameThai = location.DivisionNameThai;
            existing.DivisionNameEng = location.DivisionNameEng;
            existing.DivisionDesc = location.DivisionDesc;

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteDivisionAsync(string id)
        {
            using var context = _contextFactory.CreateDbContext();
            var affectedRows = await context.Locations
                .Where(l => l.LocationId == id)
                .ExecuteDeleteAsync();
            return affectedRows > 0;
        }
    }
}