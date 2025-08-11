using Azure.Messaging.ServiceBus;
using InventoryManagement.Application.HostedServices;
using Microsoft.Extensions.Logging;


namespace InventoryManagement.Infrastructure.HostedServices
{
    public abstract class TopicHostedServiceBase<T> : HostedServiceBase<T>
    {
        protected readonly string _subscriptionName;
        private readonly ServiceBusClient _serviceBusClient;

        public TopicHostedServiceBase(
            string subscriptionName,
            string topicName,
            ServiceBusClient serviceBusClient,
             ILogger<HostedServiceBase<T>> logger)
            : base(logger, topicName)
        {

            _subscriptionName = subscriptionName;
            _serviceBusClient = serviceBusClient;
        }

        protected async override Task<ServiceBusProcessor> CreateServiceBusProcessorAsync()
        {

            var serviceBusProcessorOptions = new ServiceBusProcessorOptions()
            {
                MaxConcurrentCalls = 10,
            };

            return await Task.FromResult(_serviceBusClient.CreateProcessor(_topicName, _subscriptionName, serviceBusProcessorOptions));
        }
    }
}
