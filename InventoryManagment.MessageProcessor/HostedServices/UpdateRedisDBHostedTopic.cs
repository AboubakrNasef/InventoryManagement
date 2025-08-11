using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using InventoryManagement.Application.HostedServices;
using InventoryManagement.Application.RedisSearch;
using InventoryManagement.Infrastructure.HostedServices;
using InventoryManagement.Infrastructure.Messaging.TopicMessages;
using InventoryManagment.DomainModels.Repositories;

namespace InventoryManagment.MessageProcessor.HostedServices
{
    public class UpdateRedisDBHostedTopic : TopicHostedServiceBase<UpdateRedisTopicMessage>
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductSearchRepository _productSearchRepository;
        public UpdateRedisDBHostedTopic(
            string subscriptionName,
            string topicName,
            ServiceBusClient serviceBusClient,
            ILogger<HostedServiceBase<UpdateRedisTopicMessage>> logger,
            IProductRepository productRepository,
            IProductSearchRepository productSearchRepository)
            : base(subscriptionName, topicName, serviceBusClient, logger)
        {
            _productRepository = productRepository;
            _productSearchRepository = productSearchRepository;
        }

        protected async override Task ProcessMessageAsync(ProcessMessageEventArgs args)
        {
            _logger.LogInformation("ProcessMessageAsync {message}", args.Message);
            var message = args.Message;
            var topicMessage = message.Body.ToObjectFromJson<UpdateRedisTopicMessage>();
            switch (topicMessage.ProductAction)
            {
                case ProductAction.Add:
                    await HandleAdd(topicMessage.ProductId);
                    break;
                case ProductAction.Update:
                    await HandleUpdate(topicMessage.ProductId);
                    break;
                case ProductAction.Delete:
                    await HandleDelete(topicMessage.ProductId);
                    break;
                default:
                    break;
            }
        }

        private async Task HandleDelete(Guid productId)
        {
            _logger.LogInformation("Deleting {id}", productId);
            await _productSearchRepository.DeleteAsync(productId);
        }

        private async Task HandleUpdate(Guid productId)
        {
            _logger.LogInformation("updating product {id}", productId);
            var productSearch = await _productRepository.GetSearchModelByIdAsync(productId);
            await _productSearchRepository.UpdateAsync(productSearch);
        }

        private async Task HandleAdd(Guid productId)
        {
            _logger.LogInformation("Adding product {id}", productId);

            var productSearch = await _productRepository.GetSearchModelByIdAsync(productId);

            await _productSearchRepository.AddAsync(productSearch);
        }
    }
}
