using Datamodels.Hrms;
using System.Net.Http.Json;

namespace HrmsSolution.Service
{
    public class TeamService : ITeamService
    {
        private readonly HttpClient _httpClient;
        private const string ApiPath = "api/Team";

        public TeamService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Api");
        }

        public async Task<List<Team>> GetAllTeamsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Team>>(ApiPath);
        }

        public async Task<Team> GetTeamByIdAsync(string id)
        {
            return await _httpClient.GetFromJsonAsync<Team>($"{ApiPath}/{id}");
        }

        public async Task<Team> AddTeamAsync(Team team)
        {
            var response = await _httpClient.PostAsJsonAsync(ApiPath, team);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Team>();
        }

        public async Task<bool> UpdateTeamAsync(string id, Team team)
        {
            var response = await _httpClient.PutAsJsonAsync($"{ApiPath}/{id}", team);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteTeamAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"{ApiPath}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}