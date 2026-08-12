using Domain;
using GenericRepository;

namespace Application.Users;

public sealed record UserCreateCommand(
    string FirstName,
    string LastName,
    string Email);

public sealed class UserCreateCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork)
{
    public async Task<Guid> Handle(UserCreateCommand request, CancellationToken cancellationToken)
    {
        var user = new User()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email
        };

        userRepository.Add(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}