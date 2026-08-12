var builder = DistributedApplication.CreateBuilder(args);

var userName = builder.AddParameter("rabbitmq-username", "admin");
var password = builder.AddParameter("rabbitmq-password", "1234", secret: true);

var rabbitMQ = builder.AddRabbitMQ("rabbitmq", userName, password)
    .WithManagementPlugin()
    .WithLifetime(ContainerLifetime.Persistent);

builder.AddProject<Projects.WebAPI>("webapi")
    .WithReference(rabbitMQ)
    .WaitFor(rabbitMQ);

builder.Build().Run();
