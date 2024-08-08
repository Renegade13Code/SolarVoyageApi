using Amazon.AspNetCore.Identity.Cognito.Extensions;
using Amazon.CognitoIdentityProvider;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StickerFactory.API.AWS.Cognito.Service.AutoMapper;
using StickerFactory.API.AWS.Cognito.Service.Models;
using StickerFactory.API.AWS.Cognito.Service.Services;
using StickerFactory.API.Core.Interfaces.UserAuth;
using StickerFactory.API.Core.Interfaces.Users;

namespace StickerFactory.API.AWS.Cognito.Service;

public static class Bootstrapper
{
    public static IServiceCollection AddAwsCognitoService(this IServiceCollection services, IConfiguration configuration)
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