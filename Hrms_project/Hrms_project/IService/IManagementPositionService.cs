using Datamodels.Hrms;

namespace HrmsSolution.Service
{
    public interface IManagementPositionService
    {
        Task<List<ManagementPosition>> GetAllManagementPositionsAsync();
        Task<ManagementPosition> GetManagementPositionByIdAsync(string id);
        Task<ManagementPosition> AddManagementPositionAsync(ManagementPosition managementPosition);
        Task<bool> UpdateManagementPositionAsync(string id, ManagementPosition managementPosition);
        Task<bool> DeleteManagementPositionAsync(string id);
    }
}