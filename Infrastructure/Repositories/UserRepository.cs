using Domain;
using GenericRepository;
using Infrastructure.Context;

namespace Infrastructure.Repositories;

internal sealed class UserRepository : Repository<User, ApplicationDbContext>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }
}