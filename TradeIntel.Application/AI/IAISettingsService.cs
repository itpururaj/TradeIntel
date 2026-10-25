using TradeIntel.Application.Models;

namespace TradeIntel.Application.AI
{
    public interface IAISettingsService
    {
        Task<AIConfigurationModel> GetAsync();
    }
}
