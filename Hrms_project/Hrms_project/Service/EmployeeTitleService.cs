using Datamodels.Hrms;
using System.Net.Http.Json;

namespace HrmsSolution.Service
{
    public class EmployeeTitleService : IEmployeeTitleService
    {
        private readonly HttpClient _httpClient;
        private const string ApiPath = "api/EmployeeTitle";

        public EmployeeTitleService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Api");
        }

        public async Task<List<EmployeeTitle>> GetAllEmployeeTitlesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<EmployeeTitle>>(ApiPath);
        }

        public async Task<EmployeeTitle> GetEmployeeTitleByIdAsync(string id)
        {
            return await _httpClient.GetFromJsonAsync<EmployeeTitle>($"{ApiPath}/{id}");
        }

        public async Task<EmployeeTitle> AddEmployeeTitleAsync(EmployeeTitle employeeTitle)
        {
            var response = await _httpClient.PostAsJsonAsync(ApiPath, employeeTitle);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<EmployeeTitle>();
        }

        public async Task<bool> UpdateEmployeeTitleAsync(string id, EmployeeTitle employeeTitle)
        {
            var response = await _httpClient.PutAsJsonAsync($"{ApiPath}/{id}", employeeTitle);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteEmployeeTitleAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"{ApiPath}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}