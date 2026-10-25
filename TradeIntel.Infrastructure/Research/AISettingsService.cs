using Microsoft.Extensions.Configuration;
using TradeIntel.Application.AI;
using TradeIntel.Application.Models;

namespace TradeIntel.Infrastructure.Research
{
    public sealed class AISettingsService : IAISettingsService
    {
        private readonly IConfiguration _configuration;

        private readonly string _apiKey;
        private readonly string _model;

        public AISettingsService(IConfiguration configuration) 
        { 
            _configuration = configuration;

            // TODO: Consider using a more secure way to store and retrieve API keys, such as Azure Key Vault.
            // also consider using IOptions<OpenAISettings> to bind configuration settings to a strongly typed class.
            _apiKey = _configuration["OpenAI:ApiKey"] ?? string.Empty;
            _model = _configuration["OpenAI:Model"] ?? string.Empty;
        }

        public async Task<AIConfigurationModel> GetAsync()
        {
            // TODO: Validate the API key and model, and handle any errors appropriately.
            await Task.Delay(100); // Simulate async operation
            return new AIConfigurationModel
            {
                AIProvider = AIProviderType.OpenAI,
                OpenAiApiKey = _apiKey,
                OpenAiModel = _model
            };
        }
    }
}
