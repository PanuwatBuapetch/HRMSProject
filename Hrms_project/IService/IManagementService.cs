using Datamodels.Hrms;

namespace HrmsSolution.Service
{
    public interface IManagementService
    {
        // Base CRUD operations
        Task<List<Management>> GetAllManagementAsync();
        Task<Management> GetManagementByIdAsync(string id);
        Task<List<ManagementPosition>> GetAllManagementPositionsAsync();
        Task<Management> AddManagementAsync(Management management);
        Task<bool> UpdateManagementAsync(string id, Management management);
        Task<bool> DeleteManagementAsync(string id);

        // View-specific operations
        Task<List<VManagementDetail>> GetAllManagementDetailsAsync();
        Task<VManagementDetail?> GetManagementDetailByIdAsync(string id);
    }
}