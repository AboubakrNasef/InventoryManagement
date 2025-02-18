using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Infrastructure.Messaging.TopicMessages
{
    public record UpdateRedisTopicMessage(int ProductId, ProductAction ProductAction);
    public enum ProductAction
    {
        Add,
        Update,
        Delete
    }
}


