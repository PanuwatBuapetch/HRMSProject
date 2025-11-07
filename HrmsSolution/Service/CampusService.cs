using System.Net.Http.Json;
using Datamodels.Hrms;

namespace HrmsAppSolution.Services
{
    public class CampusService
    {
        private readonly HttpClient _httpClient;

        public CampusService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Campus>> GetAllCampusAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<Campus>>("api/Campus");
            return result ?? new List<Campus>();
        }
    }
}
