using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.HostedServices
{
    public abstract class HostedServiceBase<T> : IHostedService
    {
        protected readonly ILogger<HostedServiceBase<T>> _logger;
        protected ServiceBusProcessor _serviceBusProcessor;
        protected string _topicName;
        protected HostedServiceBase(ILogger<HostedServiceBase<T>> logger,string topicName)
        {
            _topicName = topicName;
            _logger = logger;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var _ = _logger.BeginScope("ServiceName: {serviceName}", GetType().Name);
            _serviceBusProcessor = await CreateServiceBusProcessorAsync();
            _serviceBusProcessor.ProcessMessageAsync += ProcessMessageAsync;
            _serviceBusProcessor.ProcessErrorAsync += MessageFailedAsync;
            await _serviceBusProcessor.StartProcessingAsync(cancellationToken);
        }

        protected abstract Task ProcessMessageAsync(ProcessMessageEventArgs args);
        protected abstract Task<ServiceBusProcessor> CreateServiceBusProcessorAsync();
        private Task MessageFailedAsync(ProcessErrorEventArgs args)
        {
            _logger.LogError(args.Exception, "Failed to process message with error: {ErrorMessage}", args.Exception.Message);
            return Task.CompletedTask;
        }
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("stopping {serviceName} hosted service...", GetType().Name);
            await _serviceBusProcessor.CloseAsync(cancellationToken);
        }
    }
}
