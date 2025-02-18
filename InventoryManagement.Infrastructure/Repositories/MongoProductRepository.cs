using InventoryManagement.Application.RedisSearch;
using InventoryManagment.DomainModels.Entites;
using InventoryManagment.DomainModels.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Infrastructure.Repositories
{
    public class MongoProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _productsCollection;

        public MongoProductRepository(IMongoDatabase database)
        {
            _productsCollection = database.GetCollection<Product>("Products");
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _productsCollection.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _productsCollection.Find(_ => true).ToListAsync();
        }

        public async Task AddAsync(Product product)
        {
            await _productsCollection.InsertOneAsync(product); 
        }

        public async Task UpdateAsync(Product product)
        {
            await _productsCollection.ReplaceOneAsync(p => p.Id == product.Id, product);
        }

        public async Task DeleteAsync(int id)
        {
            await _productsCollection.DeleteOneAsync(p => p.Id == id);
        }

        public async Task<ProductSearchModel> GetSearchModelByIdAsync(int id)
        {
            var projection = Builders<Product>.Projection.Expression(p => new ProductSearchModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price
            });
            return await _productsCollection.Find(p => p.Id == id).Project(projection).FirstOrDefaultAsync();
        }
    }
}
