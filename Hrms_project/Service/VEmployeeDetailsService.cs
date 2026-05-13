using Datamodels.Hrms;
using System.Net.Http.Json;
using System.Xml.Linq;

namespace HrmsSolution.Service
{
    public class VEmployeeDetailsService : IVEmployeeDetailsService
    {
        private readonly HttpClient _httpClient;
        private const string ApiPath = "api/VEmployeeDetails";

        public VEmployeeDetailsService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Api");
        }

        public async Task<List<VEmployeeDetail>> GetAllEmployeeDetailsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<VEmployeeDetail>>(ApiPath);
        }

        public async Task<VEmployeeDetail> GetEmployeeDetailsByIdAsync(string id)
        {
            return await _httpClient.GetFromJsonAsync<VEmployeeDetail>($"{ApiPath}/{id}");
        }

        public async Task<List<VEmployeeDetail>> SearchEmployeesByNameAsync(string name)
        {
            return await _httpClient.GetFromJsonAsync<List<VEmployeeDetail>>($"{ApiPath}/Search/{name}");
        }

        public async Task<bool> AssignEmployeeToDeptAsync(string employeeId, string deptId, string divisionId)
        {
            // ต้องเรียงลำดับ Path ให้ตรงกับที่แก้ใน Controller (employeeId/deptId/divisionId)
            var content = new StringContent("", System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"api/Employee/assign/{employeeId}/{deptId}/{divisionId}", content);

            // ถ้า Response ไม่ใช่ 200 OK ให้คืนค่า false
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UnassignEmployeeAsync(string employeeId)
        {
            // ใช้ PatchAsync ตาม API ที่เราสร้างขึ้น api/Employee/unassign/{id}
            var content = new StringContent("", System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PatchAsync($"api/Employee/unassign/{employeeId}", content);
            return response.IsSuccessStatusCode;
        }
    }
}