using Application;
using Application.Users;
using Infrastructure;
using TS.MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddApplication();
builder.Services.AddInfrastructure();
builder.Services.AddFluentEmail("info@dev.com")
    .AddSmtpSender("localhost", 25);

var app = builder.Build();

app.MapDefaultEndpoints();

app.MapPost("/user-create", async (UserCreateCommand request, ISender sender, CancellationToken cancellationToken) =>
{
    var res = await sender.Send(request, cancellationToken);
    return Results.Ok(res);
});

app.Run();
