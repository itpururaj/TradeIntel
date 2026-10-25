using TradeIntel.Application.AI;
using TradeIntel.Application.Models.Request;
using TradeIntel.Application.Models.Response;

namespace TradeIntel.Infrastructure.Research
{
    public sealed class AIResearchService : IAIResearchService
    {
        private readonly IAIProviderFactory _providerFactory;
        private readonly IAISettingsService _aiSettingService;

        public AIResearchService(IAIProviderFactory providerFactory, 
            IAISettingsService aiSettingsService)
        {
            _providerFactory = providerFactory;
            _aiSettingService = aiSettingsService;
        }

        public async Task<SymbolResearchResponse> AnalyzeAsync(SymbolResearchRequest request)
        {
            var settings = await _aiSettingService.GetAsync();
            var provider = await _providerFactory.CreateAsync(settings.AIProvider);

            // TODO: Depending on requirement you can take prompt in request.
            var prompt = $"""
                    Act as a professional equity research analyst.
                    Generate a detailed Bull Case for {request.Symbol}.
                    Include:
                    1. Business Overview
                    2. Revenue Growth Drivers
                    3. Competitive Advantages
                    4. Market Opportunities
                    5. Key Financial Strengths
                    6. Why Investors May Be Optimistic
                    Use clear headings.
                    Use professional investment language.
                    Limit response to 500 words.
                    """;

            var result = await provider.ExecutePrompAsync($"Analyze stock {request.Symbol}. {prompt}")
                .ContinueWith(task =>
                {
                    if (task.IsFaulted)
                    {
                        return new SymbolResearchResponse
                        {
                            Success = false,
                            Symbol = request.Symbol,
                            ResearchData = null,
                            ErrorMessage = task.Exception?.Message
                        };
                    }

                    return new SymbolResearchResponse
                    {
                        Success = true,
                        Symbol = request.Symbol,
                        ResearchData = task.Result,
                        ErrorMessage = null
                    };
                });

            return result;
        }
    }
}
