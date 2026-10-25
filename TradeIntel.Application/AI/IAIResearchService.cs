using TradeIntel.Application.Models.Request;
using TradeIntel.Application.Models.Response;

namespace TradeIntel.Application.AI
{
    public interface IAIResearchService
    {
        Task<SymbolResearchResponse> AnalyzeAsync(SymbolResearchRequest symbolResearchModel);
    }
}
