namespace TradeIntel.Application.Models.Response
{
    public class SymbolResearchResponse
    {
        public bool Success { get; set; }
        public string? Symbol { get; set; }
        public string? ResearchData { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
