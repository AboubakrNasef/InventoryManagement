using InventoryManagement.Application.Common;
using InventoryManagment.DomainModels.Entites;
using InventoryManagment.DomainModels.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace InventoryManagement.Application.Features.PurchaseOrders.Commands
{
    public record CreatePurchaseOrderCommand(int UserId, List<int> ProductIds, float TotalAmount) : ICommand<int>;

    public class CreatePurchaseOrderCommandHandler : ICommandHandler<CreatePurchaseOrderCommand, int>
    {
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;
        private readonly ILogger<CreatePurchaseOrderCommandHandler> _logger;

        public CreatePurchaseOrderCommandHandler(IPurchaseOrderRepository purchaseOrderRepository, ILogger<CreatePurchaseOrderCommandHandler> logger)
        {
            _purchaseOrderRepository = purchaseOrderRepository ?? throw new ArgumentNullException(nameof(purchaseOrderRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<int> Handle(CreatePurchaseOrderCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Creating purchase order for user {command.UserId}");
            var order = new PurchaseOrder
            {
                UserId = command.UserId,
                ProductIds = command.ProductIds,
                TotalAmount = command.TotalAmount,
                OrderDate = DateTime.UtcNow
            };
            await _purchaseOrderRepository.AddAsync(order);
            _logger.LogInformation($"Purchase order created with id {order.Id}");
            return order.Id;
        }
    }
}
