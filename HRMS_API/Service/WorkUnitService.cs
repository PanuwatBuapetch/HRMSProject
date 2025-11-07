using Datamodels.Hrms;
using Microsoft.EntityFrameworkCore;

namespace HRMS_API.Service
{
    public class WorkUnitService
    {
        private readonly IDbContextFactory<Hrms_dbContext> _contextFactory;
        public WorkUnitService(IDbContextFactory<Hrms_dbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public async Task<List<WorkUnit>> GetAllWorkUnitsAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.WorkUnits
                .AsNoTracking()
                .OrderBy(wu => wu.UnitId)
                .ToListAsync();
        }
        public async Task<WorkUnit?> GetWorkUnitByIdAsync(string id)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.WorkUnits.FindAsync(id);
        }
        public async Task<WorkUnit> AddWorkUnitAsync(WorkUnit workUnit)
        {
            using var context = _contextFactory.CreateDbContext();
            context.WorkUnits.Add(workUnit);
            await context.SaveChangesAsync();
            return workUnit;
        }
        public async Task<bool> UpdateWorkUnitAsync(string id, WorkUnit workUnit)
        {
            if (id != workUnit.UnitId) return false;
            using var context = _contextFactory.CreateDbContext();
            var existing = await context.WorkUnits.FindAsync(id);
            if (existing == null) return false;
            existing.UnitNameThai = workUnit.UnitNameThai;
            existing.UnitNameEng = workUnit.UnitNameEng;
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteWorkUnitAsync(string id)
        {
            using var context = _contextFactory.CreateDbContext();
            var affectedRows = await context.WorkUnits
                .Where(wu => wu.UnitId == id)
                .ExecuteDeleteAsync();
            return affectedRows > 0;
        }

    }
}