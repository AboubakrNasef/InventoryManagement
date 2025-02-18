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

            return await Task.FromResult( _serviceBusClient.CreateProcessor(_topicName, _subscriptionName, serviceBusProcessorOptions));
        }
    }
}
