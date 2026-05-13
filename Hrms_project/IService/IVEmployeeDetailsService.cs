using Datamodels.Hrms;

namespace HrmsSolution.Service
{
    public interface IVEmployeeDetailsService
    {
        Task<List<VEmployeeDetail>> GetAllEmployeeDetailsAsync();
        Task<VEmployeeDetail> GetEmployeeDetailsByIdAsync(string id);
        Task<List<VEmployeeDetail>> SearchEmployeesByNameAsync(string name);
        Task<bool> AssignEmployeeToDeptAsync(string employeeId, string deptId, string divisionId);
        Task<bool> UnassignEmployeeAsync(string? employeeId);
    }
}