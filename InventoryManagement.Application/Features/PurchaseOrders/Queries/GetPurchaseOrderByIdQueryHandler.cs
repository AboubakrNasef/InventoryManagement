using InventoryManagement.Application.Common;
using InventoryManagment.DomainModels.Entites;
using InventoryManagment.DomainModels.Repositories;
using Mediator;
using Microsoft.Extensions.Logging;

namespace InventoryManagement.Application.Features.PurchaseOrders.Queries
{
    public record GetPurchaseOrderByIdQuery(int Id) : IQuery<PurchaseOrder>;

    public class GetPurchaseOrderByIdQueryHandler : IQueryHandler<GetPurchaseOrderByIdQuery, PurchaseOrder>
    {
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;
        private readonly ILogger<GetPurchaseOrderByIdQueryHandler> _logger;

        public GetPurchaseOrderByIdQueryHandler(IPurchaseOrderRepository purchaseOrderRepository, ILogger<GetPurchaseOrderByIdQueryHandler> logger)
        {
            _purchaseOrderRepository = purchaseOrderRepository ?? throw new ArgumentNullException(nameof(purchaseOrderRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<PurchaseOrder> Handle(GetPurchaseOrderByIdQuery query, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Getting purchase order by id: {query.Id}");
            var order = await _purchaseOrderRepository.GetByIdAsync(query.Id);
            return order;
        }
    }
}
