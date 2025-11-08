using Datamodels.Hrms;

namespace HrmsSolution.Service
{
    public interface ILocationService
    {
        Task<List<Location>> GetAllLocationsAsync();
        Task<Location> GetLocationByIdAsync(string id);
        Task<Location> AddLocationAsync(Location location);
        Task<bool> UpdateLocationAsync(string id, Location location);
        Task<bool> DeleteLocationAsync(string id);
    }
}