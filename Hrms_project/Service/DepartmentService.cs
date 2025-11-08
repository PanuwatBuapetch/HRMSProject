using Datamodels.Hrms;
using System.Net.Http.Json;

namespace HrmsSolution.Service
{
    public class DepartmentService : IDepartmentService
    {
        private readonly HttpClient _httpClient;
        private const string ApiPath = "api/Department";

        public DepartmentService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Api");
        }

        public async Task<List<Department>> GetAllDepartmentsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Department>>(ApiPath);
        }

        public async Task<Department> GetDepartmentByIdAsync(string id)
        {
            return await _httpClient.GetFromJsonAsync<Department>($"{ApiPath}/{id}");
        }

        public async Task<Department> AddDepartmentAsync(Department department)
        {
            var response = await _httpClient.PostAsJsonAsync(ApiPath, department);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Department>();
        }

        public async Task<bool> UpdateDepartmentAsync(string id, Department department)
        {
            var response = await _httpClient.PutAsJsonAsync($"{ApiPath}/{id}", department);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteDepartmentAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"{ApiPath}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}