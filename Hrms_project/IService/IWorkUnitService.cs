using Datamodels.Hrms;

namespace HrmsSolution.Service
{
    public interface IWorkUnitService
    {
        Task<List<WorkUnit>> GetAllWorkUnitsAsync();
        Task<WorkUnit> GetWorkUnitByIdAsync(string id);
        Task<WorkUnit> AddWorkUnitAsync(WorkUnit workUnit);
        Task<bool> UpdateWorkUnitAsync(string id, WorkUnit workUnit);
        Task<bool> DeleteWorkUnitAsync(string id);
    }
}