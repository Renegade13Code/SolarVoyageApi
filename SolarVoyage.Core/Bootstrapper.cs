using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SolarVoyage.Core.Services.User;
using SolarVoyage.Core.Services.UserAuth;

namespace SolarVoyage.Core;

public static class Bootstrapper
{
    public static IServiceCollection UseApiCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserAuthService, UserAuthService>();

        //Setup db
        // services.AddDbContext<UserDbContext>(options =>
        // {
        //     options.UseNpgsql(configuration.GetConnectionString("ConnectionStrings.StickerFactory"), b => b.MigrationsAssembly("StickerFactory.API.Core"));
        // });
        
        return services;
    }
}