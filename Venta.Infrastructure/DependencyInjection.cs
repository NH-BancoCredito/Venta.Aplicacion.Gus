using Microsoft.Extensions.DependencyInjection;
using Venta.Domain.Repositories;
using Venta.Infrastructure.Repositories;

namespace Venta.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfraestructure(this IServiceCollection services)
    {

        services.AddRepositories();

    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IProductoRepository, ProductoRepository>();
        services.AddScoped<IVentaRepository, VentaRepository>();
    }
}