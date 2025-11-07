using Datamodels.Hrms;
using Microsoft.EntityFrameworkCore;

namespace HRMS_API.Service
{
   public class JobPositionService
    {
        private readonly IDbContextFactory<Hrms_dbContext> _contextFactory;
        public JobPositionService(IDbContextFactory<Hrms_dbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public async Task<List<Datamodels.Hrms.JobPosition>> GetAllJobPositionsAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.JobPositions
                .AsNoTracking()
                .OrderBy(j => j.PositionId)
                .ToListAsync();
        }
        public async Task<Datamodels.Hrms.JobPosition?> GetJobPositionByIdAsync(string id)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.JobPositions.FindAsync(id);
        }
        public async Task<Datamodels.Hrms.JobPosition> AddJobPositionAsync(Datamodels.Hrms.JobPosition jobPosition)
        {
            using var context = _contextFactory.CreateDbContext();
            context.JobPositions.Add(jobPosition);
            await context.SaveChangesAsync();
            return jobPosition;
        }
        public async Task<bool> UpdateJobPositionAsync(string id, Datamodels.Hrms.JobPosition jobPosition)
        {
            if (id != jobPosition.PositionId) return false;
            using var context = _contextFactory.CreateDbContext();
            var existing = await context.JobPositions.FindAsync(id);
            if (existing == null) return false;
            existing.PositionNameThai = jobPosition.PositionNameThai;
            existing.PositionNameEng = jobPosition.PositionNameEng;
            existing.PositionType = jobPosition.PositionType;
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteJobPositionAsync(string id)
        {
            using var context = _contextFactory.CreateDbContext();
            var affectedRows = await context.JobPositions
                .Where(j => j.PositionId == id)
                .ExecuteDeleteAsync();
            return affectedRows > 0;
        }
    }
}