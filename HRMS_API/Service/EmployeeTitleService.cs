using Datamodels.Hrms;
using Microsoft.EntityFrameworkCore;

namespace HRMS_API.Service
{
    public class EmployeeTitleService
    {
        private readonly IDbContextFactory<Hrms_dbContext> _contextFactory;
        public EmployeeTitleService(IDbContextFactory<Hrms_dbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public async Task<List<EmployeeTitle>> GetAllEmployeeTitlesAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.EmployeeTitles
                .AsNoTracking()
                .OrderBy(et => et.TitleId)
                .ToListAsync();
        }
        public async Task<EmployeeTitle?> GetEmployeeTitleByIdAsync(string id)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.EmployeeTitles.FindAsync(id);
        }
        public async Task<EmployeeTitle> AddEmployeeTitleAsync(EmployeeTitle employeeTitle)
        {
            using var context = _contextFactory.CreateDbContext();
            context.EmployeeTitles.Add(employeeTitle);
            await context.SaveChangesAsync();
            return employeeTitle;
        }
        public async Task<bool> UpdateEmployeeTitleAsync(string id, EmployeeTitle employeeTitle)
        {
            if (id != employeeTitle.TitleId) return false;
            using var context = _contextFactory.CreateDbContext();
            var existing = await context.EmployeeTitles.FindAsync(id);
            if (existing == null) return false;
            existing.TitleNameThai = employeeTitle.TitleNameThai;
            existing.TitleNameEng = employeeTitle.TitleNameEng;
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteEmployeeTitleAsync(string id)
        {
            using var context = _contextFactory.CreateDbContext();
            var affectedRows = await context.EmployeeTitles
                .Where(et => et.TitleId == id)
                .ExecuteDeleteAsync();
            return affectedRows > 0;
        }
    }
}