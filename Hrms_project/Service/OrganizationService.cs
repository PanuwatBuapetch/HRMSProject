using System.Net.Http.Json;
using Datamodels.Hrms;
using HrmsSolution.Service;

namespace Hrms_project.Service
{
    public class OrganizationService : IOrganizationService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "api/Organization";

        public OrganizationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Division>> GetAllDivisionsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Division>>($"{BaseUrl}/divisions") ?? new List<Division>();
        }

        public async Task<List<Department>> GetDepartmentsByDivisionIdAsync(string divisionId)
        {
            return await _httpClient.GetFromJsonAsync<List<Department>>($"{BaseUrl}/divisions/{divisionId}/departments") ?? new List<Department>();
        }

        public async Task<List<WorkUnit>> GetWorkUnitsByDepartmentIdAsync(string deptId)
        {
            return await _httpClient.GetFromJsonAsync<List<WorkUnit>>($"{BaseUrl}/departments/{deptId}/units") ?? new List<WorkUnit>();
        }

        public async Task<bool> AddDivisionAsync(Division division)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/divisions", division);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> AddDepartmentAsync(Department department)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/departments", department);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> AddWorkUnitAsync(WorkUnit workUnit)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/units", workUnit);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteDivisionAsync(string divisionId)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/divisions/{divisionId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteDepartmentAsync(string deptId)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/departments/{deptId}");
            return response.IsSuccessStatusCode;
        }
    }
}