using InventoryManagement.Infrastructure.Extensions;
using InventoryManagment.MessageProcessor.Extensions;
using InventoryManagement.OpenTelemetry;

const string serviceName = "Inventory.MessageProcessor";

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddOpenTelemtryLogging(serviceName);


builder.Services.AddOpenTelemtryTracing(serviceName);
builder.Services.AddOpenTelemtryMetrics(serviceName);
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


