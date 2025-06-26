using Application;
using Core;
using Infrastructure;

namespace API;

public static class DependencyInjection
{
    public static IServiceCollection AddAppDI(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplicationDi()
            .AddInfrastructureDi()
            .AddCoreDi(configuration);
        
        return services;
    }
}