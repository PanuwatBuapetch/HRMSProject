using Datamodels.Hrms;

namespace HrmsSolution.Service
{
    public interface IEmployeeTitleService
    {
        Task<List<EmployeeTitle>> GetAllEmployeeTitlesAsync();
        Task<EmployeeTitle> GetEmployeeTitleByIdAsync(string id);
        Task<EmployeeTitle> AddEmployeeTitleAsync(EmployeeTitle employeeTitle);
        Task<bool> UpdateEmployeeTitleAsync(string id, EmployeeTitle employeeTitle);
        Task<bool> DeleteEmployeeTitleAsync(string id);
    }
}