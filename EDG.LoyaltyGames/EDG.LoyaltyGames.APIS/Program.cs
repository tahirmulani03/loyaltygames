using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using EDG.LoyaltyGames.APIS.Extensions;
using EDG.LoyaltyGames.APIS.Filters;
using EDG.LoyaltyGames.Core.Entites.ServiceBus;
using EDG.LoyaltyGames.Core.Entites.Settings;
using EDG.LoyaltyGames.Infrastructure.AppInsights;
using EDG.LoyaltyGames.Infrastructure.Extension;
using EDG.LoyaltyGames.Infrastructure.SignalR;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.Extensions.Azure;
using MongoDB.Driver;
using StackExchange.Redis;
using System.Security.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Allows both to access and to set up the config
IWebHostEnvironment environment = builder.Environment;

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.Configure<KeyVaultSettings>(builder.Configuration.GetSection("KeyVault"));
builder.Services.Configure<ServiceBusSetting>(builder.Configuration.GetSection("ServiceBus"));
builder.Services.AddAutoMapper(typeof(Program));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    //Tobe removed once done with demo
    c.OperationFilter<CustomHeaderSwaggerAttribute>();
});

var keyVaultUrl = new Uri(builder.Configuration.GetSection("KeyVault:VaultUri").Value);
var secretKey = builder.Configuration.GetSection("KeyVault:DbSecretKey").Value;
var secretClient = new SecretClient(keyVaultUrl, new DefaultAzureCredential());
var mongoConnectionString = secretClient.GetSecret(secretKey).Value.Value;

var serviceBusKey = builder.Configuration.GetSection("KeyVault:ServiceBusKey").Value;
var serviceBusConnectionString = secretClient.GetSecret(serviceBusKey).Value.Value;

var signalRKey = builder.Configuration.GetSection("KeyVault:SignalRKey").Value;
var signalRConnectionString = secretClient.GetSecret(signalRKey).Value.Value;

var redisKey = builder.Configuration.GetSection("KeyVault:RedisKey").Value;
var redisConnectionString = secretClient.GetSecret(redisKey).Value.Value;

var instrumentationKey = builder.Configuration.GetSection("KeyVault:ApplicationInsights").Value;
var instrumentationKeyValue = secretClient.GetSecret(instrumentationKey).Value.Value;

//Add Mongodb setting for SSL enabled connection 
MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(mongoConnectionString));
settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
var mongoClient = new MongoClient(settings);

builder.Services.AddSingleton<IMongoClient, MongoClient>(s => new MongoClient(settings));

builder.Services.AddAzureClients(azureBuilder =>
{
    azureBuilder.AddSecretClient(builder.Configuration.GetSection("KeyVault"));
    azureBuilder.AddServiceBusClient(serviceBusConnectionString);
});

builder.Services.AddSignalR().AddAzureSignalR(option =>
{
    option.ConnectionString = signalRConnectionString;
});


//builder.Services.AddSignalR();
//builder.Services.AddSignalRCore();

builder.Services.AddCors(cors => {
    cors.AddPolicy("CorsPolicy",builder => builder
    .WithOrigins("http://localhost:4200/")
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
    .SetIsOriginAllowed((host) => true));
});

builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnectionString));

builder.Services.AddServicesExtension();
builder.Services.AddApplicationServicesExtension();

builder.Services.AddApplicationInsightsTelemetry(instrumentationKeyValue);
builder.Services.AddApplicationInsightsTelemetryProcessor<CustomTelemetryProcessor>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.UseApplicationInsightsRequestTelemetry();
app.UseApplicationInsightsExceptionTelemetry();

// Use the custom rate limiting and throttling middleware before SignalR
//app.UseMiddleware<RateLimitingMiddleware>();

app.MapHub<GameHub>("/gamehub", option => {    
    option.Transports = HttpTransportType.WebSockets | HttpTransportType.LongPolling | HttpTransportType.ServerSentEvents;
});

app.Run();
