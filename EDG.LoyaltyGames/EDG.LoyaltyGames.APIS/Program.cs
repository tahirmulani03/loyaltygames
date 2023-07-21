using EDG.LoyaltyGames.APIS.Extensions;
using EDG.LoyaltyGames.Core.Entites;
using Microsoft.Extensions.Azure;
using MongoDB.Driver.Core.Operations;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration; // allows both to access and to set up the config
IWebHostEnvironment environment = builder.Environment;

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.Configure<KeyVaultSettings>(builder.Configuration.GetSection("KeyVault"));
builder.Services.AddAutoMapper(typeof(Program));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAzureClients(AzureClientFactoryBuilder =>
{
    AzureClientFactoryBuilder.AddSecretClient(configuration.GetSection("KeyVault"));
});

builder.Services.AddApplicationServicesExtension();

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
