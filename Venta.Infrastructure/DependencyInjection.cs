using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Venta.Domain.Repositories;
using Venta.Infrastructure.Repositories;
using Venta.Infrastructure.Repositories.Base;

namespace Venta.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfraestructure(this IServiceCollection services,string connectionString)
    {

        services.AddDbContext<VentaDbContext>(
            options=>options.UseSqlServer(connectionString)
        );

  
        services.AddRepositories();

    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IProductoRepository, ProductoRepository>();
        services.AddScoped<IVentaRepository, VentaRepository>();
    }
}