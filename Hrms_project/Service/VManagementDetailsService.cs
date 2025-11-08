using Datamodels.Hrms;
using System.Net.Http.Json;

namespace HrmsSolution.Service
{
    public class VManagementDetailsService : IVManagementDetailsService
    {
        private readonly HttpClient _httpClient;
        private const string ApiPath = "api/VManagementDetails";

        public VManagementDetailsService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Api");
        }

        public async Task<List<VManagementDetail>> GetAllManagementDetailsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<VManagementDetail>>(ApiPath);
        }

        public async Task<VManagementDetail> GetManagementDetailsByStaffIdAsync(string staffId)
        {
            return await _httpClient.GetFromJsonAsync<VManagementDetail>($"{ApiPath}/ByStaff/{staffId}");
        }

        public async Task<VManagementDetail> GetManagementDetailsByKeyAsync(string executiveKey)
        {
            // (คุณอาจต้องเพิ่ม Endpoint นี้ใน API Controller ด้วย)
            return await _httpClient.GetFromJsonAsync<VManagementDetail>($"{ApiPath}/ByKey/{executiveKey}");
        }

        public async Task<List<VManagementDetail>> SearchManagementByNameAsync(string name)
        {
            return await _httpClient.GetFromJsonAsync<List<VManagementDetail>>($"{ApiPath}/Search/{name}");
        }
    }
}