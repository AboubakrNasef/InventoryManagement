using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagment.DomainModels.Messaging
{
    public interface IMessageBus
    {
        Task SendToQueueAsync<T>(string queueName, T message);
        Task SendToTopicAsync<T>(string topicName, T message);

    }
}
