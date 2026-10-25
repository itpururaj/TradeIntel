using Microsoft.AspNetCore.Mvc;
using TradeIntel.Application.AI;
using TradeIntel.Application.Models.Request;
using TradeIntel.Application.Models.Response;

namespace TradeIntel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SymbolResearch : ControllerBase
    {
        private readonly IAIResearchService _aiResearchService;

        public SymbolResearch(IAIResearchService aiResearchService)
        {
            _aiResearchService = aiResearchService;
        }

        [HttpPost("GetSymbolDetail")]
        public async Task<ActionResult<SymbolResearchResponse>> GetSymbolDetail(
            SymbolResearchRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Symbol))
            {
                return BadRequest(new SymbolResearchResponse
                {
                    Success = false,
                    ErrorMessage = "Invalid request. Symbol is required."
                });
            }

            var result = await _aiResearchService.AnalyzeAsync(request);

            // TODO: Use AutoMapper to map the result to SymbolResearchResponse
            var response = new SymbolResearchResponse
            {
                Success = true,
                Symbol = result.Symbol,
                ResearchData = result.ResearchData
            };

            return Ok(response);
        }
    }
}
