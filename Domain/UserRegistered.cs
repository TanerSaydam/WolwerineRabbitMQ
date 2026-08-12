using FluentEmail.Core;

namespace Domain;

public sealed record UserRegistered(
    string Email);

public sealed class UserRegisteredHandler
{
    public static async Task Handle(UserRegistered request, IFluentEmail fluentEmail)
    {
        var res = await fluentEmail
            .To(request.Email)
            .Subject("Test")
            .Body("Bu bir test mesajıdır")
            .SendAsync();
    }
}