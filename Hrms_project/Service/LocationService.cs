using Datamodels.Hrms;
using System.Net.Http.Json;

namespace HrmsSolution.Service
{
    public class LocationService : ILocationService
    {
        private readonly HttpClient _httpClient;
        private const string ApiPath = "api/Location";

        public LocationService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Api");
        }

        public async Task<List<Location>> GetAllLocationsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Location>>(ApiPath);
        }

        public async Task<Location> GetLocationByIdAsync(string id)
        {
            return await _httpClient.GetFromJsonAsync<Location>($"{ApiPath}/{id}");
        }

        public async Task<Location> AddLocationAsync(Location location)
        {
            var response = await _httpClient.PostAsJsonAsync(ApiPath, location);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Location>();
        }

        public async Task<bool> UpdateLocationAsync(string id, Location location)
        {
            var response = await _httpClient.PutAsJsonAsync($"{ApiPath}/{id}", location);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteLocationAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"{ApiPath}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}