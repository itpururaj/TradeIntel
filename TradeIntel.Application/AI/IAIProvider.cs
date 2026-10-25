namespace TradeIntel.Application.AI
{
    public interface IAIProvider
    {
        Task<string> ExecutePrompAsync(string prompt);
    }
}
