using Azure.Messaging.ServiceBus;
using InventoryManagement.Application.HostedServices;
using InventoryManagement.Infrastructure.Messaging.TopicMessages;
using InventoryManagment.DomainModels.Repositories;
using InventoryManagment.MessageProcessor.HostedServices;
using System;

namespace InventoryManagment.MessageProcessor.Extensions
{
    public static class MessageProcessingServices
    {
        public static void AddMessageProcessingServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHostedService<UpdateRedisDBHostedTopic>(c =>
            {
                var topicName = configuration.GetSection("ServiceBus:TopicName").Value;
                var subscriptionName = configuration.GetSection("ServiceBus:SubscriptionName").Value;
                return new UpdateRedisDBHostedTopic(
                    subscriptionName,
                    topicName,
                    c.GetRequiredService<ServiceBusClient>(),
                    c.GetRequiredService<ILogger<HostedServiceBase<UpdateRedisTopicMessage>>>(),
                    c.GetRequiredService<IProductRepository>(),
                    c.GetRequiredService<IProductSearchRepository>());
            });

        }
    }
}
