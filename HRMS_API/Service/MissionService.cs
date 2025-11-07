using Datamodels.Hrms;
using Microsoft.EntityFrameworkCore;

namespace HRMS_API.Service
{
    public class MissionService
    {
        private readonly IDbContextFactory<Hrms_dbContext> _contextFactory;
        public MissionService(IDbContextFactory<Hrms_dbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
       
        public async Task<List<Mission>> GetAllMissionsAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Missions
                .AsNoTracking()
                .OrderBy(m => m.MissionId)
                .ToListAsync();
        }
        public async Task<Mission?> GetMissionByIdAsync(string id)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Missions.FindAsync(id);
        }
        public async Task<Mission> AddMissionAsync(Mission mission)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Missions.Add(mission);
            await context.SaveChangesAsync();
            return mission;
        }
        public async Task<bool> UpdateMissionAsync(string id, Mission mission)
        {
            if (id != mission.MissionId) return false;
            using var context = _contextFactory.CreateDbContext();
            var existing = await context.Missions.FindAsync(id);
            if (existing == null) return false;
            existing.MissionNameThai = mission.MissionNameThai;
            existing.MissionNameEng = mission.MissionNameEng;
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteMissionAsync(string id)
        {
            using var context = _contextFactory.CreateDbContext();
            var affectedRows = await context.Missions
                .Where(m => m.MissionId == id)
                .ExecuteDeleteAsync();
            return affectedRows > 0;
        }



    }
}