using InventoryManagement.Infrastructure.Repositories.Mongo;
using InventoryManagement.InfraStructure.IntegrationTest.Repositories.Mongo.Fixtures;
using InventoryManagment.DomainModels.Entites;
using InventoryManagment.DomainModels.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.InfraStructure.IntegrationTest.Repositories.Mongo
{
    public class MongoCategoryRepositoryTest : MongoBaseRepositoryTests<Category>,IClassFixture<MongoCategoryFixture>
    {
        public override IRepository<Category> Repository { get; }
        public MongoCategoryFixture _fixture { get; }
        public MongoCategoryRepositoryTest(MongoCategoryFixture fixture,IIDCreator iDCreator) : base(fixture)
        {
            Repository = new MongoCategoryRepository(fixture.Database, iDCreator);
            _fixture = fixture;
        }
    }
}
