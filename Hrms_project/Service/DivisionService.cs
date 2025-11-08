using Datamodels.Hrms;
using System.Net.Http.Json;

namespace HrmsSolution.Service
{
    public class DivisionService : IDivisionService
    {
        private readonly HttpClient _httpClient;
        private const string ApiPath = "api/Division";

        public DivisionService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Api");
        }

        public async Task<List<Division>> GetAllDivisionsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Division>>(ApiPath);
        }

        public async Task<Division> GetDivisionByIdAsync(string id)
        {
            return await _httpClient.GetFromJsonAsync<Division>($"{ApiPath}/{id}");
        }

        public async Task<Division> AddDivisionAsync(Division division)
        {
            var response = await _httpClient.PostAsJsonAsync(ApiPath, division);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Division>();
        }

        public async Task<bool> UpdateDivisionAsync(string id, Division division)
        {
            var response = await _httpClient.PutAsJsonAsync($"{ApiPath}/{id}", division);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteDivisionAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"{ApiPath}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}