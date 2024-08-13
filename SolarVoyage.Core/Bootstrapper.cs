using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SolarVoyage.Core.Data;
using SolarVoyage.Core.Services.Ships;
using SolarVoyage.Core.Services.User;
using SolarVoyage.Core.Services.UserAuth;

namespace SolarVoyage.Core;

public static class Bootstrapper
{
    public static IServiceCollection UseApiCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserAuthService, UserAuthService>();
        services.AddScoped<IShipService, ShipService>();
        
        //Register the dbContext as a dependency for dependency injection
        services.AddDbContext<SolarVoyageContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("ConnectionStrings:SolarVoyageDb"));
        });
        
        return services;
    }
}