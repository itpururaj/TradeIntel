using TradeIntel.Application.AI;
using TradeIntel.Application.Models;

namespace TradeIntel.Infrastructure.Providers
{
    public sealed class AIProviderFactory : IAIProviderFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public AIProviderFactory(IServiceProvider serviceProvider)  
        { 
            _serviceProvider = serviceProvider; 
        }
        

        public Task<IAIProvider> CreateAsync(AIProviderType providerType)
        {
            IAIProvider provider = providerType switch
            {
                AIProviderType.OpenAI => (IAIProvider)_serviceProvider.GetService(typeof(OpenAIProvider))!,

                // NOTE: Based on the provider type get the corresponding provider from the service provider
                //AIProviderType.Mock => (IAIProvider)_serviceProvider.GetService(typeof(MockAIProvider))!,
                //AIProviderType.AzureOpenAI => (IAIProvider)_serviceProvider.GetService(typeof(AzureOpenAIProvider))!,
                //AIProviderType.Claude => (IAIProvider)_serviceProvider.GetService(typeof(ClaudeAIProvider))!,
                _ => throw new NotSupportedException($"The provider type '{providerType}' is not supported.")
            };

            return Task.FromResult(provider);
        }
    }
}
