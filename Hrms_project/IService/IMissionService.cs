using Datamodels.Hrms;

namespace HrmsSolution.Service
{
    public interface IMissionService
    {
        Task<List<Mission>> GetAllMissionsAsync();
        Task<Mission> GetMissionByIdAsync(string id);
        Task<Mission> AddMissionAsync(Mission mission);
        Task<bool> UpdateMissionAsync(string id, Mission mission);
        Task<bool> DeleteMissionAsync(string id);
    }
}