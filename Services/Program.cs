using App.Persistance.Extentions;
using Services;

var builder = Host.CreateApplicationBuilder(args);

builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Debug);

builder.Services.AddHostedService<Worker>();

builder.Services.AddInfrastructure(builder.Configuration);

var host = builder.Build();
host.Run();