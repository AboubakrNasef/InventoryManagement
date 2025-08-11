using InventoryManagment.DomainModels.Entites;
using InventoryManagment.DomainModels.Repositories;
using MongoDB.Driver;

namespace InventoryManagement.Infrastructure.Repositories.Mongo
{
    public class MongoCategoryRepository : MongoBaseRepository<Category>, ICategoryRepository
    {
        public MongoCategoryRepository(IMongoDatabase database,IIDCreator iDCreator) : base(database, iDCreator)
        {
        }
    }
}
