using TradeIntel.Application.Models;

namespace TradeIntel.Application.AI
{
    public interface IAIProviderFactory
    {
        Task<IAIProvider> CreateAsync(AIProviderType providerType);
    }
}
