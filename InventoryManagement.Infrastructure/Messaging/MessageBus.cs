using Azure.Messaging.ServiceBus;
using InventoryManagment.DomainModels.Messaging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace InventoryManagement.Infrastructure.Messaging
{

    public class MessageBus : IMessageBus
    {
        private readonly ConcurrentDictionary<string, ServiceBusSender> _senders;
        private readonly ServiceBusClient _serviceBusClient;

        public MessageBus(ServiceBusClient serviceBusClient)
        {
            _senders = new();
            _serviceBusClient = serviceBusClient;
        }

        public async Task SendToQueueAsync<T>(string queueName, T message)
        {
            await SendMessageAsync(message, queueName);
        }

        public async Task SendToTopicAsync<T>(string topicName, T message)
        {
            await SendMessageAsync(message, topicName);
        }

        private async Task SendMessageAsync<T>(T message, string channelName)
        {
            if (!_senders.TryGetValue(channelName, out var sender))
            {
                sender = _serviceBusClient.CreateSender(channelName);
                _senders.TryAdd(channelName, sender);
            }

            var serializedMessage = JsonSerializer.Serialize(message);
            var serviceBusMessage = new ServiceBusMessage(serializedMessage);

            await sender.SendMessageAsync(serviceBusMessage);
        }
    }

}
