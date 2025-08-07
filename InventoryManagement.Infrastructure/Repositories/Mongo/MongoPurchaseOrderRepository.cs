using InventoryManagment.DomainModels.Entites;
using InventoryManagment.DomainModels.Interfaces;
using MongoDB.Driver;

namespace InventoryManagement.Infrastructure.Repositories.Mongo
{
    public class MongoPurchaseOrderRepository : MongoBaseRepository<PurchaseOrder>, IPurchaseOrderRepository
    {
        public MongoPurchaseOrderRepository(IMongoDatabase database) : base(database)
        {
        }
    }
}
