using Datamodels.Hrms;
using Microsoft.EntityFrameworkCore;

namespace HRMS_API.Service
{
    public class EmployeeService
    {
        private readonly IDbContextFactory<Hrms_dbContext> _contextFactory;

        public EmployeeService(IDbContextFactory<Hrms_dbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Employees
                .AsNoTracking() // ใช้อ่านอย่างเดียว จะเร็วกว่า
                .OrderBy(e => e.EmployeeId)
                .ToListAsync();
        }

        public async Task<Employee?> GetEmployeeByIdAsync(string id)
        {
            using var context = _contextFactory.CreateDbContext();
            // FindAsync จะค้นหาจาก Primary Key ซึ่งเร็วที่สุด
            return await context.Employees.FindAsync(id);
        }

        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Employees.Add(employee);
            await context.SaveChangesAsync();
            return employee;
        }

        public async Task<bool> UpdateEmployeeAsync(string id, Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return false; // ID ไม่ตรงกัน
            }

            using var context = _contextFactory.CreateDbContext();

            // ดึงข้อมูลเดิมมาอัปเดต (วิธีนี้ปลอดภัยที่สุด)
            var existingEmp = await context.Employees.FindAsync(id);
            if (existingEmp == null)
            {
                return false; // ไม่พบข้อมูล
            }

            // อัปเดตค่าที่ต้องการ
            existingEmp.FirstNameThai = employee.FirstNameThai;
            existingEmp.LastNameThai = employee.LastNameThai;
            existingEmp.Email = employee.Email;
            existingEmp.PositionId = employee.PositionId;
            existingEmp.LocationId = employee.LocationId;
            existingEmp.DivisionId = employee.DivisionId;
            existingEmp.DeptId = employee.DeptId;
            existingEmp.TeamId = employee.TeamId;
            // ... (อัปเดต Field อื่นๆ ที่อนุญาตให้แก้) ...

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteEmployeeAsync(string id)
        {
            using var context = _contextFactory.CreateDbContext();

            // ใช้วิธี ExecuteDelete เพื่อประสิทธิภาพ (ไม่ต้องดึงข้อมูลมาก่อน)
            var affectedRows = await context.Employees
                .Where(e => e.EmployeeId == id)
                .ExecuteDeleteAsync();

            return affectedRows > 0;
        }
    }
}