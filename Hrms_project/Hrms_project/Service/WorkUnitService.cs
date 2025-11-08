using Datamodels.Hrms;
using System.Net.Http.Json;

namespace HrmsSolution.Service
{
    public class WorkUnitService : IWorkUnitService
    {
        private readonly HttpClient _httpClient;
        private const string ApiPath = "api/WorkUnit";

        public WorkUnitService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Api");
        }

        public async Task<List<WorkUnit>> GetAllWorkUnitsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<WorkUnit>>(ApiPath);
        }

        public async Task<WorkUnit> GetWorkUnitByIdAsync(string id)
        {
            return await _httpClient.GetFromJsonAsync<WorkUnit>($"{ApiPath}/{id}");
        }

        public async Task<WorkUnit> AddWorkUnitAsync(WorkUnit workUnit)
        {
            var response = await _httpClient.PostAsJsonAsync(ApiPath, workUnit);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<WorkUnit>();
        }

        public async Task<bool> UpdateWorkUnitAsync(string id, WorkUnit workUnit)
        {
            var response = await _httpClient.PutAsJsonAsync($"{ApiPath}/{id}", workUnit);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteWorkUnitAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"{ApiPath}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}