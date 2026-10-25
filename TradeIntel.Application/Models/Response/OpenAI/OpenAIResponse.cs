namespace TradeIntel.Application.Models.Response.OpenAI
{
    public sealed class OpenAIResponse
    {
        public List<OpenAIOutput> Output { get; set; } = new();
    }

    public sealed class OpenAIOutput
    {
        public List<OpenAIContent> Content { get; set; } = new();
    }

    public sealed class OpenAIContent
    {
        public string Text { get; set; } = string.Empty;
    }
}
