using TradeIntel.Application.AI;
using TradeIntel.Infrastructure.Providers;
using TradeIntel.Infrastructure.Research;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile("settings.json", optional: true, reloadOnChange: true);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IAIProviderFactory, AIProviderFactory>();

builder.Services.AddTransient<IAIResearchService, AIResearchService>();
builder.Services.AddTransient<IAISettingsService, AISettingsService>();

builder.Services.AddHttpClient<OpenAIProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
