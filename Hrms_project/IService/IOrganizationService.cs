using Datamodels.Hrms;

namespace HrmsSolution.Service
{
    public interface IOrganizationService
    {
        Task<List<Division>> GetAllDivisionsAsync();
        Task<List<Department>> GetDepartmentsByDivisionIdAsync(string divisionId);
        Task<List<WorkUnit>> GetWorkUnitsByDepartmentIdAsync(string deptId);

        Task<bool> AddDepartmentAsync(Department department);
        Task<bool> DeleteDepartmentAsync(string deptId);
        Task<bool> UpdateDepartmentAsync(Department department);


        Task<bool> AddDivisionAsync(Division division);
        Task<bool> UpdateDivisionAsync(Division division);
        Task<bool> DeleteDivisionAsync(string divisionId);

        Task<bool> AddWorkUnitAsync(WorkUnit workUnit);
        Task<bool> UpdateWorkUnitAsync(WorkUnit unit); // เพิ่ม
        Task<bool> DeleteWorkUnitAsync(string unitId); // เพิ่ม
    }
}