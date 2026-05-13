using Datamodels.Hrms;

namespace HRMS_API.Service
{
    public interface IOrganizationStructureService
    {
        Task<OrganizationStructureData> GetFullOrganizationStructureAsync();

        Task<List<Division>> GetAllDivisionsAsync();
        Task<Division?> GetDivisionByIdAsync(string id);
        Task<bool> AddDivisionAsync(Division division);
        Task<bool> UpdateDivisionAsync(Division division);
        Task<bool> DeleteDivisionAsync(string divisionId);

        Task<List<Department>> GetAllDepartmentsAsync();
        Task<List<Department>> GetDepartmentsByDivisionIdAsync(string divisionId);
        Task<Department?> GetDepartmentByIdAsync(string id);
        Task<bool> AddDepartmentAsync(Department department);
        Task<bool> UpdateDepartmentAsync(Department department);
        Task<bool> DeleteDepartmentAsync(string deptId);

        Task<List<WorkUnit>> GetAllWorkUnitsAsync();
        Task<List<WorkUnit>> GetWorkUnitsByDepartmentIdAsync(string deptId);
        Task<WorkUnit?> GetWorkUnitByIdAsync(string id);
        Task<bool> AddWorkUnitAsync(WorkUnit unit);
        Task<bool> UpdateWorkUnitAsync(WorkUnit unit);
        Task<bool> DeleteWorkUnitAsync(string unitId);
        Task<bool> DeleteWorkUnitALLAsync();
    }

    public class OrganizationStructureData
    {
        public List<Division> Divisions { get; set; } = new();
        public List<Department> Departments { get; set; } = new();
        public List<WorkUnit> WorkUnits { get; set; } = new();
    }
}
