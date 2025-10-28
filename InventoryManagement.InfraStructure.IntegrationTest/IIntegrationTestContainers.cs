using Testcontainers.MongoDb;

namespace InventoryManagement.InfraStructure.IntegrationTest
{
    public interface IIntegrationTestContainers
    {
        string MongoContainerName { get; }
        MongoDbContainer MongoDbContainer { get; }
        string MongoImageName { get; }

        Task InitializeAsync();
    }
}