using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Ventas.Application;

public static class DependencyInjection
{
    public static void AddAplication(this IServiceCollection services)
    {

        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR( c => c.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

    }
}