using Microsoft.Extensions.DependencyInjection;
using TS.MediatR;

namespace Application;

public static class ServiceRegistrar
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfr =>
        {
            cfr.RegisterServicesFromAssembly(typeof(ServiceRegistrar).Assembly);
        });
    }
}