using Datamodels.Hrms;
using Microsoft.EntityFrameworkCore;

namespace HRMS_API.Service
{
    public class LocationService
    {
        private readonly IDbContextFactory<Hrms_dbContext> _contextFactory;

        public LocationService(IDbContextFactory<Hrms_dbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<Location>> GetAllLocationsAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Locations
                .AsNoTracking()
                .OrderBy(l => l.LocationId)
                .ToListAsync();
        }

        public async Task<Location?> GetLocationByIdAsync(string id)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Locations.FindAsync(id);
        }

        public async Task<Location> AddLocationAsync(Location location)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Locations.Add(location);
            await context.SaveChangesAsync();
            return location;
        }

        public async Task<bool> UpdateLocationAsync(string id, Location location)
        {
            if (id != location.LocationId) return false;

            using var context = _contextFactory.CreateDbContext();

            var existing = await context.Locations.FindAsync(id);
            if (existing == null) return false;

            existing.LocationNameThai = location.LocationNameThai;
            existing.LocationNameEng = location.LocationNameEng;
            existing.LocationAddress = location.LocationAddress;

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteLocationAsync(string id)
        {
            using var context = _contextFactory.CreateDbContext();
            var affectedRows = await context.Locations
                .Where(l => l.LocationId == id)
                .ExecuteDeleteAsync();
            return affectedRows > 0;
        }
    }
}