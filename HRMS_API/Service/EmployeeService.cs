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
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Employees
                .AsNoTracking()
                .OrderBy(e => e.EmployeeId) // เรียงตามรหัส
                .ToListAsync();
        }

        public async Task<Employee?> GetEmployeeByIdAsync(string id)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Employees.FindAsync(id);
        }

        public async Task<Employee?> AddEmployeeAsync(Employee employee)
        {
            using var context = await _contextFactory.CreateDbContextAsync();

            // 1. Validate: เช็คว่ารหัสซ้ำไหม
            if (await context.Employees.AnyAsync(e => e.EmployeeId == employee.EmployeeId))
                throw new Exception($"รหัสพนักงาน {employee.EmployeeId} มีอยู่ในระบบแล้ว");

            // 2. Validate: เช็คว่าบัตรประชาชนซ้ำไหม (ถ้าจำเป็น)
            // if (!string.IsNullOrEmpty(employee.CitizenId) && 
            //     await context.Employees.AnyAsync(e => e.CitizenId == employee.CitizenId))
            //     throw new Exception("เลขบัตรประชาชนนี้มีอยู่ในระบบแล้ว");

            context.Employees.Add(employee);
            await context.SaveChangesAsync();
            return employee;
        }

        public async Task<bool> UpdateEmployeeAsync(string id, Employee employee)
        {
            if (id != employee.EmployeeId) return false;

            using var context = await _contextFactory.CreateDbContextAsync();

            var existingEmp = await context.Employees.FindAsync(id);
            if (existingEmp == null) return false;

            // *** Perfect Update Technique ***
            // อัปเดตทุก Field ที่ตรงกัน โดยไม่ต้องเขียนทีละบรรทัด
            context.Entry(existingEmp).CurrentValues.SetValues(employee);

            // ป้องกันการแก้ไข Field สำคัญที่ห้ามแก้ (เช่น วันที่สร้าง)
            // context.Entry(existingEmp).Property(x => x.CreatedDate).IsModified = false; 

            await context.SaveChangesAsync();
            return true;
        }

        // ฟังก์ชันใหม่: สำหรับระบบ Organization Chart (ย้ายคนออก)
        public async Task<bool> UnassignEmployeeAsync(string id)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            var emp = await context.Employees.FindAsync(id);
            if (emp == null) return false;

            // ปลดสังกัด
            emp.DivisionId = null;
            emp.DeptId = null;
            emp.UnitId = null;

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteEmployeeAsync(string id)
        {
            using var context = await _contextFactory.CreateDbContextAsync();

            var emp = await context.Employees.FindAsync(id);
            if (emp == null) return false;

            context.Employees.Remove(emp);
            await context.SaveChangesAsync();
            return true;
        }
    }
}