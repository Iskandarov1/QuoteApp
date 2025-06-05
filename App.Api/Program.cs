using App.Application.Abstractions.Messaging;
using App.Application.Quotes.Commands.RemoveSubscription;
using App.Application.Quotes.Queries.GetAllQuote;
using App.Domain.Entities;
using App.Domain.Entities.Subscribe;
using App.Infrastructure.Services;
using App.Persistance.Extentions;
using App.Persistance.Repositories;
using Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(); 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<ISubscriberRepository, SubscriberRepository>();
builder.Services.AddScoped<IEmailService, MockEmailService>();
builder.Services.AddScoped<ISmsService, MockSmsService>();

builder.Services.AddHostedService<Worker>();
builder.Services.AddHostedService<DailyQuoteNotificationWorker>();



builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(GetAllQuotesQuery).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(RemoveSubscriptionCommandHandler).Assembly);

});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
