using InventoryManagment.DomainModels.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Infrastructure.Messaging
{
    internal class MessageBus
    {
        public class ServiceBusClient : IMessageBus
        {

            private readonly ServiceBusSender _queueSender;
            private readonly ServiceBusSender _topicSender;

            public ServiceBusClient(string connectionString, string queueName, string topicName)
            {
                _client = new ServiceBusClient(connectionString);
                _queueSender = _client.CreateSender(queueName);
                _topicSender = _client.CreateSender(topicName);
            }

            public async Task SendToQueueAsync(string message)
            {
                var serviceBusMessage = new ServiceBusMessage(message);
                await _queueSender.SendMessageAsync(serviceBusMessage);
            }

            public async Task SendToTopicAsync(string message)
            {
                var serviceBusMessage = new ServiceBusMessage(message);
                await _topicSender.SendMessageAsync(serviceBusMessage);
            }

            public async Task DisposeAsync()
            {
                await _queueSender.DisposeAsync();
                await _topicSender.DisposeAsync();
                await _client.DisposeAsync();
            }
        }
    }
}
