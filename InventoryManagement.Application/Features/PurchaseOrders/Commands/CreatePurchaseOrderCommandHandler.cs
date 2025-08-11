using Mediator;
using InventoryManagment.DomainModels.Entites;
using InventoryManagment.DomainModels.Repositories;
using Microsoft.Extensions.Logging;

namespace InventoryManagement.Application.Features.PurchaseOrders.Commands
{
    public record CreatePurchaseOrderCommand(Guid UserId, List<int> ProductIds, float TotalAmount) : ICommand<Guid>;

    public class CreatePurchaseOrderCommandHandler : ICommandHandler<CreatePurchaseOrderCommand, Guid>
    {
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;
        private readonly ILogger<CreatePurchaseOrderCommandHandler> _logger;
        private readonly TimeProvider _timeProvider;

        public CreatePurchaseOrderCommandHandler(IPurchaseOrderRepository purchaseOrderRepository, ILogger<CreatePurchaseOrderCommandHandler> logger, TimeProvider timeProvider)
        {
            _purchaseOrderRepository = purchaseOrderRepository ?? throw new ArgumentNullException(nameof(purchaseOrderRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _timeProvider = timeProvider ?? throw new ArgumentNullException(nameof(timeProvider));
        }

        public async ValueTask<Guid> Handle(CreatePurchaseOrderCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Creating purchase order for user {command.UserId}");
            var order = new PurchaseOrder
            {
                UserId = command.UserId,
                ProductIds = command.ProductIds,
                TotalAmount = command.TotalAmount,
                OrderDate = _timeProvider.GetUtcNow(),
                ExpectedDate = _timeProvider.GetUtcNow().AddDays(3),
            };
            await _purchaseOrderRepository.AddAsync(order);
            _logger.LogInformation($"Purchase order created with id {order.Id}");
            return order.Id;
        }
    }
}
