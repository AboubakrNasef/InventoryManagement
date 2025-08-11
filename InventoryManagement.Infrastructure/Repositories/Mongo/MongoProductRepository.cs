using InventoryManagement.Application.RedisSearch;
using InventoryManagment.DomainModels.Entites;
using InventoryManagment.DomainModels.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Infrastructure.Repositories.Mongo
{
    public class MongoProductRepository : MongoBaseRepository<Product>, IProductRepository
    {
        public MongoProductRepository(IMongoDatabase database, IIDCreator iDCreator) : base(database, iDCreator)
        {

        }

        public async Task<ProductSearchModel> GetSearchModelByIdAsync(Guid productId)
        {
            return await _collection
                .Find(p => p.Id == productId)
                .As<ProductSearchModel>()
                .FirstOrDefaultAsync();
        }
    }
}
