using Datamodels.Hrms;

namespace HrmsSolution.Service
{
    public interface IManagementService
    {
        Task<List<Management>> GetAllManagementAsync();
        Task<Management> GetManagementByIdAsync(string id);
        Task<Management> AddManagementAsync(Management management);
        Task<bool> UpdateManagementAsync(string id, Management management);
        Task<bool> DeleteManagementAsync(string id);
    }
}