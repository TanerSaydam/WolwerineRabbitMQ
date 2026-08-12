using Domain;
using GenericRepository;
using TS.MediatR;

namespace Application.Users;

public sealed record UserCreateCommand(
    string FirstName,
    string LastName,
    string Email) : IRequest<Guid>;

internal sealed class UserCreateCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<UserCreateCommand, Guid>
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