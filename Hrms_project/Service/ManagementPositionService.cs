using Datamodels.Hrms;
using System.Net.Http.Json;

namespace HrmsSolution.Service
{
    public class ManagementPositionService : IManagementPositionService
    {
        private readonly HttpClient _httpClient;
        private const string ApiPath = "api/ManagementPosition";

        public ManagementPositionService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Api");
        }

        public async Task<List<ManagementPosition>> GetAllManagementPositionsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ManagementPosition>>(ApiPath);
        }

        public async Task<ManagementPosition> GetManagementPositionByIdAsync(string id)
        {
            return await _httpClient.GetFromJsonAsync<ManagementPosition>($"{ApiPath}/{id}");
        }

        public async Task<ManagementPosition> AddManagementPositionAsync(ManagementPosition managementPosition)
        {
            var response = await _httpClient.PostAsJsonAsync(ApiPath, managementPosition);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ManagementPosition>();
        }

        public async Task<bool> UpdateManagementPositionAsync(string id, ManagementPosition managementPosition)
        {
            var response = await _httpClient.PutAsJsonAsync($"{ApiPath}/{id}", managementPosition);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteManagementPositionAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"{ApiPath}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}