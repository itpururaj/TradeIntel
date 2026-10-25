# TradeIntel API - Calling OpenAI for Equity Research

TradeIntel is an ASP.NET Core Web API that leverages AI providers such as OpenAI to generate equity research, market analysis, SWOT analysis, investment theses, and other AI-driven research insights.

## Prerequisites

- .NET 8 SDK or later
- Visual Studio 2022
- OpenAI API Key
- Internet connectivity

---

## Configuration

Before running the application, configure the OpenAI settings in `appsettings.json`.

### appsettings.json

```json
{
  "OpenAI": {
    "ApiKey": "YOUR_OPENAI_API_KEY",
    "Model": "gpt-4o"
  }
}
```

### Configuration Settings

| Setting | Description |
|----------|-------------|
| ApiKey | OpenAI API Key used to authenticate requests. |
| Model | OpenAI model to use for research generation (e.g. `gpt-4o`). |

---

## Obtaining an OpenAI API Key

1. Create or sign in to your OpenAI account.
2. Navigate to the API Keys section.
3. Generate a new API Key.
4. Copy the generated key.
5. Update the `OpenAI:ApiKey` setting in `appsettings.json`.

Example:

```json
{
  "OpenAI": {
    "ApiKey": "sk-xxxxxxxxxxxxxxxxxxxxxxxx",
    "Model": "gpt-4o"
  }
}
```

---

## Security Considerations

Do not commit real API keys to source control.

Before pushing changes to a repository:

```json
{
  "OpenAI": {
    "ApiKey": "",
    "Model": "gpt-4o"
  }
}
```

Alternatively, store API keys using:

- User Secrets
- Environment Variables
- Azure Key Vault
- Local configuration files excluded through `.gitignore`

---

## Build and Run

Restore NuGet packages:

```bash
dotnet restore
```

Build the solution:

```bash
dotnet build
```

Run the API:

```bash
dotnet run
```

---

## Example Request

### Generate Equity Research

```http
POST /api/research/GetSymbolDetail?symbol=MSFT
```

### Example Response

```json
{
  "symbol": "MSFT",
  "researchData": "Microsoft is a global technology company..."
}
```

---

## Troubleshooting

### API Key Not Found

Verify that:

- `OpenAI:ApiKey` exists in `appsettings.json`
- The API key is valid
- The application has been restarted after configuration changes

### Authentication Failed

Common causes include:

- Invalid API key
- Expired API key
- Insufficient OpenAI account credits
- Incorrect model name

---

## Architecture

The solution follows a layered architecture:

```text
TradeIntel.API
    Controllers
    Request/Response Models

TradeIntel.Application
    Business Use Cases
    Service Contracts
    DTOs

TradeIntel.Domain
    Entities
    Value Objects
    Domain Logic

TradeIntel.Infrastructure
    OpenAI Integration
    Persistence
    External Services
```

Dependency flow:

```text
API
 ↓
Application
 ↓
Domain

Infrastructure
 implements Application contracts
```

This architecture promotes separation of concerns, testability, and flexibility when replacing external providers or infrastructure components.

---

## OpenAI Settings Class

```csharp
public class OpenAISettings
{
    public string ApiKey { get; set; } = string.Empty;

    public string Model { get; set; } = "gpt-4o";
}
```

---

## Registering Configuration

```csharp
builder.Services.Configure<OpenAISettings>(
    builder.Configuration.GetSection("OpenAI"));
```

---

## Consuming Settings via IOptions

```csharp
public class OpenAIProvider
{
    private readonly OpenAISettings _settings;

    public OpenAIProvider(IOptions<OpenAISettings> options)
    {
        _settings = options.Value;
    }
}
```

