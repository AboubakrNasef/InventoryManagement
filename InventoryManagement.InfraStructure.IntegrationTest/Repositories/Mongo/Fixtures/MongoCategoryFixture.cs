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
    public class MongoCategoryFixture : MongoBaseFixture<Category>
    {
        public MongoCategoryFixture(IIntegrationTestContainers integrationTestContainers, IMongoClient client, IFaker<Category> faker) : base(integrationTestContainers, client,faker)
        {
        }
        public override async Task SeedDB()
        {
            _seededEntites.AddRange(Faker.Generate(5));//in memory
            await Collection.InsertManyAsync(_seededEntites);//insert to db
        }
    }
}
