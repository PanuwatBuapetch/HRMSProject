using Datamodels.Hrms;
using Microsoft.EntityFrameworkCore;

namespace HRMS_API.Service
{
    public class DivisionService
    {
        private readonly IDbContextFactory<Hrms_dbContext> _contextFactory;

        public DivisionService(IDbContextFactory<Hrms_dbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<Division>> GetAllDivisionsAsync()
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Divisions
                .AsNoTracking()
                .OrderBy(d => d.DivisionId)
                .ToListAsync();
        }

        public async Task<Division?> GetDivisionByIdAsync(string id)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Divisions.FindAsync(id);
        }

        public async Task<Division> AddDivisionAsync(Division division)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            context.Divisions.Add(division);
            await context.SaveChangesAsync();
            return division;
        }

        public async Task<bool> UpdateDivisionAsync(string id, Division division)
        {
            // แก้ไข: เปลี่ยนจาก LocationId เป็น DivisionId
            if (id != division.DivisionId) return false;

            await using var context = await _contextFactory.CreateDbContextAsync();

            var existing = await context.Divisions.FindAsync(id);
            if (existing == null) return false;

            // Mapping ข้อมูลใหม่
            existing.DivisionNameThai = division.DivisionNameThai;
            existing.DivisionNameEng = division.DivisionNameEng;
            existing.DivisionDesc = division.DivisionDesc;
            existing.Isactive = division.Isactive; // เพิ่ม Isactive เข้าไปด้วย

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteDivisionAsync(string id)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            // แก้ไข: เปลี่ยนจาก context.Locations เป็น context.Divisions
            var affectedRows = await context.Divisions
                .Where(d => d.DivisionId == id)
                .ExecuteDeleteAsync();
            return affectedRows > 0;
        }
    }
}