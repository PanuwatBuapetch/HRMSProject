using Datamodels.Hrms;

namespace HrmsSolution.Service
{
    public interface IDepartmentService
    {
        Task<List<Department>> GetAllDepartmentsAsync();
        Task<Department> GetDepartmentByIdAsync(string id);
        Task<Department> AddDepartmentAsync(Department department);
        Task<bool> UpdateDepartmentAsync(string id, Department department);
        Task<bool> DeleteDepartmentAsync(string id);
    }
}