using Datamodels.Hrms;

namespace HrmsSolution.Service
{
    public interface IOrganizationService
    {
        Task<List<Division>> GetAllDivisionsAsync();
        Task<List<Department>> GetDepartmentsByDivisionIdAsync(string divisionId);
        Task<List<WorkUnit>> GetWorkUnitsByDepartmentIdAsync(string deptId);
        Task<bool> AddDivisionAsync(Division division);
        Task<bool> AddDepartmentAsync(Department department);
        Task<bool> AddWorkUnitAsync(WorkUnit workUnit);
        Task<bool> DeleteDivisionAsync(string divisionId);
        Task<bool> DeleteDepartmentAsync(string deptId);
    }
}