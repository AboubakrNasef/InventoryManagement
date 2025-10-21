using Azure.Messaging.ServiceBus;
using InventoryManagement.Application.HostedServices;
using InventoryManagement.Infrastructure.Extensions;
using InventoryManagement.Infrastructure.HostedServices;
using InventoryManagement.Infrastructure.Messaging.TopicMessages;
using InventoryManagment.DomainModels.Repositories;
using InventoryManagment.MessageProcessor.Extensions;
using InventoryManagment.MessageProcessor.HostedServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Logging.AddConsole();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
builder.Services.AddInfraStructure(builder.Configuration);
builder.Services.AddMessageProcessingServices(builder.Configuration);


var app = builder.Build();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();


