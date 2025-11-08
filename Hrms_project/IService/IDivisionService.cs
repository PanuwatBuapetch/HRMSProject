using Datamodels.Hrms;

namespace HrmsSolution.Service
{
    public interface IDivisionService
    {
        Task<List<Division>> GetAllDivisionsAsync();
        Task<Division> GetDivisionByIdAsync(string id);
        Task<Division> AddDivisionAsync(Division division);
        Task<bool> UpdateDivisionAsync(string id, Division division);
        Task<bool> DeleteDivisionAsync(string id);
    }
}