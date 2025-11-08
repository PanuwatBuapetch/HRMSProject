using Datamodels.Hrms;
using System.Net.Http.Json;

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
    }
}