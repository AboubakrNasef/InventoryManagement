using InventoryManagement.Infrastructure.Extensions;
using InventoryManagement.Infrastructure.Repositories.Mongo;
using InventoryManagement.InfraStructure.IntegrationTest.Repositories.Mongo.Fixtures.Fakers;
using InventoryManagment.DomainModels.Entites;
using InventoryManagment.DomainModels.Repositories;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace InventoryManagement.InfraStructure.IntegrationTest
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IMongoClient>(c=>
            {
                var connectionString = c.GetRequiredService<IIntegrationTestContainers>().MongoDbContainer.GetConnectionString();
             
                return new MongoClient(connectionString);
            });

            services.AddSingleton<IIntegrationTestContainers, IntegrationTestContainers>();
            services.AddSingleton<IIDCreator, IDCreator>();
            services.AddSingleton<IFaker<Category>, CategoryFaker>();
            InfraStructureServices. ConfigureSerializtation();
        }
    }
}