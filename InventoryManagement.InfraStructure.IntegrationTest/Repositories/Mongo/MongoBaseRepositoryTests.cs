using InventoryManagement.InfraStructure.IntegrationTest.Repositories.Mongo.Fixtures;
using InventoryManagment.DomainModels.Entites;
using InventoryManagment.DomainModels.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Core.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.InfraStructure.IntegrationTest.Repositories.Mongo
{
    public abstract class MongoBaseRepositoryTests<T> : IAsyncLifetime where T : IEntity
    {
        public MongoBaseFixture<T> Fixture { get; }
        protected List<Guid> _createdEntityIds;
        public abstract IRepository<T> Repository { get; }
        protected MongoBaseRepositoryTests(MongoBaseFixture<T> fixture)
        {
            Fixture = fixture;
            _createdEntityIds = [];
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllEntities()
        {
            // Arrange
            var expectedCount = Fixture.SeededEntities.Count;
            // Act
            var entities = await Repository.GetAllAsync();
            // Assert
            Assert.NotNull(entities);
            Assert.Equal(Fixture.SeededEntities.Count, entities.Count());
        }

        [Fact]
        public async Task AddAsync_ShouldAddToDb()
        {
            // Arrange
            var entity = Fixture.Faker.Generate();
         
            // Act
            await Repository.AddAsync(entity);
            _createdEntityIds.Add(entity.Id);

            var retrievedEntity = await Fixture.Collection.Find(e => e.Id == entity.Id).FirstAsync();
            
            // Assert
            Assert.Equal(entity.Id, retrievedEntity.Id);
        }

        public Task InitializeAsync()
        {
            return Task.CompletedTask;
        }

        public async Task DisposeAsync()
        {
            await Fixture.Collection.DeleteManyAsync(e => _createdEntityIds.Contains(e.Id));
        }
    }
}