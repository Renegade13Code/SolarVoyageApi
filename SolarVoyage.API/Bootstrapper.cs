using AutoMapper;
using SolarVoyage.API.AutoMapper;

namespace SolarVoyage.API;

public static class Bootstrapper
{
    public static IServiceCollection UseApiServices(this IServiceCollection services)
    {
        return services.AddSingleton<Profile, ApiProfile>();
    }
}