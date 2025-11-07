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
        public async Task<ManagementPosition> AddManagementPositionAsync(ManagementPosition managementPosition)
        {
            using var context = _contextFactory.CreateDbContext();
            context.ManagementPositions.Add(managementPosition);
            await context.SaveChangesAsync();
            return managementPosition;
        }
        public async Task<bool> UpdateManagementPositionAsync(string id, ManagementPosition managementPosition)
        {
            if (id != managementPosition.ManagementPositionId) return false;
            using var context = _contextFactory.CreateDbContext();
            var existing = await context.ManagementPositions.FindAsync(id);
            if (existing == null) return false;
            existing.PositionNameThai = managementPosition.PositionNameThai;
            existing.PositionNameEng = managementPosition.PositionNameEng;
            existing.PositionLevel = managementPosition.PositionLevel;
            await context.SaveChangesAsync();
            return true;
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