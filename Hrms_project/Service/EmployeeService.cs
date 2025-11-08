using Datamodels.Hrms;
using System.Net.Http.Json;

namespace HrmsSolution.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient _httpClient;
        private const string ApiPath = "api/Employee";

        public EmployeeService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Api");
        }

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Employee>>(ApiPath);
        }

        public async Task<Employee> GetEmployeeByIdAsync(string id)
        {
            return await _httpClient.GetFromJsonAsync<Employee>($"{ApiPath}/{id}");
        }

        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            var response = await _httpClient.PostAsJsonAsync(ApiPath, employee);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Employee>();
        }

        public async Task<bool> UpdateEmployeeAsync(string id, Employee employee)
        {
            var response = await _httpClient.PutAsJsonAsync($"{ApiPath}/{id}", employee);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteEmployeeAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"{ApiPath}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}