using Datamodels.Hrms;
using Microsoft.EntityFrameworkCore;

namespace HRMS_API.Service
{
    public class TeamService
    {
        private readonly IDbContextFactory<Hrms_dbContext> _contextFactory;

        public TeamService(IDbContextFactory<Hrms_dbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<Team>> GetAllTeamsAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Teams
                .AsNoTracking()
                .OrderBy(l => l.TeamId)
                .ToListAsync();
        }

        public async Task<Team?> GetTeamByIdAsync(string id)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Teams.FindAsync(id);
        }

        public async Task<Team> AddTeamAsync(Team team)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Teams.Add(team);
            await context.SaveChangesAsync();
            return team;
        }

        public async Task<bool> UpdateTeamAsync(string id, Team team)
        {
            if (id != team.TeamId) return false;

            using var context = _contextFactory.CreateDbContext();

            var existing = await context.Teams.FindAsync(id);
            if (existing == null) return false;

            existing.TeamNameThai = team.TeamNameThai;
            existing.TeamNameEng = team.TeamNameEng;
            existing.Isactive = team.Isactive;

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteTeamAsync(string id)
        {
            using var context = _contextFactory.CreateDbContext();
            var affectedRows = await context.Teams
                .Where(l => l.TeamId == id)
                .ExecuteDeleteAsync();
            return affectedRows > 0;
        }
    }
}