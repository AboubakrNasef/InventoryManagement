using InventoryManagment.DomainModels.Entites;
using InventoryManagment.DomainModels.Interfaces;
using MongoDB.Driver;

namespace InventoryManagement.Infrastructure.Repositories.Mongo
{
    public class MongoCategoryRepository : MongoBaseRepository<Category>, ICategoryRepository
    {
        public MongoCategoryRepository(IMongoDatabase database) : base(database)
        {
        }
    }
}
