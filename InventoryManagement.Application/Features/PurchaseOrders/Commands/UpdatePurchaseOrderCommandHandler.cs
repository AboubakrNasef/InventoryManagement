using InventoryManagment.DomainModels.Repositories;
using Mediator;
using Microsoft.Extensions.Logging;

namespace InventoryManagement.Application.Features.PurchaseOrders.Commands
{
    public record UpdatePurchaseOrderCommand(Guid Id, Guid UserId, List<int> ProductIds, float TotalAmount) : ICommand<bool>;

    public class UpdatePurchaseOrderCommandHandler : ICommandHandler<UpdatePurchaseOrderCommand, bool>
    {
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;
        private readonly ILogger<UpdatePurchaseOrderCommandHandler> _logger;

        public UpdatePurchaseOrderCommandHandler(IPurchaseOrderRepository purchaseOrderRepository, ILogger<UpdatePurchaseOrderCommandHandler> logger)
        {
            _purchaseOrderRepository = purchaseOrderRepository ?? throw new ArgumentNullException(nameof(purchaseOrderRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async ValueTask<bool> Handle(UpdatePurchaseOrderCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Updating purchase order: {command.Id}");
            var order = await _purchaseOrderRepository.GetByIdAsync(command.Id);
            if (order == null) return false;
            order.UserId = command.UserId;
            order.ProductIds = command.ProductIds;
            order.TotalAmount = command.TotalAmount;
            var result = await _purchaseOrderRepository.UpdateAsync(order);
            return result == 1;
        }
    }
}
