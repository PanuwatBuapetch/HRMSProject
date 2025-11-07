using System.Net.Http.Json;
using Datamodels.Hrms;
using HrmsSolution.Components;

namespace HrmsAppSolution.Services
{
    public class EmployeeService
    {
        private readonly HttpClient _httpClient;

        public EmployeeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<StaffDetail>> GetAllStaffDetailsAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<StaffDetail>>("api/StaffDetail");
            return result ?? new List<StaffDetail>();
        }
        public async Task<StaffDetail?> GetStaffDetailAsync(string id)
        {
            return await _httpClient.GetFromJsonAsync<StaffDetail>($"api/StaffDetail/{id}");
        }
        public async Task AddEmployeeAsync(StaffDetail staffDetail)
        {
            await _httpClient.PostAsJsonAsync("api/StaffDetail", staffDetail);
        }
        public async Task UpdateEmployeesAsync(StaffDetail staffDetail)
        {
            await _httpClient.PutAsJsonAsync($"api/StaffDetail/{staffDetail.StaffId}", staffDetail);
        }
        public async Task DeleteEmployeeAsync(string id)
        {
            await _httpClient.DeleteAsync($"api/StaffDetail/{id}");
        }
    }
}
