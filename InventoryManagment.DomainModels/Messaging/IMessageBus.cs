using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagment.DomainModels.Messaging
{
    public interface IMessageBus
    {
        Task SendTopicMessageAsync(string message);
        Task SendQueueMessageAsync(string message);
    }
}
