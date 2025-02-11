using InventoryManagement.Application.RedisSearch;
using InventoryManagment.DomainModels.Interfaces;
using Redis.OM;
using Redis.OM.Contracts;
using Redis.OM.Searching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Infrastructure.Repositories
{
    public class ProductSearchRepository : IProductSearchRepository
    {

        private readonly IRedisConnectionProvider _redisConnectionProvider;
        private readonly IRedisCollection<ProductSearchModel> _collection;

        public ProductSearchRepository(IRedisConnectionProvider redisConnectionProvider, IRedisCollection<ProductSearchModel> collection)
        {
            _redisConnectionProvider = redisConnectionProvider;
            _collection = collection;
        }

        public async Task AddAsync(ProductSearchModel product)
        {
            await _collection.InsertAsync(product);
        }

        public async Task DeleteAsync(int id)
        {
            await _redisConnectionProvider.Connection.UnlinkAsync($"ProductSearchModel:{id}");
        }

        public async Task<IList<ProductSearchModel>> GetAllAsync()
        {
            return await _collection.ToListAsync();
        }

        public async Task<ProductSearchModel> GetByIdAsync(int id)
        {
            return await _collection.Where(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(ProductSearchModel product)
        {
            await _collection.UpdateAsync(product);
        }
    }
}
