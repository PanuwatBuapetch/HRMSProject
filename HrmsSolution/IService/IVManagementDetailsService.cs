using Datamodels.Hrms;

namespace HrmsSolution.Service
{
    public interface IVManagementDetailsService
    {
        Task<List<VManagementDetail>> GetAllManagementDetailsAsync();
        Task<VManagementDetail> GetManagementDetailsByStaffIdAsync(string staffId);
        Task<VManagementDetail> GetManagementDetailsByKeyAsync(string executiveKey);
        Task<List<VManagementDetail>> SearchManagementByNameAsync(string name);
    }
}