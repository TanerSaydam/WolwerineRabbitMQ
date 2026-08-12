using Application.Users;
using Domain;
using Infrastructure;
using JasperFx.CodeGeneration.Model;
using Wolverine;
using Wolverine.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddInfrastructure();
builder.Services
    .AddFluentEmail("info@dev.com")
    .AddSmtpSender("localhost", 25);

builder.Host.UseWolverine(x =>
{
    x.Discovery
    .IncludeAssembly(typeof(UserCreateCommand).Assembly)
    .IncludeAssembly(typeof(User).Assembly);
    x.ServiceLocationPolicy = ServiceLocationPolicy.AllowedButWarn;

    var rabbitMQCon = builder.Configuration.GetConnectionString("rabbitmq");
    x.UseRabbitMq(rabbitMQCon!).AutoProvision();

    x.PublishMessage<UserRegistered>().ToRabbitQueue("user-create");
    x.ListenToRabbitQueue("user-create");
});

var app = builder.Build();

app.MapDefaultEndpoints();

app.MapPost("/user-create", async (
    UserCreateCommand request,
    IMessageBus messageBus,
    CancellationToken cancellationToken) =>
{
    var res = await messageBus.InvokeAsync<Guid>(request, cancellationToken);
    return Results.Ok(res);
});

app.Run();
