namespace TradeIntel.Application.Models
{
    public sealed class AIConfigurationModel
    {
        public string OpenAiApiKey
        {
            get;
            set;
        } = string.Empty;

        public string AzureOpenAiEndpoint
        {
            get;
            set;
        } = string.Empty;

        public string AzureOpenAiApiKey
        {
            get;
            set;
        } = string.Empty;

        public string DefaultTradingProvider
        {
            get;
            set;
        } = "PaperTrading";

        public bool EnableAiFeatures
        {
            get;
            set;
        } = true;

        public AIProviderType AIProvider
        {
            get;
            set;
        } = AIProviderType.Mock;

        public string ClaudeApiKey
        {
            get;
            set;
        } = string.Empty;

        public string OpenAiModel
        {
            get;
            set;
        } = "gpt-4o-mini";

        public string ClaudeModel
        {
            get;
            set;
        } = "claude-sonnet";
    }
}
