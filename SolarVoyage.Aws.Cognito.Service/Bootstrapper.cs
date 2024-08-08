using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SolarVoyage.Aws.Cognito.Service.AutoMapper;
using SolarVoyage.Aws.Cognito.Service.Models;
using SolarVoyage.Aws.Cognito.Service.Services;
using SolarVoyage.Core.Interfaces.UserAuth;
using SolarVoyage.Core.Interfaces.Users;

namespace SolarVoyage.Aws.Cognito.Service;

public static class Bootstrapper
{
    public static IServiceCollection UseAwsCognitoService(this IServiceCollection services, IConfiguration configuration)
    {
        //Add AWS Cognito Services
        services.AddCognitoIdentity();

        //Register operations in DI container
        services.Configure<AwsCognitoUserPoolOptions>(configuration.GetSection(AwsCognitoUserPoolOptions.Section));
        
        return services
            .AddSingleton<Profile, AwsCognitoServiceProfile>()
            .AddScoped<IExternalUserService, AwsCognitoUserService>()
            .AddScoped<IExternalAuthService, AwsCognitoUserAuthService>();
    }
}