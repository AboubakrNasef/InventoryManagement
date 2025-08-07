using InventoryManagment.DomainModels.Entites;
using InventoryManagment.DomainModels.Repositories;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryManagement.Infrastructure.Repositories.Mongo
{
    public abstract class MongoBaseRepository<T> : IRepository<T> where T : IEntity
    {
        protected readonly IMongoCollection<T> _collection;
        protected readonly IMongoDatabase database;

        protected MongoBaseRepository(IMongoDatabase database)
        {
            this.database = database ?? throw new ArgumentNullException(nameof(database));
            _collection = database.GetCollection<T>(typeof(T).Name);
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            // Assumes T has an Id property of type int
            return await _collection.Find(Builders<T>.Filter.Eq("Id", id)).FirstOrDefaultAsync();
        }

        public virtual Task<IEnumerable<T>> GetAllAsync()
        {
            return Task.FromResult(_collection.Find(_ => true).ToEnumerable());
        }

        public virtual async Task AddAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public virtual async Task<long> UpdateAsync(T entity)
        {
            var result = await _collection.ReplaceOneAsync(Builders<T>.Filter.Eq("Id", entity.Id), entity);
            return result.ModifiedCount;
        }

        public virtual async Task<long> DeleteAsync(int id)
        {
            var result = await _collection.DeleteOneAsync(Builders<T>.Filter.Eq("Id", id));
            return result.DeletedCount;
        }
    }
}
