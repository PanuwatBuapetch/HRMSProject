using Datamodels.Hrms;

namespace HrmsSolution.Service
{
    public interface ITeamService
    {
        Task<List<Team>> GetAllTeamsAsync();
        Task<Team> GetTeamByIdAsync(string id);
        Task<Team> AddTeamAsync(Team team);
        Task<bool> UpdateTeamAsync(string id, Team team);
        Task<bool> DeleteTeamAsync(string id);
    }
}