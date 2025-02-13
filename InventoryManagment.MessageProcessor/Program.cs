using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using InventoryManagement.Application.HostedServices;
using InventoryManagement.Infrastructure.Extensions;
using InventoryManagement.Infrastructure.Messaging.TopicMessages;
using InventoryManagment.DomainModels.Interfaces;
using InventoryManagment.MessageProcessor.HostedServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Logging.AddConsole();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfraStructure(builder.Configuration);

builder.Services.AddHostedService<UpdateRedisDBHostedTopic>(c =>
{
    var topicName = builder.Configuration.GetSection("ServiceBus:TopicName").Value;
    var subscriptionName = builder.Configuration.GetSection("ServiceBus:SubscriptionName").Value;
    return new UpdateRedisDBHostedTopic(
        subscriptionName,
        topicName,
        c.GetRequiredService<ServiceBusClient>(),
        c.GetRequiredService<ILogger<HostedServiceBase<UpdateRedisTopicMessage>>>(),
        c.GetRequiredService<IProductRepository>(),
        c.GetRequiredService<IProductSearchRepository>());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
