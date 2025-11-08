using Datamodels.Hrms;
using System.Net.Http.Json;

namespace HrmsSolution.Service
{
    public class JobPositionService : IJobPositionService
    {
        private readonly HttpClient _httpClient;
        private const string ApiPath = "api/JobPosition";

        public JobPositionService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Api");
        }

        public async Task<List<JobPosition>> GetAllJobPositionsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<JobPosition>>(ApiPath);
        }

        public async Task<JobPosition> GetJobPositionByIdAsync(string id)
        {
            return await _httpClient.GetFromJsonAsync<JobPosition>($"{ApiPath}/{id}");
        }

        public async Task<JobPosition> AddJobPositionAsync(JobPosition jobPosition)
        {
            var response = await _httpClient.PostAsJsonAsync(ApiPath, jobPosition);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<JobPosition>();
        }

        public async Task<bool> UpdateJobPositionAsync(string id, JobPosition jobPosition)
        {
            var response = await _httpClient.PutAsJsonAsync($"{ApiPath}/{id}", jobPosition);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteJobPositionAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"{ApiPath}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}