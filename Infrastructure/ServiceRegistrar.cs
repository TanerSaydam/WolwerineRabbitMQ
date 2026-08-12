using Domain;
using GenericRepository;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ServiceRegistrar
{
    public static void AddInfrastructure(this IServiceCollection serivces)
    {
        serivces.AddDbContext<ApplicationDbContext>(opt =>
        {
            opt.UseInMemoryDatabase("MyDb");
        });
        serivces.AddScoped<IUnitOfWork>(srv => srv.GetRequiredService<ApplicationDbContext>());
        serivces.AddScoped<IUserRepository, UserRepository>();
    }
}