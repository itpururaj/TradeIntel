using System.Net.Http.Headers;
using System.Net.Http.Json;
using TradeIntel.Application.AI;
using TradeIntel.Application.Models.Response.OpenAI;

namespace TradeIntel.Infrastructure.Providers
{
    public sealed class OpenAIProvider : IAIProvider
    {
        private readonly HttpClient _httpClient;
        private readonly IAISettingsService _aiSettingsService;

        public OpenAIProvider(HttpClient httpClient, IAISettingsService aiSettingsService)
        {
            _httpClient = httpClient;
            _httpClient.Timeout = TimeSpan.FromSeconds(60);
            _aiSettingsService = aiSettingsService;
        }

        public async Task<string> ExecutePrompAsync(string prompt)
        {
            var settings = await _aiSettingsService.GetAsync();

            if (string.IsNullOrWhiteSpace(settings.OpenAiApiKey))
            {
                return "OpenAI API key not configured.";
            }

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", settings.OpenAiApiKey);

            var request = new 
            {
                model = settings.OpenAiModel,
                input = prompt
            };

            // TODO: Get URL from settings or configuration
            var response = await _httpClient.PostAsJsonAsync(
                    "https://api.openai.com/v1/responses", request);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();

                return $"""
                        OpenAI Error

                        Status:
                        {response.StatusCode}

                        Details:
                        {errorContent}
                        """;
            }

            var result = await response.Content.ReadFromJsonAsync<OpenAIResponse>();
            var output = result?.Output?.FirstOrDefault()?.Content?.FirstOrDefault()?.Text;

            return output ?? "No response returned.";
        }
    }
}
