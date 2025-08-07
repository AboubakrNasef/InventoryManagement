using InventoryManagement.Application.Common;
using InventoryManagment.DomainModels.Entites;
using InventoryManagment.DomainModels.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace InventoryManagement.Application.Features.PurchaseOrders.Queries
{
    public record GetAllPurchaseOrdersQuery() : IQuery<List<PurchaseOrder>>;

    public class GetAllPurchaseOrdersQueryHandler : IQueryHandler<GetAllPurchaseOrdersQuery, List<PurchaseOrder>>
    {
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;
        private readonly ILogger<GetAllPurchaseOrdersQueryHandler> _logger;

        public GetAllPurchaseOrdersQueryHandler(IPurchaseOrderRepository purchaseOrderRepository, ILogger<GetAllPurchaseOrdersQueryHandler> logger)
        {
            _purchaseOrderRepository = purchaseOrderRepository ?? throw new ArgumentNullException(nameof(purchaseOrderRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<List<PurchaseOrder>> Handle(GetAllPurchaseOrdersQuery query, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Getting all purchase orders");
            var orders = await _purchaseOrderRepository.GetAllAsync();
            return orders.ToList();
        }
    }
}
