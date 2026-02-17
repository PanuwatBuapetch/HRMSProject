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

        public async Task<bool> UpdateEmployeeAsync(Employee employee)
        {
            // ใช้ employee.EmployeeId ส่งไปใน URL
            var response = await _httpClient.PutAsJsonAsync($"{ApiPath}/{employee.EmployeeId}", employee);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteEmployeeAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"{ApiPath}/{id}");
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> UnassignEmployeeAsync(string id)
        {
            // ยิงไปที่ Endpoint: api/Employee/unassign/{id}
            // ใช้ PatchAsync เพราะเป็นการแก้ไขข้อมูลบางส่วน (Patch)
            var response = await _httpClient.PatchAsync($"{ApiPath}/unassign/{id}", null);
            return response.IsSuccessStatusCode;
        }
    }
}