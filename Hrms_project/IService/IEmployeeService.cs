using Datamodels.Hrms;

namespace HrmsSolution.Service
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetAllEmployeesAsync();
        Task<Employee?> GetEmployeeByIdAsync(string id); // ใช้ Employee? เผื่อหาไม่เจอ
        Task<Employee?> AddEmployeeAsync(Employee employee);
        Task<bool> UpdateEmployeeAsync(Employee employee);
        Task<bool> UnassignEmployeeAsync(string id); // เพิ่มตัวนี้
        Task<bool> DeleteEmployeeAsync(string id);
    }
}