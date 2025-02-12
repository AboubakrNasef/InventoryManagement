using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using InventoryManagement.Application.HostedServices;
using InventoryManagement.Application.RedisSearch;
using InventoryManagement.Infrastructure.HostedServices;
using InventoryManagement.Infrastructure.Messaging.TopicMessages;
using InventoryManagment.DomainModels.Interfaces;

namespace InventoryManagment.MessageProcessor.HostedServices
{
    public class UpdateRedisDBHostedTopic : TopicHostedServiceBase<UpdateRedisTopicMessage>
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductSearchRepository _productSearchRepository;
        public UpdateRedisDBHostedTopic(ServiceBusAdministrationClient serviceBusAdmin,
            string subscriptionName,
            ServiceBusClient serviceBusClient,
            ILogger<HostedServiceBase<UpdateRedisTopicMessage>> logger,
            IProductRepository productRepository,
            IProductSearchRepository productSearchRepository)
            : base(serviceBusAdmin, subscriptionName, serviceBusClient, logger)
        {
            _productRepository = productRepository;
            _productSearchRepository = productSearchRepository;
        }

        protected async override Task ProcessMessageAsync(ProcessMessageEventArgs args)
        {
            var message = args.Message;
            var messageBody = message.Body.ToObjectFromJson<UpdateRedisTopicMessage>();
            var product = await _productRepository.GetByIdAsync(messageBody.ProductId);
            var productSearch = new ProductSearchModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
            };
            await _productSearchRepository.UpdateAsync(productSearch);

        }
    }
}
