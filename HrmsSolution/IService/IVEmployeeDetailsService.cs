using Datamodels.Hrms;

namespace HrmsSolution.Service
{
    public interface IVEmployeeDetailsService
    {
        Task<List<VEmployeeDetail>> GetAllEmployeeDetailsAsync();
        Task<VEmployeeDetail> GetEmployeeDetailsByIdAsync(string id);
        Task<List<VEmployeeDetail>> SearchEmployeesByNameAsync(string name);
    }
}