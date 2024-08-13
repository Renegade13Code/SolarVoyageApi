using System.Reflection;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SolarVoyage.API;
using SolarVoyage.Aws.Cognito.Service;
using SolarVoyage.Core;
using AutoMapper;

//Setup API config file
var configFileBasePath = Environment.GetEnvironmentVariable("CONFIG_BASEPATH");

var configBuilder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true);

if (!string.IsNullOrEmpty(configFileBasePath))
    configBuilder.SetBasePath(configFileBasePath);

IConfiguration configuration = configBuilder.Build();

var builder = WebApplication.CreateBuilder(args);

//Dont need .AddAuthorization with this method?
builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add API core and services
//This should be registered before authentication setup for some reason?
builder.Services.UseAwsCognitoService(configuration);
builder.Services.UseApiCore(configuration);
builder.Services.UseApiServices();

builder.Services.AddAutoMapper((provider, expression) =>
{
    expression.AddProfiles(provider.GetServices<Profile>());
}, new List<Assembly>());

builder.Services.Configure<JsonSerializerOptions>(options =>
{
    options.PropertyNameCaseInsensitive = true;
});

//Setup cognito authentication
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.Authority = configuration.GetSection("Cognito:Authority").Value;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidIssuer = configuration.GetSection("Cognito:Authority").Value,
        };

    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();