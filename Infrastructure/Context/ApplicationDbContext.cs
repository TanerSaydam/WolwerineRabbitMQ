using Domain;
using GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public sealed class ApplicationDbContext : DbContext, IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
}