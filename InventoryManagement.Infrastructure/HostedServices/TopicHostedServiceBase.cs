using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using InventoryManagement.Application.HostedServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Infrastructure.HostedServices
{
    public abstract class TopicHostedServiceBase<T> : HostedServiceBase<T>
    {
        private readonly ServiceBusAdministrationClient _serviceBusAdmin;
        protected readonly string _subscriptionName;
        private readonly ServiceBusClient _serviceBusClient;
        public TopicHostedServiceBase(ServiceBusAdministrationClient serviceBusAdmin,
            string subscriptionName,
            ServiceBusClient serviceBusClient,
            ServiceBusProcessor serviceBusProcessor, ILogger<HostedServiceBase<T>> logger)
            : base(serviceBusProcessor, logger)
        {
            _serviceBusAdmin = serviceBusAdmin;
            _subscriptionName = subscriptionName;
            _serviceBusClient = serviceBusClient;
        }

        protected async override Task<ServiceBusProcessor> CreateServiceBusProcessorAsync()
        {

            if (await _serviceBusAdmin.SubscriptionExistsAsync(_serviceName, _subscriptionName))
            {
                await _serviceBusAdmin.CreateTopicAsync(_serviceName);
            }

            var subscriptionDiscriptor = new CreateSubscriptionOptions(_serviceName, _subscriptionName);
            await _serviceBusAdmin.CreateSubscriptionAsync(subscriptionDiscriptor);

            var serviceBusProcessorOptions = new ServiceBusProcessorOptions()
            {
                MaxConcurrentCalls = 10,
            };

            return _serviceBusClient.CreateProcessor(_serviceName, _subscriptionName, serviceBusProcessorOptions);
        }
    }
}
