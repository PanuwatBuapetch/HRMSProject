using Datamodels.Hrms;
using System.Net.Http.Json;

namespace HrmsSolution.Service
{
    public class MissionService : IMissionService
    {
        private readonly HttpClient _httpClient;
        private const string ApiPath = "api/Mission";

        public MissionService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Api");
        }

        public async Task<List<Mission>> GetAllMissionsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Mission>>(ApiPath);
        }

        public async Task<Mission> GetMissionByIdAsync(string id)
        {
            return await _httpClient.GetFromJsonAsync<Mission>($"{ApiPath}/{id}");
        }

        public async Task<Mission> AddMissionAsync(Mission mission)
        {
            var response = await _httpClient.PostAsJsonAsync(ApiPath, mission);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Mission>();
        }

        public async Task<bool> UpdateMissionAsync(string id, Mission mission)
        {
            var response = await _httpClient.PutAsJsonAsync($"{ApiPath}/{id}", mission);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteMissionAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"{ApiPath}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}