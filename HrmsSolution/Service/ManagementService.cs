using Datamodels.Hrms;
using System.Net.Http.Json;

namespace HrmsSolution.Service
{
    public class ManagementService : IManagementService
    {
        private readonly HttpClient _httpClient;
        private const string ApiPath = "api/Management";

        public ManagementService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Api");
        }

        public async Task<List<Management>> GetAllManagementAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Management>>(ApiPath);
        }

        public async Task<Management> GetManagementByIdAsync(string id)
        {
            return await _httpClient.GetFromJsonAsync<Management>($"{ApiPath}/{id}");
        }

        public async Task<Management> AddManagementAsync(Management management)
        {
            var response = await _httpClient.PostAsJsonAsync(ApiPath, management);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Management>();
        }

        public async Task<bool> UpdateManagementAsync(string id, Management management)
        {
            var response = await _httpClient.PutAsJsonAsync($"{ApiPath}/{id}", management);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteManagementAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"{ApiPath}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}