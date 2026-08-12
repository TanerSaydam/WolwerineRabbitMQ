using Application.Users;
using Infrastructure;
using JasperFx.CodeGeneration.Model;
using Wolverine;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddInfrastructure();
builder.Services.AddFluentEmail("info@dev.com")
    .AddSmtpSender("localhost", 25);

builder.Host.UseWolverine(x =>
{
    x.Discovery.IncludeAssembly(typeof(UserCreateCommand).Assembly);
    x.ServiceLocationPolicy = ServiceLocationPolicy.AllowedButWarn;
});

var app = builder.Build();

app.MapDefaultEndpoints();

app.MapPost("/user-create", async (UserCreateCommand request, IMessageBus messageBus, CancellationToken cancellationToken) =>
{
    var res = await messageBus.InvokeAsync<Guid>(request, cancellationToken);
    return Results.Ok(res);
});

app.Run();
