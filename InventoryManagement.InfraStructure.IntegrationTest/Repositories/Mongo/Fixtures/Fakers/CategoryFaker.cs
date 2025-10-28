using Bogus;
using InventoryManagment.DomainModels.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.InfraStructure.IntegrationTest.Repositories.Mongo.Fixtures.Fakers
{
    public class CategoryFaker:IFaker<Category>
    {
        private Faker<Category> Faker { get; }

        public CategoryFaker()
        {
            Faker = new Faker<Category>()
                .RuleFor(c => c.Id, f => Guid.NewGuid())
                .RuleFor(c => c.Name, f => f.Commerce.Categories(1).First())
                .RuleFor(c => c.Description, f => f.Lorem.Sentence());
        }

        public List<Category> Generate(int count)
        {
           return Faker.Generate(count);
        }

        public Category Generate()
        {
           return Faker.Generate();
        }
    }
}
