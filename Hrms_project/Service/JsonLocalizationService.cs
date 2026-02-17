using Microsoft.AspNetCore.Hosting;
using System.Text.Json;

namespace Hrms_project.Service
{
    public class JsonLocalizationService
    {
        private readonly IWebHostEnvironment _env;
        private Dictionary<string, string> _localizationData = new();
        public event Action? OnLanguageChanged;
        public string CurrentCulture { get; private set; } = "th-TH";

        public JsonLocalizationService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public string this[string key] =>
            _localizationData.TryGetValue(key, out var value) ? value : key;

        public async Task SetCulture(string culture)
        {
            try
            {
                string fileName = culture.Split('-')[0];

                string path = Path.Combine(
                    _env.WebRootPath,
                    "languages",
                    $"{fileName}.json"
                );

                if (File.Exists(path))
                {
                    string jsonString = await File.ReadAllTextAsync(path);

                    _localizationData =
                        JsonSerializer.Deserialize<Dictionary<string, string>>(jsonString)
                        ?? new();

                    CurrentCulture = culture;

                    OnLanguageChanged?.Invoke();
                }
                else
                {
                    Console.WriteLine($"[Localization] File not found: {path}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error] Localization: {ex.Message}");
            }
        }
    }
}