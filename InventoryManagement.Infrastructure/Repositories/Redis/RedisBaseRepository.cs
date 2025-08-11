using InventoryManagment.DomainModels.Repositories;
using Redis.OM;
using Redis.OM.Contracts;
using Redis.OM.Searching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using InventoryManagment.DomainModels.Entites;

namespace InventoryManagement.Infrastructure.Repositories.Redis
{
    public class RedisBaseRepository<T> : IRepository<T> where T : IEntity
    {
        protected readonly IRedisConnectionProvider _redisConnectionProvider;
        protected readonly IRedisCollection<T> _collection;

        public RedisBaseRepository(IRedisConnectionProvider redisConnectionProvider)
        {
            _redisConnectionProvider = redisConnectionProvider ?? throw new ArgumentNullException(nameof(redisConnectionProvider));
            _collection = _redisConnectionProvider.RedisCollection<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _collection.InsertAsync(entity);
        }

        public async Task<long> DeleteAsync(Guid id)
        {
            var key = $"{typeof(T).Name}:{id}";
            var result = await _redisConnectionProvider.Connection.UnlinkAsync(key);
            return result;
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            return Task.FromResult(_collection.AsEnumerable());
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _collection.Where(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<long> UpdateAsync(T entity)
        {
            await _collection.UpdateAsync(entity);
            return 1;
        }
    }
}
