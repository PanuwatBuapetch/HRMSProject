using Datamodels.Hrms;

namespace HrmsSolution.Service
{
    public interface IJobPositionService
    {
        Task<List<JobPosition>> GetAllJobPositionsAsync();
        Task<JobPosition> GetJobPositionByIdAsync(string id);
        Task<JobPosition> AddJobPositionAsync(JobPosition jobPosition);
        Task<bool> UpdateJobPositionAsync(string id, JobPosition jobPosition);
        Task<bool> DeleteJobPositionAsync(string id);
    }
}