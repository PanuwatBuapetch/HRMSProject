using Datamodels.Hrms;
using Microsoft.EntityFrameworkCore;

namespace HRMS_API.Service
{
    public class DepartmentService
    {
        private readonly IDbContextFactory<Hrms_dbContext> _contextFactory;

        public DepartmentService(IDbContextFactory<Hrms_dbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<Department>> GetAllDepartmentsAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Departments
                .AsNoTracking()
                .OrderBy(l => l.DeptId)
                .ToListAsync();
        }

        public async Task<Department?> GetDepartmentByIdAsync(string id)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Departments.FindAsync(id);
        }

        public async Task<Department> AddDepartmentAsync(Department department)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Departments.Add(department);
            await context.SaveChangesAsync();
            return department;
        }

        public async Task<bool> UpdateDepartmentAsync(string id, Department department)
        {
            if (id != department.DeptId) return false;

            using var context = _contextFactory.CreateDbContext();

            var existing = await context.Departments.FindAsync(id);
            if (existing == null) return false;

            existing.DeptNameThai = department.DeptNameThai;
            existing.DeptNameEng = department.DeptNameEng;
            existing.DeptDesc = department.DeptDesc;

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteDepartmentAsync(string id)
        {
            using var context = _contextFactory.CreateDbContext();
            var affectedRows = await context.Departments
                .Where(l => l.DeptId == id)
                .ExecuteDeleteAsync();
            return affectedRows > 0;
        }
    }
}