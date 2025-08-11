using InventoryManagment.DomainModels.Entites;
using InventoryManagment.DomainModels.Repositories;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryManagement.Infrastructure.Repositories.Mongo
{
    public abstract class MongoBaseRepository<T> : IRepository<T> where T : IEntity
    {
        protected readonly IMongoCollection<T> _collection;
        protected readonly IMongoDatabase _database;
        protected readonly IIDCreator _idGenerator;

        protected MongoBaseRepository(IMongoDatabase database, IIDCreator idGenerator)
        {
            _database = database ?? throw new ArgumentNullException(nameof(database));
            _collection = database.GetCollection<T>(typeof(T).Name) ?? throw new ArgumentNullException(nameof(_collection)); ;
            _idGenerator = idGenerator ?? throw new ArgumentNullException(nameof(idGenerator));
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await _collection.Find(Builders<T>.Filter.Eq(e => e.Id, id)).FirstOrDefaultAsync();
        }

        public virtual Task<IEnumerable<T>> GetAllAsync()
        {
            return Task.FromResult(_collection.Find(_ => true).ToEnumerable());
        }

        public virtual async Task AddAsync(T entity)
        {
            entity.Id = _idGenerator.CreateId();
            await _collection.InsertOneAsync(entity);
        }

        public virtual async Task<long> UpdateAsync(T entity)
        {
            var result = await _collection.ReplaceOneAsync(Builders<T>.Filter.Eq("Id", entity.Id), entity);
            return result.ModifiedCount;
        }

        public virtual async Task<long> DeleteAsync(Guid id)
        {
            var result = await _collection.DeleteOneAsync(Builders<T>.Filter.Eq("Id", id));
            return result.DeletedCount;
        }
    }
}
