using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcontainers.MongoDb;
using Testcontainers.Redis;

namespace InventoryManagement.InfraStructure.IntegrationTest
{
    public class IntegrationTestContainers : IIntegrationTestContainers , IAsyncLifetime
    {
        public string MongoContainerName { get; protected set; } = "Mongo-IntegrationTest";
        public MongoDbContainer MongoDbContainer { get; protected set; }
        public string MongoImageName { get; protected set; } = "mongo:8.0";

        public IntegrationTestContainers()
        {
            MongoDbContainer = new MongoDbBuilder()
                .WithImage(MongoImageName)
                .WithName(MongoContainerName)
                .WithPortBinding(27030, 27017)
                .Build();
            MongoDbContainer.StartAsync().GetAwaiter().GetResult();
        }

        public async Task InitializeAsync()
        {
            await MongoDbContainer.StartAsync();
        }

        async Task IAsyncLifetime.DisposeAsync()
        {
            await MongoDbContainer.StopAsync();
        }
    }
}
