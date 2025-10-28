using InventoryManagement.InfraStructure.IntegrationTest.Repositories.Mongo.Fixtures.Fakers;
using InventoryManagment.DomainModels.Entites;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.InfraStructure.IntegrationTest.Repositories.Mongo.Fixtures
{
    public abstract class MongoBaseFixture<T> : IAsyncLifetime
    {
        protected readonly List<T> _seededEntites = new();
        protected readonly IIntegrationTestContainers _integrationTestContainers;
        public IMongoClient Client { get; protected set; }
        public IMongoDatabase Database { get; protected set; }
        public IMongoCollection<T> Collection { get; protected set; }
        public IReadOnlyList<T> SeededEntities => _seededEntites.AsReadOnly();
        public IFaker<T> Faker { get; }

        public MongoBaseFixture(IIntegrationTestContainers integrationTestContainers, IMongoClient client, IFaker<T> faker)
        {
            this._integrationTestContainers = integrationTestContainers;
            Client = client;
            Database = Client.GetDatabase("TestDB");
            Collection = Database.GetCollection<T>(typeof(T).Name);
            Faker = faker;
        }
        public abstract Task SeedDB();
        public async Task InitializeAsync()
        {
            await SeedDB();
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }
    }
}
