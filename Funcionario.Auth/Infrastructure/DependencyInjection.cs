using Core.Interfaces;
using Core.Options;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureDi(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>((provider, options) =>
        {
            options.UseNpgsql(provider.GetRequiredService<IOptionsSnapshot<ConnectionStringOptions>>().Value.DefaultConnection);
        });

        services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();        

        return services;
    }
}